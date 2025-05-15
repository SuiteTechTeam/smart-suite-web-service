namespace SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication
{
    public record SignInUserCommand(string Email, string Password, int RoleId);

}