using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class CreateNotificationHistoryCommandFromResourceAssembler
    {
        public static CreateNotificationHistoryCommand ToCommandFromResource(CreateNotificationHistoryResource resource)
            => new(resource.RoomDeviceId, resource.Metric);
    }
}