using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SweetManagerIotWebService.API.Reservations.Domain.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;

namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;

public partial class Room
{
    public int Id { get; set; }
    public int? TypeRoomId { get; set; }
    public int? HotelId { get; set; }
    public string? State { get; set; } 
    
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Hotel? Hotel { get; set; }

    public virtual TypeRoom? TypeRoom { get; set; }

    public Room()
    {
        this.TypeRoomId = 0;
        this.HotelId = 0;
        this.State = string.Empty;
    }
    public Room(int id, int typeRoomId, int hotelId, string state)
    {
        Id = id;
        TypeRoomId = typeRoomId;
        HotelId = hotelId;
        State = state.ToUpper(); 
    }

    public Room(CreateRoomCommand command)
    {
        TypeRoomId = command.TypeRoomId;
        HotelId = command.HotelId;
        State = command.State?.ToUpper();
        
        Validate(1);
    }

    public Room(UpdateRoomStateCommand command)
    {
        this.Id = command.Id;
        this.State = command.State;
        
        Validate(2);
    }
    
    public void Validate(int type)
    {
        switch (type)
        {
            // Tipo 1: Validación para CreateRoomCommand
            case 1:
                if (TypeRoomId == null || TypeRoomId <= 0)
                    throw new ArgumentException("El campo 'TypeRoomId' es obligatorio y debe ser válido.");

                if (HotelId == null || HotelId <= 0)
                    throw new ArgumentException("El campo 'HotelId' es obligatorio y debe ser válido.");

                if (string.IsNullOrWhiteSpace(State))
                    throw new ArgumentException("El campo 'State' no puede estar vacío.");
                break;

            // Tipo 2: Validación para UpdateRoomStateCommand
            case 2:
                if (Id <= 0)
                    throw new ArgumentException("El campo 'Id' debe ser válido.");

                if (string.IsNullOrWhiteSpace(State))
                    throw new ArgumentException("El nuevo estado de la habitación es obligatorio.");
                break;

            default:
                throw new ArgumentException("Tipo de validación desconocido.");
        }
    }

}