using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Preferences;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Preferences;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Preferences
{
    public static class CreateGuestPreferenceCommandFromResourceAssembler
    {
        public static CreateGuestPreferenceCommand ToCommandFromResource(CreateGuestPreferenceResource resource)
        {
            return new CreateGuestPreferenceCommand(resource.GuestId, resource.Temperature);
        }
    }
}