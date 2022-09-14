#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KobeTaiGuidance_API/KobeTaiGuidance_API.csproj", "KobeTaiGuidance_API/"]
RUN dotnet restore "KobeTaiGuidance_API/KobeTaiGuidance_API.csproj"
COPY . .
WORKDIR "/src/KobeTaiGuidance_API"
RUN dotnet build "KobeTaiGuidance_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KobeTaiGuidance_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KobeTaiGuidance_API.dll"]