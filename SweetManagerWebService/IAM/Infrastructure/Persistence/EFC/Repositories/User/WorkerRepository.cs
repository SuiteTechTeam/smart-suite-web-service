using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.User;

public class WorkerRepository(SweetManagerContext context) : BaseRepository<Worker>(context), IWorkerRepository
{
    public async Task<IEnumerable<Worker>> FindAllByHotelId(int hotelId)
        => await (from wo in Context.Set<Worker>()
            join nt in Context.Set<Notification>() on wo.Id equals nt.WorkersId
            join ow in Context.Set<Owner>() on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where ho.Id == hotelId
            select wo)
            .ToListAsync();public async Task<Worker?> FindById(int id)
        => await Context.Set<Worker>()
            .Where(wo => wo.Id == id)
            .FirstOrDefaultAsync();

    public async Task<Worker?> FindByEmail(string email)
        => await Context.Set<Worker>()
            .Where(wo => wo.Email == email)
            .FirstOrDefaultAsync();

    public async Task<int?> FindIdByEmail(string email)
        => await Context.Set<Worker>()
            .Where(wo => wo.Email == email)
            .Select(wo => wo.Id)
            .FirstOrDefaultAsync();

    public async Task<bool> ExecuteUpdateWorkerEmailAsync(string email, int id)
        => await Context.Set<Worker>().Where(w => w.Id.Equals(id))
            .ExecuteUpdateAsync(w => w.SetProperty(p => p.Email, email)) > 0;    public async Task<bool> ExecuteUpdateWorkerPhoneAsync(int phone, int id)
        => await Context.Set<Worker>().Where(w => w.Id.Equals(id))
            .ExecuteUpdateAsync(w => w.SetProperty(p => p.Phone, phone)) > 0;

    public async Task<int?> FindHotelIdByWorkerId(int id)
        => await (from wo in Context.Set<Worker>()
            join nt in Context.Set<Notification>() on wo.Id equals nt.WorkersId
            join ow in Context.Set<Owner>() on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where wo.Id == id
            select ho.Id)
            .FirstOrDefaultAsync();
}