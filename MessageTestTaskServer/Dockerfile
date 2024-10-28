FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MessageTestTaskServer/MessageTestTaskServer.csproj", "MessageTestTaskServer/"]
RUN dotnet restore "./MessageTestTaskServer/MessageTestTaskServer.csproj"
COPY . .
WORKDIR "/src/MessageTestTaskServer"
RUN dotnet build "./MessageTestTaskServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessageTestTaskServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageTestTaskServer.dll"]