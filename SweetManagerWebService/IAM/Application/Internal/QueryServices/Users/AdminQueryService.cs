using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Users
{
    public class AdminQueryService(IAdminRepository adminRepository) : IAdminQueryService
    {
        public async Task<IEnumerable<Admin>> Handle(GetAllUsersFromOrganizationQuery query)
         => await adminRepository.FindAllByHotelIdAsync(query.HotelId);

        public async Task<dynamic> Handle(GetAllFilteredUsersQuery query)
         => await adminRepository.FindAllByFiltersAsync(query.Email, query.Phone, query.State);

        public async Task<Admin?> Handle(GetUserByIdQuery query)
         => await adminRepository.FindByIdAsync(query.Id);

    }
}
