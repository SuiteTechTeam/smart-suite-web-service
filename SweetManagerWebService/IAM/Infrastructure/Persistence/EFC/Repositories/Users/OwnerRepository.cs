using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Users
{
    public class OwnerRepository(SweetManagerContext context) : BaseRepository<Owner>(context),
        IOwnerRepository
    {
        public async Task<dynamic?> FindAllByFiltersAsync(string? email, string? phone, string? state)
        {
            var query = Context.Owners.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
                return await query.Where(a => a.Email!.Equals(email)).FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(a => a.Phone!.Equals(phone));

            if (!string.IsNullOrWhiteSpace(state))
                query = query.Where(a => a.State!.Equals(state));

            return await query.ToListAsync();
        }

        public async Task<Owner?> FindByHotelIdAsync(int hotelId)
         => await Task.Run(() => (
            from ow in Context.Set<Owner>().ToList()
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals ho.OwnerId
            where ho.Id.Equals(hotelId)
            select ow
         ).FirstOrDefault());

        public async Task<int?> FindHotelIdByIdAsync(int id)
         => await Task.Run(() => (
            from ow in Context.Set<Owner>().ToList()
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals ho.OwnerId
            where ow.Id.Equals(id)
            select ow
         ).FirstOrDefault()?.Id);
    }
}
