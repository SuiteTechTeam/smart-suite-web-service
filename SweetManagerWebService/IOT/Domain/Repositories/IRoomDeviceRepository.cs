using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Aggregates;

namespace SweetManagerWebService.IOT.Domain.Repositories
{
    public interface IRoomDeviceRepository : IBaseRepository<RoomDevice>
    {
        public Task<IEnumerable<RoomDevice>> FindByIoTDeviceIdAsync(int ioTDeviceId);
        public Task<IEnumerable<RoomDevice>> FindByRoomIdAsync(int roomId);
    }
}