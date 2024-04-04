#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Identity/ZooExpoOrg.Identity/ZooExpoOrg.Identity.csproj", "Systems/Identity/ZooExpoOrg.Identity/"]
COPY ["Data/ZooExpoOrg.Context.Entities/ZooExpoOrg.Context.Entities.csproj", "Data/ZooExpoOrg.Context.Entities/"]
COPY ["Shared/ZooExpoOrg.Common/ZooExpoOrg.Common.csproj", "Shared/ZooExpoOrg.Common/"]
COPY ["Data/ZooExpoOrg.Context/ZooExpoOrg.Context.csproj", "Data/ZooExpoOrg.Context/"]
COPY ["Services/ZooExpoOrg.Services.Settings/ZooExpoOrg.Services.Settings.csproj", "Services/ZooExpoOrg.Services.Settings/"]
RUN dotnet restore "Systems/Identity/ZooExpoOrg.Identity/ZooExpoOrg.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/ZooExpoOrg.Identity"
RUN dotnet build "ZooExpoOrg.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZooExpoOrg.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZooExpoOrg.Identity.dll"]