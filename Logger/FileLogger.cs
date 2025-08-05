using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace PruebaChatMVC.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _filename;

        private readonly string _filePath;

        private readonly string _categoryName;

        private readonly string _realativePath;
        public FileLogger(string categoryName, string filename)
        {
            _categoryName = categoryName;
            _filename = filename;

            _realativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            Directory.CreateDirectory(_realativePath);

            _filePath = Path.Combine(_realativePath, filename);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (exception is not null)
                File.AppendAllText(_filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {_categoryName}: {formatter(state, exception)}{Environment.NewLine}");
            else
                File.AppendAllText(_filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {_categoryName}: {formatter(state, exception)}{Environment.NewLine}");
        }
    }
}
