// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: todo.proto
// </auto-generated>
#pragma warning disable 1591
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace Todo.Proto {
  /// <summary>
  /// Interface exported by the server.
  /// </summary>
  public static partial class TodoApi
  {
    static readonly string __ServiceName = "todo.TodoApi";

    static readonly grpc::Marshaller<global::Todo.Proto.GetTodoItemRequest> __Marshaller_GetTodoItemRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Todo.Proto.GetTodoItemRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Todo.Proto.TodoItem> __Marshaller_TodoItem = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Todo.Proto.TodoItem.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Todo.Proto.AddTodoItemRequest> __Marshaller_AddTodoItemRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Todo.Proto.AddTodoItemRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Todo.Proto.UpdateTodoItemRequest> __Marshaller_UpdateTodoItemRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Todo.Proto.UpdateTodoItemRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Todo.Proto.DeleteTodoItemRequest> __Marshaller_DeleteTodoItemRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Todo.Proto.DeleteTodoItemRequest.Parser.ParseFrom);

    static readonly grpc::Method<global::Todo.Proto.GetTodoItemRequest, global::Todo.Proto.TodoItem> __Method_GetTodoItem = new grpc::Method<global::Todo.Proto.GetTodoItemRequest, global::Todo.Proto.TodoItem>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetTodoItem",
        __Marshaller_GetTodoItemRequest,
        __Marshaller_TodoItem);

    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Todo.Proto.TodoItem> __Method_GetAllTodoItems = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Todo.Proto.TodoItem>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "GetAllTodoItems",
        __Marshaller_Empty,
        __Marshaller_TodoItem);

    static readonly grpc::Method<global::Todo.Proto.AddTodoItemRequest, global::Todo.Proto.TodoItem> __Method_AddTodoItem = new grpc::Method<global::Todo.Proto.AddTodoItemRequest, global::Todo.Proto.TodoItem>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddTodoItem",
        __Marshaller_AddTodoItemRequest,
        __Marshaller_TodoItem);

    static readonly grpc::Method<global::Todo.Proto.UpdateTodoItemRequest, global::Todo.Proto.TodoItem> __Method_UpdateTodoItem = new grpc::Method<global::Todo.Proto.UpdateTodoItemRequest, global::Todo.Proto.TodoItem>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateTodoItem",
        __Marshaller_UpdateTodoItemRequest,
        __Marshaller_TodoItem);

    static readonly grpc::Method<global::Todo.Proto.DeleteTodoItemRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_DeleteTodoItem = new grpc::Method<global::Todo.Proto.DeleteTodoItemRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteTodoItem",
        __Marshaller_DeleteTodoItemRequest,
        __Marshaller_Empty);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Todo.Proto.TodoReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of TodoApi</summary>
    public abstract partial class TodoApiBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Todo.Proto.TodoItem> GetTodoItem(global::Todo.Proto.GetTodoItemRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task GetAllTodoItems(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::IServerStreamWriter<global::Todo.Proto.TodoItem> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Todo.Proto.TodoItem> AddTodoItem(global::Todo.Proto.AddTodoItemRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Todo.Proto.TodoItem> UpdateTodoItem(global::Todo.Proto.UpdateTodoItemRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> DeleteTodoItem(global::Todo.Proto.DeleteTodoItemRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for TodoApi</summary>
    public partial class TodoApiClient : grpc::ClientBase<TodoApiClient>
    {
      /// <summary>Creates a new client for TodoApi</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public TodoApiClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for TodoApi that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public TodoApiClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected TodoApiClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected TodoApiClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Todo.Proto.TodoItem GetTodoItem(global::Todo.Proto.GetTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetTodoItem(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Todo.Proto.TodoItem GetTodoItem(global::Todo.Proto.GetTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetTodoItem, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Todo.Proto.TodoItem> GetTodoItemAsync(global::Todo.Proto.GetTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetTodoItemAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Todo.Proto.TodoItem> GetTodoItemAsync(global::Todo.Proto.GetTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetTodoItem, null, options, request);
      }
      public virtual grpc::AsyncServerStreamingCall<global::Todo.Proto.TodoItem> GetAllTodoItems(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetAllTodoItems(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::Todo.Proto.TodoItem> GetAllTodoItems(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_GetAllTodoItems, null, options, request);
      }
      public virtual global::Todo.Proto.TodoItem AddTodoItem(global::Todo.Proto.AddTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return AddTodoItem(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Todo.Proto.TodoItem AddTodoItem(global::Todo.Proto.AddTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddTodoItem, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Todo.Proto.TodoItem> AddTodoItemAsync(global::Todo.Proto.AddTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return AddTodoItemAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Todo.Proto.TodoItem> AddTodoItemAsync(global::Todo.Proto.AddTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddTodoItem, null, options, request);
      }
      public virtual global::Todo.Proto.TodoItem UpdateTodoItem(global::Todo.Proto.UpdateTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return UpdateTodoItem(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Todo.Proto.TodoItem UpdateTodoItem(global::Todo.Proto.UpdateTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UpdateTodoItem, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Todo.Proto.TodoItem> UpdateTodoItemAsync(global::Todo.Proto.UpdateTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return UpdateTodoItemAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Todo.Proto.TodoItem> UpdateTodoItemAsync(global::Todo.Proto.UpdateTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UpdateTodoItem, null, options, request);
      }
      public virtual global::Google.Protobuf.WellKnownTypes.Empty DeleteTodoItem(global::Todo.Proto.DeleteTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return DeleteTodoItem(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Google.Protobuf.WellKnownTypes.Empty DeleteTodoItem(global::Todo.Proto.DeleteTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeleteTodoItem, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteTodoItemAsync(global::Todo.Proto.DeleteTodoItemRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return DeleteTodoItemAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteTodoItemAsync(global::Todo.Proto.DeleteTodoItemRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeleteTodoItem, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override TodoApiClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new TodoApiClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(TodoApiBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetTodoItem, serviceImpl.GetTodoItem)
          .AddMethod(__Method_GetAllTodoItems, serviceImpl.GetAllTodoItems)
          .AddMethod(__Method_AddTodoItem, serviceImpl.AddTodoItem)
          .AddMethod(__Method_UpdateTodoItem, serviceImpl.UpdateTodoItem)
          .AddMethod(__Method_DeleteTodoItem, serviceImpl.DeleteTodoItem).Build();
    }

  }
}
#endregion
