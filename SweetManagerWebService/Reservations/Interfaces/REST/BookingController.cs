using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Booking;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Booking;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Booking;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST;
[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class BookingController(IBookingQueryServices bookingQueryServices, IBookingCommandServices bookingCommandServices) : ControllerBase
{
    [HttpPost("create-booking")]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingResource resource)
    {
        try
        {
            var command = CreateBookingCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await bookingCommandServices.Handle(command);

            return Ok("Booking created successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[CreateBooking] Argument error: {ex.Message}");
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

    [HttpPut("update-booking-state")]
    public async Task<IActionResult> UpdateBookingState([FromBody] UpdateBookingStateResource resource)
    {
       var command = UpdateBookingStateCommandFromResourceAssembler.ToCommandFromResource(resource);
       var result = await bookingCommandServices.Handle(command);
        if (result is false)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    [HttpPut("update-booking-end-date")]
    public async Task<IActionResult> UpdateBookingEndDate([FromBody] UpdateBookingEndDateCommand resource)
    {
        var command = UpdateBookingEndDateCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await bookingCommandServices.Handle(command);
        if (result is false)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("get-booking-by-id")]
    public async Task <IActionResult> BookingById([FromQuery] int id){
        var booking = await bookingQueryServices.Handle(new GetBookingByIdQuery(id));
        if (booking is null)
            return BadRequest();
        return Ok(booking);
    }
    [HttpGet("get-booking-by-customer-id")]
    public async Task <IActionResult> BookingByCustomerId([FromQuery] int customerId){
        var booking = await bookingQueryServices.Handle(new GetBookingByCustomerIdQuery(customerId));
        if (booking is null)
            return BadRequest();
        return Ok(booking);
    }
    
    [HttpGet("get-booking-by-hotel-id-and-state")]
    public async Task <IActionResult> BookingByHotelIdAndState([FromQuery] int hotelId, string state){
        var booking = await bookingQueryServices.Handle(new GetBookingByHotelIdAndState(hotelId, state));
        if (booking is null)
            return BadRequest();
        return Ok(booking);
    }
    
    [HttpGet("get-all-bookings")]
    public async Task <IActionResult> AllBookings([FromQuery] int hotelId){
        var booking = await bookingQueryServices.Handle(new GetAllBookingsQuery(hotelId));
        if (booking is null)
            return BadRequest();
        return Ok(booking);
    }
    
   
    
    
}