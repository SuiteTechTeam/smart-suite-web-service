using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Booking;

namespace SweetManagerIotWebService.API.Reservations.Application.Internal.QueryServices;

public class BookingQueryService(IBookingRepository bookingRepository) : IBookingQueryServices
{
    public async Task<IEnumerable<Booking>> Handle(GetAllBookingsQuery query)
    {
        return await bookingRepository.FindAllByHotelIdAsync(query.HotelId);
    }

    public async Task<IEnumerable<BookingDto>> Handle(GetBookingByCustomerIdQuery query)
    {
        var bookings = await bookingRepository.FindByCustomerIdAsync(query.CustomerId);

        return bookings.Select(b => new BookingDto
        {
            Id = b.Id,
            Description = b.Description,
            StartDate = b.StartDate,
            FinalDate = b.FinalDate,
            PriceRoom = b.PriceRoom,
            Amount = b.Amount,
            State = b.State,
            PreferenceTemperature = b.Preference?.Temperature
        });
    }


    public async Task<IEnumerable<Booking>> Handle(GetBookingByHotelIdAndState query)
    {
        return await bookingRepository.FindByHotelIdAndStateAsync(query.HotelId, query.State);
    }

    public async Task<Booking?> Handle(GetBookingByIdQuery query)
    {
        return await bookingRepository.FindByIdAsync(query.Id);
    }

}