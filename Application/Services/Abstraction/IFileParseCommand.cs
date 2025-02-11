using Notifyer.Domain;

namespace Notifyer.Application.Services.Abstraction
{
    public interface IFileParseCommand
    {
        Task<List<NotificationReciver>> ExecuteAsync(string filePath);
    }
}
