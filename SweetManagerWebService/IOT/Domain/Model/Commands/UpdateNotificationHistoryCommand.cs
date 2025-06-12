namespace SweetManagerWebService.IOT.Domain.Model.Commands
{
    public record UpdateNotificationHistoryCommand(int Id, int RoomDeviceId, string Metric);
}