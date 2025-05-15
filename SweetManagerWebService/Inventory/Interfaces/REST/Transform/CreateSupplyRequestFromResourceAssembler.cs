using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;


namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

public class CreateSupplyRequestCommandFromResourceAssembler
{
    public static CreateSupplyRequestCommand ToCommandFromResource(CreateSupplyRequestResource resource)
    {
        return new CreateSupplyRequestCommand(
            resource.PaymentOwnerId,
            resource.SupplyId,
            resource.Count,
            resource.Amount
        );
    }
}