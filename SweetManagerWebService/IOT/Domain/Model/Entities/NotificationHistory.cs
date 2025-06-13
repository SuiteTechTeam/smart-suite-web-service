using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Commands;

namespace SweetManagerWebService.IOT.Domain.Model.Entities
{
    public partial class NotificationHistory
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
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
            this.RegistrationDate = DateTime.Now;
            this.RoomDeviceId = command.RoomDeviceId;
        }
        public NotificationHistory(UpdateNotificationHistoryCommand command)
        {
            this.Id = command.Id;
            this.Metric = command.Metric;
            this.RoomDeviceId = command.RoomDeviceId;
        }
    }
}