using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Repositories;

public interface IProviderRepository : IBaseRepository<Provider>
{
    Task<IEnumerable<Provider>> GetAllProvidersAsync();
}