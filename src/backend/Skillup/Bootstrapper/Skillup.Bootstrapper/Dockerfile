#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bootstrapper/Skillup.Bootstrapper/Skillup.Bootstrapper.csproj", "Bootstrapper/Skillup.Bootstrapper/"]

COPY ["Shared/Skillup.Shared.Infrastructure/Skillup.Shared.Infrastructure.csproj", "Shared/Skillup.Shared.Infrastructure/"]
COPY ["Shared/Skillup.Shared.Abstractions/Skillup.Shared.Abstractions.csproj", "Shared/Skillup.Shared.Abstractions/"]

COPY ["Modules/Auth/Skillup.Modules.Auth.Api/Skillup.Modules.Auth.Api.csproj", "Modules/Auth/Skillup.Modules.Auth.Api/"]
COPY ["Modules/Auth/Skillup.Modules.Auth.Core/Skillup.Modules.Auth.Core.csproj", "Modules/Auth/Skillup.Modules.Auth.Core/"]

COPY ["Modules/Chat/Skillup.Modules.Chat.Api/Skillup.Modules.Chat.Api.csproj", "Modules/Chat/Skillup.Modules.Chat.Api/"]

COPY ["Modules/Courses/Skillup.Modules.Courses.Api/Skillup.Modules.Courses.Api.csproj", "Modules/Courses/Skillup.Modules.Courses.Api/"]
COPY ["Modules/Courses/Skillup.Modules.Courses.Application/Skillup.Modules.Courses.Application.csproj", "Modules/Courses/Skillup.Modules.Courses.Application/"]
COPY ["Modules/Courses/Skillup.Modules.Courses.Core/Skillup.Modules.Courses.Core.csproj", "Modules/Courses/Skillup.Modules.Courses.Core/"]
COPY ["Modules/Courses/Skillup.Modules.Courses.Infrastracture/Skillup.Modules.Courses.Infrastracture.csproj", "Modules/Courses/Skillup.Modules.Courses.Infrastracture/"]

COPY ["Modules/Finances/Skillup.Modules.Finances.Api/Skillup.Modules.Finances.Api.csproj", "Modules/Finances/Skillup.Modules.Finances.Api/"]
COPY ["Modules/Finances/Skillup.Modules.Finances.Core/Skillup.Modules.Finances.Core.csproj", "Modules/Finances/Skillup.Modules.Finances.Core/"]

COPY ["Modules/Mails/Skillup.Modules.Mails.Api/Skillup.Modules.Mails.Api.csproj", "Modules/Mails/Skillup.Modules.Mails.Api/"]
COPY ["Modules/Mails/Skillup.Modules.Mails.Core/Skillup.Modules.Mails.Core.csproj", "Modules/Mails/Skillup.Modules.Mails.Core/"]

COPY ["Modules/Notifications/Skillup.Modules.Notifications.Api/Skillup.Modules.Notifications.Api.csproj", "Modules/Notifications/Skillup.Modules.Notifications.Api/"]

RUN dotnet restore "./Bootstrapper/Skillup.Bootstrapper/Skillup.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/Bootstrapper/Skillup.Bootstrapper"
RUN dotnet build "./Skillup.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Skillup.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skillup.Bootstrapper.dll"]