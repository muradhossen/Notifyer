using Notifyer.Domain;

namespace Notifyer.Application.Services.Abstraction
{
    public interface INotificationStrategy
    {
        Task<bool> SendNotification(NotificationReciver reciver);
    }
}
