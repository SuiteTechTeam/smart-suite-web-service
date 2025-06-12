using SweetManagerWebService.IOT.Domain.Model.Commands;

namespace SweetManagerWebService.IOT.Domain.Model.Aggregates
{
    public class IoTDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RoomDevice> RoomDevices { get; set; } = [];

        public IoTDevice()
        {
            this.Name = string.Empty;
        }
        public IoTDevice(CreateIoTDeviceCommand command)
        {
            this.Name = command.Name;
        }
        public IoTDevice(UpdateIoTDeviceCommand command)
        {
            this.Id = command.Id;
            this.Name = command.Name;
        }
    }
}