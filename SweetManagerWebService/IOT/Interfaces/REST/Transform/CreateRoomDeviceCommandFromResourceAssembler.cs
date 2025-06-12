using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class CreateRoomDeviceCommandFromResourceAssembler
    {
        public static CreateRoomDeviceCommand ToCommandFromResource(CreateRoomDeviceResource resource)
            => new(resource.IoTDeviceId, resource.RoomId);
    }
}