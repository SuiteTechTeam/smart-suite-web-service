using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.TypeRoom;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.TypeRoom;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.TypeRoom;

public class CreateTypeRoomCommandFromResourceAssembler
{
    public static CreateTypeRoomCommand CreateTypeRoomCommandFromResource(
        CreateTypeRoomResource resource)
    {
        return new CreateTypeRoomCommand(
            resource.Description,
            resource.Price);
    }
}