FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set environment variables and work directory

WORKDIR /app

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

# Check if the user with the specified ID already exists, and if not, create a new user
RUN if ! id -u $APP_UID > /dev/null 2>&1; then adduser --disabled-password --gecos '' --uid $APP_UID appuser; fi
USER $APP_UID


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["SweetManagerWebService/SweetManagerWebService.csproj", "SweetManagerWebService/"]
RUN dotnet restore "SweetManagerWebService/SweetManagerWebService.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/SweetManagerWebService"
RUN dotnet build "SweetManagerWebService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SweetManagerWebService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
# Copy the published output from the build stage

COPY --from=publish /app/publish .

# Change ownership and permissions of the app directory
USER root
RUN chown -R $APP_UID:$APP_UID /app
RUN chmod -R 755 /app

# Switch to the custom user
USER $APP_UID

ENTRYPOINT ["dotnet", "SweetManagerWebService.dll"]
