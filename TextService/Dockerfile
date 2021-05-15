#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TextService/TextService.csproj", "TextService/"]
COPY ["TextService.Services/TextService.Services.csproj", "TextService.Services/"]
COPY ["TextService.Repositories/TextService.Repositories.csproj", "TextService.Repositories/"]
COPY ["../RepositoryBase/RepositoryBase/RepositoryBase.csproj", "../RepositoryBase/RepositoryBase/"]
COPY ["TextService.Entities/TextService.Entities.csproj", "TextService.Entities/"]
COPY ["../AuthenticationBase/AuthenticationBase/AuthenticationBase.csproj", "../AuthenticationBase/AuthenticationBase/"]
RUN dotnet restore "TextService/TextService.csproj"
COPY . .
WORKDIR "/src/TextService"
RUN dotnet build "TextService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TextService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextService.dll"]