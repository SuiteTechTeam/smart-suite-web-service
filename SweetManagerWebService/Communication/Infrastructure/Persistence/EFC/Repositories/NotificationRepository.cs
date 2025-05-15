using SweetManagerIotWebService.API.Communication.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Communication.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Communication.Infrastructure.Persistence.EFC.Repositories;

public class NotificationRepository(SweetManagerContext context) : BaseRepository<Notification>(context), INotificationRepository
{
    
}