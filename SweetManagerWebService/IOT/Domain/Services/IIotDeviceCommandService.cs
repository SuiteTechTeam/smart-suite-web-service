using SweetManagerWebService.IOT.Domain.Model.Commands;

namespace SweetManagerWebService.IOT.Domain.Services
{
    public interface IIotDeviceCommandService
    {
        public Task<bool> Handle(CreateIoTDeviceCommand command);
        public Task<bool> Handle(UpdateIoTDeviceCommand command);
    }
}