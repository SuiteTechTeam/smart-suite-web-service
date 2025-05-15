namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

public record HotelResource(
    int Id,
    string Name,
    string Description,
    string Email,
    string Address,
    string Phone);