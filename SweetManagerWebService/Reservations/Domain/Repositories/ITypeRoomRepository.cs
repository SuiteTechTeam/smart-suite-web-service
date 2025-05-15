using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Domain.Repositories;

public interface ITypeRoomRepository : IBaseRepository<TypeRoom>
{
    Task <IEnumerable<TypeRoom>> FindAllByHotelIdAsync(int? hotelId);
    
    
}