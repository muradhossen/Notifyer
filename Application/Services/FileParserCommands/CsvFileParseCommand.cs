using Notifyer.Application.Services.Abstraction;
using Notifyer.Domain;

namespace Notifyer.Application.Services.FileParserCommands
{
    public class CsvFileParseCommand : IFileParseCommand
    {
        public async Task<List<NotificationReciver>> ExecuteAsync(string filePath, INotificationStrategy notificationStrategy)
        {
            var result = new List<NotificationReciver>();

            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');

                var reciver = new NotificationReciver(parts[0], parts[1], parts[2]);
                result.Add(reciver);

               await notificationStrategy.SendNotification(reciver);
            }
            return result;
        }
    }
}
