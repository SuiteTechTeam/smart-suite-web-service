using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;

public interface IHotelCommandService
{
    Task<Hotel?> Handle(CreateHotelCommand command);
    Task<Hotel?> Handle(UpdateHotelCommand command);
}