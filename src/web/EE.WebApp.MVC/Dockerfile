FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/web/EE.WebApp.MVC/EE.WebApp.MVC.csproj", "EE.WebApp.MVC/"]
RUN dotnet restore "src/web/EE.WebApp.MVC/EE.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/EE.WebApp.MVC"
RUN dotnet build "EE.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EE.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EE.WebApp.MVC.dll"]
