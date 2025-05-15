using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users;
using SweetManagerIotWebService.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Pipeline.Middleware.Components
{
    public class RequestAuthorizationMiddleware(RequestDelegate next, ILogger<RequestAuthorizationMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context, ITokenService tokenService, IAdminQueryService adminQueryService,
            IGuestQueryService guestQueryService, IOwnerQueryService ownerQueryService)
        {
            try
            {
                var endpoint = context.Request.HttpContext.GetEndpoint();

                var allowAnonymous =
                    context.Request.HttpContext.GetEndpoint()!.Metadata.Any(m =>
                        m.GetType() == typeof(AllowAnonymousAttribute));

                logger.LogInformation($"Endpoint: {endpoint?.DisplayName}, AllowAnonymous: {allowAnonymous}");

                if (allowAnonymous)
                {
                    await next(context);

                    return;
                }

                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    await context.Response.WriteAsync("Token is required");
                    return;
                }

                var tokenResult = tokenService.ValidateToken(token);

                if (tokenResult == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is invalid");
                    return;
                }

                dynamic? validation = null;

                // Only if I have more than 1 Aggregate 
                if (tokenResult.Role == "ROLE_ADMIN")
                    validation = await adminQueryService.Handle(new GetUserByIdQuery(tokenResult.Id));

                else if (tokenResult.Role == "ROLE_GUEST")
                    validation = await guestQueryService.Handle(new GetUserByIdQuery(tokenResult.Id));

                else if (tokenResult.Role == "ROLE_OWNER")
                    validation = await ownerQueryService.Handle(new GetUserByIdQuery(tokenResult.Id));

                if (validation is null)
                    throw new Exception("Invalid credentials!");

                context.Items["Credentials"] = tokenResult;

                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Token validation error");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid Token!");
            }

        }
    }
}
