using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Queries;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;
using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;
using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Transform;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST;

/*
 * HotelsController
 * This controller is responsible for handling HTTP requests related to hotels.
 *
 * CreateHotel: Handles the creation of a new hotel.
 * GetHotelById: Retrieves a hotel by its ID.
 *
 * Author: Arian Rodriguez
 */
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class HotelsController(IHotelCommandService hotelCommandService, IHotelQueryService hotelQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateHotel(CreateHotelResource resource)
    {
        try
        {
            var createHotelCommand = CreateHotelCommandFromResourceAssembler.ToCommandFromResource(resource);
            var hotel = await hotelCommandService.Handle(createHotelCommand);

            if (hotel is null) return BadRequest();
            var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
            return CreatedAtAction(nameof(GetHotelById), new {hotelId = hotelResource.Id}, hotelResource);
        }catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{hotelId:int}")]
    public async Task<IActionResult> GetHotelById(int hotelId)
    {
        try
        {
            var getHotelByIdQuery = new GetHotelByIdQuery(hotelId);
            var hotel = await hotelQueryService.Handle(getHotelByIdQuery);
        
            if (hotel == null) return NotFound(new { message = "The hotel was not found." });
            var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
            return Ok(hotelResource);
        }catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllHotels()
    {
        try
        {
            var getAllHotelsQuery = new GetAllHotelsQuery();
            var hotels = await hotelQueryService.Handle(getAllHotelsQuery);
            var hotelResources = hotels.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(hotelResources);
        }catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("owner/{ownerId:int}")]
    public async Task<IActionResult> GetHotelByOwnerId(int ownerId)
    {
        try
        {
            var getHotelByOwnerIdQuery = new GetHotelByOwnerId(ownerId);
            var hotels = await hotelQueryService.Handle(getHotelByOwnerIdQuery);
            if (hotels == null) return NotFound(new { message = "The hotel was not found." });
            
            var hotelResources = hotels.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(hotelResources);
        } catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }   

    [HttpPut("{hotelId:int}")]
    public async Task<IActionResult> UpdateHotel(int hotelId, UpdateHotelResource resource)
    {
        try
        {
            var updateHotelCommand = UpdateHotelCommandFromResourceAssembler.ToCommandFromResource(hotelId, resource);
            var hotel = await hotelCommandService.Handle(updateHotelCommand);
        
            if (hotel == null) return NotFound(new { message = "The hotel was not found." });
            var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
            return Ok(hotelResource);
        }catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}