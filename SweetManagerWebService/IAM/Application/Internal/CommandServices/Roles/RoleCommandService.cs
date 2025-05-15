using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.ValueObjects;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Roles;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Roles
{
    public class RoleCommandService(IRoleRepository roleRepository,
        IUnitOfWork unitOfWork) : IRoleCommandService
    {
        public async Task<bool> Handle(SeedRolesCommand command)
        {
            foreach (var role in Enum.GetValues(typeof(ERoles)))
            {
                if (await roleRepository.FindByNameAsync(role.ToString()!) is null)
                {
                    await roleRepository.AddAsync(new Role(role.ToString()!));
                }
            }

            await unitOfWork.CommitAsync();

            return true;
        }
    }
}
