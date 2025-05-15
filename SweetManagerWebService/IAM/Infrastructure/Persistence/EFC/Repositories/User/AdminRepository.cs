using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.User;

public class AdminRepository(SweetManagerContext context) : BaseRepository<Admin>(context), IAdminRepository
{
    public async Task<IEnumerable<Admin>> FindAllByHotelId(int hotelId)
        => await (from ad in Context.Set<Admin>()
            join nt in Context.Set<Notification>() on ad.Id equals nt.AdminsId
            join ow in Context.Set<Owner>() on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where ho.Id == hotelId
            select ad)
            .ToListAsync();


    public async Task<Admin?> FindById(int id)
        => await Context.Set<Admin>()
            .Where(ad => ad.Id == id)
            .FirstOrDefaultAsync();

    public async Task<Admin?> FindByEmail(string email)
        => await Context.Set<Admin>()
            .Where(ad => ad.Email == email)
            .FirstOrDefaultAsync();

    public async Task<int?> FindIdByEmail(string email)
        => await Context.Set<Admin>()
            .Where(ad => ad.Email == email)
            .Select(ad => ad.Id)
            .FirstOrDefaultAsync();

    public async Task<bool> ExecuteUpdateAdminEmailAsync(string email, int id)
        => await Context.Set<Admin>().Where(a => a.Id == id).
            ExecuteUpdateAsync(ad => ad.SetProperty(p => p.Email, email)) > 0;    public async Task<bool> ExecuteUpdateAdminPhoneAsync(int phone, int id)
        => await Context.Set<Admin>().Where(a => a.Id == id)
            .ExecuteUpdateAsync(a => a.SetProperty(p => p.Phone, phone)) > 0;

    public async Task<int?> FindHotelIdByAdminId(int id)
        => await (from ad in Context.Set<Admin>()
            join nt in Context.Set<Notification>() on ad.Id equals nt.AdminsId
            join ow in Context.Set<Owner>() on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where ad.Id == id
            select ho.Id)
            .FirstOrDefaultAsync();
}