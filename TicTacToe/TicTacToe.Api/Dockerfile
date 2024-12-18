# 1. Базовый образ для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. Образ для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все csproj-файлы и восстанавливаем зависимости
COPY ["TicTacToe.Api/TicTacToe.Api.csproj", "TicTacToe.Api/"]
COPY ["TicTacToe.Data/TicTacToe.Data.csproj", "TicTacToe.Data/"]
COPY ["TicTacToe.Contracts/TicTacToe.Contracts.csproj", "TicTacToe.Contracts/"]
COPY ["TicTacToe.Domain/TicTacToe.Domain.csproj", "TicTacToe.Domain/"]
COPY ["TicTacToe.DTO/TicTacToe.DTO.csproj", "TicTacToe.DTO/"]
COPY ["TicTacToe.Application/TicTacToe.Application.csproj", "TicTacToe.Application/"]
COPY ["TicTacToe.Validation/TicTacToe.Validation.csproj", "TicTacToe.Validation/"]
RUN dotnet restore "TicTacToe.Api/TicTacToe.Api.csproj"

# Копируем исходники и выполняем сборку
COPY . . 
WORKDIR "/src/TicTacToe.Api"

# Строим проект
RUN dotnet build "TicTacToe.Api.csproj" -c Release -o /app/build

# 3. Публикация сборки
FROM build AS publish
RUN dotnet publish "TicTacToe.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


# 4. Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "TicTacToe.Api.dll"]
