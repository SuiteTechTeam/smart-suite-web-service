using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Reservations.Domain.Services.TypeRoom;

public interface ITypeRoomQueryService
{
    Task<IEnumerable<Model.Entities.TypeRoom>> Handle(GetAllTypeRoomsQuery query);
    
    Task<Model.Entities.TypeRoom?> Handle(GetTypeRoomByIdQuery query);
}