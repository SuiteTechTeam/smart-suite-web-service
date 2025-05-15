namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Queries;

public record GetAllRoomsByBookingAvailabilityInARangeQuery(DateTime StartDate, DateTime  FinalDate, int HotelId);