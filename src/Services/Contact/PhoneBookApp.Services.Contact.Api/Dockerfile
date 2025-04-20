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
COPY ["src/Services/Contact/PhoneBookApp.Services.Contact.Api/PhoneBookApp.Services.Contact.Api.csproj", "src/Services/Contact/PhoneBookApp.Services.Contact.Api/"]
COPY ["src/Core/PhoneBookApp.Core.Presentation/PhoneBookApp.Core.Presentation.csproj", "src/Core/PhoneBookApp.Core.Presentation/"]
COPY ["src/Core/PhoneBookApp.Core.Domain/PhoneBookApp.Core.Domain.csproj", "src/Core/PhoneBookApp.Core.Domain/"]
COPY ["src/Services/Contact/PhoneBookApp.Services.Contact.Infrastructure/PhoneBookApp.Services.Contact.Infrastructure.csproj", "src/Services/Contact/PhoneBookApp.Services.Contact.Infrastructure/"]
COPY ["src/Core/PhoneBookApp.Core.Infrastructure/PhoneBookApp.Core.Infrastructure.csproj", "src/Core/PhoneBookApp.Core.Infrastructure/"]
COPY ["src/Core/PhoneBookApp.Core.Application/PhoneBookApp.Core.Application.csproj", "src/Core/PhoneBookApp.Core.Application/"]
COPY ["src/Services/Contact/PhoneBookApp.Services.Contact.Application/PhoneBookApp.Services.Contact.Application.csproj", "src/Services/Contact/PhoneBookApp.Services.Contact.Application/"]
COPY ["src/Services/Contact/PhoneBookApp.Services.Contact.Domain/PhoneBookApp.Services.Contact.Domain.csproj", "src/Services/Contact/PhoneBookApp.Services.Contact.Domain/"]
RUN dotnet restore "./src/Services/Contact/PhoneBookApp.Services.Contact.Api/PhoneBookApp.Services.Contact.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Contact/PhoneBookApp.Services.Contact.Api"
RUN dotnet build "./PhoneBookApp.Services.Contact.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PhoneBookApp.Services.Contact.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBookApp.Services.Contact.Api.dll"]