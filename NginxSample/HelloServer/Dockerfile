FROM microsoft/dotnet:2.1-runtime

WORKDIR /app
EXPOSE 30001
COPY bin/Release/Publish/ .
ENTRYPOINT ["dotnet", "HelloServer.dll"]
