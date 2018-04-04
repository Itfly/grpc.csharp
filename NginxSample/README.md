# Get started with gRPC and Nginx
> DOcker Image for gRPC servers (HelloServer, WorldServer), ASP.NET Core server(HelloworldWeb) and Nginx

## run container
```
docker-compose up 
```

## Request Diagram
```
Ruquest --> Nginx (port:8080) --> HelloworldWeb --> Nginx (port: 80) --> HelloServer 
                                         |
                                         |--> Nginx (port: 80) --> WorldServer
```
