using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;

namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

public record UpdateContractOwnerCommand(
    int Id,
    int? OwnerId,
    DateTime? StartDate,
    DateTime? FinalDate,
    int? SubscriptionId,
    EStates Status);