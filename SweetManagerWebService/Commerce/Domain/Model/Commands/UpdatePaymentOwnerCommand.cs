namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

public record UpdatePaymentOwnerCommand(int Id, int? OwnerId, string? Description, decimal? FinalAmount);