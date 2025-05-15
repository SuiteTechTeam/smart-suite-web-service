namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

public record CreateSupplyRequestResource(int PaymentOwnerId, int SupplyId, int Count, decimal Amount);
