using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query)
    {
        return await subscriptionRepository.ListAsync();
    }

    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
    {
        return await subscriptionRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByNameQuery query)
    {
        return await subscriptionRepository.FindByNameAsync(query.Name);
    }

    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByStatusQuery query)
    {
        return await subscriptionRepository.FindByStatusAsync(query.Status);
    }
}