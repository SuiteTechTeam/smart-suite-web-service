using System;
using System.Collections.Generic;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;

public partial class PaymentCustomer
{
    public int Id { get; set; }

    public int? GuestId { get; set; }

    public decimal? FinalAmount { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Guest? Guest { get; set; }

    public PaymentCustomer(int? guestId, decimal? finalAmount)
    {
        GuestId = guestId;
        FinalAmount = finalAmount;
    }
    
    public PaymentCustomer(CreatePaymentCustomerCommand command)
    {
        GuestId = command.GuestId;
        FinalAmount = command.FinalAmount;
    }
    
    public PaymentCustomer(UpdatePaymentCustomerCommand command)
    {
        Id = command.Id;
        GuestId = command.GuestId;
        FinalAmount = command.FinalAmount;
    }
    
}
