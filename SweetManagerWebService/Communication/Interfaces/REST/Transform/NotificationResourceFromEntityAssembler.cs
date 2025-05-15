using SweetManagerIotWebService.API.Communication.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Communication.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Communication.Interfaces.REST.Transform;

public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(Notification entity) 
    {
        return new NotificationResource(entity.Id, entity.Title, entity.Content, entity.SenderType, entity.SenderId,
            entity.ReceiverId);
    }
}