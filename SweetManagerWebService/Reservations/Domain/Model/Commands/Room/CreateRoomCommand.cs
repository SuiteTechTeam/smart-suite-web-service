using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;

namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Room;

public record CreateRoomCommand(
    int? TypeRoomId,
    int? HotelId,
    string? State);
