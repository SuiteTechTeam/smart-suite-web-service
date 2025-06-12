using SweetManagerWebService.IOT.Domain.Model.Commands;

namespace SweetManagerWebService.IOT.Domain.Services
{
    public interface IRoomDeviceCommandService
    {
        public Task<bool> Handle(CreateRoomDeviceCommand command);
        public Task<bool> Handle(UpdateRoomDeviceCommand command);
    }
}