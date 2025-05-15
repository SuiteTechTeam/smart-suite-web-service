using SweetManagerIotWebService.API.Communication.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Communication.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Communication.Domain.Services;

public interface INotificationQueryService
{
    Task<Notification?> Handle(GetNotificationByIdQuery query);
    
}