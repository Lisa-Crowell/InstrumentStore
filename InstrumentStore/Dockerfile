FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InstrumentStore/InstrumentStore.csproj", "InstrumentStore/"]
RUN dotnet restore "InstrumentStore/InstrumentStore.csproj"
COPY . .
WORKDIR "/src/InstrumentStore"
RUN dotnet build "InstrumentStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstrumentStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstrumentStore.dll"]
