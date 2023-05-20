FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ChatHistoryAggregator/ChatHistoryAggregator.csproj", "ChatHistoryAggregator/"]
RUN dotnet restore "ChatHistoryAggregator/ChatHistoryAggregator.csproj"
COPY . .
WORKDIR "/src/ChatHistoryAggregator"
RUN dotnet build "ChatHistoryAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatHistoryAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatHistoryAggregator.dll"]
