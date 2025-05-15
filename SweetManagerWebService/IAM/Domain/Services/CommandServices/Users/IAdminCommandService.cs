using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users
{
    public interface IAdminCommandService
    {

        Task<Admin?> Handle(SignUpUserCommand command);

        Task<Admin?> Handle(UpdateUserCommand command);

        Task<dynamic?> Handle(SignInUserCommand command);
    }
}