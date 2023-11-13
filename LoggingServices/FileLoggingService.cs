using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingServices
{
    public class FileLoggingService : ILoggingService
    {
        private readonly string _logFilePath;

        public FileLoggingService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public async Task LogAsync(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            await File.AppendAllTextAsync(_logFilePath, logMessage + Environment.NewLine);
        }
    }
}
