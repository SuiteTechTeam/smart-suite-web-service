namespace SweetManagerIotWebService.API.Communication.Interfaces.REST.Resources;

public record NotificationResource(int Id, string Title, string Content, string SenderType, int? SenderId, int? ReceiverId);