# Get started with gRPC and Nginx
> A simple demo for running gRPC server and load balanced by Nginx.

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
