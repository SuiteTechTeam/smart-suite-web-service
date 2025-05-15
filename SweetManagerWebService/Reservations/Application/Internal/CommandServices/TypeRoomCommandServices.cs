using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.TypeRoom;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Reservations.Domain.Services.TypeRoom;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Application.Internal.CommandServices;

public class TypeRoomCommandServices(ITypeRoomRepository typeRoomRepository, IUnitOfWork unitOfWork) :ITypeRoomCommandService
{
    IUnitOfWork _unitOfWork = unitOfWork;
    ITypeRoomRepository _roomRepository = typeRoomRepository;
    
    public async Task<bool> Handle(CreateTypeRoomCommand command)
    {
        
        if (string.IsNullOrWhiteSpace(command.Description))
            throw new ArgumentException("Description is required.");
        if (command.Price <= 0)
            throw new ArgumentException("Price is required.");


        var typeRoom = new TypeRoom(command);
        await _roomRepository.AddAsync(typeRoom);
        await _unitOfWork.CommitAsync();

        return true;
    }
}