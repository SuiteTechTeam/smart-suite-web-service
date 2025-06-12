using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class RoomDeviceResourceFromEntityAssembler
    {
        public static RoomDeviceResource ToResourceFromEntity(RoomDevice entity)
            => new(entity.Id, entity.IoTDeviceId, entity.RoomId);
    }
}