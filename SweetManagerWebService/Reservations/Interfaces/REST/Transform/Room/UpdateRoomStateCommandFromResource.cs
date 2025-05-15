using SweetManagerIotWebService.API.Reservations.Domain.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Room;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Room;

public class UpdateRoomStateCommandFromResource
{
    public static UpdateRoomStateCommand ToCommandFromResource(UpdateRoomStateResource resource) =>
        new(resource.Id, resource.State);
}