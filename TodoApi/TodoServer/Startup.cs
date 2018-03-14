namespace TodoServer
{
    using System;
    using System.Threading;

    using Grpc.Common.Logging;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using TodoServer.Service;
    using TodoServer.Storage;

    public class Startup
    {
        private static ILogger<Startup> _logger;
        private static ServiceProvider _serviceProvider;
        private static RpcApp _app;

        private static IConfigurationRoot Configuration { get; set; }

        public static void Initialize()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _logger = _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Startup>();

            // Global unhandled exception handler
            AppDomain.CurrentDomain.UnhandledException += LogUnhandledException;

            _app = new RpcApp(_serviceProvider);
        }

        public static void Start()
        {
            _app.Start();

            WaitForShutdownSignal();

            // shutdown server and dispose resources
            _app.ShutdownAsync().Wait();
            _serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            AddConfiguration(serviceCollection);
            AddServices(serviceCollection);
            // add logging
            ConfigureLogger(serviceCollection);
        }

        private static void AddConfiguration(IServiceCollection serviceCollection)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            serviceCollection.AddSingleton(Configuration);
        }

        private static void ConfigureLogger(IServiceCollection serviceCollection)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            serviceCollection.AddSingleton(new LoggerFactory()
                .AddSerilog(Log.Logger, dispose: true)
                .LogGrpc());
            serviceCollection.AddLogging();
        }

        private static void AddServices(IServiceCollection serviceCollection)
        {
            // add db context
            var dbName = Configuration["Database:DbName"];
            serviceCollection.AddDbContext<TodoContext>(
                options => options.UseInMemoryDatabase(dbName),
                /* only because it's InMemory */
                ServiceLifetime.Singleton);

            serviceCollection.AddScoped<ITodoService, TodoService>();
        }

        private static void WaitForShutdownSignal()
        {
            _logger.LogInformation("Press ctrl+c to stop the server...");

            var resetEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                _logger.LogInformation("Receive terminal signal.");
                resetEvent.Set();
            };

            resetEvent.WaitOne();
        }

        private static void LogUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            _logger.LogError(ex, "Unhandled Exception:");
            _logger.LogError(($"Runtime terminating: {e.IsTerminating}"));
        }
    }
}
