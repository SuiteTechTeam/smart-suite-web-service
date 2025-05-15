using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials
{
    public interface IAdminCredentialCommandService
    {
        Task<AdminCredential?> Handle(CreateUserCredentialCommand command);

        Task<AdminCredential?> Handle(UpdateUserCredentialCommand command);
    }
}