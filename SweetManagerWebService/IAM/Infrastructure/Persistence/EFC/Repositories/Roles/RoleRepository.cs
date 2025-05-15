using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Roles;

public class RoleRepository(SweetManagerContext context) : BaseRepository<Role>(context) , IRoleRepository
{
    public async Task<IEnumerable<Role>> FindAllAsync()
        => await Context.Set<Role>().ToListAsync();

    public async Task<Role?> FindByName(string name)
        => await Context.Set<Role>()
            .Where(rl => rl.Name == name)
            .FirstOrDefaultAsync();
    
    public async Task<int?> FindIdByName(string name)
        => await Context.Set<Role>()
            .Where(rl => rl.Name == name)
            .Select(rl => rl.Id)
            .FirstOrDefaultAsync();

}