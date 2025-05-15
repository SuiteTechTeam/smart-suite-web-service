namespace SweetManagerIotWebService.API.Communication.Interfaces.REST.Resources;

public record CreateNotificationResource(string Title, string Content, string SenderType, int SenderId, int ReceiverId);