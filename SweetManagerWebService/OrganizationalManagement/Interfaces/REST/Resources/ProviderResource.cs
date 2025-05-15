namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

public record ProviderResource(
    int Id,
    string Name,
    string Email,
    string Phone,
    string State
    );