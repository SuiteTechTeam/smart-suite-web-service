using SweetManagerIotWebService.API.Commerce.Domain.Model.Entities;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.QueryServices;

public class ContractOwnerQueryService(IContractOwnerRepository contractOwnerRepository) : IContractOwnerQueryService
{
    public async Task<IEnumerable<ContractOwner>> Handle(GetAllContractOwnersQuery query)
    {
        return await contractOwnerRepository.ListAsync();
    }

    public async Task<ContractOwner?> Handle(GetContractOwnerByIdQuery query)
    {
        return await contractOwnerRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<ContractOwner>> Handle(GetAllContractOwnersByOwnerIdQuery query)
    {
        return await contractOwnerRepository.FindByOwnerIdAsync(query.OwnerId);
    }

    public async Task<IEnumerable<ContractOwner>> Handle(GetAllContractOwnersBySubscriptionIdQuery query)
    {
        return await contractOwnerRepository.FindBySubscriptionIdAsync(query.SubscriptionId);
    }
}