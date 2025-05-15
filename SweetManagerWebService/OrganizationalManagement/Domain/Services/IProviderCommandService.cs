using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;

public interface IProviderCommandService
{
    Task<Provider?> Handle(CreateProviderCommand command);
    Task<Provider?> Handle(UpdateProviderCommand command);
    Task<bool> Handle(DeleteProviderCommand command);
}