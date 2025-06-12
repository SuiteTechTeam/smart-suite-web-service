namespace SweetManagerWebService.IOT.Domain.Model.Commands
{
    public record CreateRoomDeviceCommand(int IoTDeviceId, int RoomId);
}