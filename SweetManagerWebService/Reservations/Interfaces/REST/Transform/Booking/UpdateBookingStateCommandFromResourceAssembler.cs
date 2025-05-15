using Mysqlx.Crud;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;
using SweetManagerIotWebService.API.Reservations.Interfaces.REST.Resources.Booking;

namespace SweetManagerIotWebService.API.Reservations.Interfaces.REST.Transform.Booking;

public class UpdateBookingStateCommandFromResourceAssembler
{
    public static UpdateBookingStateCommand ToCommandFromResource(UpdateBookingStateResource resource)
    {
        return new UpdateBookingStateCommand(resource.Id, resource.State);
    }
}

