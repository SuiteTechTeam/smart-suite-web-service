using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Queries.Supply;

namespace SweetManagerIotWebService.API.Inventory.Domain.Services;

public interface ISupplyQueryService
{
    Task<Supply?> Handle(GetSupplyByIdQuery query);
    
    Task<IEnumerable<Supply>> Handle(GetAllSuppliesQuery query);
    
    Task<IEnumerable<Supply>> Handle(GetSupplyByProviderIdQuery query);
}