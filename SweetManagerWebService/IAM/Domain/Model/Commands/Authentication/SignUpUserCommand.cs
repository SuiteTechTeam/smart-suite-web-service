namespace SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication
{
    public record SignUpUserCommand(string Name, string Surname, string Phone, string Email, string Password);
}