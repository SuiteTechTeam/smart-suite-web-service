using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.User;

public class OwnerRepository(SweetManagerContext context) : BaseRepository<Owner>(context), IOwnerRepository
{
    public async Task<Owner?> FindByHotelId(int hotelId)
        => await (from ow in Context.Set<Owner>()
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where ho.Id == hotelId
            select ow)
            .FirstOrDefaultAsync();

    public async Task<Owner?> FindById(int id)
        => await Context.Set<Owner>()
            .Where(ow => ow.Id == id)
            .FirstOrDefaultAsync();

    public async Task<Owner?> FindByEmail(string email)
        => await Context.Set<Owner>()
            .Where(ow => ow.Email == email)
            .FirstOrDefaultAsync();

    public async Task<int?> FindIdByEmail(string email)
        => await Context.Set<Owner>()
            .Where(ow => ow.Email == email)
            .Select(ow => ow.Id)
            .FirstOrDefaultAsync();

    public async Task<bool> ExecuteUpdateOwnerEmailAsync(string email, int id)
        => await Context.Set<Owner>().Where(o => o.Id == id)
            .ExecuteUpdateAsync(o => o.SetProperty(p => p.Email, email)) > 0;    public async Task<bool> ExecuteUpdateOwnerPhoneAsync(int phone, int id)
        => await Context.Set<Owner>().Where(o => o.Id == id)
            .ExecuteUpdateAsync(o => o.SetProperty(p => p.Phone, phone)) > 0;

    public async Task<int?> FindHotelIdByOwnerId(int id)
        => await (from ow in Context.Set<Owner>()
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where ow.Id == id
            select ho.Id)
            .FirstOrDefaultAsync();
    
}