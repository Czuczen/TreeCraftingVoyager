namespace TreeCraftingVoyager.Server.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private static ILogger? _loggerInstance;
    private readonly FileLoggerConfiguration _configuration;

    public FileLoggerProvider(FileLoggerConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ILogger CreateLogger(string categoryName) => _loggerInstance ??= new FileLogger(_configuration);

    public void Dispose()
    {
        if (_loggerInstance is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
