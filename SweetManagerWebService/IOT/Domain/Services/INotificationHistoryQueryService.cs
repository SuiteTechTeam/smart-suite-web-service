using SweetManagerWebService.IOT.Domain.Model.Entities;
using SweetManagerWebService.IOT.Domain.Model.Queries;

namespace SweetManagerWebService.IOT.Domain.Services
{
    public interface INotificationHistoryQueryService
    {
        public Task<IEnumerable<NotificationHistory>> Handle(GetNotificationHistoryByRoomDeviceIdQuery query);
    }
}