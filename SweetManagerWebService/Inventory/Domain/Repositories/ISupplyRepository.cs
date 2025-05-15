using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.Inventory.Domain.Repositories;

public interface ISupplyRepository : IBaseRepository<Supply>
{
    public Task<IEnumerable<Supply>> FindByProviderId(int providerId);
    
    public Task<IEnumerable<Supply>> FindSuppliesByHotelIdAsync(int hotelId); 

}