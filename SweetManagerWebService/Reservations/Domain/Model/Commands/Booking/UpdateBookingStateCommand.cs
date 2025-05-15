namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;

public record UpdateBookingStateCommand(int Id, string State);