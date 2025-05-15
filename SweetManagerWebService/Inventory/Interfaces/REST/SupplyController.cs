using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Queries.Supply;
using SweetManagerIotWebService.API.Inventory.Domain.Services;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Resources;
using SweetManagerIotWebService.API.Inventory.Interfaces.REST.Transform;

namespace SweetManagerIotWebService.API.Inventory.Interfaces.REST
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class SupplyController(ISupplyCommandService supplyCommandService, ISupplyQueryService supplyQueryService) : ControllerBase
    {
        [HttpPost("create-supply")]
        public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyResource resource)
        {
            try
            {
                var result = await supplyCommandService.Handle(CreateSupplyCommandFromResourceAssembler.ToCommandFromResource(resource));
                if (!result)
                {
                    return BadRequest("Failed to create supply.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupply(int id, [FromBody] UpdateSupplyResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await supplyCommandService.Handle(UpdateSupplyCommandFromResource.FromResource(id, resource));

                if (!result)
                {
                    return BadRequest("Failed to update supply.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplyById([FromRoute] int id)
        {
            try
            {
                var result = await supplyQueryService.Handle(new GetSupplyByIdQuery(id));
                if (result == null)
                {
                    return NotFound($"Supply with ID {id} not found.");
                }

                var supplyResource = SupplyResourceFromEntityAssembler.ToResourceFromEntity(result);

                return Ok(supplyResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("get-all-supplies")]
        public async Task<IActionResult> GetAllSupplies([FromQuery] int hotelId)
        {
            try
            {
                var result = await supplyQueryService.Handle(new GetAllSuppliesQuery(hotelId));

                var supplyResource = result.Select(SupplyResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(supplyResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [HttpGet("provider/{providerId}")]

        public async Task<IActionResult> GetSupplyByProviderId([FromRoute] int providerId)
        {
            try
            {
                var result = await supplyQueryService.Handle(new GetSupplyByProviderIdQuery(providerId));
                if (result == null)
                {
                    return NotFound($"Supply with Provider ID {providerId} not found.");
                }

                var supplyResource = result.Select(SupplyResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(supplyResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
    
}

