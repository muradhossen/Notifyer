using Notifyer.Application.Services;
using Notifyer.Application.Services.Abstraction;

namespace Notifyer.Application.Factories;

public class NotificationFactory
{
    public static INotificationStrategy GetNotificationStrategy(string operatorName)
    {
        return operatorName switch
        {
            "BL" => new BLNotification(),
            "Robi" => new RobiNotification(),
            "GP" => new GPNotification(),
            _ => throw new NotImplementedException("Operator not supported")
        };
    }
}
