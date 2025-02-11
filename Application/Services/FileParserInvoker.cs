using Notifyer.Application.Services.Abstraction;
using Notifyer.Application.Services.FileParserCommands;
using Notifyer.Domain;

namespace Notifyer.Application.Services
{
    public class FileParserInvoker
    {
        private readonly Dictionary<string, IFileParseCommand> _commands;
        private readonly ILogger<LoggingFileParseCommand> _logger;

        public FileParserInvoker(IEnumerable<IFileParseCommand> commands, ILogger<LoggingFileParseCommand> logger)
        {
            _commands = new Dictionary<string, IFileParseCommand>();
            _logger = logger;

            foreach (var command in commands)
            {
                string key = command.GetType().Name.Replace("FileParseCommand", "").ToLower();
                _commands[key] = new LoggingFileParseCommand(command, _logger);  
            }
        }

        public async Task<List<NotificationReciver>> ExecuteCommand(string fileType, string filePath, INotificationStrategy notificationStrategy)
        {
            if (_commands.TryGetValue(fileType.ToLower(), out var command))
            {
                return await command.ExecuteAsync(filePath, notificationStrategy);
            }
            throw new NotSupportedException($"No command found for file type: {fileType}");
        }
    }
}
