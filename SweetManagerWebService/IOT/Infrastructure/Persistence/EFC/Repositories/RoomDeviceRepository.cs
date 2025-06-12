using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Aggregates;
using SweetManagerWebService.IOT.Domain.Repositories;

namespace SweetManagerWebService.IOT.Infrastructure.Persistence.EFC.Repositories
{
    public class RoomDeviceRepository
        (SweetManagerContext context) :
        BaseRepository<RoomDevice>(context),
        IRoomDeviceRepository
    {
        public async Task<IEnumerable<RoomDevice>> FindByIoTDeviceIdAsync(int ioTDeviceId)
            => await Context.Set<RoomDevice>().Where(r => r.IoTDeviceId == ioTDeviceId).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<RoomDevice>> FindByRoomIdAsync(int roomId)
            => await Context.Set<RoomDevice>().Where(r => r.RoomId == roomId).AsNoTracking().ToListAsync();
    }
}