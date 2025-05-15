using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Transform;

public class ProviderResourceFromEntityAssembler
{
    public static ProviderResource ToResourceFromEntity(Provider entity)
    {
        return new ProviderResource(entity.Id, entity.Name, entity.Email, entity.Phone, entity.State.ToString().ToLower());
    }
}