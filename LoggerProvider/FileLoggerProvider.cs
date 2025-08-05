using Microsoft.Extensions.Logging;
using PruebaChatMVC.Logger;

namespace PruebaChatMVC.LoggerProvider
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _fileName;
        public FileLoggerProvider(string fileName)
        {
            _fileName = fileName;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, _fileName);
        }

        public void Dispose()
        {
            
        }
    }
}
