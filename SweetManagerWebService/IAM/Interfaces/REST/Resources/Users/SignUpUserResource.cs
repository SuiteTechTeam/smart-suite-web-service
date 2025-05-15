namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users
{
    public record SignUpUserResource(int Id, string Name, string Surname, string Phone, string Email, string Password);
}