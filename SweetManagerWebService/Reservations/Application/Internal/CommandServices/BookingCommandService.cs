using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Booking;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Application.Internal.CommandServices;

public class BookingCommandService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork) : IBookingCommandServices
{
    IBookingRepository _bookingRepository = bookingRepository;
    IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(CreateBookingCommand command)
    {
        var booking = new Booking(command);
        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.CommitAsync();
        return true;
    }
    
    public async Task<bool> Handle(UpdateBookingStateCommand command)
    {
        try
        {
            await _bookingRepository.UpdateBookingStateAsync(command.Id, command.State);
            return true; 
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public async Task<bool> Handle(UpdateBookingEndDateCommand command)
    {
        try
        {
            await _bookingRepository.UpdateBookingEndDateAsync(command.Id, command.EndDate);
            return true; 
        }
        catch (Exception e)
        {
            return false;
        }
    }
}