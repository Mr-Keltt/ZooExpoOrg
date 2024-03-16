#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Workers/ZooExpoOrg.Worker/ZooExpoOrg.Worker.csproj", "Systems/Workers/ZooExpoOrg.Worker/"]
COPY ["Data/ZooExpoOrg.Context/ZooExpoOrg.Context.csproj", "Data/ZooExpoOrg.Context/"]
COPY ["Services/ZooExpoOrg.Services.Settings/ZooExpoOrg.Services.Settings.csproj", "Services/ZooExpoOrg.Services.Settings/"]
COPY ["Shared/ZooExpoOrg.Common/ZooExpoOrg.Common.csproj", "Shared/ZooExpoOrg.Common/"]
COPY ["Data/ZooExpoOrg.Context.Entities/ZooExpoOrg.Context.Entities.csproj", "Data/ZooExpoOrg.Context.Entities/"]
COPY ["Services/ZooExpoOrg.Services.Logger/ZooExpoOrg.Services.Logger.csproj", "Services/ZooExpoOrg.Services.Logger/"]
COPY ["Services/ZooExpoOrg.Services.RabbitMq/ZooExpoOrg.Services.RabbitMq.csproj", "Services/ZooExpoOrg.Services.RabbitMq/"]
RUN dotnet restore "Systems/Workers/ZooExpoOrg.Worker/ZooExpoOrg.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Workers/ZooExpoOrg.Worker"
RUN dotnet build "ZooExpoOrg.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZooExpoOrg.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZooExpoOrg.Worker.dll"]