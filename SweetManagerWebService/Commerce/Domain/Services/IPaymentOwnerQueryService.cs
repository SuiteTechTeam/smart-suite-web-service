using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Commerce.Domain.Services;

public interface IPaymentOwnerQueryService
{
    Task<IEnumerable<PaymentOwner>> Handle(GetAllPaymentOwnersQuery query);
    Task<PaymentOwner?> Handle(GetPaymentOwnerByIdQuery query);
    Task<IEnumerable<PaymentOwner>> Handle(GetAllPaymentOwnersByOwnerIdQuery query);
}