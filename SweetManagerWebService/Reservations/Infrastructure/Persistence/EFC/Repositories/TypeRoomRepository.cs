using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Infrastructure.Persistence.EFC.Repositories;

public class TypeRoomRepository(SweetManagerContext context):BaseRepository<TypeRoom>(context), ITypeRoomRepository
{
    public async Task<IEnumerable<TypeRoom>> FindAllByHotelIdAsync(int? hotelId)
    {
        Task<IEnumerable<TypeRoom>> queryAsync = new(() => (
            from tr in Context.Set<TypeRoom>().ToList()
            join ro in Context.Set<Room>().ToList() on tr.Id equals ro.TypeRoomId
            where ro.HotelId == hotelId
            select tr
        ).ToList());

        queryAsync.Start();
        var result = await queryAsync;
        return result;
    }
}