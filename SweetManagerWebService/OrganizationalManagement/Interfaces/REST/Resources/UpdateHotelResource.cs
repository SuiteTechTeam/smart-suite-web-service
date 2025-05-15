namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

public record UpdateHotelResource(
    string Description,
    string Email,
    string Address,
    string Phone,
    int OwnerId
    );