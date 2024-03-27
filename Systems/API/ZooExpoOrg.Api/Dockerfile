#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/API/ZooExpoOrg.Api/ZooExpoOrg.Api.csproj", "Systems/API/ZooExpoOrg.Api/"]
COPY ["Data/ZooExpoOrg.Context.Entities/ZooExpoOrg.Context.Entities.csproj", "Data/ZooExpoOrg.Context.Entities/"]
COPY ["Shared/ZooExpoOrg.Common/ZooExpoOrg.Common.csproj", "Shared/ZooExpoOrg.Common/"]
COPY ["Data/ZooExpoOrg.Context.Migrations/ZooExpoOrg.Context.Migrations.MSSQL/ZooExpoOrg.Context.Migrations.MSSQL.csproj", "Data/ZooExpoOrg.Context.Migrations/ZooExpoOrg.Context.Migrations.MSSQL/"]
COPY ["Data/ZooExpoOrg.Context/ZooExpoOrg.Context.csproj", "Data/ZooExpoOrg.Context/"]
COPY ["Services/ZooExpoOrg.Services.Settings/ZooExpoOrg.Services.Settings.csproj", "Services/ZooExpoOrg.Services.Settings/"]
COPY ["Data/ZooExpoOrg.Context.Migrations/ZooExpoOrg.Context.Migrations.PgSql/ZooExpoOrg.Context.Migrations.PgSql.csproj", "Data/ZooExpoOrg.Context.Migrations/ZooExpoOrg.Context.Migrations.PgSql/"]
COPY ["Services/ZooExpoOrg.Services.Animals/ZooExpoOrg.Services.Animals.csproj", "Services/ZooExpoOrg.Services.Animals/"]
COPY ["Services/ZooExpoOrg.Services.Logger/ZooExpoOrg.Services.Logger.csproj", "Services/ZooExpoOrg.Services.Logger/"]
COPY ["Services/ZooExpoOrg.Services.Photos/ZooExpoOrg.Services.Photos.csproj", "Services/ZooExpoOrg.Services.Photos/"]
COPY ["Services/ZooExpoOrg.Services.RabbitMq/ZooExpoOrg.Services.RabbitMq.csproj", "Services/ZooExpoOrg.Services.RabbitMq/"]
COPY ["Systems/API/ZooExpoOrg.Api.Settings/ZooExpoOrg.Api.Settings.csproj", "Systems/API/ZooExpoOrg.Api.Settings/"]
RUN dotnet restore "Systems/API/ZooExpoOrg.Api/ZooExpoOrg.Api.csproj"
COPY . .
WORKDIR "/src/Systems/API/ZooExpoOrg.Api"
RUN dotnet build "ZooExpoOrg.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZooExpoOrg.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZooExpoOrg.Api.dll"]