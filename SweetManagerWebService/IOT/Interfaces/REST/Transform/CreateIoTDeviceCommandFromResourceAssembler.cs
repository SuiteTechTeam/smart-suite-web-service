using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class CreateIoTDeviceCommandFromResourceAssembler
    {
        public static CreateIoTDeviceCommand ToCommandFromResource(CreateIoTDeviceResource resource)
            => new(resource.Name);
    }
}