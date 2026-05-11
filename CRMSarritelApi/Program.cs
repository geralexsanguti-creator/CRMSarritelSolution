using CRMSarritelApi.Data;
using CRMSarritelApi.Repositories;
using CRMSarritelApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CRMSarritelApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using BCrypt.Net;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.MaxDepth = 128;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        var builtInFactory = options.InvalidModelStateResponseFactory;
        options.InvalidModelStateResponseFactory = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            foreach (var state in context.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    logger.LogError("==========> VALIDATION ERROR -> Field: {Key} | Error: {Message}", state.Key, error.ErrorMessage);
                }
            }
            return builtInFactory(context);
        };
    });

builder.Services.AddSignalR();

// OpenTelemetry
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


// Cargar configuración desde .ENV si existe
string connectionString = "";
try 
{
    var currentDir = Directory.GetCurrentDirectory();
    var envPaths = new[]
    {
        Path.Combine(currentDir, ".ENV"),
        Path.Combine(currentDir, "CRMSarritelApi", ".ENV"),
        Path.Combine(currentDir, "..", ".ENV"),
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".ENV"),
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".ENV"),
        "C:\\Users\\German\\Desktop\\CRMSarritelSolution\\CRMSarritelSolution\\.ENV"
    };

    string? envFilePath = envPaths.FirstOrDefault(File.Exists);

    if (envFilePath != null)
    {
        Console.WriteLine($"DEBUG: Found .ENV at {envFilePath}");
        var lines = File.ReadAllLines(envFilePath);
        var envVars = new Dictionary<string, string>();
        foreach (var line in lines)
        {
            var parts = line.Split(':', 2);
            if (parts.Length == 2)
            {
                envVars[parts[0].Trim()] = parts[1].Trim();
            }
        }

        if (envVars.ContainsKey("DATA_HOST"))
        {
            connectionString = $"Host={envVars["DATA_HOST"]};Port={envVars["DATA_PORT"]};Database={envVars["DATA_DATABASE"]};Username={envVars["DATA_USER"]};Password={envVars["DATA_PASSWORD"]};";
            Console.WriteLine("DEBUG: Using connection string from .ENV");
        }
    }
    else 
    {
        Console.WriteLine("DEBUG: .ENV file not found in searched paths.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"WARNING: Error loading .ENV file: {ex.Message}");
}

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("CRMSarritelConnection") ?? "";
    Console.WriteLine("DEBUG: Using connection string from appsettings.json");
}


// Append resilience parameters to the connection string if not already present
if (!string.IsNullOrEmpty(connectionString) && !connectionString.Contains("Keepalive"))
{
    connectionString += ";Keepalive=30;Connection Idle Lifetime=300;Minimum Pool Size=1;Maximum Pool Size=20;Timeout=30;CommandTimeout=60;";
}

builder.Services.AddDbContext<CRMSarritelDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null);
    });
    options.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});



builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<ICommissionService, CommissionService>();
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddAuthentication(config => 
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        var jwtKey = builder.Configuration["AppSettings:Token"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            Console.WriteLine("CRITICAL ERROR: AppSettings:Token is missing in configuration!");
            throw new Exception("AppSettings:Token is missing");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = false,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = System.Security.Claims.ClaimTypes.Role,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7082", 
                "http://localhost:5173", // Puerto local Vite
                "http://localhost:8080", // Puerto alternativo
                "http://localhost:8081", // Puerto alternativo 2
                "http://localhost:3000"
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Endpoint que Prometheus va a leer
app.MapPrometheusScrapingEndpoint();   // URL: /metrics

// 1. CORS DEBE IR PRIMERO PARA QUE INCLUSO LOS ERRORES 500 TENGAN CABECERAS
app.UseCors("AllowSpecificOrigins");

// 2. MIDDLEWARE DE LOGGING Y MANEJO DE EXCEPCIONES GLOBAL
app.Use(async (context, next) =>
{
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;

    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        // Log EXCEPTION a archivo
        string logMsg = $"[{DateTime.Now:HH:mm:ss}] EXCEPTION 500 on {context.Request.Method} {context.Request.Path}: {ex.Message}\n{ex.StackTrace}\n\n";
        File.AppendAllText("api_errors.log", logMsg);
        
        // Si la respuesta no ha empezado, devolvemos un JSON de error estructurado
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            
            var errorResponse = new { 
                message = "Internal Server Error (Captured by Global Middleware)",
                error = ex.Message,
                path = context.Request.Path.Value
            };
            
            var json = System.Text.Json.JsonSerializer.Serialize(errorResponse);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);
            return; // Detenemos aquí, no procesamos más
        }
    }
    finally
    {
        // Log de Errores HTTP (400+) que NO fueron excepciones
        if (context.Response.StatusCode >= 400 && responseBody.Length > 0 && !context.Response.HasStarted)
        {
            try 
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(responseBody).ReadToEndAsync();
                
                context.Request.EnableBuffering();
                context.Request.Body.Position = 0;
                var requestBodyText = await new StreamReader(context.Request.Body).ReadToEndAsync();

                File.AppendAllText("api_errors.log", $"[{DateTime.Now:HH:mm:ss}] HTTP ERROR {context.Response.StatusCode} on {context.Request.Method} {context.Request.Path}\nREQUEST: {requestBodyText}\nRESPONSE: {responseText}\n\n");
            } 
            catch { /* Ignorar errores de logging */ }
        }

        // Copiar el stream de vuelta al original si no se ha enviado ya (vía Exception)
        if (responseBody.Length > 0 && !context.Response.HasStarted)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Ensure wwwroot/uploads exists so StaticFile middleware hooks correctly
var webRootPath = builder.Environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
var uploadsPath = Path.Combine(webRootPath, "uploads");
if (!Directory.Exists(uploadsPath)) {
    Directory.CreateDirectory(uploadsPath);
}

// 1. General static files (React app, maybe, or other assets)
app.UseStaticFiles();

// 2. Specific static files route for uploads guarantees no cross-pollution and proper CORS
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, OPTIONS");
        ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=3600");
    }
});

// app.UseHttpsRedirection();
// app.UseCors("AllowSpecificOrigins"); // Movidito arriba
// app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CRMSarritelDbContext>();
    // NOTE: Removed old Productos.Activo migration - table schema has changed
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CRMSarritelApi.Hubs.ChatHub>("/chatHub");

// Aplicar migraciones al inicio
await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CRMSarritelDbContext>();
        Console.WriteLine("DEBUG: Checking/Applying migrations...");
        await context.Database.MigrateAsync();
        
        Console.WriteLine("DEBUG: Seed complete. Checking admin user...");
        
        // Ensure Admin user exists even if migration seed was skipped
        if (!await context.Usuarios.AnyAsync(u => u.Username == "admin"))
        {
            Console.WriteLine("DEBUG: Admin user not found. Seeding manually...");
            var adminUser = new Usuario
            {
                Username = "admin",
                Nombre = "Administrador",
                Email = "admin@sarritel.com",
                Activo = true,
                FechaCreation = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
            };
            context.Usuarios.Add(adminUser);
            await context.SaveChangesAsync();
            
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Admin");
            if (adminRole != null)
            {
                context.UsuarioRoles.Add(new UsuarioRol { UsuarioId = adminUser.Id, RolId = adminRole.Id, FechaAsignacion = DateTime.UtcNow });
                await context.SaveChangesAsync();
            }
            Console.WriteLine("DEBUG: Admin user seeded successfully.");
        }
        else
        {
            Console.WriteLine("DEBUG: Admin user already exists. Skipping.");
        }
        
        Console.WriteLine("DEBUG: Migrations and Seeding checked successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR during startup: {ex.Message}");
        if (ex.InnerException != null) Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    }
}

app.Run();
