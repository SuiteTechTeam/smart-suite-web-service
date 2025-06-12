using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class UpdateRoomDeviceCommandFromResourceAssembler
    {
        public static UpdateRoomDeviceCommand ToCommandFromResource(UpdateRoomDeviceResource resource)
            => new(resource.Id, resource.IoTDeviceId, resource.RoomId);
    }
}