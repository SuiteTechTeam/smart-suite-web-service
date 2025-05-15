using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Preferences;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Preferences;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Preferences
{
    public static class UpdateGuestPreferenceCommandFromResourceAssembler
    {
        public static UpdateGuestPreferenceCommand ToCommandFromResource(UpdateGuestPreferenceResource resource)
        {
            return new UpdateGuestPreferenceCommand(resource.Id, resource.GuestId, resource.Temperature);
        }
    }
}