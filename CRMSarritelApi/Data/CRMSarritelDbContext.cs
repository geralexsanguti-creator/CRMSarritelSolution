using CRMSarritelApi.Data.Seeds;
using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;

namespace CRMSarritelApi.Data
{
    public class CRMSarritelDbContext: DbContext
    {
        public CRMSarritelDbContext(DbContextOptions<CRMSarritelDbContext> options) : base(options)
        {
            
        }



        public DbSet<Comision> Comisiones => Set<Comision>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<DetalleVenta> DetalleVentas => Set<DetalleVenta>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<TipoVenta> TiposVentas => Set<TipoVenta>();
        public DbSet<ReglaComision> ReglasComisiones => Set<ReglaComision>();
        public DbSet<Rol> Roles => Set<Rol>();   
        public DbSet<Equipo> Equipos => Set<Equipo>();
        public DbSet<UsuarioEquipo> UsuarioEquipos => Set<UsuarioEquipo>();
        public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Sucursal> Sucursales => Set<Sucursal>(); 
        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Provincia> Provincias => Set<Provincia>();
        public DbSet<Municipio> Municipios => Set<Municipio>();
        public DbSet<CodigoPostal> CodigosPostales => Set<CodigoPostal>();
        public DbSet<Fichaje> Fichajes => Set<Fichaje>();
        public DbSet<Pausa> Pausas => Set<Pausa>();
        public DbSet<Contrato> Contratos => Set<Contrato>();
        public DbSet<ArchivoAdjunto> ArchivosAdjuntos => Set<ArchivoAdjunto>();
        public DbSet<Permiso> Permisos => Set<Permiso>();
        public DbSet<RolPermiso> RolPermisos => Set<RolPermiso>();
        public DbSet<ReparticionComision> ReparticionesComision => Set<ReparticionComision>();
        public DbSet<ReglaComisionTier> ReglaComisionTiers => Set<ReglaComisionTier>();
        public DbSet<PeriodoFacturacion> PeriodosFacturacion => Set<PeriodoFacturacion>();
        public DbSet<CarpetaReglas> CarpetasReglas => Set<CarpetaReglas>();
        public DbSet<CarpetaReglaComision> CarpetaReglasComision => Set<CarpetaReglaComision>();
        public DbSet<ProductoCarpeta> ProductoCarpetas => Set<ProductoCarpeta>();
        public DbSet<HistorialVenta> HistorialVentas => Set<HistorialVenta>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ──────────────────────────────────────────────────────────────
            // 0. Historial de Ventas
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<HistorialVenta>()
                .HasOne(h => h.Venta)
                .WithMany()
                .HasForeignKey(h => h.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistorialVenta>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => h.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);

            // ──────────────────────────────────────────────────────────────
            // 1. Relación principal: Usuario (como Comercial/vendedor) → muchas Ventas
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Usuario)                           // navegación en Venta
                .WithMany(u => u.VentasRealizadas)                // colección en Usuario
                .HasForeignKey(v => v.UsuarioId)                  // FK obligatoria
                .OnDelete(DeleteBehavior.Restrict);               // no borra ventas si se elimina usuario

