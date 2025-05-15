using SweetManagerIotWebService.API.Reservations.Domain.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Room;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Application.Internal.CommandServices;

public class RoomCommandService(IRoomRepository roomRepository, IUnitOfWork unitOfWork): IRoomCommandService
{
     IRoomRepository _roomRepository = roomRepository;
     IUnitOfWork _unitOfWork = unitOfWork;
    
     public async Task<bool> Handle(CreateRoomCommand command)
     {
         if (command.TypeRoomId is null)
             throw new ArgumentException("TypeRoomId is required.");
         if (command.HotelId is null)
             throw new ArgumentException("HotelId is required.");
         if (string.IsNullOrWhiteSpace(command.State))
             throw new ArgumentException("State is required.");

         var room = new Room(command);
         await _roomRepository.AddAsync(room);
         await _unitOfWork.CommitAsync();

         return true;
     }

    
    public async Task<bool> Handle(UpdateRoomStateCommand command)
    {
        try
        {
            await _roomRepository.UpdateRoomStateAsync(command.Id, command.State);
            return true; 
        }
        catch (Exception e)
        {
            return false;
        }
    }
}