namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

public record UpdatePaymentCustomerCommand(int Id, int? GuestId, decimal? FinalAmount);