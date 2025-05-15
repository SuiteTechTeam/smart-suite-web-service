using System;
using System.Collections.Generic;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;

namespace SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;

public partial class Guest
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? State { get; set; }

    public int? RoleId { get; set; }

    public virtual GuestCredential? GuestCredential { get; set; }

    public virtual ICollection<GuestPreference> GuestPreferences { get; set; } = new List<GuestPreference>();

    public virtual ICollection<PaymentCustomer> PaymentCustomers { get; set; } = new List<PaymentCustomer>();

    public virtual Role? Role { get; set; }

    public Guest() { }

    public Guest(int id, string name, string surname, string phone, string email, string state, int roleId)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Phone = phone;
        Email = email;
        State = state;
        RoleId = roleId;
    }

    public Guest(UpdateUserCommand command)
    {
        Id = command.Id;
        Name = command.Name;
        Surname = command.Surname;
        Phone = command.Phone;
        Email = command.Email;
        State = command.State;
    }

    public Guest Update(UpdateUserCommand command)
    {
        Id = command.Id;
        Name = command.Name;
        Surname = command.Surname;
        Phone = command.Phone;
        Email = command.Email;
        State = command.State;

        return this;
    }
}
