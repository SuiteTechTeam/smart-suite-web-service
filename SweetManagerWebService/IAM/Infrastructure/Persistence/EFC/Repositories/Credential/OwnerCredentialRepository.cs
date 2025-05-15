using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Credential;

public class OwnerCredentialRepository(SweetManagerContext context): BaseRepository<OwnerCredential>(context), IOwnerCredentialRepository 
{
    public async Task<OwnerCredential?> FindByOwnersIdAsync(int ownersId)
        => await Context.Set<OwnerCredential>()
            .Where(oc => oc.OwnersId == ownersId)
            .FirstOrDefaultAsync();
}