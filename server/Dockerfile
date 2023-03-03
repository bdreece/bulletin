# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /usr/share/bulletin/

COPY ./Bulletin.Server.csproj .
RUN dotnet restore

COPY ./* ./
RUN dotnet publish -c Release --no-restore

# Run stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime
WORKDIR /usr/share/locker/bin

COPY --from=build /usr/share/bulletin/bin/Release/net7.0/publish/ ./
ENTRYPOINT ["dotnet", "Bulletin.Server.dll"]