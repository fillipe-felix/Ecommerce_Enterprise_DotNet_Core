FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/services/EE.Pedido.API/EE.Pedido.API.csproj", "EE.Pedido.API/"]
RUN dotnet restore "src/services/EE.Pedido.API/EE.Pedido.API.csproj"
COPY . .
WORKDIR "/src/EE.Pedido.API"
RUN dotnet build "EE.Pedido.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EE.Pedido.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EE.Pedido.API.dll"]
