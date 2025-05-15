using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;

public interface IHotelQueryService
{
    Task<Hotel?> Handle(GetHotelByIdQuery query);
    Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query);
    Task<IEnumerable<Hotel>> Handle(GetHotelByOwnerId query);
}