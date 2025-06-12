using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Aggregates;

namespace SweetManagerWebService.IOT.Domain.Repositories
{
    public interface IIoTDeviceRepository : IBaseRepository<IoTDevice>
    { }
}