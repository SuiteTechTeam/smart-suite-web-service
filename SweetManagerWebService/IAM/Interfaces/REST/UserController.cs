using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users;
using SweetManagerIotWebService.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Users;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Users;
using System.Net.Mime;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController(IGuestCommandService guestCommandService,
    IAdminCommandService adminCommandService,
    IOwnerCommandService ownerCommandService,
    IAdminQueryService adminQueryService,
    IGuestQueryService guestQueryService,
    IOwnerQueryService ownerQueryService) : ControllerBase
    {
        [Authorize]
        [HttpGet("owners/{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            try
            {
                var owner = await ownerQueryService.Handle(new GetUserByIdQuery(id));

                var ownerResource = UserResourceFromEntityAssembler.ToResourceFromEntity(owner!);

                return Ok(ownerResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("owners")]
        public async Task<IActionResult> GetAllOwners([FromQuery]int hotelId = 0, [FromQuery] string email = "", [FromQuery] string phone = "", [FromQuery] string state = "")
        {
            try
            {
                if (hotelId != 0)
                {
                    var owner = await ownerQueryService.Handle(new GetOwnerFromAnOrganizationQuery(hotelId));

                    var ownerResource = UserResourceFromEntityAssembler.ToResourceFromEntity(owner!);

                    return Ok(ownerResource);
                } 
                else
                {
                    var owners = await ownerQueryService.Handle(new GetAllFilteredUsersQuery(email, phone, state));

                    dynamic ownerResources;

                    if (email != string.Empty)
                        ownerResources = UserResourceFromEntityAssembler.ToResourceFromEntity(owners!);
                    else
                    {
                        IEnumerable<Owner> ownerList;
                        ownerList = owners;
                        ownerResources = ownerList.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
                    }

                    return Ok(ownerResources);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("admins/{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            try
            {
                var admin = await adminQueryService.Handle(new GetUserByIdQuery(id));

                var adminResource = UserResourceFromEntityAssembler.ToResourceFromEntity(admin!);

                return Ok(adminResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins([FromQuery] int hotelId = 0, [FromQuery] string email = "", [FromQuery] string phone = "", [FromQuery] string state = "")
        {
            try
            {
                if (hotelId != 0)
                {
                    var admin = await adminQueryService.Handle(new GetAllUsersFromOrganizationQuery(hotelId));

                    var adminResource = UserResourceFromEntityAssembler.ToResourceFromEntity(admin!);

                    return Ok(adminResource);
                }
                else
                {
                    var admins = await adminQueryService.Handle(new GetAllFilteredUsersQuery(email, phone, state));

                    dynamic adminResources;

                    if (email != string.Empty)
                        adminResources = UserResourceFromEntityAssembler.ToResourceFromEntity(admins!);
                    else
                    {
                        IEnumerable<Admin> adminList;
                        adminList = admins;
                        adminResources = adminList.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
                    }

                    return Ok(adminResources);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("guests/{id}")]
        public async Task<IActionResult> GetGuestById(int id)
        {
            try
            {
                var guest = await guestQueryService.Handle(new GetUserByIdQuery(id));

                var guestResource = UserResourceFromEntityAssembler.ToResourceFromEntity(guest!);

                return Ok(guestResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("guests")]
        public async Task<IActionResult> GetAllGuests([FromQuery] int hotelId = 0, [FromQuery] string email = "", [FromQuery] string phone = "", [FromQuery] string state = "")
        {
            try
            {
                if (hotelId != 0)
                {
                    var guest = await guestQueryService.Handle(new GetAllUsersFromOrganizationQuery(hotelId));

                    var guestResource = UserResourceFromEntityAssembler.ToResourceFromEntity(guest!);

                    return Ok(guestResource);
                }
                else
                {
                    var guests = await guestQueryService.Handle(new GetAllFilteredUsersQuery(email, phone, state));

                    dynamic guestResources;

                    if (email != string.Empty)
                        guestResources = UserResourceFromEntityAssembler.ToResourceFromEntity(guests!);
                    else
                    {
                        IEnumerable<Guest> guestList;
                        guestList = guests;
                        guestResources = guestList.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
                    }

                    return Ok(guestResources);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("admins/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateUserResource resource, int id)
        {
            try
            {
                var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource, id);

                var result = await adminCommandService.Handle(updateUserCommand);

                var adminResource = UserResourceFromEntityAssembler.ToResourceFromEntity(result!);

                return Ok(adminResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("owners/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOwner([FromBody] UpdateUserResource resource, int id)
        {
            try
            {
                var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource, id);

                var result = await ownerCommandService.Handle(updateUserCommand);

                var ownerResource = UserResourceFromEntityAssembler.ToResourceFromEntity(result!);

                return Ok(ownerResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("guests/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateGuest([FromBody] UpdateUserResource resource, int id)
        {
            try
            {
                var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource, id);

                var result = await guestCommandService.Handle(updateUserCommand);

                var guestResource = UserResourceFromEntityAssembler.ToResourceFromEntity(result!);

                return Ok(guestResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}