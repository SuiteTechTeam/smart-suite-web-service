using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Domain.Repositories;

public interface IPaymentCustomerRepository : IBaseRepository<PaymentCustomer>
{
    Task<IEnumerable<PaymentCustomer>> FindByCustomerIdAsync(int customerId);
}