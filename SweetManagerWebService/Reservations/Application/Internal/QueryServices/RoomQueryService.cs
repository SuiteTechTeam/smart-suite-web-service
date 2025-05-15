using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Room;

namespace SweetManagerIotWebService.API.Reservations.Application.Internal.QueryServices;

public class RoomQueryService(IRoomRepository roomRepository) : IRoomQueryService
{
    public async Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query)
    {
        return await roomRepository.FindAllByHotelIdAsync(query.HotelId);
    }

    public async Task<Room?> Handle(GetRoomsByIdQuery query)
    {
        return await roomRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Room>> Handle(GetRoomsByStateQuery query)
    {
        return await roomRepository.FindByStateAsync(query.State);
    }

    public async Task<IEnumerable<Room>> Handle(GetRoomsByTypeRoomIdQuery query)
    {
        return await roomRepository.FindByTypeRoomIdAsync(query.TypeRoomId);
    }

    public async Task<IEnumerable<Room>> Handle(GetAllRoomsByBookingAvailabilityInARangeQuery query)
    {
        return await roomRepository.FindByRange(query.StartDate, query.FinalDate, query.HotelId);
    }
}