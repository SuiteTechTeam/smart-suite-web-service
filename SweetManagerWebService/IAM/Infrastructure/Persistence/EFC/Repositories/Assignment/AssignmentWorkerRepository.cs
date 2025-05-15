using Microsoft.EntityFrameworkCore;
using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Assignments;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Assignment;

public class AssignmentWorkerRepository(SweetManagerContext context) : BaseRepository<AssignmentWorker>(context), IAssignmentWorkerRepository
{
    public async Task<IEnumerable<AssignmentWorker>> FindByWorkerIdAsync(int workerId)
        => await (from aw in Context.Set<AssignmentWorker>()
            join wk in Context.Set<Worker>() on aw.WorkersId equals wk.Id
            where wk.Id == workerId && aw.FinalDate > DateTime.Now
            select aw)
            .ToListAsync();

    public async Task<IEnumerable<AssignmentWorker>> FindByAdminIdAsync(int adminId)
        => await Context.Set<AssignmentWorker>()
            .Where(aw => aw.AdminsId == adminId)
            .ToListAsync();

    public async Task<IEnumerable<AssignmentWorker>> FindByWorkerAreaIdAsync(int workerAreaId)
        => await Context.Set<AssignmentWorker>()
            .Where(aw => aw.WorkersAreasId == workerAreaId)
            .ToListAsync();
}