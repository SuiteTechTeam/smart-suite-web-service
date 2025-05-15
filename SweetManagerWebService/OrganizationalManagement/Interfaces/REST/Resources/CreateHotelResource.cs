using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

public record CreateHotelResource(
    int OwnerId,
    string Name,
    string Description,
    string Email,
    string Address,
    string Phone);