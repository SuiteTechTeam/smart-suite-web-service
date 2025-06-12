using SweetManagerWebService.IOT.Domain.Model.Entities;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class NotificationHistoryResourceFromEntityAssembler
    {
        public static NotificationHistoryResource ToResourceFromEntity(NotificationHistory entity)
            => new(entity.Id, entity.RoomDeviceId, entity.Metric);
    }
}