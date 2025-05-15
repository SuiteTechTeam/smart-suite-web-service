using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Infrastructure.Persistence.EFC.Repositories;

public class RoomRepository(SweetManagerContext context): BaseRepository<Room>(context), IRoomRepository
{
    

    public async Task<IEnumerable<Room>> FindAllByHotelIdAsync(int? hotelId)
    {
        return await Context.Set<Room>()
            .Where(r => r.HotelId == hotelId).ToListAsync(); 
    }

    public async Task<IEnumerable<Room>> FindByStateAsync(string? state)
    {
        return await Context.Set<Room>()
            .Where(r => r.State == state).ToListAsync();

    }

    public async Task<IEnumerable<Room>> FindByTypeRoomIdAsync(int typeroomid)
    {
        return await Context.Set<Room>()
            .Where(r => r.TypeRoomId == typeroomid).ToListAsync();
    }

    public async Task<IEnumerable<Room>> FindByRange(DateTime startDate, DateTime finalDate, int hotelId)
    {
        return await Context.Set<Room>()
            .Where(r =>
                r.HotelId == hotelId &&
                !r.Bookings.Any(b =>
                    b.StartDate <= finalDate && b.FinalDate >= startDate))
            .ToListAsync();
    }



    public async Task<bool> UpdateRoomStateAsync(int id, string state)
    {
        var room = await Context.Set<Room>().FindAsync(id);

        if (room == null)
            return false;

        room.State = state;
        await Context.SaveChangesAsync();

        return true;
    }

}