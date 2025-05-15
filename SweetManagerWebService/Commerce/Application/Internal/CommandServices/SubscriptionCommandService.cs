using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.CommandServices;

public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        var subscription = new Subscription(command);
        try
        {
            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CommitAsync();
            return subscription;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the subscription: {e.Message}");
            return null;
        }
    }

    public async Task<Subscription?> Handle(UpdateSubscriptionCommand command)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.Id);
        if (subscription == null)
        {
            Console.WriteLine($"Subscription with ID {command.Id} not found.");
            return null;
        }

        var newSubscription = new Subscription(command);

        try
        {
            subscriptionRepository.Remove(subscription);
            await subscriptionRepository.AddAsync(newSubscription);
            await unitOfWork.CommitAsync();
            return newSubscription;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the subscription: {e.Message}");
            return null;
        }
    }
}