            // ──────────────────────────────────────────────────────────────
            // 2. Relación secundaria: CreadoPor (quién registró la venta) → Usuario
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.CreadoPor)
                .WithMany(u => u.VentasCreadas)                                       // unidireccional o bidireccional
                .HasForeignKey(v => v.CreadoPorId)
                .OnDelete(DeleteBehavior.SetNull);                // o Restrict según tu regla

            // ──────────────────────────────────────────────────────────────
            // 3. Relación Venta → Cliente (uno a muchos)
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)                          // si existe en Cliente
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ──────────────────────────────────────────────────────────────
            // 4. Relación Venta → ProductoPrincipal (opcional)
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.ProductoPrincipal)
                .WithMany()
                .HasForeignKey(v => v.ProductoPrincipalId)
                .OnDelete(DeleteBehavior.SetNull);

            // ──────────────────────────────────────────────────────────────
            // 5. Relación Venta → DetalleVenta (uno a muchos)
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.Cascade);                // si borras venta → borran detalles

            // ──────────────────────────────────────────────────────────────
            // 6. Relación DetalleVenta → Producto
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // ──────────────────────────────────────────────────────────────
            // 6b. Relaciones de Proveedores y Productos
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(pv => pv.Productos)
                .HasForeignKey(p => p.ProveedorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ReglaComision>()
                .HasOne(r => r.Proveedor)
                .WithMany(pv => pv.Reglas)
                .HasForeignKey(r => r.ProveedorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReglaComision>()
                .HasOne(r => r.TipoVenta)
                .WithMany()
                .HasForeignKey(r => r.TipoVentaId)
                .OnDelete(DeleteBehavior.SetNull);

            // ──────────────────────────────────────────────────────────────
            // 6c. Configuración de Reparticiones y Tiers
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<ReparticionComision>()
                .HasOne(r => r.ReglaComision)
                .WithMany(rc => rc.ReparticionesBase)
                .HasForeignKey(r => r.ReglaComisionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReparticionComision>()
                .HasOne(r => r.Tier)
                .WithMany(t => t.Reparticiones)
                .HasForeignKey(r => r.TierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReglaComisionTier>()
                .HasOne(t => t.ReglaComision)
                .WithMany(rc => rc.Tiers)
                .HasForeignKey(t => t.ReglaComisionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ──────────────────────────────────────────────────────────────
            // 6d. Many-to-Many: Producto ↔ ReglaComision (REMOVIDO Y REEMPLAZADO POR CARPETAS)
            // ──────────────────────────────────────────────────────────────


            // ──────────────────────────────────────────────────────────────
            // 6e. Carpetas de Reglas y N:M
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<CarpetaReglas>()
                .HasOne(c => c.Proveedor)
                .WithMany()
                .HasForeignKey(c => c.ProveedorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CarpetaReglaComision>()
                .HasKey(cr => new { cr.CarpetaReglasId, cr.ReglaComisionId });
            modelBuilder.Entity<CarpetaReglaComision>()
                .HasOne(cr => cr.CarpetaReglas)
                .WithMany(c => c.CarpetaReglasComision)
                .HasForeignKey(cr => cr.CarpetaReglasId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CarpetaReglaComision>()
                .HasOne(cr => cr.ReglaComision)
                .WithMany(r => r.CarpetaReglasComision)
                .HasForeignKey(cr => cr.ReglaComisionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductoCarpeta>()
                .HasKey(pc => new { pc.ProductoId, pc.CarpetaReglasId });
            modelBuilder.Entity<ProductoCarpeta>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.ProductoCarpetas)
                .HasForeignKey(pc => pc.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductoCarpeta>()
                .HasOne(pc => pc.CarpetaReglas)
                .WithMany(c => c.ProductoCarpetas)
                .HasForeignKey(pc => pc.CarpetaReglasId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Venta>(venta =>
            {
                venta.OwnsOne(v => v.Estado, ev =>
                {
                    ev.Property(e => e.Codigo).HasColumnName("Estado_Codigo");
                    ev.Property(e => e.Nombre).HasColumnName("Estado_Nombre");
                    ev.Property(e => e.Icono).HasColumnName("Estado_Icono");
                    ev.Property(e => e.Color).HasColumnName("Estado_Color");
                    ev.Property(e => e.Orden).HasColumnName("Estado_Orden");
                    ev.Property(e => e.PermiteEdicion).HasColumnName("Estado_PermiteEdicion");
                    ev.Property(e => e.PermiteEliminar).HasColumnName("Estado_PermiteEliminar");
                    ev.Property(e => e.EsFinal).HasColumnName("Estado_EsFinal");
                    ev.Property(e => e.Activo).HasColumnName("Estado_Activo");
                    ev.Property(e => e.EsInicial).HasColumnName("Estado_EsInicial");
                });
            });

            // ──────────────────────────────────────────────────────────────
            // 7. Relación Venta → Comision (uno a muchos)
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Comision>()
                .HasOne(c => c.Venta)
                .WithMany(v => v.Comisiones)
                .HasForeignKey(c => c.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            // ──────────────────────────────────────────────────────────────
            // 8. Relación Comision → Usuario (Empleado)
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Comision>()
                .HasOne(c => c.Empleado)
                .WithMany(u => u.ComisionesGeneradas)                      // si existe colección Comisiones en Usuario
                .HasForeignKey(c => c.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);
            // ──────────────────────────────────────────────────────────────
            // Value objects embebidos en Comision
            // ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Comision>(comision =>
            {
                // Tipo (value object)
                comision.OwnsOne(c => c.Tipo, tipo =>
                {
                    tipo.Property(t => t.Codigo).HasColumnName("Tipo_Codigo").HasMaxLength(50);
                    tipo.Property(t => t.Nombre).HasColumnName("Tipo_Nombre").HasMaxLength(100);
                });

                // Estado (value object)
                comision.OwnsOne(c => c.Estado, estado =>
                {
                    estado.Property(e => e.Codigo).HasColumnName("Estado_Codigo").HasMaxLength(20);
                    estado.Property(e => e.Nombre).HasColumnName("Estado_Nombre").HasMaxLength(80);
                    estado.Property(e => e.Color).HasColumnName("Estado_Color").HasMaxLength(30);
                    estado.Property(e => e.Icono).HasColumnName("Estado_Icono").HasMaxLength(10);
                    estado.Property(e => e.EsPagable).HasColumnName("Estado_EsPagable");
                    estado.Property(e => e.EsFinal).HasColumnName("Estado_EsFinal");
                    estado.Property(e => e.Orden).HasColumnName("Estado_Orden");
                });
            });

            // ────────────────────────────────────────────────
            // Many-to-Many: Usuario ↔ Rol vía UsuarioRol
            // ────────────────────────────────────────────────

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.UsuarioId, ur.RolId });  // composite key

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRoles)
                .HasForeignKey(ur => ur.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Rol)
                .WithMany(r => r.UsuarioRoles)
                .HasForeignKey(ur => ur.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            // ────────────────────────────────────────────────
            // Many-to-Many: Rol ↔ Permiso vía RolPermiso
            // ────────────────────────────────────────────────
            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.RolId, rp.PermisoId });

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rp => rp.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(rp => rp.PermisoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional: table name customization
            modelBuilder.Entity<UsuarioRol>()
                .ToTable("UsuarioRoles");

            // ────────────────────────────────────────────────
            // Many-to-Many: Usuario ↔ Equipo vía UsuarioEquipo
            // ────────────────────────────────────────────────
            modelBuilder.Entity<UsuarioEquipo>()
                .HasKey(ue => new { ue.UsuarioId, ue.EquipoId });

            modelBuilder.Entity<UsuarioEquipo>()
                .HasOne(ue => ue.Usuario)
                .WithMany(u => u.UsuarioEquipos)
                .HasForeignKey(ue => ue.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioEquipo>()
                .HasOne(ue => ue.Equipo)
                .WithMany(e => e.UsuarioEquipos)
                .HasForeignKey(ue => ue.EquipoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReparticionComision>()
                .HasOne(r => r.Equipo)
                .WithMany()
                .HasForeignKey(r => r.EquipoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Cliente>(cliente =>
            {
                cliente.OwnsOne(c => c.Direccion, d =>
                {
                    d.Property(x => x.Calle).HasColumnName("Direccion_Calle").HasMaxLength(500);
                    d.Property(x => x.CodigoPostal).HasColumnName("Direccion_CodigoPostal").HasMaxLength(10);
                    d.Property(x => x.Poblacion).HasColumnName("Direccion_Poblacion").HasMaxLength(200);
                    d.Property(x => x.Provincia).HasColumnName("Direccion_Provincia").HasMaxLength(100);
                });
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.PasswordHash)
                      .HasColumnType("text") // BCrypt = 60 caracteres exactos
                      .IsRequired()
                      .HasComment("BCrypt hash");

                // Fix other long fields while you're here
                entity.Property(u => u.Username)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasMaxLength(254);         
            });


            Data.Seeds.AdminSeed.Seed(modelBuilder);
            Data.Seeds.ProvinciasSeed.Seed(modelBuilder);
            Data.Seeds.MunicipiosSeed.Seed(modelBuilder);
            Data.Seeds.CodigosPostalesSeed.Seed(modelBuilder);
            //Data.Seeds.ClientesSeed.Seed(modelBuilder);
        }
    }

    public class CRMSarritelDbContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<CRMSarritelDbContext>
    {
        public CRMSarritelDbContext CreateDbContext(string[] args)
        {
            var connectionString = "";
            try 
            {
                var currentDir = Directory.GetCurrentDirectory();
                var envFilePath = Path.Combine(currentDir, ".ENV");
                if (!File.Exists(envFilePath))
                {
                    envFilePath = Path.Combine(currentDir, "..", ".ENV");
                }

                if (File.Exists(envFilePath))
                {
                    var lines = File.ReadAllLines(envFilePath);
                    var envVars = new Dictionary<string, string>();
                    foreach (var line in lines)
                    {
                        var parts = line.Split(':', 2);
                        if (parts.Length == 2)
                            envVars[parts[0].Trim()] = parts[1].Trim();
                    }

                    if (envVars.ContainsKey("DATA_HOST"))
                    {
                        connectionString = $"Host={envVars["DATA_HOST"]};Port={envVars["DATA_PORT"]};Database={envVars["DATA_DATABASE"]};Username={envVars["DATA_USER"]};Password={envVars["DATA_PASSWORD"]};";
                    }
                }
            }
            catch {}

            if (string.IsNullOrEmpty(connectionString))
            {
                 var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();
                 connectionString = configuration.GetConnectionString("CRMSarritelConnection");
            }

            var optionsBuilder = new DbContextOptionsBuilder<CRMSarritelDbContext>();
            optionsBuilder.UseNpgsql(connectionString ?? "");
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));

            return new CRMSarritelDbContext(optionsBuilder.Options);

        }
    }
}
