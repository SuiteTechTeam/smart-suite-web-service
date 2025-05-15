namespace SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Preferences
{
    public record CreateGuestPreferenceCommand(int GuestId, int Temperature);
}