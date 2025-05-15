namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;

public record UpdateSupplyCommand(int Id, int ProviderId, int HotelId, string Name, decimal Price, int Stock, string State);