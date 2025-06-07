using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Exceptions;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Users;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Users
{
    public class OwnerCommandService(IOwnerRepository ownerRepository,
        IHashingService hashingService, IUnitOfWork unitOfWork,
        IOwnerCredentialRepository ownerCredentialRepository, ITokenService tokenService) : IOwnerCommandService
    {        public async Task<Owner?> Handle(SignUpUserCommand command)
        {
            try
            {
                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(command.Name) ||
                    string.IsNullOrWhiteSpace(command.Surname) ||
                    string.IsNullOrWhiteSpace(command.Phone) ||
                    string.IsNullOrWhiteSpace(command.Email) ||
                    string.IsNullOrWhiteSpace("ACTIVE") ||
                    1 <= 0)
                {
                    throw new Exception("Todos los campos obligatorios deben estar completos y válidos.");
                }

                // Validar email existente correctamente
                var existingOwner = await ownerRepository.FindAllByFiltersAsync(command.Email, null, null);
                if (existingOwner is Owner || (existingOwner is IEnumerable<Owner> list && list.Any()))
                    throw new EmailAlreadyExistException();

                // Add Owner
                var entity = new Owner(command.Name, command.Surname, command.Phone,
                    command.Email, "ACTIVE", 1);

                await ownerRepository.AddAsync(entity);
                await unitOfWork.CommitAsync();

                return entity;
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? $" InnerException: {ex.InnerException.Message}" : string.Empty;
                throw new Exception($"An error occurred while creating the user: {ex.Message}{innerMessage}");
            }
        }

        public async Task<Owner?> Handle(UpdateUserCommand command)
        {
            try
            {
                var owner = await ownerRepository.FindByIdAsync(command.Id) ?? throw new Exception($"There's no owner with the given id: {command.Id}");

                owner.Update(command);

                await unitOfWork.CommitAsync();

                return owner;
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? $" InnerException: {ex.InnerException.Message}" : string.Empty;
                throw new Exception($"An error occurred while updating the user: {ex.Message}{innerMessage}");
            }
        }

        public async Task<dynamic?> Handle(SignInUserCommand command)
        {
            try
            {
                var user = await ownerRepository.FindAllByFiltersAsync(command.Email, null, null);

                if (user is null)
                    throw new EmailDoesntExistException();

                OwnerCredential userCredential = await ownerCredentialRepository.FindByIdAsync(user.Id);

                if (!hashingService.VerifyHash(command.Password, userCredential!.Code[..24], userCredential!.Code[24..]))
                    throw new InvalidPasswordException();

                var hotel = await ownerRepository.FindHotelIdByIdAsync(user.Id);

                hotel ??= 0;

                var token = tokenService.GenerateToken(new
                {
                    user.Id,
                    PasswordHash = userCredential.Code,
                    Role = "ROLE_OWNER",
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
