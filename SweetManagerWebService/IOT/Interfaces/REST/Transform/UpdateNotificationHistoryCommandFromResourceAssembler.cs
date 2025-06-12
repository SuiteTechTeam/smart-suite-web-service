using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class UpdateNotificationHistoryCommandFromResourceAssembler
    {
        public static UpdateNotificationHistoryCommand ToCommandFromResource(UpdateNotificationHistoryResource resource)
            => new(resource.Id, resource.RoomDeviceId, resource.Metric);
    }
}