FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EasyPC.Subscriber/EasyPC.Subscriber.csproj", "EasyPC.Subscriber/"]
COPY ["EasyPC.Model/EasyPC.Model.csproj", "EasyPC.Model/"]
RUN dotnet restore "EasyPC.Subscriber/EasyPC.Subscriber.csproj"
COPY . .
WORKDIR "/src/EasyPC.Subscriber"
RUN dotnet build "EasyPC.Subscriber.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EasyPC.Subscriber.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyPC.Subscriber.dll"]
