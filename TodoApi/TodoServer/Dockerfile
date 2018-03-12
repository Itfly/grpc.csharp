FROM microsoft/dotnet:2.0-runtime-nanoserver-1709 

WORKDIR /app
COPY bin/Release/netcoreapp2.0/Publish .

ENTRYPOINT ["dotnet", "TodoServer.dll"]