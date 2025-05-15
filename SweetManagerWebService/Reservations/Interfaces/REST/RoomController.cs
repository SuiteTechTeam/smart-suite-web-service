using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Room;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Room;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Room;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class RoomController(IRoomCommandService roomCommandService, IRoomQueryService roomQueryService) : ControllerBase
{
    [HttpPost("create-room")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource resource)
    {
        try
        {
            var command = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await roomCommandService.Handle(command);

            return Ok("Room created successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[CreateRoom] Argument error: {ex.Message}");
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

    [HttpPut("update-room-state")]
    public async Task<IActionResult> updateRoomState([FromBody] UpdateRoomStateResource resource)
    {
        var result = await roomCommandService.Handle(
            UpdateRoomStateCommandFromResource.ToCommandFromResource(resource));
        if (result is false)
        {
            return BadRequest();
        }

        return Ok(result); 
    }
    
    [HttpGet("get-room-by-id")]
    public async Task <IActionResult> RoomById([FromQuery] int id)
    {
        var room = await roomQueryService.Handle(new GetRoomsByIdQuery(id));
        if (room is null)
            return BadRequest();

        var roomResource = RoomResourceFromEntityAssembler
            .ToResourceFromEntity(room);
        return Ok(roomResource);
    }
    
    [HttpGet("get-room-by-state")]
    public async Task <IActionResult> RoomByState([FromQuery] string state)
    {
        var rooms = await roomQueryService.Handle(new GetRoomsByStateQuery(state));
        if (state is null)
            return BadRequest();

        var roomResource = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResource);
    }
    
    [HttpGet("get-all-rooms")]
    public async Task <IActionResult> AllRooms([FromQuery] int hotelId)
    {
        var rooms = await roomQueryService.Handle(new GetAllRoomsQuery(hotelId));

        var roomResource = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResource);
    }
    
   [HttpGet("get-room-by-type-room")]
    public async Task <IActionResult> RoomByTypeRoomId([FromQuery] int typeRoomId)
    {
        var rooms = await roomQueryService.Handle(new GetRoomsByTypeRoomIdQuery(typeRoomId));

        var roomResource = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResource);
    }
    
    [HttpGet("get-room-by-booking-availability")]
    public async Task <IActionResult> RoomByBookingAvailability([FromQuery] DateTime startDate, DateTime finalDate, int hotelId)
    {
        var rooms = await roomQueryService.Handle(new GetAllRoomsByBookingAvailabilityInARangeQuery(startDate, finalDate, hotelId));

        var roomResource = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResource);
    }
    
}