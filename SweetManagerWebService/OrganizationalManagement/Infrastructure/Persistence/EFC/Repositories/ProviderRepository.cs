using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Infrastructure.Persistence.EFC.Repositories;

public class ProviderRepository(SweetManagerContext context) : BaseRepository<Provider>(context), IProviderRepository
{
    public async Task<IEnumerable<Provider>> GetAllProvidersAsync()
    {
        return await Context.Set<Provider>().ToListAsync();
    }
}