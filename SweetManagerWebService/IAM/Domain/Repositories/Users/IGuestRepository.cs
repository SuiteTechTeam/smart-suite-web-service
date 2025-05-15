using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Domain.Repositories.Users
{
    public interface IGuestRepository : IBaseRepository<Guest>
    {
        Task<dynamic?> FindAllByFiltersAsync(string? email, string? phone, string? state);

        Task<IEnumerable<Guest>> FindAllByHotelIdAsync(int hotelId);

        Task<int?> FindHotelIdByIdAsync(int id);
    }
}