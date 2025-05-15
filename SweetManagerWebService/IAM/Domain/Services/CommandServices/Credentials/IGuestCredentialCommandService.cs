using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials
{
    public interface IGuestCredentialCommandService
    {
        Task<GuestCredential?> Handle(CreateUserCredentialCommand command);

        Task<GuestCredential?> Handle(UpdateUserCredentialCommand command);
    }
}