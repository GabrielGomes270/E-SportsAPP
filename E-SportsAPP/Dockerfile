# ===== Etapa 1: Build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj e restaura dependências
COPY ["E-SportsAPP.csproj", "./"]
RUN dotnet restore "./E-SportsAPP.csproj"

# Copia tudo e publica
COPY . .
RUN dotnet publish "E-SportsAPP.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== Etapa 2: Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Porta padrão
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Inicia a API
ENTRYPOINT ["dotnet", "E-SportsAPP.dll"]