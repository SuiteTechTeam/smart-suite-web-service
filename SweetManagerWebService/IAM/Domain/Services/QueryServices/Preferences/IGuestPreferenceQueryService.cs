using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Preferences;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Preferences
{
    public interface IGuestPreferenceQueryService
    {
        Task<GuestPreference?> Handle(GetGuestPreferenceByGuestIdQuery query);

        Task<GuestPreference?> Handle(GetGuestPreferenceByIdQuery query);
    }
}