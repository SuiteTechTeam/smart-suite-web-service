namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users
{
    public record SignInResource(string Email, string Password, int RoleId);
}