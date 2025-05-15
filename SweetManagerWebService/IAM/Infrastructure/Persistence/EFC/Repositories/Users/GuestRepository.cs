using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Users
{
    public class GuestRepository(SweetManagerContext context) : BaseRepository<Guest>(context), IGuestRepository
    {
        public async Task<dynamic?> FindAllByFiltersAsync(string? email, string? phone, string? state)
        {
            var query = Context.Guests.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
                return await query.Where(a => a.Email!.Equals(email)).FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(a => a.Phone!.Equals(phone));

            if (!string.IsNullOrWhiteSpace(state))
                query = query.Where(a => a.State!.Equals(state));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Guest>> FindAllByHotelIdAsync(int hotelId)
         => await Task.Run(() => (
            from gu in Context.Set<Guest>().ToList()
            join pc in Context.Set<PaymentCustomer>().ToList()
                on gu.Id equals pc.GuestId
            join bo in Context.Set<Booking>().ToList()
                on pc.Id equals bo.PaymentCustomerId
            join ro in Context.Set<Room>().ToList()
                on bo.RoomId equals ro.Id
            join ho in Context.Set<Hotel>().ToList()
                on ro.HotelId equals ho.Id
            where ho.Id.Equals(hotelId)

            select gu
         ).ToList());

        public async Task<int?> FindHotelIdByIdAsync(int id)
         => await Task.Run(() => (
            from gu in Context.Set<Guest>().ToList()
            join pc in Context.Set<PaymentCustomer>().ToList()
                on gu.Id equals pc.GuestId
            join bo in Context.Set<Booking>().ToList()
                on pc.Id equals bo.PaymentCustomerId
            join ro in Context.Set<Room>().ToList()
                on bo.RoomId equals ro.Id
            join ho in Context.Set<Hotel>().ToList()
                on ro.HotelId equals ho.Id
            where gu.Id.Equals(id)

            select gu
         ).FirstOrDefault()?.Id);
    }
}