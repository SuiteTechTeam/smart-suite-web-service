using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;
using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;
using SweetManagerIotWebService.API.Commerce.Domain.Services;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionResource resource)
    {
        var createSubscriptionCommand = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await subscriptionCommandService.Handle(createSubscriptionCommand);
        if (subscription is null) return BadRequest();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return CreatedAtAction(nameof(GetSubscriptionById), new { subscriptionId = subscriptionResource.Id }, subscriptionResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var getAllSubscriptionsQuery = new GetAllSubscriptionsQuery();
        var subscriptions = await subscriptionQueryService.Handle(getAllSubscriptionsQuery);
        var subscriptionResources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(subscriptionResources);
    }
    
    [HttpGet("{subscriptionId:int}")]
    public async Task<IActionResult> GetSubscriptionById(int subscriptionId)
    {
        var getSubscriptionByIdQuery = new GetSubscriptionByIdQuery(subscriptionId);
        var subscription = await subscriptionQueryService.Handle(getSubscriptionByIdQuery);
        if (subscription == null) return NotFound();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(subscriptionResource);
    }
    
    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetSubscriptionsByName(ESubscriptionTypes name)
    {
        var getSubscriptionsByNameQuery = new GetAllSubscriptionsByNameQuery(name);
        var subscriptions = await subscriptionQueryService.Handle(getSubscriptionsByNameQuery);
        var subscriptionResources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(subscriptionResources);
    }
    
    [HttpGet("by-status/{status}")]
    public async Task<IActionResult> GetSubscriptionsByStatus(EStates status)
    {
        var getSubscriptionsByStatusQuery = new GetAllSubscriptionsByStatusQuery(status);
        var subscriptions = await subscriptionQueryService.Handle(getSubscriptionsByStatusQuery);
        var subscriptionResources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(subscriptionResources);
    }
    
    [HttpPut("{subscriptionId:int}")]
    public async Task<IActionResult> UpdateSubscription(int subscriptionId, UpdateSubscriptionResource resource)
    {
        if (subscriptionId != resource.Id)
        {
            return BadRequest("Subscription ID mismatch.");
        }

        var updateSubscriptionCommand = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await subscriptionCommandService.Handle(updateSubscriptionCommand);
        if (subscription is null) return NotFound();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(subscriptionResource);
    }
}