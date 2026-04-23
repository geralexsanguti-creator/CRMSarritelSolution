using Blazored.LocalStorage;
using CRMSarritelClient;
using CRMSarritelClient.Providers;
using CRMSarritelClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var url = "https://localhost:7130/";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    // etc.
});
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomAuthProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>(x => x.GetRequiredService<CustomAuthProvider>());
builder.Services.AddScoped<IAuthService, CustomAuthProvider>(x => x.GetRequiredService<CustomAuthProvider>());


await builder.Build().RunAsync();
