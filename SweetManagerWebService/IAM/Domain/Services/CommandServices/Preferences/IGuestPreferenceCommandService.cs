using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Preferences
{
    public interface IGuestPreferenceCommandService
    {
        Task<GuestPreference?> Handle(CreateGuestPreferenceCommand command);

        Task<GuestPreference?> Handle(UpdateGuestPreferenceCommand command);
    }
}