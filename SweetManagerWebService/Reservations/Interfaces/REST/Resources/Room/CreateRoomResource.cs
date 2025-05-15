namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Room;

public record CreateRoomResource(int TypeRoomId, int HotelId, string State);

