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
public class PaymentCustomerController(
    IPaymentCustomerCommandService paymentCustomerCommandService, 
    IPaymentCustomerQueryService paymentCustomerQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePaymentCustomer(CreatePaymentCustomerResource resource)
    {
        var createPaymentCustomerCommand = CreatePaymentCustomerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var paymentCustomer = await paymentCustomerCommandService.Handle(createPaymentCustomerCommand);
        if (paymentCustomer is null) return BadRequest();
        var paymentCustomerResource = PaymentCustomerResourceFromEntityAssembler.ToResourceFromEntity(paymentCustomer);
        return CreatedAtAction(nameof(GetPaymentCustomerById), new { paymentCustomerId = paymentCustomerResource.Id }, paymentCustomerResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPaymentCustomers()
    {
        var getAllPaymentCustomersQuery = new GetAllPaymentCustomersQuery();
        var paymentCustomers = await paymentCustomerQueryService.Handle(getAllPaymentCustomersQuery);
        var paymentCustomerResources = paymentCustomers.Select(PaymentCustomerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(paymentCustomerResources);
    }
    
    [HttpGet("{paymentCustomerId:int}")]
    public async Task<IActionResult> GetPaymentCustomerById(int paymentCustomerId)
    {
        var getPaymentCustomerByIdQuery = new GetPaymentCustomerByIdQuery(paymentCustomerId);
        var paymentCustomer = await paymentCustomerQueryService.Handle(getPaymentCustomerByIdQuery);
        if (paymentCustomer == null) return NotFound();
        var paymentCustomerResource = PaymentCustomerResourceFromEntityAssembler.ToResourceFromEntity(paymentCustomer);
        return Ok(paymentCustomerResource);
    }
    
    [HttpGet("by-customer/{customerId:int}")]
    public async Task<IActionResult> GetPaymentCustomersByCustomerId(int customerId)
    {
        var getPaymentCustomerByCustomerIdQuery = new GetAllPaymentCustomersByCustomerIdQuery(customerId);
        var paymentCustomers = await paymentCustomerQueryService.Handle(getPaymentCustomerByCustomerIdQuery);
        var paymentCustomerResources = paymentCustomers.Select(PaymentCustomerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(paymentCustomerResources);
    }
    
    [HttpPut("{paymentCustomerId:int}")]
    public async Task<IActionResult> UpdatePaymentCustomer(int paymentCustomerId, UpdatePaymentCustomerResource resource)
    {
        if (paymentCustomerId != resource.Id)
        {
            return BadRequest("Payment customer ID mismatch.");
        }

        var updatePaymentCustomerCommand = UpdatePaymentCustomerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var paymentCustomer = await paymentCustomerCommandService.Handle(updatePaymentCustomerCommand);
        if (paymentCustomer is null) return NotFound();
        var paymentCustomerResource = PaymentCustomerResourceFromEntityAssembler.ToResourceFromEntity(paymentCustomer);
        return Ok(paymentCustomerResource);
    }
}