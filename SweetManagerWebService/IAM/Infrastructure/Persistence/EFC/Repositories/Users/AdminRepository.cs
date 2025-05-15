using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Users
{
    public class AdminRepository(SweetManagerContext context) : BaseRepository<Admin>(context), IAdminRepository
    {
        public async Task<dynamic?> FindAllByFiltersAsync(string? email, string? phone, string? state)
        {
            var query = Context.Admins.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
                return await query.Where(a => a.Email!.Equals(email)).FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(a => a.Phone!.Equals(phone));

            if (!string.IsNullOrWhiteSpace(state))
                query = query.Where(a => a.State!.Equals(state));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Admin>> FindAllByHotelIdAsync(int hotelId)
          => await Context.Set<Admin>().Where(a => a.HotelId.Equals(hotelId)).ToListAsync();

        public async Task<int?> FindHotelIdByIdAsync(int id)
        {
            var result = await Context.Set<Admin>().Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();

            return result?.HotelId;
        }
    }
}
