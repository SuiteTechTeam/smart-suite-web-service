using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Roles;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Population.Roles
{
    public class RolesInitializer(IRoleCommandService roleCommandService, IRoleQueryService roleQueryService,
        SweetManagerContext context)
    {
        public async Task InitializeAsync()
        {
            // Check if the role table is empty

            var result = await roleQueryService.Handle(new GetAllRolesQuery());

            if (!result.Any())
            {
                // Prepopulate the empty table

                await roleCommandService.Handle(new SeedRolesCommand());
            }
        }
    }
}