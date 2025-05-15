using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Commerce.Domain.Services;

public interface IPaymentCustomerQueryService
{
    Task<IEnumerable<PaymentCustomer>> Handle(GetAllPaymentCustomersQuery query);
    Task<PaymentCustomer?> Handle(GetPaymentCustomerByIdQuery query);
    Task<IEnumerable<PaymentCustomer>> Handle(GetAllPaymentCustomersByCustomerIdQuery query);
}