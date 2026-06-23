# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["server/TripPlanner.sln", "server/"]
COPY ["server/src/TripPlanner.Api/TripPlanner.Api.csproj", "server/src/TripPlanner.Api/"]
COPY ["server/src/TripPlanner.Application/TripPlanner.Application.csproj", "server/src/TripPlanner.Application/"]
COPY ["server/src/TripPlanner.Core/TripPlanner.Core.csproj", "server/src/TripPlanner.Core/"]
COPY ["server/src/TripPlanner.Infrastructure/TripPlanner.Infrastructure.csproj", "server/src/TripPlanner.Infrastructure/"]
COPY ["server/tests/TripPlanner.UnitTests/TripPlanner.UnitTests.csproj", "server/tests/TripPlanner.UnitTests/"]
COPY ["server/tests/TripPlanner.IntegrationTests/TripPlanner.IntegrationTests.csproj", "server/tests/TripPlanner.IntegrationTests/"]

# Restore dependencies
RUN dotnet restore "server/src/TripPlanner.Api/TripPlanner.Api.csproj"

# Copy full source and build
COPY server/ server/
WORKDIR "/src/server/src/TripPlanner.Api"
RUN dotnet build "TripPlanner.Api.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "TripPlanner.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Production Stage (Alpine for minimal size & security)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install dependencies required for QuestPDF in Alpine (fontconfig, fonts, libgdiplus if necessary)
RUN apk add --no-cache fontconfig ttf-dejavu

# Create non-root user
RUN adduser -u 1000 -D -s /bin/sh -h /app appuser
USER appuser

COPY --from=publish /app/publish .

# Enable OpenTelemetry/Prometheus metrics by default if used
ENV DOTNET_EnableDiagnostics=1

ENTRYPOINT ["dotnet", "TripPlanner.Api.dll"]
