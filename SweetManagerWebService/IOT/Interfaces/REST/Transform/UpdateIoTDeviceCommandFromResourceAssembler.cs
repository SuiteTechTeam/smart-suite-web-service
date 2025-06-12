using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;

namespace SweetManagerWebService.IOT.Interfaces.REST.Transform
{
    public class UpdateIoTDeviceCommandFromResourceAssembler
    {
        public static UpdateIoTDeviceCommand ToCommandFromResource(UpdateIoTDeviceResource resource)
            => new(resource.Id, resource.Name);
    }
}