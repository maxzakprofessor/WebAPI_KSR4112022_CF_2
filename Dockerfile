# 1. Сборка (SDK 6.0)
FROM ://mcr.microsoft.com AS build
WORKDIR /src

# Копируем файл проекта из подпапки во внутреннюю папку Docker
# Это критически важно для восстановления NuGet пакетов
COPY ["WebAPI_KSR4112022_CF_2/WebAPI_KSR4112022_CF_2.csproj", "WebAPI_KSR4112022_CF_2/"]
RUN dotnet restore "WebAPI_KSR4112022_CF_2/WebAPI_KSR4112022_CF_2.csproj"

# Копируем все файлы из корня (включая папку с кодом)
COPY . .

# Переходим во вложенную папку для сборки
WORKDIR "/src/WebAPI_KSR4112022_CF_2"
RUN dotnet publish -c Release -o /app/publish

# 2. Запуск (Runtime 6.0)
FROM ://mcr.microsoft.com
WORKDIR /app
COPY --from=build /app/publish .

# Настройка порта 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5000

# Запускаем DLL (она лежит в корне папки /app после публикации)
ENTRYPOINT ["dotnet", "WebAPI_KSR4112022_CF_2.dll"]
