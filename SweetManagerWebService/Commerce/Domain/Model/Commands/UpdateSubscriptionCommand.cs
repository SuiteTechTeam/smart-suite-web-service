using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;

namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

public record UpdateSubscriptionCommand(
    int Id,
    ESubscriptionTypes Name, 
    string? Content, 
    decimal? Price, 
    EStates Status);