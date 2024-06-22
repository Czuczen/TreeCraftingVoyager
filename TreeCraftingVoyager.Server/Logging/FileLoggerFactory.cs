namespace TreeCraftingVoyager.Server.Logging;

public static class FileLoggerFactory
{
    private static FileLoggerProvider? _providerInstance;
    private static ILogger? _loggerInstance;
    private static readonly object _lock = new();

    public static ILogger GetLogger()
    {
        if (_loggerInstance == null)
        {
            lock (_lock)
            {
                if (_loggerInstance == null)
                {
                    _loggerInstance = CreateLoggerInstance();
                }
            }
        }

        return _loggerInstance;
    }

    public static WebApplicationBuilder AddFileLogger(this WebApplicationBuilder builder)
    {
        var fileLoggingConfig = builder.Configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        if (fileLoggingConfig.Enabled)
        {
            builder.Logging.AddProvider(_providerInstance ??= new FileLoggerProvider(fileLoggingConfig));
        }

        return builder;
    }

    private static ILogger CreateLoggerInstance()
    {
        var fileLoggingConfig = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build().GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        return (_providerInstance ??= new FileLoggerProvider(fileLoggingConfig)).CreateLogger("");
    }
}
