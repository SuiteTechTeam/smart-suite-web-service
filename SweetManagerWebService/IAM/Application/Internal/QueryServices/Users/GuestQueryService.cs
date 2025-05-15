using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Users
{
    public class GuestQueryService(IGuestRepository guestRepository) : IGuestQueryService
    {
        public async Task<dynamic> Handle(GetAllFilteredUsersQuery query)
         => await guestRepository.FindAllByFiltersAsync(query.Email, query.Phone, query.State);

        public async Task<IEnumerable<Guest>> Handle(GetAllUsersFromOrganizationQuery query)
         => await guestRepository.FindAllByHotelIdAsync(query.HotelId);

        public async Task<Guest?> Handle(GetUserByIdQuery query)
         => await guestRepository.FindByIdAsync(query.Id);

    }
}
