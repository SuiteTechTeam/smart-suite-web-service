using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials;
using SweetManagerIotWebService.API.IAM.Infrastructure.Hashing.Argon2Id.Services;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Credentials
{
    public class AdminCredentialCommandService(IAdminCredentialRepository adminCredentialRepository, 
        IHashingService hashingService, IUnitOfWork unitOfWork) : IAdminCredentialCommandService
    {
        public async Task<AdminCredential?> Handle(CreateUserCredentialCommand command)
        {
            try
            {
                var salt = hashingService.CreateSalt();

                var code = hashingService.HashCode(command.Code, salt);

                await adminCredentialRepository.AddAsync(new AdminCredential(command.Id, string.Concat(salt, code)));

                await unitOfWork.CommitAsync();

                return new AdminCredential(command.Id, string.Concat(salt, code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AdminCredential?> Handle(UpdateUserCredentialCommand command)
        {
            try
            {
                var salt = hashingService.CreateSalt();

                var code = hashingService.HashCode(command.Code, salt);

                adminCredentialRepository.Update(new AdminCredential(command.Id, string.Concat(salt, code)));

                await unitOfWork.CommitAsync();

                return new AdminCredential(command.Id, string.Concat(salt, code));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
