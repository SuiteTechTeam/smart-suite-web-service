using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Roles
{
    public interface IRoleCommandService
    {
        Task<bool> Handle(SeedRolesCommand command);

    }
}