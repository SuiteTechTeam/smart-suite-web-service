using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.TypeRoom;

namespace SweetManagerIotWebService.API.Reservations.Domain.Services.TypeRoom;

public interface ITypeRoomCommandService
{
    Task<bool> Handle(CreateTypeRoomCommand command);

}