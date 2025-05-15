namespace SweetManagerIotWebService.API.Shared.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
