namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;

public record CreateSupplyCommand(int ProviderId, int HotelId, string Name, decimal Price, int Stock, string State);
