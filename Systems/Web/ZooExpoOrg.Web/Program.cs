using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZooExpoOrg.Web;
using ZooExpoOrg.Web.Services;
using ZooExpoOrg.Web.Services.Achievements;
using ZooExpoOrg.Web.Services.Animals;
using ZooExpoOrg.Web.Services.Clients;
using ZooExpoOrg.Web.Services.Comments;
using ZooExpoOrg.Web.Services.Configuration;
using ZooExpoOrg.Web.Services.Expositions;
using ZooExpoOrg.Web.Services.Photos;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
    
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

services.AddMudServices();

services.AddBlazoredLocalStorage();

services.AddScoped<IConfigurationService, ConfigurationService>();

services.AddScoped<IAchievementService, AchievementService>();
services.AddScoped<IAnimalService, AnimalService>();
services.AddScoped<IClientService, ClientService>();
services.AddScoped<ICommentService, CommentService>();
services.AddScoped<IExpositionService, ExpositionService>();
services.AddScoped<IPhotoService, PhotoService>();

await builder.Build().RunAsync();
