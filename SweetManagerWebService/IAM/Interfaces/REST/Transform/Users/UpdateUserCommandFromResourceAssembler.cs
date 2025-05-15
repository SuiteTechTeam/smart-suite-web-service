using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users
{
    public static class UpdateUserCommandFromResourceAssembler
    {
        public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource, int id)
        {
            return new UpdateUserCommand(id, resource.Name, resource.Surname,
                resource.Phone, resource.Email, resource.State);
        }
    }
}