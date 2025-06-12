using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Commands;

namespace SweetManagerWebService.IOT.Domain.Model.Entities
{
    public class NotificationHistory
    {
        public int Id { get; set; }
        public string Metric { get; set; }
        public int RoomDeviceId { get; set; }

        public RoomDevice RoomDevice { get; set; }

        public NotificationHistory()
        {
            this.Metric = string.Empty;
            this.RoomDeviceId = 0;
        }
        public NotificationHistory(CreateNotificationHistoryCommand command)
        {
            this.Metric = command.Metric;
            this.RoomDeviceId = command.RoomDeviceId;
        }
        public NotificationHistory(UpdateNotificationHistoryCommand command)
        {
            this.Metric = command.Metric;
            this.RoomDeviceId = command.RoomDeviceId;
        }
    }
}