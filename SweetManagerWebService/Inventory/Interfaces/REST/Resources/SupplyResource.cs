namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

public record SupplyResource(int Id, int ProviderId, int HotelId, string Name, decimal Price, int Stock, string State);