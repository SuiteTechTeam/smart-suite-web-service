using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Repositories;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Application.Internal.CommandServices;

public class HotelCommandService(IHotelRepository hotelRepository, IUnitOfWork unitOfWork) : IHotelCommandService
{
    public async Task<Hotel?> Handle(CreateHotelCommand command)
    {
        
        // checking if the hotel name and email already exists
        var existingHotel = await hotelRepository.FindByNameAndEmailAsync(command.Name, command.Email);
        if (existingHotel != null)
        {
            Console.WriteLine("Hotel with the same name and email already exists.");
            throw new Exception("Parece que ya existe un hotel con el mismo nombre y correo electrónico. Por favor, verifica los datos e intenta nuevamente.");
        }
        
        // checking if the owner id exists - when the ACL for IAM is implemented
        if(command.OwnerId == 0) throw new Exception("El id del owner de hotel no existe. Por favor, verifica los datos e intenta nuevamente.");
        
        var hotel = new Hotel(command);
        await hotelRepository.AddAsync(hotel);
        await unitOfWork.CommitAsync();
        return hotel;
        
    }
    
    public async Task<Hotel?> Handle(UpdateHotelCommand command)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.HotelId);
        if (hotel == null)
        {
            Console.WriteLine("Hotel not found.");
            throw new Exception("No se encontró el hotel. Por favor, verifica los datos e intenta nuevamente.");
        }

        hotel.UpdateData(command);
        hotelRepository.Update(hotel);
        await unitOfWork.CommitAsync();
        return hotel;
    }
}