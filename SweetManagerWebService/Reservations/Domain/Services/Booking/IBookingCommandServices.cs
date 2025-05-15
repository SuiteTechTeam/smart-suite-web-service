using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;

namespace SweetManagerIotWebService.API.Reservations.Domain.Services.Booking;

public interface IBookingCommandServices
{
    Task<bool> Handle(CreateBookingCommand command);
    Task<bool> Handle(UpdateBookingStateCommand command);
    Task<bool> Handle(UpdateBookingEndDateCommand command);
}