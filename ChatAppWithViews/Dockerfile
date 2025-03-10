# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["SimpleChatAppRabbitMQ/SimpleChatAppRabbitMQ.csproj", "SimpleChatAppRabbitMQ/"]
RUN dotnet restore "SimpleChatAppRabbitMQ/SimpleChatAppRabbitMQ.csproj"

# Copy the rest of the application and build
COPY . .
WORKDIR "/src/SimpleChatAppRabbitMQ"
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleChatAppRabbitMQ.dll"]
