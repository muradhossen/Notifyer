using Notifyer.Application.Services.Abstraction;
using Notifyer.Domain;

namespace Notifyer.Application.Services
{
    public class RobiNotification : INotificationStrategy
    {
        public Task<bool> SendNotification(NotificationReciver reciver)
        {
            throw new NotImplementedException();
        }
    }
}
