using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.TypeRoom;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.TypeRoom;

public class TypeRoomResourceFromEntityAssembler
{
    public static TypeRoomResource ToResourceFromEntity(Domain.Model.Entities.TypeRoom entity) =>
        new TypeRoomResource(
            entity.Id,
            entity.Description,
            entity.Price
        );
}