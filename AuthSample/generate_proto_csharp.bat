
setlocal

@rem enter this directory
cd /d %~dp0

set TOOLS_PATH=..\packages\grpc.tools\1.9.0\tools\windows_x64
set PROTOBUF_PATH=..\
set PROTO_PATH=Ping\protos

%TOOLS_PATH%\protoc.exe -I %PROTO_PATH% --csharp_out %PROTO_PATH%  %PROTO_PATH%\ping.proto --grpc_out %PROTO_PATH% --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe

endlocal