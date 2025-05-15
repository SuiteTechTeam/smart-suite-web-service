using SweetManagerIotWebService.API.Inventory.Domain.Model.Queries.SupplyRequest;
using SweetManagerIotWebService.API.Inventory.Domain.Repositories;
using SweetManagerIotWebService.API.Inventory.Domain.Services;

namespace SweetManagerIotWebService.API.Inventory.Application.Internal.QueryServices;

public class SupplyRequestQueryService(ISupplyRequestRepository supplyRequestRepository) : ISupplyRequestQueryService
{
    private ISupplyRequestRepository _supplyRequestRepository = supplyRequestRepository;
    
    public async Task<Domain.Model.Entities.SupplyRequest?> Handle(GetSupplyRequestByIdQuery query)
    {
        return await _supplyRequestRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<IEnumerable<Domain.Model.Entities.SupplyRequest>> Handle(GetAllSupplyRequestQuery query)
    {
        return await _supplyRequestRepository.FindAllSuppliesRequestsAsync(query.HotelId);
    }
    
    public async Task<Domain.Model.Entities.SupplyRequest?> Handle(GetSupplyRequestByPaymentOwnerIdQuery query)
    {
        return await _supplyRequestRepository.FindByPaymentOwnerId(query.PaymentOwnerId);
    }
    
    public async Task<Domain.Model.Entities.SupplyRequest?> Handle(GetSupplyRequestBySupplyIdQuery query)
    {
        return await _supplyRequestRepository.FindBySupplyId(query.SupplyId);
    }
    
}