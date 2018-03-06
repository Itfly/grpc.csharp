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

    class Program
    {
        const int Port = 50052;

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

            Console.WriteLine("RouteGuide server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
