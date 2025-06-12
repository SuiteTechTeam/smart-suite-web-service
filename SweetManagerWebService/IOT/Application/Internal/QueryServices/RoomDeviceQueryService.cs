using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Queries;
using SweetManagerWebService.IOT.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Services;

namespace SweetManagerWebService.IOT.Application.Internal.QueryServices
{
    public class RoomDeviceQueryService
        (IRoomDeviceRepository repository) :
        IRoomDeviceQueryService
    {
        public async Task<IEnumerable<RoomDevice>> Handle(GetRoomDevicesByIoTDeviceIdQuery query)
            => await repository.FindByIoTDeviceIdAsync(query.IoTDeviceId);

        public async Task<IEnumerable<RoomDevice>> Handle(GetRoomDevicesByRoomIdQuery query)
            => await repository.FindByRoomIdAsync(query.RoomId);
    }
}