namespace SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Preferences
{
    public record UpdateGuestPreferenceCommand(int Id, int GuestId, int Temperature);

}