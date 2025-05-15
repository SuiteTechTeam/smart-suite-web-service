using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;
using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Transform;

public class UpdateProviderCommandFromResourceAssembler
{
    public static UpdateProviderCommand ToCommandFromResource(int providerId, UpdateProviderResource resource)
    {
        return new UpdateProviderCommand(providerId, resource.Name, resource.Email, resource.Phone);
    }
}