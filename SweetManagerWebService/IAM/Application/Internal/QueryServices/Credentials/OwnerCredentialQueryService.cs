using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Credentials;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Credentials
{
    public class OwnerCredentialQueryService(IOwnerCredentialRepository ownerCredentialRepository) : IOwnerCredentialQueryService
    {
        public async Task<OwnerCredential?> Handle(GetUserCredentialByIdQuery query)
         => await ownerCredentialRepository.FindByIdAsync(query.Id);
    }
}
