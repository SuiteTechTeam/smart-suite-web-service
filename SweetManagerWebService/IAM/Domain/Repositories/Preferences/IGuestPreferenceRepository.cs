using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.IAM.Domain.Repositories.Preferences
{
    public interface IGuestPreferenceRepository : IBaseRepository<GuestPreference>
    {
        Task<GuestPreference?> FindByGuestId(int guestId); 

    }
}