namespace SweetManagerWebService.IOT.Domain.Model.Commands
{
    public record CreateNotificationHistoryCommand(int RoomDeviceId, string Metric);
}