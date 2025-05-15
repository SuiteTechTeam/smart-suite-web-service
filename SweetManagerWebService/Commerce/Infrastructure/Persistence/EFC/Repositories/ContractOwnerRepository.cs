using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Entities;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.EFC.Repositories;

public class ContractOwnerRepository(SweetManagerContext context) : BaseRepository<ContractOwner>(context), IContractOwnerRepository
{
    public async Task<IEnumerable<ContractOwner>> FindByOwnerIdAsync(int ownerId)
    {
        return await Context.Set<ContractOwner>().Where(contractOwner => contractOwner.OwnerId.Equals(ownerId)).ToListAsync();
    }

    public async Task<IEnumerable<ContractOwner>> FindBySubscriptionIdAsync(int subscriptionId)
    {
        return await Context.Set<ContractOwner>().Where(contractOwner => contractOwner.SubscriptionId.Equals(subscriptionId)).ToListAsync();
    }
}