using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users
{
    public interface IAdminQueryService
    {
        Task<IEnumerable<Admin>> Handle(GetAllUsersFromOrganizationQuery query);

        Task<dynamic> Handle(GetAllFilteredUsersQuery query);

        Task<Admin?> Handle(GetUserByIdQuery query);
    }
}