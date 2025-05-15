namespace SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication
{
    public record SignUpUserCommand(int Id, string Name, string Surname, string Phone, string Email, string Password);
}