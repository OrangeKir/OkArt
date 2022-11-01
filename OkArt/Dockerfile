FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OkArt/OkArt.csproj", "OkArt/"]
RUN dotnet restore "OkArt/OkArt.csproj"
COPY . .
WORKDIR "/src/OkArt"
RUN dotnet build "OkArt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OkArt.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OkArt.dll"]
