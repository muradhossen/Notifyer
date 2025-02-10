using Notifyer.Domain;

namespace Notifyer.Application.Dtos;

public record NotificationReciverDto(string Msisdn, string serviceId, string Sms);
