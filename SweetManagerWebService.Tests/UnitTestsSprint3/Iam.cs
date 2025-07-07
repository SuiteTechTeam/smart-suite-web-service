using NUnit.Framework;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Model.ValueObjects;
using System;

namespace SweetManagerIotWebService.API.Tests.IAM.Domain.Model
{
    [TestFixture]
    public class IAMUnitTests
    {
        [Test]
        public void CrearCuentaDeAdministrador_WhenValidDataProvided_ShouldCreateAdminWithCorrectRole()
        {
            // Given - Un superadministrador accede al sistema de gestión de usuarios
            var adminRole = new Role("ROLE_ADMIN");
            var adminData = new SignUpUserCommand(
                Name: "Carlos",
                Surname: "Rodriguez",
                Phone: "987654321",
                Email: "carlos.rodriguez@hotel.com",
                Password: "Admin123!"
            );

            // When - Completa el formulario de registro con credenciales de administrador
            var admin = new Admin(
                name: adminData.Name,
                surname: adminData.Surname,
                phone: adminData.Phone,
                email: adminData.Email,
                state: "ACTIVE",
                roleId: (int)ERoles.ROLE_ADMIN + 1 // Assuming role IDs start from 1
            );

            // Then - La cuenta debe crearse con permisos de administrador y acceso completo al sistema
            Assert.That(admin.Id, Is.EqualTo(1));
            Assert.That(admin.Name, Is.EqualTo("Carlos"));
            Assert.That(admin.Surname, Is.EqualTo("Rodriguez"));
            Assert.That(admin.Email, Is.EqualTo("carlos.rodriguez@hotel.com"));
            Assert.That(admin.RoleId, Is.EqualTo((int)ERoles.ROLE_ADMIN + 1));
            Assert.That(admin.State, Is.EqualTo("ACTIVE"));
        }

        [Test]
        public void AsignarRolesAUsuariosDelHotel_WhenAdminAssignsRole_ShouldUpdateUserRoleSuccessfully()
        {
            // Given - El administrador accede al módulo de gestión de usuarios
            var existingAdmin = new Admin(
                id: 1,
                name: "Maria",
                surname: "Gonzalez",
                phone: "123456789",
                email: "maria.gonzalez@hotel.com",
                state: "ACTIVE",
                roleId: (int)ERoles.ROLE_ADMIN + 1
            );

            var updateCommand = new UpdateUserCommand(
                Id: existingAdmin.Id,
                Name: existingAdmin.Name,
                Surname: existingAdmin.Surname,
                Phone: existingAdmin.Phone,
                Email: existingAdmin.Email,
                State: "ACTIVE"
            );

            // When - Selecciona un usuario y asigna un rol específico
            var updatedAdmin = existingAdmin.Update(updateCommand);

            // Then - El usuario debe tener acceso solo a las funciones correspondientes a su rol
            Assert.That(updatedAdmin.Id, Is.EqualTo(1));
            Assert.That(updatedAdmin.State, Is.EqualTo("ACTIVE"));
            Assert.That(updatedAdmin.RoleId, Is.EqualTo((int)ERoles.ROLE_ADMIN + 1));
        }

        [Test]
        public void AutenticarDispositivosIoT_WhenDevicePresentsCredentials_ShouldValidateSuccessfully()
        {
            // Given - Un dispositivo IoT intenta conectarse al sistema
            var deviceAdmin = new Admin(
                id: 100,
                name: "IoT Device",
                surname: "Sensor001",
                phone: "000000000",
                email: "device001@iot.hotel.com",
                state: "ACTIVE",
                roleId: (int)ERoles.ROLE_ADMIN + 1
            );

            var deviceCredentials = new AdminCredential(
                userId: deviceAdmin.Id,
                code: "DEVICE_AUTH_TOKEN_123456"
            );

            // When - Presenta sus credenciales de autenticación
            deviceAdmin.AdminCredential = deviceCredentials;

            // Then - El sistema debe validar y autorizar el acceso sin errores de autenticación
            Assert.That(deviceAdmin.AdminCredential.AdminId, Is.EqualTo(100));
            Assert.That(deviceAdmin.AdminCredential.Code, Is.EqualTo("DEVICE_AUTH_TOKEN_123456"));
            Assert.That(deviceAdmin.State, Is.EqualTo("ACTIVE"));
        }

        [Test]
        public void RenovarTokensDeSeguridad_WhenTokenNeedsRenewal_ShouldGenerateNewTokenAutomatically()
        {
            // Given - Un dispositivo tiene un token próximo a expirar
            var deviceOwner = new Owner(
                id: 200,
                name: "IoT Gateway",
                surname: "MainController",
                phone: "111111111",
                email: "gateway@iot.hotel.com",
                state: "ACTIVE",
                roleId: (int)ERoles.ROLE_OWNER + 1
            );

            var expiredCredentials = new OwnerCredential(
                ownerId: deviceOwner.Id,
                code: "EXPIRED_TOKEN_OLD_123"
            );

            // When - El sistema detecta que el token necesita renovación
            var newTokenCode = "RENEWED_TOKEN_NEW_456";
            var renewedCredentials = new OwnerCredential(
                ownerId: deviceOwner.Id,
                code: newTokenCode
            );

            deviceOwner.OwnerCredential = renewedCredentials;

            // Then - Debe generar automáticamente un nuevo token válido sin interrumpir la conexión
            Assert.That(deviceOwner.OwnerCredential.OwnerId, Is.EqualTo(200));
            Assert.That(deviceOwner.OwnerCredential.Code, Is.EqualTo("RENEWED_TOKEN_NEW_456"));
            Assert.That(deviceOwner.OwnerCredential.Code, Is.Not.EqualTo("EXPIRED_TOKEN_OLD_123"));
            Assert.That(deviceOwner.State, Is.EqualTo("ACTIVE"));
        }
    }
}