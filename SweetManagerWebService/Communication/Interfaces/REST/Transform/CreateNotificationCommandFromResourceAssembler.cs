using SweetManagerIotWebService.API.Communication.Domain.Model.Commands;
using SweetManagerIotWebService.API.Communication.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Communication.Interfaces.REST.Transform;

public static class CreateNotificationCommandFromResourceAssembler
{
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
    {
        return new CreateNotificationCommand(
            resource.Title, 
            resource.Content, 
            resource.SenderType, 
            resource.SenderId, 
            resource.ReceiverId);
    }
}