using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Pipeline.Middleware.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute(params string[]? roles) : Attribute, IAuthorizationFilter
    {
        private readonly string[]? _listRoles = roles ?? [];

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous) return;

            var credential = context.HttpContext.Items["Credentials"] as dynamic;
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<AuthorizeAttribute>)) as ILogger<AuthorizeAttribute>;

            if (credential is null)
            {
                logger?.LogWarning("Autorización fallida: credentials es null");
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                if (_listRoles != null && (_listRoles.Length <= 0 || HasRequiredRole(credential.Role))) 
                {
                    logger?.LogInformation($"Autorización exitosa: ID={credential.Id}, Role={credential.Role}");
                    return;
                }

                logger?.LogWarning($"Acceso prohibido: ID={credential.Id}, Role={credential.Role}, Roles requeridos: {string.Join(",", _listRoles ?? [])}");
                context.Result = new ForbidResult();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Error durante la autorización");
                context.Result = new StatusCodeResult(500);
            }
        }

        private bool HasRequiredRole(string role) => _listRoles != null && _listRoles.Contains(role);

    }
}