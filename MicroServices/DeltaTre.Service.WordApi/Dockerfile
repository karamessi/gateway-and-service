#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroServices/DeltaTre.Service.WordApi/DeltaTre.Service.WordApi.csproj", "MicroServices/DeltaTre.Service.WordApi/"]
RUN dotnet restore "MicroServices/DeltaTre.Service.WordApi/DeltaTre.Service.WordApi.csproj"
COPY . .
WORKDIR "/src/MicroServices/DeltaTre.Service.WordApi"
RUN dotnet build "DeltaTre.Service.WordApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeltaTre.Service.WordApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeltaTre.Service.WordApi.dll"]
