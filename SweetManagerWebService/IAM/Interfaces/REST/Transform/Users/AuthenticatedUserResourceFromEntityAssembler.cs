using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users
{
    public static class AuthenticatedUserResourceFromEntityAssembler
    {
        public static AuthenticatedUserResource ToResourceFromEntity(dynamic entity, string token)
        {
            // Extraer roleId con manejo de posibles casos dinámicos
            int roleId;
            try {
                roleId = entity.RoleId;
            } catch {
                // Inferir el roleId basado en otros atributos si es necesario
                // Por defecto, asignar un valor seguro
                roleId = 0;
            }
            
            // Extraer email con manejo de posibles casos dinámicos
            string email;
            try {
                email = entity.Email;
            } catch {
                // Si no hay email, usar ID como cadena
                email = entity.Id.ToString();
            }
            
            return new AuthenticatedUserResource(entity.Id, email, token, roleId);
        }
    }
}