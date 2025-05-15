
using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

public class SupplyResourceFromEntityAssembler
{
    public static SupplyResource ToResourceFromEntity(Supply supply)
    {
        return new SupplyResource(supply.Id, supply.ProviderId, supply.HotelId, supply.Name, supply.Price, supply.Stock, supply.State);
    }
}

