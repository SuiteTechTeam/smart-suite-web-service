using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Repositories;

public interface IHotelRepository : IBaseRepository<Hotel>
{
    Task<Hotel?> FindByNameAndEmailAsync(string name, string email);
    Task<IEnumerable<Hotel>> GetAllHotelsAsync();
    Task<IEnumerable<Hotel>> FindByOwnerIdAsync(int ownerId);
}