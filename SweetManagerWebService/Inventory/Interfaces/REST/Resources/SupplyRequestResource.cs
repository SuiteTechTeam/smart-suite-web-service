namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

public record SupplyRequestResource(int Id, int PaymentOwnerId, int SupplyId, int Count, decimal Amount);
