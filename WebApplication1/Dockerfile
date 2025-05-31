# 使用官方 .NET SDK 建構映像檔
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# 複製專案檔並還原相依
COPY . ./
RUN dotnet restore

# 建立 Release 版本
RUN dotnet publish -c Release -o out

# 建立 Runtime 映像檔
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# 啟動應用程式
ENTRYPOINT ["dotnet", "WebApplication1.dll"]