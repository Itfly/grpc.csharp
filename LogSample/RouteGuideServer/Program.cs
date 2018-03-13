// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Routeguide
{
    using Grpc.Core.Interceptors;
    using Grpc.Core;
    using System;
    using Serilog;
    using Serilog.Sinks.SystemConsole.Themes;
    using System.Threading;
    using System.Runtime.InteropServices;

    class Program
    {
        const int Port = 50052;

        [DllImport("Kernel32")]
        internal static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool Add);

        internal delegate bool HandlerRoutine(CtrlTypes ctrlType);

        internal enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                    , theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            GrpcEnvironment.SetLogger(new SerilogGrpcLogger(Log.Logger));

            var features = RouteGuideUtil.ParseFeatures(RouteGuideUtil.DefaultFeaturesFile);

            var server = new Server
            {
                Services = { RouteGuide.BindService(new RouteGuideImpl(features)).Intercept(new LoggingInterceptor()) },
                Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) },
            };
            server.Start();

            Log.Logger.Information("RouteGuide server listening on port " + Port);

            var shutdown = new ManualResetEvent(false);
            var complete = new ManualResetEventSlim();

            var hr = new HandlerRoutine(type =>
            {
                Log.Logger.Information($"ConsoleCtrlHandler got signal: {type}");

                shutdown.Set();
                complete.Wait();

                return false;
            });
            SetConsoleCtrlHandler(hr, true);

            Log.Logger.Information("Waiting on handler to trigger...");

            shutdown.WaitOne();

            Log.Logger.Information("Stopping server...");
            Log.CloseAndFlush();
            server.ShutdownAsync().Wait();

            complete.Set();
            GC.KeepAlive(hr);
        }
    }
}
