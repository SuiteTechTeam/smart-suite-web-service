using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Booking;

public class UpdateBookingEndDateCommandFromResourceAssembler
{
    public static UpdateBookingEndDateCommand ToCommandFromResource(UpdateBookingEndDateCommand resource)
    {
        return new UpdateBookingEndDateCommand(resource.Id, resource.EndDate);
    }
}