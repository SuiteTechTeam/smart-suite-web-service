using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials
{
    public interface IOwnerCredentialCommandService
    {
        Task<OwnerCredential?> Handle(CreateUserCredentialCommand command);

        Task<OwnerCredential?> Handle(UpdateUserCredentialCommand command);
    }
}