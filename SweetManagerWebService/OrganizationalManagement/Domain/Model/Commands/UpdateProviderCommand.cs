namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

public record UpdateProviderCommand(int Id, string Name, string Email, string Phone);