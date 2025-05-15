using SweetManagerIotWebService.API.Reservations.Domain.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Room;

namespace SweetManagerIotWebService.API.Reservations.Domain.Services.Room;

public interface IRoomCommandService
{
    Task<bool> Handle(CreateRoomCommand command);

    Task<bool> Handle(UpdateRoomStateCommand command);
}