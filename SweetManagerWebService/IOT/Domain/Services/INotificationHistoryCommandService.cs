using SweetManagerWebService.IOT.Domain.Model.Commands;

namespace SweetManagerWebService.IOT.Domain.Services
{
    public interface INotificationHistoryCommandService
    {
        public Task<bool> Handle(CreateNotificationHistoryCommand command);
        public Task<bool> Handle(UpdateNotificationHistoryCommand command);
    }
}