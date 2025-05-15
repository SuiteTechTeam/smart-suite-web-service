using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Roles;

public class WorkerAreaRepository(SweetManagerContext context) : BaseRepository<WorkerArea>(context), IWorkerAreaRepository
{
    public async Task<IEnumerable<WorkerArea>> FindAllAsync(int hotelId)
        => await (from wa in Context.Set<WorkerArea>()
            join aw in Context.Set<AssignmentWorker>() on wa.Id equals aw.WorkersAreasId
            join ad in Context.Set<Admin>() on aw.AdminsId equals ad.Id
            join no in Context.Set<Notification>() on ad.Id equals no.AdminsId
            join ow in Context.Set<Owner>() on no.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where ho.Id == hotelId
            select wa)
            .ToListAsync();    public async Task<WorkerArea?> FindByNameAsync(string name, int hotelId)
        => await (from wa in Context.Set<WorkerArea>()
            join aw in Context.Set<AssignmentWorker>() on wa.Id equals aw.WorkersAreasId
            join ad in Context.Set<Admin>() on wa.Id equals ad.Id
            join nt in Context.Set<Notification>() on ad.Id equals nt.AdminsId
            join ow in Context.Set<Owner>() on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where wa.Name == name && ho.Id == hotelId
            select wa)
            .FirstOrDefaultAsync();

    public async Task<int?> FindIdByNameAsync(string name, int hotelId)
        => await (from wa in Context.Set<WorkerArea>()
            join aw in Context.Set<AssignmentWorker>() on wa.Id equals aw.WorkersAreasId
            join ad in Context.Set<Admin>() on wa.Id equals ad.Id
            join nt in Context.Set<Notification>() on ad.Id equals nt.AdminsId
            join ow in Context.Set<Owner>() on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
            where wa.Name == name && ho.Id == hotelId
            select wa.Id)
            .FirstOrDefaultAsync();    public async Task<string?> FindByWorkerIdAsync(int workerId)
    {
        return await (from wo in Context.Set<Worker>()
            join ass in Context.Set<AssignmentWorker>() on wo.Id equals ass.WorkersId
            join wor in Context.Set<WorkerArea>() on ass.WorkersAreasId equals wor.Id
            where wo.Id == workerId && ass.FinalDate > DateTime.Now
            select wor.Name)
            .FirstOrDefaultAsync();
    }
}