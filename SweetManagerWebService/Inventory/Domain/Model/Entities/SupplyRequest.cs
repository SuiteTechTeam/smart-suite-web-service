using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;

public partial class SupplyRequest
{
    public int Id { get; set; }

    public int PaymentOwnerId { get; set; }

    public int SupplyId { get; set; }

    public int Count { get; set; }

    public decimal Amount { get; set; }

    public virtual PaymentOwner? PaymentOwner { get; set; }

    public virtual Supply? Supply { get; set; }
    
    public SupplyRequest(int id, int paymentOwnerId, int supplyId, int count, decimal amount)
    {
        Id = id;
        PaymentOwnerId = paymentOwnerId;
        SupplyId = supplyId;
        Count = count;
        Amount = amount;
    }
    
    public SupplyRequest(CreateSupplyRequestCommand command)
    {
        PaymentOwnerId = command.PaymentOwnerId;
        SupplyId = command.SupplyId;
        Count = command.Count;
        Amount = command.Amount;
    }
}