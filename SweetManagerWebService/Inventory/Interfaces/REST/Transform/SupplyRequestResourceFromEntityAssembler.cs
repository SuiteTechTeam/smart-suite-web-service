

using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

public class SupplyRequestResourceFromEntityAssembler
{
    public static SupplyRequestResource ToResourceFromEntity(SupplyRequest resource)
    {
        return new SupplyRequestResource(resource.Id, resource.PaymentOwnerId, resource.SupplyId, resource.Count,resource.Amount);
    }
}