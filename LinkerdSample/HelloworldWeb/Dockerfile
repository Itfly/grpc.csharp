FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80
COPY bin/Release/Publish/ .
ENTRYPOINT ["dotnet", "HelloworldWeb.dll"]
