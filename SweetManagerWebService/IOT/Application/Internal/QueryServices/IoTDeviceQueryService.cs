using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Queries;
using SweetManagerWebService.IOT.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Services;

namespace SweetManagerWebService.IOT.Application.Internal.QueryServices
{
    public class IoTDeviceQueryService
        (IIoTDeviceRepository repository) :
        IIoTDeviceQueryService
    {
        public async Task<IEnumerable<IoTDevice>> Handle(GetAllIoTDevicesQuery query)
            => await repository.ListAsync();

        public async Task<IoTDevice?> Handle(GetIoTDeviceByIdQuery query)
            => await repository.FindByIdAsync(query.Id);
    }
}