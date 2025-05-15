using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerIotWebService.API.IAM.Domain.Model.Exceptions;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Users
{
    public class AdminCommandService(IAdminRepository adminRepository,
        IHashingService hashingService, IUnitOfWork unitOfWork, 
        IAdminCredentialRepository adminCredentialRepository, ITokenService tokenService) : IAdminCommandService
    {
        public async Task<Admin?> Handle(SignUpUserCommand command)
        {
            try
            {
                if (await adminRepository.FindAllByFiltersAsync(command.Email, null, null) is not null)
                    throw new EmailAlreadyExistException();

                // Add Admin

                var entity = new Admin(command.Id, command.Name, command.Surname, command.Phone,
                    command.Email, "ACTIVE", 2);

                await adminRepository.AddAsync(entity);

                await unitOfWork.CommitAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the user: {ex.Message}");
            }
        }

        public async Task<Admin?> Handle(UpdateUserCommand command)
        {
            try
            {
                var admin = await adminRepository.FindByIdAsync(command.Id) ?? throw new Exception($"There's no admin with the given id: {command.Id}");

                admin.Update(command);

                await unitOfWork.CommitAsync();

                return admin;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the user: {ex.Message}");
            }
        }

        public async Task<dynamic?> Handle(SignInUserCommand command)
        {
            try
            {
                var user = await adminRepository.FindAllByFiltersAsync(command.Email, null, null);

                if (user is null)
                    throw new EmailDoesntExistException();

                AdminCredential userCredential = await adminCredentialRepository.FindByIdAsync(user.Id);

                if (!hashingService.VerifyHash(command.Password, userCredential!.Code[..24], userCredential!.Code[24..]))
                    throw new InvalidPasswordException();

                var hotel = await adminRepository.FindHotelIdByIdAsync(user.Id);

                hotel ??= 0;

                var token = tokenService.GenerateToken(new
                {
                    Id = user.Id,
                    PasswordHash = userCredential.Code,
                    Role = "ROLE_ADMIN",
                    Hotel = hotel
                });

                return new
                {
                    User = user,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
