namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record PaymentOwnerResource(int Id, int? OwnerId, string? Description, decimal? FinalAmount);