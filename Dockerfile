FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR ./

COPY . .

RUN dotnet restore

RUN dotnet build

RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://+:5999
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5999

ENTRYPOINT ["dotnet", "CoreService.API.dll"]