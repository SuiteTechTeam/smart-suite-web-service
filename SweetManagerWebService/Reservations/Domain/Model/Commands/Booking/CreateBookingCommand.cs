namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;

public record CreateBookingCommand(
    int? PaymentCustomerId,
    int? RoomId,
    string? Description,
    DateTime? StartDate,
    DateTime? FinalDate,
    decimal? PriceRoom,
    int? NightCount,
    decimal? Amount,
    string? State,
    int? PreferenceId);
