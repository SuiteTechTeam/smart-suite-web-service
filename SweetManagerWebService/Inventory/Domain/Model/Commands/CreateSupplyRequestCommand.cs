namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;

public record CreateSupplyRequestCommand(int PaymentOwnerId, int SupplyId, int Count, decimal Amount);