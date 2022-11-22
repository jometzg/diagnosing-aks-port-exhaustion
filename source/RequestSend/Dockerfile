#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV one=https://jjtargetone.azurewebsites.net/weatherforecast
ENV two=https://jjtargettwo.azurewebsites.net/weatherforecast
ENV three=https://jjtargetthree.azurewebsites.net/weatherforecast
ENV threenew=true

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RequestSend/RequestSend.csproj", "RequestSend/"]
RUN dotnet restore "RequestSend/RequestSend.csproj"
COPY . .
WORKDIR "/src/RequestSend"
RUN dotnet build "RequestSend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RequestSend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RequestSend.dll"]