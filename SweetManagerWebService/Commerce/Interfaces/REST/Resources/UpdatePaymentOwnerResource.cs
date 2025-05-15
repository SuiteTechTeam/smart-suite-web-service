namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record UpdatePaymentOwnerResource(int Id, int? OwnerId, string? Description, decimal? FinalAmount);