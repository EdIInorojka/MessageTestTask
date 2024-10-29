FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MessageTestTaskClient/MessageTestTaskClient.csproj", "MessageTestTaskClient/"]
RUN dotnet restore "./MessageTestTaskClient/MessageTestTaskClient.csproj"
COPY . .
WORKDIR "/src/MessageTestTaskClient"
RUN dotnet build "./MessageTestTaskClient.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessageTestTaskClient.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageTestTaskClient.dll"]

