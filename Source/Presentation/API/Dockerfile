#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Source/Presentation/API/API.csproj", "Source/Presentation/API/"]
COPY ["Source/Infrastructure/Infrastructure.csproj", "Source/Infrastructure/"]
COPY ["Source/Core/Application/Application.csproj", "Source/Core/Application/"]
COPY ["Source/Core/Domain/Domain.csproj", "Source/Core/Domain/"]
RUN dotnet restore "Source/Presentation/API/API.csproj"
COPY . .
WORKDIR "/src/Source/Presentation/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]