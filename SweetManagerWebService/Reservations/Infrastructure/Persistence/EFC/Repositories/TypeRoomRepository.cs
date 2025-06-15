using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SweetManagerIotWebService.API.Reservations.Infrastructure.Persistence.EFC.Repositories;

public class TypeRoomRepository(SweetManagerContext context):BaseRepository<TypeRoom>(context), ITypeRoomRepository
{
    public async Task<IEnumerable<TypeRoom>> FindAllByHotelIdAsync(int? hotelId)
    {
        return await Context.Set<TypeRoom>()
            .Where(tr => tr.HotelId == hotelId)
            .ToListAsync();
    }
}