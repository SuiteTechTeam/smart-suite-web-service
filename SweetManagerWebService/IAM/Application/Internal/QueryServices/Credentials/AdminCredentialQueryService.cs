using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Credentials;

namespace SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Credentials
{
    public class AdminCredentialQueryService(IAdminCredentialRepository adminCredentialRepository) : IAdminCredentialQueryService
    {
        public async Task<AdminCredential?> Handle(GetUserCredentialByIdQuery query)
         => await adminCredentialRepository.FindByIdAsync(query.Id);
    }
}