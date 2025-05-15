using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.QueryServices;

public class PaymentOwnerQueryService(IPaymentOwnerRepository paymentOwnerRepository) : IPaymentOwnerQueryService
{
    public async Task<IEnumerable<PaymentOwner>> Handle(GetAllPaymentOwnersQuery query)
    {
        return await paymentOwnerRepository.ListAsync();
    }

    public async Task<PaymentOwner?> Handle(GetPaymentOwnerByIdQuery query)
    {
        return await paymentOwnerRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<PaymentOwner>> Handle(GetAllPaymentOwnersByOwnerIdQuery query)
    {
        return await paymentOwnerRepository.FindByOwnerIdAsync(query.OwnerId);
    }
}