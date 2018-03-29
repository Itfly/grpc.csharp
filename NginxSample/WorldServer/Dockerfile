FROM microsoft/dotnet:2.1-runtime

WORKDIR /app
EXPOSE 30002
COPY bin/Release/Publish/ .
ENTRYPOINT ["dotnet", "WorldServer.dll"]
