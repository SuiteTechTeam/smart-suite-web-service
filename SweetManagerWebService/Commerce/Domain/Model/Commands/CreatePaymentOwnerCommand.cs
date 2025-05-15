namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

public record CreatePaymentOwnerCommand(int? OwnerId, string? Description, decimal? FinalAmount);