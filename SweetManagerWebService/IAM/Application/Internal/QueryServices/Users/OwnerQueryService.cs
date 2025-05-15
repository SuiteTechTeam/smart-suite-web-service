using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Users
{
    public class OwnerQueryService(IOwnerRepository ownerRepository) : IOwnerQueryService
    {
        public async Task<dynamic> Handle(GetAllFilteredUsersQuery query)
         => await ownerRepository.FindAllByFiltersAsync(query.Email, query.Phone, query.State);

        public async Task<Owner?> Handle(GetUserByIdQuery query)
         => await ownerRepository.FindByIdAsync(query.Id);

        public async Task<Owner?> Handle(GetOwnerFromAnOrganizationQuery query)
         => await ownerRepository.FindByHotelIdAsync(query.HotelId);

    }
}