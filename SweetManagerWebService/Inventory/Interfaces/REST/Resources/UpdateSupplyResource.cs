namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

public record UpdateSupplyResource(int Id, int ProviderId, int HotelId, string Name, decimal Price, int Stock, string State);