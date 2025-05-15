using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Entities;

namespace SweetManagerIotWebService.API.Commerce.Domain.Services;

public interface IContractOwnerCommandService
{
    Task<ContractOwner?> Handle(CreateContractOwnerCommand command);
    Task<ContractOwner?> Handle(UpdateContractOwnerCommand command);
}