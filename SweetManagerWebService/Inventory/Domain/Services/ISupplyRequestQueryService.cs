using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Queries.SupplyRequest;

namespace SweetManagerIotWebService.API.Inventory.Domain.Services;

public interface ISupplyRequestQueryService
{
    Task<SupplyRequest?> Handle(GetSupplyRequestByIdQuery query);
    
    Task<IEnumerable<SupplyRequest>> Handle(GetAllSupplyRequestQuery query);
    
    Task<SupplyRequest?> Handle(GetSupplyRequestByPaymentOwnerIdQuery query);
    
    Task<SupplyRequest?> Handle(GetSupplyRequestBySupplyIdQuery query);
}