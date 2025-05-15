using SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.OrganizationalManagement.Interfaces.REST.Validation;

public static class CreateHotelResourceValidator
{
    public static void Validate(CreateHotelResource resource)
    {
        if (string.IsNullOrWhiteSpace(resource.Name))
            throw new ArgumentException("Name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(resource.Description))
            throw new ArgumentException("Description cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(resource.Email))
            throw new ArgumentException("Email cannot be null or empty.");

        if (!System.Text.RegularExpressions.Regex.IsMatch(resource.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Email is not valid.");

        if (string.IsNullOrWhiteSpace(resource.Address))
            throw new ArgumentException("Address cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(resource.Phone))
            throw new ArgumentException("Phone cannot be null or empty.");

        if (!System.Text.RegularExpressions.Regex.IsMatch(resource.Phone, @"^\d{9}$"))
            throw new ArgumentException("Phone is not valid.");
    }
}