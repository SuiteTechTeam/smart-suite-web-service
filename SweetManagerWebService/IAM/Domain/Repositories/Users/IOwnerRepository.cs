using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Domain.Repositories.Users
{
    public interface IOwnerRepository : IBaseRepository<Owner>
    {
        Task<dynamic?> FindAllByFiltersAsync(string? email, string? phone, string? state);

        Task<Owner?> FindByHotelIdAsync(int hotelId);

        Task<int?> FindHotelIdByIdAsync(int id);
    }
}