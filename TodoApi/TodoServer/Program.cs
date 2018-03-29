using Grpc.Common.Interceptor;
using Grpc.Common.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;
using TodoServer.Rpc;
using TodoServer.Service;
using TodoServer.Storage;

namespace TodoServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureLogging((context, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .CreateLogger();
                    logging.AddSerilog(Log.Logger, dispose: true);
                })
                .ConfigureServices((context, serviceCollection) =>
                {
                    var config = context.Configuration;
                    //ConfigureLogger(serviceCollection, config);
                    AddServices(serviceCollection, config);
                });

            await hostBuilder.RunConsoleAsync();
        }

        private static void ConfigureLogger(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            serviceCollection.AddSingleton(new LoggerFactory()
                .AddSerilog(Log.Logger, dispose: true)
                .LogGrpc());
            serviceCollection.AddLogging();
        }

        private static void AddServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // add db context
            var dbName = configuration["Database:DbName"];
            serviceCollection.AddDbContext<TodoContext>(
                options => options.UseInMemoryDatabase(dbName),
                /* only because it's InMemory, for real DB, use a DbContextFactory or just use using statement */
                ServiceLifetime.Singleton);

            serviceCollection.AddSingleton<ITodoService, TodoService>();

            serviceCollection.AddSingleton<RpcResolver>();

            serviceCollection.AddGrpcServer(builder =>
            {
                builder.SetConfig(configuration);
                builder.AddInterceptor(new LoggingInterceptor());
            },
            // TODO: any good idea to resolve server
            serviceProvider => {
                return Todo.Proto.TodoApi.BindService(serviceProvider.GetRequiredService<RpcResolver>());
                }
            );
            
        }
    }
}
