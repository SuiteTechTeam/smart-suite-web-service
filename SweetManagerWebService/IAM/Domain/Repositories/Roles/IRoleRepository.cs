using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Domain.Repositories.Roles
{
    public interface IRoleRepository : IBaseRepository<Role>
    {

        Task<Role?> FindByNameAsync(string name);

        Task<int?> FindIdByNameAsync(string name);
    }
}