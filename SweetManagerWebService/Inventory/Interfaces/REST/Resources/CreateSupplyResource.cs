namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

public record CreateSupplyResource(int ProviderId, int HotelId, string Name, decimal Price, int Stock, string State);