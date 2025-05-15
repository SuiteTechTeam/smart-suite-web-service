using System;
using System.Collections.Generic;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.TypeRoom;

namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;

public partial class TypeRoom
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public TypeRoom()
    {
        this.Description = string.Empty;
        this.Price = 0;
    }
    
    public TypeRoom(int id, string description, decimal price)
    {
        Id = id;
        Description = description;
        Price = price;
    }
    
    public TypeRoom(CreateTypeRoomCommand command)
    {
        Description = command.Description;
        Price = command.Price;
        
        Validate();
    }
    
    public void Validate()
    {
                if (string.IsNullOrWhiteSpace(Description))
                    throw new ArgumentException("El campo 'Description' es obligatorio y no puede estar vacío.");

                if (Price <= 0)
                    throw new ArgumentException("El campo 'Price' debe ser mayor a 0.");
                
    }

}