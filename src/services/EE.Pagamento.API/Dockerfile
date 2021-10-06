FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/services/EE.Pagamento.API/EE.Pagamento.API.csproj", "EE.Pagamento.API/"]
RUN dotnet restore "src/services/EE.Pagamento.API/EE.Pagamento.API.csproj"
COPY . .
WORKDIR "/src/EE.Pagamento.API"
RUN dotnet build "EE.Pagamento.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EE.Pagamento.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EE.Pagamento.API.dll"]
