using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.Commerce.Domain.Services;

public interface IPaymentOwnerCommandService
{
    Task<PaymentOwner?> Handle(CreatePaymentOwnerCommand command);
    Task<PaymentOwner?> Handle(UpdatePaymentOwnerCommand command);  
}