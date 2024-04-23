using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZooExpoOrg.Web;
using ZooExpoOrg.Web.Services;
using ZooExpoOrg.Web.Services.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
    
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

services.AddMudServices();

services.AddBlazoredLocalStorage();

services.AddScoped<IConfigurationService, ConfigurationService>();

await builder.Build().RunAsync();
