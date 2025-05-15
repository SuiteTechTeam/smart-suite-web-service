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
    {
        [HttpPost("sign-up-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAdmin([FromBody] SignUpUserResource resource)
        {
            try
            {
                var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

                var result = await adminCommandService.Handle(signUpCommand);

                await adminCredentialCommandService.Handle(new Domain.Model.Commands.Credentials.CreateUserCredentialCommand(signUpCommand.Id, resource.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-up-guest")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpWorker([FromBody] SignUpUserResource resource)
        {
            try
            {
                var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

                var result = await guestCommandService.Handle(signUpCommand);

                await guestCredentialCommandService.Handle(new Domain.Model.Commands.Credentials.CreateUserCredentialCommand(resource.Id, resource.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-up-owner")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpOwner([FromBody] SignUpUserResource resource)
        {
            try
            {
                var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

                var result = await ownerCommandService.Handle(signUpCommand);

                await ownerCredentialCommandService.Handle(new Domain.Model.Commands.Credentials.CreateUserCredentialCommand(resource.Id, resource.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
        {
            try
            {
                if (resource.RoleId is < 1 or > 3)
                    throw new Exception();

                var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);

                dynamic? authenticatedUser = "";

                if (resource.RoleId is 1) 
                    authenticatedUser = await ownerCommandService.Handle(signInCommand);
                else if (resource.RoleId is 2)
                    authenticatedUser = await adminCommandService.Handle(signInCommand);
                else if (resource.RoleId is 3)
                    authenticatedUser = await guestCommandService.Handle(signInCommand);

                var authenticatedUserResource =
                    AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser!.User,
                        authenticatedUser.Token);

                return Ok(authenticatedUserResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}