using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;

public partial class Admin
{
    public int Id { get; private set; }

    public string? Name { get; private set; }

    public string? Surname { get; private set; }

    public string? Phone { get; private set; }

    public string? Email { get; private set; }

    public string? State { get; private set; }

    public int? RoleId { get; private set; }

    public int? HotelId { get; private set; }

    public virtual AdminCredential? AdminCredential { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public Admin() { }

    public Admin(int id, string name, string surname, string phone, string email, string state, int roleId)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Phone = phone;
        Email = email;
        State = state;
        RoleId = roleId;
    }

    public Admin(UpdateUserCommand command)
    {
        Id = command.Id;
        Name = command.Name;
        Surname = command.Surname;
        Phone = command.Phone;
        Email = command.Email;
        State = command.State;
    }

    public Admin Update(UpdateUserCommand command)
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