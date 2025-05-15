using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(SweetManagerContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<IEnumerable<Subscription>> FindByNameAsync(ESubscriptionTypes name)
    {
        return await Context.Set<Subscription>().Where(subscription => subscription.Name.Equals(name)).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> FindByStatusAsync(EStates status)
    {
        return await Context.Set<Subscription>().Where(subscription => subscription.Status.Equals(status)).ToListAsync();
    }
}