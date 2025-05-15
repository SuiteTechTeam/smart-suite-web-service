using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;
using SweetManagerIotWebService.API.Reservations.Domain.Services.TypeRoom;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.TypeRoom;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.TypeRoom;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST;


[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class TypeRoomController(ITypeRoomQueryService typeRoomQueryService, ITypeRoomCommandService typeRoomCommandService) : ControllerBase
{
    [HttpPost("create-type-room")]
    public async Task<IActionResult> CreateTypeRoom([FromBody] CreateTypeRoomResource resource)
    {
        try
        {
            var command = CreateTypeRoomCommandFromResourceAssembler.CreateTypeRoomCommandFromResource(resource);
            var result = await typeRoomCommandService.Handle(command);

            return Ok("Type room created successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[CreateTypeRoom] Argument error: {ex.Message}");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = ex.Message,
                stack = ex.StackTrace
            });
        }
    }
    
    [HttpGet("get-type-room-by-id")]
    public async Task<IActionResult> TypeRoomById([FromQuery] int id)
    {
        var typeRoom = await typeRoomQueryService.Handle(new GetTypeRoomByIdQuery(id));
        if (typeRoom is null)
            return BadRequest();
        
        var roomResource = TypeRoomResourceFromEntityAssembler
            .ToResourceFromEntity(typeRoom);
        return Ok(typeRoom);
    }
    
    [HttpGet("get-all-type-rooms")]
    public async Task<IActionResult> GetAllTypeRooms([FromQuery] int hotelid)
    {
        var typeRooms = await typeRoomQueryService.Handle(new GetAllTypeRoomsQuery(hotelid));
        if (typeRooms is null)
            return BadRequest();

        var roomResources = typeRooms.Select(TypeRoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResources);
    }
}