# Secure gRPC service with TLS/SSL

This tutorial builds a security gRPC service with TLS/SSL authentication to provide generic mechanisms to authenticate the server and client, and to encrypt all the data exchanged.

## Create SSL/TLS Certificates

Install openssl by choco

```cmd
choco install openssl.light
```

reference:
https://grpc.io/docs/guides/auth.html#supported-auth-mechanisms
https://bbengfort.github.io/programmer/2017/03/03/secure-grpc.html
