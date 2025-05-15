using SweetManagerIotWebService.API.Reservations.Domain.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Room;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Room;

public class CreateRoomCommandFromResourceAssembler
{
    public static CreateRoomCommand ToCommandFromResource(CreateRoomResource resource)
    {
        return new CreateRoomCommand(resource.TypeRoomId, resource.HotelId, resource.State);
    }
    
    
    
}