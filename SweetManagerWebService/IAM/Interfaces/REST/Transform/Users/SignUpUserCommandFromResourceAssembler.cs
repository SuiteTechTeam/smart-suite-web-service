using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users
{
    public static class SignUpUserCommandFromResourceAssembler
    {
        public static SignUpUserCommand ToCommandFromResource(SignUpUserResource resource)
        {
            return new(resource.Id, resource.Name, resource.Surname, resource.Phone,
                resource.Email, resource.Password);
        }
    }
}