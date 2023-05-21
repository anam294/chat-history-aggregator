FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ChatAggregator.Web/ChatAggregator.Web.csproj", "ChatAggregator.Web/"]
RUN dotnet restore "ChatAggregator.Web/ChatAggregator.Web.csproj"
COPY . .
WORKDIR "/src/ChatAggregator.Web"
RUN dotnet build "ChatAggregator.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatAggregator.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatAggregator.Web.dll"]
