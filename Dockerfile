FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Microservice.Authentication/Microservice.Authentication.csproj", "Microservice.Authentication/"]
COPY ["Microservice.Infrastructure/Microservice.Infrastructure.csproj", "Microservice.Infrastructure/"]
COPY ["Microservice.Authentication.Domain/Microservice.Authentication.Domain.csproj", "Microservice.Authentication.Domain/"]
RUN dotnet restore "Microservice.Authentication/Microservice.Authentication.csproj"
COPY . .
WORKDIR "/src/Microservice.Authentication"
RUN dotnet build "Microservice.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Authentication.dll"]
