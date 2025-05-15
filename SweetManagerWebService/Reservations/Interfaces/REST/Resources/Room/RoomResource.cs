namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Room;

public record RoomResource(
    int Id, int? TypeRoomId, int? HotelId, string? State);
    
