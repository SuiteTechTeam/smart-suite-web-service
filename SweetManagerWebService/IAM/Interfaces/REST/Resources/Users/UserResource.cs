namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users
{
    public record UserResource(int Id, string Name, string Surname, string Phone, string Email, string State, int RoleId);
}