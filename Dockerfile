FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["Judoca/Judoca/Judoca.csproj", "Judoca/Judoca/"]
RUN dotnet restore "Judoca\Judoca\Judoca.csproj"
COPY . .
WORKDIR "/src/Judoca/Judoca"
RUN dotnet build "Judoca.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Judoca.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Judoca.dll