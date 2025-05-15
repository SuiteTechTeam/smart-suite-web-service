using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

public class UpdateSupplyCommandFromResource
{
    public static UpdateSupplyCommand FromResource(int Id, UpdateSupplyResource resource)
    {
        return new UpdateSupplyCommand(Id,  resource.ProviderId, resource.HotelId, resource.Name, resource.Price,resource.Stock, resource.State);
    }
}
