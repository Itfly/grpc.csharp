FROM microsoft/dotnet:2.0-runtime-nanoserver-1709 
WORKDIR /app
COPY bin/Release/Publish/ .
ENTRYPOINT ["dotnet", "RouteGuideServer.dll"]
