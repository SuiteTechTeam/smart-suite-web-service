namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record UpdateSubscriptionResource(int Id, string Name, string? Content, decimal? Price, string Status);