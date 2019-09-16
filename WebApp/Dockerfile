FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-preview8-alpine3.9 AS base

### The following libraries has been added to save download time during demos and shouldn't be installed on production env
RUN apk add curl
RUN curl -fSL "https://github.com/genuinetools/amicontained/releases/download/v0.4.7/amicontained-linux-amd64" -o "/usr/local/bin/amicontained" \
    && chmod a+x "/usr/local/bin/amicontained"
RUN curl -fSL https://storage.googleapis.com/kubernetes-release/release/$(curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt)/bin/linux/amd64/kubectl -o "/usr/local/bin/kubectl" \
    && chmod a+x "/usr/local/bin/kubectl"
##############################
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-preview8-alpine3.9 AS build
WORKDIR /src
COPY ["WebApp.csproj", ""]
RUN dotnet restore "WebApp.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app

FROM base AS final
ENV ASPNETCORE_URLS=http://*:3000
ENV COMPlus_EnableDiagnostics=0
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApp.dll"]