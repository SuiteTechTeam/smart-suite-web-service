using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Roles;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Roles
{
    public interface IRoleQueryService
    {
        Task<IEnumerable<Role>> Handle(GetAllRolesQuery query);

        Task<Role?> Handle(GetRoleByNameQuery query);

        Task<int?> Handle(GetRoleIdByNameQuery query);
    }
}