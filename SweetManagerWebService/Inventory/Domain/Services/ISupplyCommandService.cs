using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.Inventory.Domain.Services;

public interface ISupplyCommandService
{
    Task<bool> Handle(CreateSupplyCommand command);
    Task<bool> Handle(UpdateSupplyCommand command);
}