using Notifyer.Application.Services.Abstraction;
using Notifyer.Domain;

namespace Notifyer.Application.Services.FileParserCommands
{
    public class LoggingFileParseCommand : IFileParseCommand
    {
        private readonly IFileParseCommand _innerCommand;
        private readonly ILogger<LoggingFileParseCommand> _logger;

        public LoggingFileParseCommand(IFileParseCommand innerCommand, ILogger<LoggingFileParseCommand> logger)
        {
            _innerCommand = innerCommand;
            _logger = logger;
        }

        public async Task<List<NotificationReciver>> ExecuteAsync(string filePath)
        {
            _logger.LogInformation($"[START] Processing file: {filePath}");

            var result = await _innerCommand.ExecuteAsync(filePath);

            _logger.LogInformation($"[END] Completed processing file: {filePath}");
            return result;
        }
    }
}
