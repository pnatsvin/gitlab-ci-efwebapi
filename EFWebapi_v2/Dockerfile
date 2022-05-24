#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:44319
ENV ASPNETCORE_ENVIROMENT=Development

EXPOSE 44319
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EFWebapi_v2/EFWebapi_v2.csproj", "EFWebapi_v2/"]
RUN dotnet restore "EFWebapi_v2/EFWebapi_v2.csproj"

COPY . .
WORKDIR "/src/EFWebapi_v2"
RUN dotnet build "EFWebapi_v2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFWebapi_v2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFWebapi_v2.dll"]
