using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Credentials;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Credentials
{
    public class GuestCredentialCommandService(IGuestCredentialRepository guestCredentialRepository,
        IHashingService hashingService, IUnitOfWork unitOfWork) : IGuestCredentialCommandService
    {
        public async Task<GuestCredential?> Handle(CreateUserCredentialCommand command)
        {
            try
            {
                var salt = hashingService.CreateSalt();
                var code = hashingService.HashCode(command.Code, salt);

                await guestCredentialRepository.AddAsync(new GuestCredential(command.Id, string.Concat(salt, code)));

                await unitOfWork.CommitAsync();

                return new GuestCredential(command.Id, string.Concat(salt, code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GuestCredential?> Handle(UpdateUserCredentialCommand command)
        {
            try
            {
                var salt = hashingService.CreateSalt();
                var code = hashingService.HashCode(command.Code, salt);

                await guestCredentialRepository.AddAsync(new GuestCredential(command.Id, string.Concat(salt, code)));

                await unitOfWork.CommitAsync();

                return new GuestCredential(command.Id, string.Concat(salt, code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
