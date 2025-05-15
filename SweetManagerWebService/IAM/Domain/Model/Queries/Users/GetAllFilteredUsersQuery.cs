namespace SweetManagerIotWebService.API.IAM.Domain.Model.Queries.Users
{
    public record GetAllFilteredUsersQuery(string? Email, string? Phone, string? State);
}