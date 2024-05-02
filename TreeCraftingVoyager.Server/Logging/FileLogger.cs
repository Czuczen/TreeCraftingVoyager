using TreeCraftingVoyager.Server.Logging;

public class FileLogger : ILogger, IDisposable
{
    private readonly FileLoggerConfiguration _configuration;
    private readonly object _lockObject = new();
    private StreamWriter? _streamWriter;
    private bool _disposed;

    public FileLogger(FileLoggerConfiguration configuration)
    {
        _configuration = configuration;
        var logDirectory = Path.GetDirectoryName(configuration.LogFilePath) ?? throw new InvalidOperationException("Log file path is not valid.");

        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        _streamWriter = new StreamWriter(configuration.LogFilePath, true);
    }

    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _configuration.ToFileLogLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel) || _disposed) return;

        lock (_lockObject)
        {
            if (_streamWriter!.BaseStream.Length > _configuration.MaxFileSizeBytes)
            {
                RotateLogFiles();
            }

            var nowDateTime = DateTime.Now;
            var logMessage = $"{logLevel.ToString().ToUpper()} {nowDateTime},{nowDateTime.Millisecond} || {formatter(state, exception)}";
            if (exception != null)
            {
                logMessage += Environment.NewLine + exception;
            }

            _streamWriter.WriteLine(logMessage);
            _streamWriter.Flush();
        }
    }

    private void RotateLogFiles()
    {
        _streamWriter?.Close();
        _streamWriter = null;

        var logFilePath = _configuration.LogFilePath;
        var logDirectory = Path.GetDirectoryName(logFilePath);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFilePath);
        var fileExtension = Path.GetExtension(logFilePath);

        var logFiles = Directory.GetFiles(logDirectory, $"{fileNameWithoutExtension}*{fileExtension}")
            .OrderByDescending(File.GetLastWriteTime)
            .ToList();

        while (logFiles.Count >= _configuration.MaxFileCount)
        {
            var fileToRemove = logFiles.Last();
            File.Delete(fileToRemove);
            logFiles.Remove(fileToRemove);
        }

        for (int i = logFiles.Count - 1; i >= 0; i--)
        {
            var newFileName = $"{fileNameWithoutExtension}_{i + 1}{fileExtension}";
            var newFilePath = Path.Combine(logDirectory, newFileName);

            File.Move(logFiles[i], newFilePath);
        }

        _streamWriter = new StreamWriter(logFilePath, true);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _streamWriter?.Close();
            }

            _disposed = true;
        }
    }
}
