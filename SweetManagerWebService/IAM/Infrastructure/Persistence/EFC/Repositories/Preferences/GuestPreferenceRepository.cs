using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Preferences;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Preferences
{
    public class GuestPreferenceRepository(SweetManagerContext context) : BaseRepository<GuestPreference>(context),
        IGuestPreferenceRepository
    {
        public async Task<GuestPreference?> FindByGuestId(int guestId)
        => await Context.Set<GuestPreference>().Where(g => g.GuestId.Equals(guestId)).FirstOrDefaultAsync();

    }
}