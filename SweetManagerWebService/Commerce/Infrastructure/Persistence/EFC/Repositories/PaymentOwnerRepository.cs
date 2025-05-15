using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.EFC.Repositories;

public class PaymentOwnerRepository(SweetManagerContext context) : BaseRepository<PaymentOwner>(context), IPaymentOwnerRepository
{
    public async Task<IEnumerable<PaymentOwner>> FindByOwnerIdAsync(int ownerId)
    {
        return await Context.Set<PaymentOwner>().Where(paymentOwner => paymentOwner.OwnerId.Equals(ownerId)).ToListAsync();
    }
}