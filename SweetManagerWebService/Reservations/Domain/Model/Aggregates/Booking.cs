using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;

namespace SweetManagerIotWebService.API;

public partial class Booking
{
    public int Id { get; set; }

    public int? PaymentCustomerId { get; set; }

    public int? RoomId { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? FinalDate { get; set; }

    public decimal? PriceRoom { get; set; }

    public int? NightCount { get; set; }

    public decimal? Amount { get; set; }

    public string? State { get; set; }

    public int? PreferenceId { get; set; }

    public virtual PaymentCustomer? PaymentCustomer { get; set; }

    public virtual GuestPreference? Preference { get; set; }

    [JsonIgnore]
    public virtual Room? Room { get; set; }
    
    public Booking(int id, int? paymentCustomerId, int? roomId, string? description, DateTime? startDate, DateTime? finalDate, decimal? priceRoom, int? nightCount, decimal? amount, string? state, int? preferenceId)
    {
        Id = id;
        PaymentCustomerId = paymentCustomerId;
        RoomId = roomId;
        Description = description;
        StartDate = startDate;
        FinalDate = finalDate;
        PriceRoom = priceRoom;
        NightCount = nightCount;
        Amount = amount;
        State = state?.ToUpper();
        PreferenceId = preferenceId;
    }
    
    public Booking(CreateBookingCommand command)
    {
        PaymentCustomerId = command.PaymentCustomerId;
        RoomId = command.RoomId;
        Description = command.Description;
        StartDate = command.StartDate;
        FinalDate = command.FinalDate;
        PriceRoom = command.PriceRoom;
        NightCount = command.NightCount;
        Amount = command.Amount;
        State = command.State?.ToUpper();
        PreferenceId = command.PreferenceId;

        Validate(1);

    }
    
    public Booking(UpdateBookingStateCommand command)
    {
        Id = command.Id;
        State = command.State?.ToUpper();
        Validate(2);
    }
    
    public Booking(UpdateBookingEndDateCommand command)
    {
        Id = command.Id;
        FinalDate = command.EndDate;
        Validate(3);
    }
    
    public void Validate(int type)
{
    switch (type)
    {
        // Tipo 1: Validación para CreateBookingCommand
        case 1:
            if (PaymentCustomerId == null)
                throw new ArgumentException("El campo 'PaymentCustomerId' es obligatorio.");

            if (RoomId == null)
                throw new ArgumentException("El campo 'RoomId' es obligatorio.");

            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("La 'Descripción' no puede estar vacía.");

            if (StartDate == null || FinalDate == null)
                throw new ArgumentException("Las fechas de inicio y fin son obligatorias.");

            if (StartDate >= FinalDate)
                throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");

            if (PriceRoom == null || PriceRoom < 0)
                throw new ArgumentException("El precio por habitación debe ser un número positivo.");

            if (NightCount == null || NightCount <= 0)
                throw new ArgumentException("La cantidad de noches debe ser mayor a cero.");

            if (Amount == null || Amount < 0)
                throw new ArgumentException("El monto total debe ser un número positivo.");

            if (string.IsNullOrWhiteSpace(State))
                throw new ArgumentException("El campo 'Estado' es obligatorio.");

            if (PreferenceId == null)
                throw new ArgumentException("El campo 'PreferenceId' es obligatorio.");
            break;

        // Tipo 2: Validación para UpdateBookingStateCommand
        case 2:
            if (Id <= 0)
                throw new ArgumentException("El Id debe ser válido.");

            if (string.IsNullOrWhiteSpace(State))
                throw new ArgumentException("El nuevo estado es obligatorio.");
            break;

        // Tipo 3: Validación para UpdateBookingEndDateCommand
        case 3:
            if (Id <= 0)
                throw new ArgumentException("El Id debe ser válido.");

            if (FinalDate == null)
                throw new ArgumentException("La nueva fecha de fin es obligatoria.");
            break;

        default:
            throw new ArgumentException("Tipo de validación desconocido.");
    }
}


}
