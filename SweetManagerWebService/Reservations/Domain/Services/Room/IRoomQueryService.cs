using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Reservations.Domain.Services.Room;

public interface IRoomQueryService
{
    Task<IEnumerable<Model.Aggregates.Room>> Handle(GetAllRoomsQuery query);

    Task<Model.Aggregates.Room?> Handle(GetRoomsByIdQuery query);

    Task<IEnumerable<Model.Aggregates.Room>> Handle(GetRoomsByStateQuery query);

    Task<IEnumerable<Model.Aggregates.Room>> Handle(GetRoomsByTypeRoomIdQuery query); 
    
    Task<IEnumerable<Model.Aggregates.Room>> Handle(GetAllRoomsByBookingAvailabilityInARangeQuery query);
}