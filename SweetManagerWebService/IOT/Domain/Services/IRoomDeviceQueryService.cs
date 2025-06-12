using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Model.Queries;

namespace SweetManagerWebService.IOT.Domain.Services
{
    public interface IRoomDeviceQueryService
    {
        public Task<IEnumerable<RoomDevice>> Handle(GetRoomDevicesByIoTDeviceIdQuery query);
        public Task<IEnumerable<RoomDevice>> Handle(GetRoomDevicesByRoomIdQuery query);
    }
}