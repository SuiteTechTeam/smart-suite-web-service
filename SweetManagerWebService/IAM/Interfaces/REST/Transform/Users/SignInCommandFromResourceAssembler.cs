using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users
{
    public static class SignInCommandFromResourceAssembler
    {
        public static SignInUserCommand ToCommandFromResource(SignInResource resource)
        {
            return new SignInUserCommand(resource.Email, resource.Password, resource.RoleId);
        }
    }
}