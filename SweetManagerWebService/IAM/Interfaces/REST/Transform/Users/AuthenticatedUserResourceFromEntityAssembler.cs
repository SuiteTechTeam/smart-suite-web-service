using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users
{
    public static class AuthenticatedUserResourceFromEntityAssembler
    {
        public static AuthenticatedUserResource ToResourceFromEntity(dynamic entity, string token)
        {
            return new AuthenticatedUserResource(entity.Id, entity.Email, token);
        }
    }
}