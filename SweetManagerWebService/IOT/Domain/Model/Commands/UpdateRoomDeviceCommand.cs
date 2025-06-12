namespace SweetManagerWebService.IOT.Domain.Model.Commands
{
    public record UpdateRoomDeviceCommand(int Id, int IoTDeviceId, int RoomId);
}