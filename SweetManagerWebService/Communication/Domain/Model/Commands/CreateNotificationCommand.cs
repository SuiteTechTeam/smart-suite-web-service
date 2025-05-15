namespace SweetManagerIotWebService.API.Communication.Domain.Model.Commands;

public record CreateNotificationCommand(string Title, string Content, string SenderType, int SenderId, int ReceiverId);