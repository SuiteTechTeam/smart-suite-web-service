namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users
{
    public record AuthenticatedUserResource(int Id, string Email, string Token, int RoleId = 0);
}