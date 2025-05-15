namespace SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication
{
    public record UpdateUserCommand(int Id, string Name, string Surname, string Phone, string Email, string State);
}