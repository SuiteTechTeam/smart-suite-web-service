using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Credentials;

namespace SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Credentials
{
    public interface IAdminCredentialQueryService
    {
        Task<AdminCredential?> Handle(GetUserCredentialByIdQuery query);
    }
}