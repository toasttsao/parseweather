# Stage 1. dotnet 程式編譯環境
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build_env
# 參數
ARG DOTNET_BUILD_CONFIG=Release
# 複製原始碼
COPY ./ ./Parseweather
# 移至要建置的專案
WORKDIR ./Parseweather
# 編譯發布  --disable-parallel 
RUN dotnet publish -o /app -c $DOTNET_BUILD_CONFIG
#移至編譯過後指定的的資料夾 
WORKDIR /app

# Stage 2. 建置程式執行環境
FROM mcr.microsoft.com/dotnet/core/runtime:2.2 as runtime
# 從Stage 1 複製編譯過後的檔案至執行環境
COPY --from=build_env /app /app
# 切換執行位置
WORKDIR /app
#執行
ENTRYPOINT ["dotnet", "Parseweather.dll"]

