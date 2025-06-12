using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Queries;

namespace SweetManagerWebService.IOT.Domain.Services
{
    public interface IIoTDeviceQueryService
    {
        public Task<IEnumerable<IoTDevice>> Handle(GetAllIoTDevicesQuery query);
        public Task<IoTDevice?> Handle(GetIoTDeviceByIdQuery query);
    }
}