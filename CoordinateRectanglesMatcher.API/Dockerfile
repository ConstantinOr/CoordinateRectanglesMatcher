FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoordinateRectanglesMatcher.API/CoordinateRectanglesMatcher.API.csproj", "CoordinateRectanglesMatcher.API/"]
COPY ["CoordinateRectanglesMatcher.DAO/CoordinateRectanglesMatcher.DAO.csproj", "CoordinateRectanglesMatcher.DAO/"]
COPY ["CoordinateRectanglesMatcher.Models/CoordinateRectanglesMatcher.Models.csproj", "CoordinateRectanglesMatcher.Models/"]
COPY ["CoordinateRectanglesMatcher.Services/CoordinateRectanglesMatcher.Services.csproj", "CoordinateRectanglesMatcher.Services/"]
RUN dotnet restore "CoordinateRectanglesMatcher.API/CoordinateRectanglesMatcher.API.csproj"
COPY . .
WORKDIR "/src/CoordinateRectanglesMatcher.API"
RUN dotnet build "CoordinateRectanglesMatcher.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoordinateRectanglesMatcher.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoordinateRectanglesMatcher.API.dll"]
