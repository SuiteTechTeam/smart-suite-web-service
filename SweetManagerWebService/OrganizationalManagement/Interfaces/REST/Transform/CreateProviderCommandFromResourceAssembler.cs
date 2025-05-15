using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;
using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Transform;

public class CreateProviderCommandFromResourceAssembler
{
    public static CreateProviderCommand ToCommandFromResource(CreateProviderResource resource)
    {
        return new CreateProviderCommand(resource.Name, resource.Email, resource.Phone, resource.State);
    }
}