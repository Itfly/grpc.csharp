FROM microsoft/dotnet:2.0-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 30002
COPY bin/Release/Publish/ .
ENTRYPOINT ["dotnet", "WorldServer.dll"]
