using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Domain.Model.Entities;

namespace SweetManagerWebService.IOT.Domain.Model.Aggregates
{
    public class RoomDevice
    {
        public int Id { get; set; }
        public int IoTDeviceId { get; set; }
        public int RoomId { get; set; }

        public IoTDevice IoTDevice { get; set; }
        public Room Room { get; set; }

        public ICollection<NotificationHistory> NotificationHistories { get; set; } = [];

        public RoomDevice()
        {
            this.IoTDeviceId = 0;
            this.RoomId = 0;
        }
        public RoomDevice(CreateRoomDeviceCommand command)
        {
            this.IoTDeviceId = command.IoTDeviceId;
            this.RoomId = command.RoomId;
        }
        public RoomDevice(UpdateRoomDeviceCommand command)
        {
            this.Id = command.Id;
            this.IoTDeviceId = command.IoTDeviceId;
            this.RoomId = command.RoomId;
        }
    }
}