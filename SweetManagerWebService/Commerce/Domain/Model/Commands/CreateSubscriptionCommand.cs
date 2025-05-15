using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;

namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

public record CreateSubscriptionCommand(
    ESubscriptionTypes Name, 
    string? Content, 
    decimal? Price, 
    EStates Status);