using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Credentials;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Credentials
{
    public class GuestCredentialQueryService(IGuestCredentialRepository guestCredentialRepository) : IGuestCredentialQueryService
    {
        public async Task<GuestCredential?> Handle(GetUserCredentialByIdQuery query)
         => await guestCredentialRepository.FindByIdAsync(query.Id);
    }
}
