using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Booking;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Booking;

public class BookingResourceFromEntityAssembler
{
    public static BookingResource ToResourceFromEntity(API.Booking booking)
    {
        return new BookingResource(
            booking.Id,
            booking.PaymentCustomerId,
            booking.RoomId,
            booking.Description,
            booking.StartDate,
            booking.FinalDate,
            booking.PriceRoom,
            booking.NightCount,
            booking.Amount,
            booking.State,
            booking.PreferenceId);
    }
}