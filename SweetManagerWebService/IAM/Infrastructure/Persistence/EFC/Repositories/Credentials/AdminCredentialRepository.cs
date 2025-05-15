using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Credentials
{
    public class AdminCredentialRepository(SweetManagerContext context) : BaseRepository<AdminCredential>(context), 
        IAdminCredentialRepository
    {
        
    }
}
