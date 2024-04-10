using ZooExpoOrg.Api;
using ZooExpoOrg.Api.Configuration;
using ZooExpoOrg.Common.Settings;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var idetitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext();

services.AddAppAutoMappers();

services.AddAppValidator();

services.AddAppCors();

services.AddAppControllerAndViews();

services.AddAppVersioning();

services.AddAppHealthChecks();

services.AddAppAuth(idetitySettings);

services.AddAppSwagger(mainSettings, swaggerSettings, idetitySettings);

services.RegisterServices(builder.Configuration);


var app = builder.Build();


app.UseAppCors();

app.UseAppControllerAndViews();

app.UseAppHealthChecks();

app.UseAppAuth();

app.UseAppSwagger();

DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);

app.Run();
