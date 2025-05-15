using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Credentials;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Credentials
{
    public class OwnerCredentialCommandService(IOwnerCredentialRepository ownerCredentialRepository,
        IHashingService hashingService, IUnitOfWork unitOfWork) : IOwnerCredentialCommandService
    {
        public async Task<OwnerCredential?> Handle(CreateUserCredentialCommand command)
        {
            try
            {
                var salt = hashingService.CreateSalt();
                var code = hashingService.HashCode(command.Code, salt);

                await ownerCredentialRepository.AddAsync(new OwnerCredential(command.Id, string.Concat(salt, code)));

                await unitOfWork.CommitAsync();

                return new OwnerCredential(command.Id, string.Concat(salt, code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OwnerCredential?> Handle(UpdateUserCredentialCommand command)
        {
            try
            {
                var salt = hashingService.CreateSalt();
                var code = hashingService.HashCode(command.Code, salt);

                await ownerCredentialRepository.AddAsync(new OwnerCredential(command.Id, string.Concat(salt, code)));

                await unitOfWork.CommitAsync();

                return new OwnerCredential(command.Id, string.Concat(salt, code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
