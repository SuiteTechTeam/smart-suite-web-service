namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record CreateContractOwnerResource(int? OwnerId, DateTime? StartDate, DateTime? FinalDate, 
    int? SubscriptionId, string Status);