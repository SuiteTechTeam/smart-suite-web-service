using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.IAM.Domain.Services.Credential.Admin;
using SweetManagerWebService.IAM.Domain.Services.Credential.Owner;
using SweetManagerWebService.IAM.Domain.Services.Credential.Worker;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;
using SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController(IAdminCommandService adminCommandService, 
    IAdminCredentialCommandService adminCredentialCommandService,
    IWorkerCommandService workerCommandService,
    IWorkerCredentialCommandService workerCredentialCommandService,
    IOwnerCommandService ownerCommandService,
    IOwnerCredentialCommandService ownerCredentialCommandService) : ControllerBase
{    [HttpPost("sign-up-admin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAdmin([FromBody] SignUpUserResource resource)
    {
        try
        {
            var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            // First create the user
            await adminCommandService.Handle(signUpCommand);
            
            // Get the newly created user by email
            var admin = await adminCommandService.GetUserByEmail(signUpCommand.Email);
            if (admin == null)
                throw new Exception("Error creating admin account");
            
            // Then create the credentials
            await adminCredentialCommandService.Handle(new(admin.Id, resource.Password));

            return Ok("User created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }[HttpPost("sign-up-worker")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpWorker([FromBody] SignUpUserResource resource)
    {
        try
        {
            var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            // First create the worker
            await workerCommandService.Handle(signUpCommand);
            
            // Use FindIdByEmail to get the new user's ID
            var worker = await workerCommandService.GetUserByEmail(signUpCommand.Email);
            if (worker == null)
                throw new Exception("Error creating worker account");
                
            // Then create credentials with the new ID
            await workerCredentialCommandService.Handle(new(worker.Id, resource.Password));

            return Ok("User created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }    [HttpPost("sign-up-owner")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpOwner([FromBody] SignUpUserResource resource)
    {
        try
        {
            var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await ownerCommandService.Handle(signUpCommand);

            // Get the newly created user by email
            var owner = await ownerCommandService.GetUserByEmail(signUpCommand.Email);
            if (owner == null)
                throw new Exception("Error creating owner account");

            await ownerCredentialCommandService.Handle(new(owner.Id, resource.Password));

            return Ok("User created correctly!");
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
            if (resource.RolesId is < 1 or > 3)
                throw new Exception();
            
            var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);

            dynamic? authenticatedUser = "";

            if (resource.RolesId is 1)
                authenticatedUser = await ownerCommandService.Handle(signInCommand);
            else if (resource.RolesId is 2)
                authenticatedUser = await adminCommandService.Handle(signInCommand);
            else if(resource.RolesId is 3)
                authenticatedUser = await workerCommandService.Handle(signInCommand);

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