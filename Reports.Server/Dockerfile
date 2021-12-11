FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Reports.Server/Reports.Server.csproj", "Reports.Server/"]
RUN dotnet restore "Reports.Server/Reports.Server.csproj"
COPY . .
WORKDIR "/src/Reports.Server"
RUN dotnet build "Reports.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reports.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reports.Server.dll"]
