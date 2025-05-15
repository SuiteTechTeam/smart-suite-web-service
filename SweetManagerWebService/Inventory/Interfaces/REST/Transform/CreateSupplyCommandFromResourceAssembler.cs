using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;


namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

public class CreateSupplyCommandFromResourceAssembler
{
    public static CreateSupplyCommand ToCommandFromResource(CreateSupplyResource resource)
    {
        return new CreateSupplyCommand(resource.ProviderId, resource.HotelId, resource.Name, resource.Price, resource.Stock, resource.State);
    }
}