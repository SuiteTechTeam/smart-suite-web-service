using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.Commerce.Domain.Services;

public interface IPaymentCustomerCommandService
{
    Task<PaymentCustomer?> Handle(CreatePaymentCustomerCommand command);
    Task<PaymentCustomer?> Handle(UpdatePaymentCustomerCommand command);
}