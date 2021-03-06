// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ping.proto
// </auto-generated>
#pragma warning disable 1591
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace Ping.Proto {
  /// <summary>
  /// Interface exported by the server.
  /// </summary>
  public static partial class PingApi
  {
    static readonly string __ServiceName = "Ping.PingApi";

    static readonly grpc::Marshaller<global::Ping.Proto.Ping> __Marshaller_Ping = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Ping.Proto.Ping.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Ping.Proto.Pong> __Marshaller_Pong = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Ping.Proto.Pong.Parser.ParseFrom);

    static readonly grpc::Method<global::Ping.Proto.Ping, global::Ping.Proto.Pong> __Method_Echo = new grpc::Method<global::Ping.Proto.Ping, global::Ping.Proto.Pong>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Echo",
        __Marshaller_Ping,
        __Marshaller_Pong);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Ping.Proto.PingReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of PingApi</summary>
    public abstract partial class PingApiBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Ping.Proto.Pong> Echo(global::Ping.Proto.Ping request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for PingApi</summary>
    public partial class PingApiClient : grpc::ClientBase<PingApiClient>
    {
      /// <summary>Creates a new client for PingApi</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public PingApiClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for PingApi that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public PingApiClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected PingApiClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected PingApiClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Ping.Proto.Pong Echo(global::Ping.Proto.Ping request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return Echo(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Ping.Proto.Pong Echo(global::Ping.Proto.Ping request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Echo, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Ping.Proto.Pong> EchoAsync(global::Ping.Proto.Ping request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return EchoAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Ping.Proto.Pong> EchoAsync(global::Ping.Proto.Ping request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Echo, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override PingApiClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new PingApiClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(PingApiBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Echo, serviceImpl.Echo).Build();
    }

  }
}
#endregion
