using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Inventory.Domain.Repositories;

public interface ISupplyRequestRepository: IBaseRepository<SupplyRequest>
{
    public Task<SupplyRequest?> FindBySupplyId(int supplyId);
    
    public Task<SupplyRequest?> FindByPaymentOwnerId(int paymentOwnerId);

    public Task<IEnumerable<SupplyRequest>> FindAllSuppliesRequestsAsync(int HotelId);
}