FROM microsoft/aspnetcore:2.0

WORKDIR /app
COPY bin/Release/Publish/ .

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "HelloworldWeb.dll"]
