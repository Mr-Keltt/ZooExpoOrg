using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZooExpoOrg.Web;
using ZooExpoOrg.Web.Providers;
using ZooExpoOrg.Web.Services.Accounts;
using ZooExpoOrg.Web.Services.Achievements;
using ZooExpoOrg.Web.Services.Animals;
using ZooExpoOrg.Web.Services.Auth;
using ZooExpoOrg.Web.Services.Clients;
using ZooExpoOrg.Web.Services.Comments;
using ZooExpoOrg.Web.Services.Configuration;
using ZooExpoOrg.Web.Services.Expositions;
using ZooExpoOrg.Web.Services.GetIdHelper;
using ZooExpoOrg.Web.Services.GetRsultHelper;
using ZooExpoOrg.Web.Services.Photos;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
    
services.AddAuthorizationCore();
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

services.AddMudServices();

services.AddBlazoredLocalStorage();

services.AddScoped<IConfigurationService, ConfigurationService>();

services.AddScoped<IAccountService, AccountService>();
services.AddScoped<IAchievementService, AchievementService>();
services.AddScoped<IAnimalService, AnimalService>();
services.AddScoped<IClientService, ClientService>();
services.AddScoped<ICommentService, CommentService>();
services.AddScoped<IExpositionService, ExpositionService>();
services.AddScoped<IPhotoService, PhotoService>();

services.AddScoped<IGetIdHelperService, GetIdHelperService>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
