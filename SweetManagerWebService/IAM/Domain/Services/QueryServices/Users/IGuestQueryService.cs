using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users
{
    public interface IGuestQueryService
    {
        Task<dynamic> Handle(GetAllFilteredUsersQuery query);

        Task<IEnumerable<Guest>> Handle(GetAllUsersFromOrganizationQuery query);

        Task<Guest?> Handle(GetUserByIdQuery query);

    }
}