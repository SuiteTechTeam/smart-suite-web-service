namespace SweetManagerIotWebService.API.IAM.Interfaces.REST.Resources.Preferences
{
    public record UpdateGuestPreferenceResource(int Id, int GuestId, int Temperature);
}