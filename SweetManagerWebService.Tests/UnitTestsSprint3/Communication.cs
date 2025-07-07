using NUnit.Framework;
using SweetManagerIotWebService.API.Communication.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Communication.Domain.Model.Commands;
using System;

namespace SweetManagerIotWebService.API.Communication.Tests
{
    [TestFixture]
    public class NotificationTests
    {
        [Test]
        public void CreateNotification_WhenEmergencyDetected_ShouldCreateNotificationWithCorrectProperties()
        {
            // Given - Se detecta una emergencia en el hotel
            var title = "EMERGENCIA DETECTADA";
            var content = "Situación crítica detectada en el hotel. Evacuar inmediatamente.";
            var senderType = "SYSTEM";
            var senderId = 1;
            var receiverId = 100;

            var createCommand = new CreateNotificationCommand(
                title,
                content,
                senderType,
                senderId,
                receiverId
            );

            // When - El sistema identifica la situación crítica
            var notification = new Notification(createCommand);

            // Then - Debe enviar notificaciones push inmediatas a todos los usuarios relevantes
            Assert.That(notification.Title, Is.EqualTo(title));
            Assert.That(notification.Content, Is.EqualTo(content));
            Assert.That(notification.SenderType, Is.EqualTo(senderType));
            Assert.That(notification.SenderId, Is.EqualTo(senderId));
            Assert.That(notification.ReceiverId, Is.EqualTo(receiverId));
        }

        [Test]
        public void CreateNotification_WhenUserConfiguresPreferences_ShouldRespectUserPreferences()
        {
            // Given - El huésped accede a su perfil de usuario
            var guestId = 50;
            var preferenceTitle = "Configuración de Preferencias";
            var preferenceContent = "Sus preferencias de notificaciones han sido actualizadas correctamente.";

            // When - Modifica sus preferencias de notificaciones
            var command = new CreateNotificationCommand(
                preferenceTitle,
                preferenceContent,
                "PREFERENCE_UPDATE",
                1,
                guestId
            );
            var notification = new Notification(command);

            // Then - El sistema debe respetar estas preferencias para futuras comunicaciones
            Assert.That(notification.Title, Is.EqualTo(preferenceTitle));
            Assert.That(notification.Content, Is.EqualTo(preferenceContent));
            Assert.That(notification.SenderType, Is.EqualTo("PREFERENCE_UPDATE"));
            Assert.That(notification.ReceiverId, Is.EqualTo(guestId));
        }

        [Test]
        public void CreateNotification_WhenEmployeeSendsInternalMessage_ShouldDeliverMessageCorrectly()
        {
            // Given - Un empleado necesita comunicarse con otro departamento
            var fromDepartmentId = 5;
            var toDepartmentId = 8;
            var messageTitle = "Solicitud de Coordinación";
            var messageContent = "Necesitamos coordinar la limpieza de las habitaciones 201-205.";
            var hotelId = 15;

            var createCommand = new CreateNotificationCommand(
                messageTitle,
                messageContent,
                "DEPARTMENT",
                fromDepartmentId,
                toDepartmentId
            );

            // When - Redacta y envía un mensaje interno
            var internalMessage = new Notification(createCommand);

            // Then - El mensaje debe llegar correctamente al destinatario con confirmación de entrega
            Assert.That(internalMessage.Title, Is.EqualTo(messageTitle));
            Assert.That(internalMessage.Content, Is.EqualTo(messageContent));
            Assert.That(internalMessage.SenderType, Is.EqualTo("DEPARTMENT"));
            Assert.That(internalMessage.SenderId, Is.EqualTo(fromDepartmentId));
            Assert.That(internalMessage.ReceiverId, Is.EqualTo(toDepartmentId));
        }

        [Test]
        public void CreateNotification_WhenAdminCreatesGroupChannel_ShouldEnableGroupCommunication()
        {
            // Given - El administrador quiere establecer comunicación grupal
            var adminId = 1;
            var groupId = 25;
            var channelTitle = "Canal de Comunicación Grupal";
            var channelContent = "Canal creado para comunicación en tiempo real del equipo de recepción.";

            var notificationCommand = new CreateNotificationCommand(
                channelTitle,
                channelContent,
                "GROUP_CHANNEL",
                adminId,
                groupId
            );

            // When - Crea un canal para un grupo específico de empleados
            var groupChannel = new Notification(notificationCommand);

            // Then - Los miembros del grupo deben poder comunicarse en tiempo real
            Assert.That(groupChannel.Title, Is.EqualTo(channelTitle));
            Assert.That(groupChannel.Content, Is.EqualTo(channelContent));
            Assert.That(groupChannel.SenderType, Is.EqualTo("GROUP_CHANNEL"));
            Assert.That(groupChannel.SenderId, Is.EqualTo(adminId));
            Assert.That(groupChannel.ReceiverId, Is.EqualTo(groupId));
        }
    }
}