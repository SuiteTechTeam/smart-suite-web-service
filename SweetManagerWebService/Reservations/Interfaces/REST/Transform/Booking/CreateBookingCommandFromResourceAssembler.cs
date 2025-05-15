using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Booking;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Booking;

public class CreateBookingCommandFromResourceAssembler
{
    public static CreateBookingCommand ToCommandFromResource(CreateBookingResource resource)
    {
        return new CreateBookingCommand(resource.PaymentCustomerId,
        resource.RoomId, resource.Description, resource.StartDate,
            resource.FinalDate, resource.PriceRoom, resource.NightCount, resource.Amount, resource.State,
            resource.PreferenceId);
    }
}