using System;
using System.Collections.Generic;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;

public partial class Supply
{
    public int Id { get; set; }

    public int ProviderId { get; set; }

    public int HotelId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string State { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual ICollection<SupplyRequest> SupplyRequests { get; set; } = new List<SupplyRequest>();
    
    public Supply(int id,int hotelId, int providerId, string name, decimal price, int stock, string state)
    {
        Id = id;
        ProviderId = providerId;
        HotelId = hotelId;
        Name = name.ToUpper();
        Price = price;
        Stock = stock;
        State = state.ToUpper();
    }
    
    public Supply(CreateSupplyCommand command)
    {
        ProviderId = command.ProviderId; 
        HotelId = command.HotelId;
        Name = command.Name.ToUpper();
        Price = command.Price;
        Stock = command.Stock;
        State = command.State.ToUpper();
    }
    
    public void Update(UpdateSupplyCommand command)
    {
        ProviderId = command.ProviderId;
        HotelId = command.HotelId;
        Name = command.Name.ToUpper();
        Price = command.Price;
        Stock = command.Stock;
        State = command.State.ToUpper();
    }
}
