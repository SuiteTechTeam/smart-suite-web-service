namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

public record UpdateHotelCommand(
    int HotelId,
    string Description,
    string Email,
    string Address,
    string Phone,
    int OwnerId);