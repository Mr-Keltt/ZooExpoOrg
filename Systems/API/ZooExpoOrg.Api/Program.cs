using ZooExpoOrg.Api;
using ZooExpoOrg.Api.Configuration;
using ZooExpoOrg.Common.Settings;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

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

services.AddAppSwagger(mainSettings, swaggerSettings);

services.RegisterServices();


var app = builder.Build();


app.UseAppCors();

app.UseAppControllerAndViews();

app.UseAppHealthChecks();

app.UseAppSwagger();

DbInitializer.Execute(app.Services);

app.Run();
