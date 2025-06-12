using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class IotDeviceResourceFromEntityAssembler
    {
        public static IotDeviceResource ToResourceFromEntity(IoTDevice entity)
            => new(entity.Id, entity.Name);
    }
}