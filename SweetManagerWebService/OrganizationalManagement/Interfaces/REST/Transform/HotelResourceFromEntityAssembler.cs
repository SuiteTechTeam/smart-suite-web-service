using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Transform;

public class HotelResourceFromEntityAssembler
{
    public static HotelResource ToResourceFromEntity(Hotel entity)
    {
        return new HotelResource(entity.Id, entity.Name, entity.Description, entity.Email, entity.Address, entity.Phone);
    }
}