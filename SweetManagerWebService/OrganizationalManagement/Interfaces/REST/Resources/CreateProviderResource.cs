namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

public record CreateProviderResource(
    string Name,
    string Email,
    string Phone,
    string State
    );