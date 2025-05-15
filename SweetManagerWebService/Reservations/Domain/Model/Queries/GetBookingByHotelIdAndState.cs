namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;

public record GetBookingByHotelIdAndState(int HotelId, string State);