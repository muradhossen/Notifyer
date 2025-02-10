using Notifyer.Domain;

namespace Notifyer.Application.Dtos
{
    public static class MappingExtention
    {
        public static NotificationReciver MapToEntity(this NotificationReciverDto notification)
        {

            return new NotificationReciver(notification.Msisdn, notification.serviceId, notification.Sms);
        }
        public static NotificationReciverDto MapToDto(this NotificationReciver notification)
        {

            return new NotificationReciverDto(notification.Msisdn, notification.ServiceId, notification.Message);
        }
    }
}
