using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;
using SweetManagerIotWebService.API.Commerce.Domain.Services;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ContractOwnerController(
    IContractOwnerCommandService contractOwnerCommandService,
    IContractOwnerQueryService contractOwnerQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateContractOwner(CreateContractOwnerResource resource)
    {
        var createContractOwnerCommand = CreateContractOwnerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var contractOwner = await contractOwnerCommandService.Handle(createContractOwnerCommand);
        if (contractOwner is null) return BadRequest();
        var contractOwnerResource = ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity(contractOwner);
        return CreatedAtAction(nameof(GetContractOwnerById), new { contractOwnerId = contractOwnerResource.Id }, contractOwnerResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllContractOwners()
    {
        var getAllContractOwnersQuery = new GetAllContractOwnersQuery();
        var contractOwners = await contractOwnerQueryService.Handle(getAllContractOwnersQuery);
        var contractOwnerResources = contractOwners.Select(ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contractOwnerResources);
    }
    
    [HttpGet("{contractOwnerId:int}")]
    public async Task<IActionResult> GetContractOwnerById(int contractOwnerId)
    {
        var getContractOwnerByIdQuery = new GetContractOwnerByIdQuery(contractOwnerId);
        var contractOwner = await contractOwnerQueryService.Handle(getContractOwnerByIdQuery);
        if (contractOwner == null) return NotFound();
        var contractOwnerResource = ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity(contractOwner);
        return Ok(contractOwnerResource);
    }
    
    [HttpGet("by-owner/{ownerId:int}")]
    public async Task<IActionResult> GetContractOwnersByOwnerId(int ownerId)
    {
        var getContractOwnerByOwnerIdQuery = new GetAllContractOwnersByOwnerIdQuery(ownerId);
        var contractOwners = await contractOwnerQueryService.Handle(getContractOwnerByOwnerIdQuery);
        var contractOwnerResources = contractOwners.Select(ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contractOwnerResources);
    }
    
    [HttpGet("by-subscription/{subscriptionId:int}")]
    public async Task<IActionResult> GetContractOwnersBySubscriptionId(int subscriptionId)
    {
        var getContractOwnerBySubscriptionIdQuery = new GetAllContractOwnersBySubscriptionIdQuery(subscriptionId);
        var contractOwners = await contractOwnerQueryService.Handle(getContractOwnerBySubscriptionIdQuery);
        var contractOwnerResources = contractOwners.Select(ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contractOwnerResources);
    }
    
    [HttpPut("{contractOwnerId:int}")]
    public async Task<IActionResult> UpdateContractOwner(int contractOwnerId, UpdateContractOwnerResource resource)
    {
        if (contractOwnerId != resource.Id)
        {
            return BadRequest("Contract Owner ID mismatch.");
        }

        var updateContractOwnerCommand = UpdateContractOwnerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var contractOwner = await contractOwnerCommandService.Handle(updateContractOwnerCommand);
        if (contractOwner is null) return NotFound();
        var contractOwnerResource = ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity(contractOwner);
        return Ok(contractOwnerResource);
    }
}