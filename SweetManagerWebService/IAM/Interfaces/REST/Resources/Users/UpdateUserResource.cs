namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users
{
    public record UpdateUserResource(string Name, string Surname, string Phone, string Email, string State, int RoleId);
}