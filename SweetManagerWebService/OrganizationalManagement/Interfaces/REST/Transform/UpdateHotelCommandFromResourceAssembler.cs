using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;
using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Transform;

public class UpdateHotelCommandFromResourceAssembler
{
    public static UpdateHotelCommand ToCommandFromResource(int id, UpdateHotelResource resource)
    {
        return new UpdateHotelCommand(id, resource.Description, resource.Email, resource.Address, resource.Phone, resource.OwnerId);
    }
}