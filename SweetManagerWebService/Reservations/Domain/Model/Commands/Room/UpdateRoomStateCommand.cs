namespace SweetManagerIotWebService.API.Reservations.Domain.Commands.Room;

public record UpdateRoomStateCommand(
    int Id, string State);