
setlocal

@rem enter this directory
cd /d %~dp0

set TOOLS_PATH=..\Tools
set PROTOBUF_PATH=..\
set PROTO_PATH=Todo\protos

%TOOLS_PATH%\protoc.exe -I%PROTOBUF_PATH% -I %PROTO_PATH% --csharp_out %PROTO_PATH%  %PROTO_PATH%\todo.proto --grpc_out %PROTO_PATH% --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe

endlocal