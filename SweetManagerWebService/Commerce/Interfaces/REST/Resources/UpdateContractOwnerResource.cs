namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record UpdateContractOwnerResource(int Id, int? OwnerId, DateTime? StartDate, DateTime? FinalDate, 
    int? SubscriptionId, string Status);