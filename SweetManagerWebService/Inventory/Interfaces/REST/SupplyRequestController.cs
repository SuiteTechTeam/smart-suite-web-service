using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Queries.SupplyRequest;
using SweetManagerIotWebService.API.Inventory.Domain.Services;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]

public class SupplyRequestController : ControllerBase
{
    private readonly ISupplyRequestQueryService _queryService;
    private readonly ISupplyRequestCommandService _commandService;
    
    public SupplyRequestController(ISupplyRequestQueryService queryService, ISupplyRequestCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSupplyRequest([FromBody] CreateSupplyRequestResource resource)
    {
        try
        {
            var result = await _commandService.Handle(CreateSupplyRequestCommandFromResourceAssembler.ToCommandFromResource(resource));
            if (!result)
            {
                return BadRequest("Failed to create supply request.");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    
    [HttpGet("HotelId/{HotelId}")]
    public async Task<IActionResult> GetAllSupplyRequest([FromRoute] int HotelId)
    {
        try
        {
            var result = await _queryService.Handle(new GetAllSupplyRequestQuery(HotelId));

            if (result == null || !result.Any())
            {
                return BadRequest("No supply requests found for this hotel.");
            }

            var supplyRequestResources = result.Select(SupplyRequestResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(supplyRequestResources);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSuppliesRequestById([FromRoute] int id)
    {
        try
        {
            var result = await _queryService.Handle(new GetSupplyRequestByIdQuery(id));
            if (result is null)
            {
                return BadRequest($"Supplies request with ID {id} not found.");
            }

            var suppliesRequestResource = SupplyRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(suppliesRequestResource);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("paymentOwner/{paymentOwnerId}")]
    public async Task<IActionResult> GetSuppliesRequestByPaymentOwnerId([FromRoute] int paymentOwnerId)
    {
        try
        {
            var result = await _queryService.Handle(new GetSupplyRequestByPaymentOwnerIdQuery(paymentOwnerId));
            if (result is null)
            {
                return BadRequest($"No supplies requests found for PaymentOwnerId {paymentOwnerId}.");
            }

            var suppliesRequestResource = SupplyRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(suppliesRequestResource);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("supply/{supplyId}")]
    public async Task<IActionResult> GetSuppliesRequestBySupplyId([FromRoute] int supplyId)
    {
        try
        {
            var result = await _queryService.Handle(new GetSupplyRequestBySupplyIdQuery(supplyId));
            if (result is null)
            {
                return BadRequest($"No supplies requests found for SupplyId {supplyId}.");
            }

            var suppliesRequestResource = SupplyRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(suppliesRequestResource);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}