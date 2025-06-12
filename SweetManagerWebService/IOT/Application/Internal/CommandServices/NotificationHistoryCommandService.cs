using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Model.Commands;
using SweetManagerWebService.IOT.Domain.Repositories;
using SweetManagerWebService.IOT.Domain.Services;

namespace SweetManagerWebService.IOT.Application.Internal.CommandServices
{
    public class NotificationHistoryCommandService
        (INotificationHistoryRepository repository, IUnitOfWork unitOfWork) :
        INotificationHistoryCommandService
    {
        public async Task<bool> Handle(CreateNotificationHistoryCommand command)
        {
            try
            {
                await repository.AddAsync(new(command));

                await unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> Handle(UpdateNotificationHistoryCommand command)
        {
            try
            {
                repository.Update(new(command));

                await unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception) { return false; }
        }
    }
}