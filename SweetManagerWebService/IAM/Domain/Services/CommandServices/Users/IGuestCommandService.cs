using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users
{
    public interface IGuestCommandService
    {
        Task<Guest?> Handle(SignUpUserCommand command);

        Task<Guest?> Handle(UpdateUserCommand command);

        Task<dynamic?> Handle(SignInUserCommand command);
    }
}