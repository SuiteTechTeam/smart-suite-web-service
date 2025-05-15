using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Queries;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Repositories;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Application.Internal.QueryServices;

public class HotelQueryService(IHotelRepository hotelRepository) : IHotelQueryService
{
    public async Task<Hotel?> Handle(GetHotelByIdQuery query)
    {
        return await hotelRepository.FindByIdAsync(query.HotelId);
    }
    
    public async Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query)
    {
        return await hotelRepository.GetAllHotelsAsync();
    }
    
    public async Task<IEnumerable<Hotel>> Handle(GetHotelByOwnerId query)
    {
        return await hotelRepository.FindByOwnerIdAsync(query.OwnerId);
    }
}