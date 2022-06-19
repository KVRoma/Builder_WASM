using Builder_WASM.Client;
using Builder_WASM.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

//await builder.Build().RunAsync();
var host = builder.Build();

var local = host.Services.GetRequiredService<ILocalStorageService>();
//local.AddStorage("sessionStorage"); // Використовую для зміни типу сховища в браузері.

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();

await host.RunAsync();
