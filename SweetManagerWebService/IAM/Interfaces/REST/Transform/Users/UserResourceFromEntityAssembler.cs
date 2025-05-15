using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users
{
    public static class UserResourceFromEntityAssembler
    {
        public static UserResource ToResourceFromEntity(dynamic user)
        {
            return new UserResource(user.Id, user.Name, user.Surname, user.Phone, 
                user.Email, user.State, user.RoleId);
        }
    }
}