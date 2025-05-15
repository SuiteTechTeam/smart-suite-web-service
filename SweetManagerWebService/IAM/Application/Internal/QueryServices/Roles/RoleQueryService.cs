using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Roles;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Roles
{
    public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
    {
        public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query)
         => await roleRepository.ListAsync();

        public async Task<Role?> Handle(GetRoleByNameQuery query)
         => await roleRepository.FindByNameAsync(query.Name);

        public async Task<int?> Handle(GetRoleIdByNameQuery query)
         => await roleRepository.FindIdByNameAsync(query.Name);
    }
}
