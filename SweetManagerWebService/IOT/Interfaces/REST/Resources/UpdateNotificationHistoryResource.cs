namespace SweetManagerWebService.IOT.Interfaces.REST.Resources
{
    public record UpdateNotificationHistoryResource(int Id, int RoomDeviceId, string Metric);
}