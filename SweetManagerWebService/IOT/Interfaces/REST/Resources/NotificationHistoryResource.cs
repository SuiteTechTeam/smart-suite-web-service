namespace SweetManagerWebService.IOT.Interfaces.REST.Resources
{
    public record NotificationHistoryResource(int Id, DateTime RegistrationDate, int RoomDeviceId, string Metric);
}