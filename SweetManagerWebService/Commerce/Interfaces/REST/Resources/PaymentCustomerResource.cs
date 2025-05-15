namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record PaymentCustomerResource(int Id, int? GuestId, decimal? FinalAmount);