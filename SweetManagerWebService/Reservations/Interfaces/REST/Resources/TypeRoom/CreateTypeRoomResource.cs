namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.TypeRoom;

public record CreateTypeRoomResource(
    string Description,
    decimal Price);