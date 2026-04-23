# https://hub.docker.com/_/microsoft/dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["bookstore.csproj", "./"]
RUN dotnet restore bookstore.csproj

# Copy remaining files and build
COPY . ./
RUN dotnet publish bookstore.csproj -c Release -o /app/publish --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install SQL Server tools and fonts for Azure SQL connectivity
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    curl \
    apt-transport-https && \
    rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 8081

# Run as non-root user for security
RUN useradd -m -u 1000 appuser && chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "bookstore.dll"]
CMD ["ASPNETCORE_URLS=http://+:8080;ASPNETCORE_URLS=https://+:8081"]
