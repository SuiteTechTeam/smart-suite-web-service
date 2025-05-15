using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.Inventory.Domain.Services;

public interface ISupplyRequestCommandService
{
    Task<bool> Handle(CreateSupplyRequestCommand command);
}