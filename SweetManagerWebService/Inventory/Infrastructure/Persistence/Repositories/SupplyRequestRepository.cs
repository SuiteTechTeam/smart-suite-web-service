using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.Inventory.Domain.Repositories;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.Inventory.Infrastructure.Persistence.Repositories;

public class SupplyRequestRepository(SweetManagerContext context) : BaseRepository<SupplyRequest>(context), ISupplyRequestRepository
{
    public async Task<SupplyRequest?> FindBySupplyId(int supplyId)
    {
        return await Context.Set<SupplyRequest>().FirstOrDefaultAsync(f => f.SupplyId == supplyId);
    }

    public async Task<SupplyRequest?> FindByPaymentOwnerId(int paymentOwnerId)
    {
        return await Context.Set<SupplyRequest>().FirstOrDefaultAsync(f => f.PaymentOwnerId == paymentOwnerId);
    }
    
    public async Task<IEnumerable<SupplyRequest>> FindAllSuppliesRequestsAsync(int queryHotelId)
    {
        return await Task.Run(() => (
            from sp in Context.Set<SupplyRequest>().ToList()
            join po in Context.Set<PaymentOwner>().ToList() on sp.PaymentOwnerId equals po.Id
            join ow in Context.Set<Owner>().ToList() on po.OwnerId equals ow.Id
            join ho in Context.Set<Hotel>().ToList() on ow.Id equals ho.OwnerId
            where ho.Id.Equals(queryHotelId)
            select sp
        ).ToList());
    }
}