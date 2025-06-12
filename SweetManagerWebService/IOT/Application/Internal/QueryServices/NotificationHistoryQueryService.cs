using SweetManagerWebService.IOT.Domain.Model.Entities;
using SweetManagerWebService.IOT.Domain.Model.Queries;
using SweetManagerWebService.IOT.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Services;

namespace SweetManagerWebService.IOT.Application.Internal.QueryServices
{
    public class NotificationHistoryQueryService
        (INotificationHistoryRepository repository) :
        INotificationHistoryQueryService
    {
        public async Task<IEnumerable<NotificationHistory>> Handle(GetNotificationHistoryByRoomDeviceIdQuery query)
            => await repository.FindByRoomDeviceIdAsync(query.RoomDeviceId);
    }
}