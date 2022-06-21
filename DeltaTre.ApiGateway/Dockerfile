#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5100
ENV ASPNETCORE_URLS=http://+:5100
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeltaTre.ApiGateway/DeltaTre.ApiGateway.csproj", "DeltaTre.ApiGateway/"]
RUN dotnet restore "DeltaTre.ApiGateway/DeltaTre.ApiGateway.csproj"
COPY . .
WORKDIR "/src/DeltaTre.ApiGateway"
RUN dotnet build "DeltaTre.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeltaTre.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeltaTre.ApiGateway.dll"]
