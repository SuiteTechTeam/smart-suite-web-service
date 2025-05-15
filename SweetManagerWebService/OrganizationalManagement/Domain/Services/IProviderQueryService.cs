using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;

public interface IProviderQueryService
{
    Task<Provider?> Handle(GetProviderByIdQuery query);
    Task<IEnumerable<Provider>> Handle(GetAllProvidersQuery query);
}