using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Repositories;

namespace SweetManagerWebService.IOT.Infrastructure.Persistence.EFC.Repositories
{
    public class IoTDeviceRepository
        (SweetManagerContext context) :
        BaseRepository<IoTDevice>(context),
        IIoTDeviceRepository
    { }
}