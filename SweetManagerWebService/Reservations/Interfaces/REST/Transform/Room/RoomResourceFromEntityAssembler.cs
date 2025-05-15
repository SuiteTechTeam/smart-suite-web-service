using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Room;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Room;

public class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Domain.Model.Aggregates.Room entity)
        => new(entity.Id, entity.TypeRoomId, entity.HotelId, entity.State); 
}