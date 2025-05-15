using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Preferences;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Preferences
{
    public class GuestPreferenceQueryService(IGuestPreferenceRepository guestPreferenceRepository) : IGuestPreferenceQueryService
    {
        public async Task<GuestPreference?> Handle(GetGuestPreferenceByGuestIdQuery query)
         => await guestPreferenceRepository.FindByGuestId(query.GuestId);

        public async Task<GuestPreference?> Handle(GetGuestPreferenceByIdQuery query)
         => await guestPreferenceRepository.FindByIdAsync(query.Id);

    }
}
