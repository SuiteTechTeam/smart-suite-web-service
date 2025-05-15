using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Roles;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Roles
{
    public class RoleRepository(SweetManagerContext context) : BaseRepository<Role>(context), IRoleRepository
    {
        public async Task<Role?> FindByNameAsync(string name)
        => await Context.Set<Role>().Where(r => r.Name.Equals(name)).FirstOrDefaultAsync();

        public async Task<int?> FindIdByNameAsync(string name)
        {
            var result = await Context.Set<Role>().Where(r => r.Name.Equals(name)).FirstOrDefaultAsync();

            return result?.Id;
        }
    }
}
