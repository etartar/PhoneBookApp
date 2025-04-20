# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Report/PhoneBookApp.Services.Report.Api/PhoneBookApp.Services.Report.Api.csproj", "src/Services/Report/PhoneBookApp.Services.Report.Api/"]
COPY ["src/Core/PhoneBookApp.Core.Presentation/PhoneBookApp.Core.Presentation.csproj", "src/Core/PhoneBookApp.Core.Presentation/"]
COPY ["src/Core/PhoneBookApp.Core.Domain/PhoneBookApp.Core.Domain.csproj", "src/Core/PhoneBookApp.Core.Domain/"]
COPY ["src/Services/Report/PhoneBookApp.Services.Report.Infrastructure/PhoneBookApp.Services.Report.Infrastructure.csproj", "src/Services/Report/PhoneBookApp.Services.Report.Infrastructure/"]
COPY ["src/Core/PhoneBookApp.Core.Infrastructure/PhoneBookApp.Core.Infrastructure.csproj", "src/Core/PhoneBookApp.Core.Infrastructure/"]
COPY ["src/Core/PhoneBookApp.Core.Application/PhoneBookApp.Core.Application.csproj", "src/Core/PhoneBookApp.Core.Application/"]
COPY ["src/Services/Report/PhoneBookApp.Services.Report.Application/PhoneBookApp.Services.Report.Application.csproj", "src/Services/Report/PhoneBookApp.Services.Report.Application/"]
COPY ["src/Services/Report/PhoneBookApp.Services.Report.Domain/PhoneBookApp.Services.Report.Domain.csproj", "src/Services/Report/PhoneBookApp.Services.Report.Domain/"]
RUN dotnet restore "./src/Services/Report/PhoneBookApp.Services.Report.Api/PhoneBookApp.Services.Report.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Report/PhoneBookApp.Services.Report.Api"
RUN dotnet build "./PhoneBookApp.Services.Report.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PhoneBookApp.Services.Report.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBookApp.Services.Report.Api.dll"]