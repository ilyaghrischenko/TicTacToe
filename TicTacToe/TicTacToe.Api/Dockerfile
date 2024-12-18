# 1. Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 2. Копируем все файлы решения и восстанавливаем зависимости
COPY *.sln ./
COPY TicTacToe.Api/*.csproj ./TicTacToe.Api/
COPY TicTacToe.Application/*.csproj ./TicTacToe.Application/
COPY TicTacToe.Contracts/*.csproj ./TicTacToe.Contracts/
COPY TicTacToe.Data/*.csproj ./TicTacToe.Data/
COPY TicTacToe.Domain/*.csproj ./TicTacToe.Domain/
COPY TicTacToe.DTO/*.csproj ./TicTacToe.DTO/
COPY TicTacToe.Validation/*.csproj ./TicTacToe.Validation/
RUN dotnet restore

# 3. Копируем остальной исходный код и публикуем проект API
COPY . ./
RUN dotnet publish TicTacToe.Api/TicTacToe.Api.csproj -c Release -o out

# 4. Используем ASP.NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# 5. Настройка порта и запуска приложения
EXPOSE 8080
ENTRYPOINT ["dotnet", "TicTacToe.Api.dll"]
