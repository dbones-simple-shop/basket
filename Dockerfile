FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
COPY ./src/Basket/bin/Release/netcoreapp2.2 /app
WORKDIR /app
ENTRYPOINT ["dotnet", "Basket.dll"]
EXPOSE 7001