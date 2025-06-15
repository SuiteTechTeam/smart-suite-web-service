using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users;
using SweetManagerIotWebService.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users;
using System.Net.Mime;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST
{

    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthenticationController(IAdminCommandService adminCommandService,
    IAdminCredentialCommandService adminCredentialCommandService,
    IGuestCommandService guestCommandService,
    IGuestCredentialCommandService guestCredentialCommandService,
    IOwnerCommandService ownerCommandService,
    IOwnerCredentialCommandService ownerCredentialCommandService) : ControllerBase
    {        [HttpPost("sign-up-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAdmin([FromBody] SignUpUserResource resource)
        {
            try
            {
                var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

                var result = await adminCommandService.Handle(signUpCommand);

                if (result == null)
                    return BadRequest("Error creating admin user");

                await adminCredentialCommandService.Handle(new Domain.Model.Commands.Credentials.CreateUserCredentialCommand(result.Id, resource.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        [HttpPost("sign-up-guest")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpWorker([FromBody] SignUpUserResource resource)
        {
            try
            {
                var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

                var result = await guestCommandService.Handle(signUpCommand);

                if (result == null)
                    return BadRequest("Error creating guest user");

                await guestCredentialCommandService.Handle(new Domain.Model.Commands.Credentials.CreateUserCredentialCommand(result.Id, resource.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        [HttpPost("sign-up-owner")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpOwner([FromBody] SignUpUserResource resource)
        {
            try
            {
                var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

                var result = await ownerCommandService.Handle(signUpCommand);

                if (result == null)
                    return BadRequest("Error creating owner user");

                await ownerCredentialCommandService.Handle(new Domain.Model.Commands.Credentials.CreateUserCredentialCommand(result.Id, resource.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
        {
            if (resource.RoleId < 1 || resource.RoleId > 3)
                return BadRequest("RoleId inválido. Debe ser 1 (Owner), 2 (Admin) o 3 (Guest).");

            try
            {
                var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
                dynamic? authenticatedUser = null;

                switch (resource.RoleId)
                {
                    case 1:
                        authenticatedUser = await ownerCommandService.Handle(signInCommand);
                        break;
                    case 2:
                        authenticatedUser = await adminCommandService.Handle(signInCommand);
                        break;
                    case 3:
                        authenticatedUser = await guestCommandService.Handle(signInCommand);
                        break;
                }

                if (authenticatedUser == null)
                    return Unauthorized("Credenciales incorrectas o usuario no encontrado.");
                    
                var userProp = authenticatedUser.GetType().GetProperty("User");
                var tokenProp = authenticatedUser.GetType().GetProperty("Token");
                
                if (userProp == null || tokenProp == null)
                    return Unauthorized("Respuesta de autenticación inválida.");

                object? user = userProp.GetValue(authenticatedUser, null);
                string? token = tokenProp.GetValue(authenticatedUser, null) as string;

                if (user == null || string.IsNullOrEmpty(token))
                    return Unauthorized("Credenciales incorrectas o usuario no encontrado.");
                  try {
                    // Convertir el objeto user a dynamic para acceder a sus propiedades
                    dynamic dynamicUser = user;
                    
                    // Crear el recurso de respuesta
                    var result = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(dynamicUser, token);
                    
                    return Ok(result);
                } catch (Exception ex) {
                    // Captura específicamente errores relacionados con la conversión dinámica
                    return StatusCode(500, "Error al procesar la autenticación: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                // Loguear el error real en producción
                return StatusCode(500, "Error interno al autenticar: " + ex.Message);
            }
        }
    }
}