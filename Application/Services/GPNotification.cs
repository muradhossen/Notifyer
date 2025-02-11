using Notifyer.Application.Services.Abstraction;
using Notifyer.Domain;

namespace Notifyer.Application.Services
{
    public class GPNotification : INotificationStrategy
    {
        const string SMS_ULR = "https://api.gp.com/notifications";
        const string AUTH_KEY = "GP_AUTH_KEY";
        public async Task<bool> SendNotification(NotificationReciver reciver)
        {
            //string acr = await GetPaymentInfoAsync(reciver.Msisdn, reciver.ServiceId);

            //if (string.IsNullOrWhiteSpace(acr)) return false;

            //return await SentMessageAsync(acr, reciver.Message);

            Console.WriteLine($"Notification send for {reciver.Msisdn} {reciver.ServiceId}");
            return true;
        }

        public async Task<bool> SentMessageAsync(string acr, string message)
        {
           
            return false;
        }

        public async Task<string> GetPaymentInfoAsync(string msisdn, string serviceId)
        {
            return string.Empty;
        }
    }
}
