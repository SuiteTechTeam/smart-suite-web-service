using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users
{
    public interface IOwnerCommandService
    {
        Task<Owner?> Handle(SignUpUserCommand command);

        Task<Owner?> Handle(UpdateUserCommand command);

        Task<dynamic?> Handle(SignInUserCommand command);
    }
}