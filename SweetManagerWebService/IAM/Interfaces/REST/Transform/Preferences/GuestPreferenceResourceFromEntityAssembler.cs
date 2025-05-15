using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Preferences;

namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Transform.Preferences
{
    public static class GuestPreferenceResourceFromEntityAssembler
    {
        public static GuestPreferenceResource ToResourceFromEntity(GuestPreference entity)
        {
            return new GuestPreferenceResource(entity.Id,entity.GuestId, entity.Temperature);
        }
    }
}