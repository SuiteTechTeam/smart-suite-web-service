namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

public record UpdateProviderResource(
    string Name,
    string Email,
    string Phone
    );