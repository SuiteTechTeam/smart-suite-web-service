namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

public record CreateProviderCommand(
    string Name,
    string Email,
    string Phone,
    string State
    );