using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.EFC.Repositories;

public class PaymentCustomerRepository(SweetManagerContext context) : BaseRepository<PaymentCustomer>(context), IPaymentCustomerRepository
{
    public async Task<IEnumerable<PaymentCustomer>> FindByCustomerIdAsync(int customerId)
    {
        return await Context.Set<PaymentCustomer>().Where(paymentCustomer => paymentCustomer.GuestId.Equals(customerId)).ToListAsync();
    }
}