using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories
{
    public class UnitOfWork(SweetManagerContext context) : IUnitOfWork
    {
        public async Task CommitAsync() => await context.SaveChangesAsync();
    }
}