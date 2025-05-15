using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

public record CreateHotelCommand(
    int OwnerId,
    string Name,
    string Description,
    string Email,
    string Address,
    string Phone);