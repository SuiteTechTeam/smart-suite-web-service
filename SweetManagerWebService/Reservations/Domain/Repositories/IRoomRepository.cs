using System.Collections;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<IEnumerable<Room>> FindAllByHotelIdAsync(int? hotelId);

    Task<IEnumerable<Room>> FindByStateAsync(string? state);

    Task<IEnumerable<Room>> FindByTypeRoomIdAsync(int typeroomid);
    
    Task<IEnumerable<Room>> FindByRange(DateTime start, DateTime end, int HotelId);

    Task<bool> UpdateRoomStateAsync(int id, string state);
}