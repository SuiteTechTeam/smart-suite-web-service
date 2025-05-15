using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Reservations.Domain.Services.Booking;

public interface IBookingQueryServices
{
    Task<IEnumerable<API.Booking>> Handle(GetAllBookingsQuery query);
    Task<IEnumerable<BookingDto>> Handle(GetBookingByCustomerIdQuery query);
    Task<IEnumerable<API.Booking>> Handle(GetBookingByHotelIdAndState query);
    Task<API.Booking?> Handle(GetBookingByIdQuery query);
    
}