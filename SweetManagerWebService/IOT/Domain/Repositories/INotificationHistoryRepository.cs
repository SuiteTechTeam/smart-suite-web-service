using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Entities;

namespace SweetManagerWebService.IOT.Domain.Repositories
{
    public interface INotificationHistoryRepository : IBaseRepository<NotificationHistory>
    {
        public Task<IEnumerable<NotificationHistory>> FindByRoomDeviceIdAsync(int roomDeviceId);
    }
}