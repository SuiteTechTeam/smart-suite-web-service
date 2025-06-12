using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Entities;
using SweetManagerWebService.IOT.Domain.Repositories;

namespace SweetManagerWebService.IOT.Infrastructure.Persistence.EFC.Repositories
{
    public class NotificationHistoryRepository
        (SweetManagerContext context) :
        BaseRepository<NotificationHistory>(context),
        INotificationHistoryRepository
    {
        public async Task<IEnumerable<NotificationHistory>> FindByRoomDeviceIdAsync(int roomDeviceId)
            => await Context.Set<NotificationHistory>().Where(n => n.RoomDeviceId == roomDeviceId).AsNoTracking().ToListAsync();
    }
}