using NUnit.Framework;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Authentication;
using System;

namespace SweetManagerIotWebService.Tests.IAMManagementTests
{
    [TestFixture]
    public class UserRoleManagementTests
    {
        #region Scenario: Crear cuenta de administrador

        [Test]
        public void CreateAdminAccount_WithValidData_ShouldCreateAdminSuccessfully()
        {
            // Given - El gerente accede al formulario de registro
            var updateCommand = new UpdateUserCommand(
                Id: 1,
                Name: "Carlos",
                Surname: "Rodriguez",
                Phone: "987654321",
                Email: "carlos.rodriguez@hotel.com",
                State: "ACTIVO"
            );

            // When - Ingresa los datos necesarios para crear una cuenta de administrador
            var admin = new Admin(updateCommand);

            // Then - El sistema debe registrar la cuenta y asignarle permisos administrativos
            Assert.That(admin.Id, Is.EqualTo(1));
            Assert.That(admin.Name, Is.EqualTo("Carlos"));
            Assert.That(admin.Surname, Is.EqualTo("Rodriguez"));
            Assert.That(admin.Email, Is.EqualTo("carlos.rodriguez@hotel.com"));
            Assert.That(admin.Phone, Is.EqualTo("987654321"));
            Assert.That(admin.State, Is.EqualTo("ACTIVO"));
        }

        #endregion

        #region Scenario: Invitar a un nuevo administrador

        [Test]
        public void InviteNewAdmin_WithValidEmail_ShouldCreateAdminInvitation()
        {
            // Given - El gerente accede a la sección de administradores
            var inviteCommand = new UpdateUserCommand(
                Id: 2,
                Name: "Maria",
                Surname: "Lopez",
                Phone: "912345678",
                Email: "maria.lopez@hotel.com",
                State: "PENDIENTE"
            );

            // When - Envía una invitación por correo a un nuevo miembro
            var newAdmin = new Admin(inviteCommand);

            // Then - El nuevo administrador debe recibir un enlace de registro y unirse a la organización
            Assert.That(newAdmin.Id, Is.EqualTo(2));
            Assert.That(newAdmin.Name, Is.EqualTo("Maria"));
            Assert.That(newAdmin.Surname, Is.EqualTo("Lopez"));
            Assert.That(newAdmin.Email, Is.EqualTo("maria.lopez@hotel.com"));
            Assert.That(newAdmin.State, Is.EqualTo("PENDIENTE"));
        }

        #endregion

        #region Scenario: Desvincular administrador de la organización

        [Test]
        public void UnlinkAdminFromOrganization_WithValidAdmin_ShouldUpdateStateToInactive()
        {
            // Given - El propietario del hotel visualiza la lista de administradores
            var existingAdmin = new Admin(1, "Pedro", "Sanchez", "923456789", 
                "pedro.sanchez@hotel.com", "ACTIVO", 2);

            var unlinkCommand = new UpdateUserCommand(
                Id: 1,
                Name: "Pedro",
                Surname: "Sanchez",
                Phone: "923456789",
                Email: "pedro.sanchez@hotel.com",
                State: "INACTIVO"
            );

            // When - Selecciona uno para desvincular
            var unlinkedAdmin = existingAdmin.Update(unlinkCommand);

            // Then - El administrador debe ser removido de la organización y perder acceso
            Assert.That(unlinkedAdmin.Id, Is.EqualTo(1));
            Assert.That(unlinkedAdmin.Name, Is.EqualTo("Pedro"));
            Assert.That(unlinkedAdmin.Surname, Is.EqualTo("Sanchez"));
            Assert.That(unlinkedAdmin.State, Is.EqualTo("INACTIVO"));
            Assert.That(unlinkedAdmin.Email, Is.EqualTo("pedro.sanchez@hotel.com"));
        }

        #endregion

        #region Scenario: Crear perfil de huésped

        [Test]
        public void CreateGuestProfile_WithValidData_ShouldCreateGuestSuccessfully()
        {
            // Given - Un nuevo usuario accede a la aplicación móvil
            var createGuestCommand = new UpdateUserCommand(
                Id: 1,
                Name: "Ana",
                Surname: "Martinez",
                Phone: "934567890",
                Email: "ana.martinez@gmail.com",
                State: "ACTIVO"
            );

            // When - Completa el formulario de registro con sus datos y preferencias
            var guest = new Guest(createGuestCommand);

            // Then - El sistema debe crear un perfil global con sus preferencias guardadas
            Assert.That(guest.Id, Is.EqualTo(1));
            Assert.That(guest.Name, Is.EqualTo("Ana"));
            Assert.That(guest.Surname, Is.EqualTo("Martinez"));
            Assert.That(guest.Email, Is.EqualTo("ana.martinez@gmail.com"));
            Assert.That(guest.Phone, Is.EqualTo("934567890"));
            Assert.That(guest.State, Is.EqualTo("ACTIVO"));
        }

        #endregion

        #region Scenario: Actualizar perfil de propietario

        [Test]
        public void UpdateOwnerProfile_WithValidData_ShouldUpdateOwnerSuccessfully()
        {
            // Given - Un propietario existente quiere actualizar su perfil
            var existingOwner = new Owner(1, "Luis", "Garcia", "945678901", 
                "luis.garcia@owner.com", "ACTIVO", 1);

            var updateCommand = new UpdateUserCommand(
                Id: 1,
                Name: "Luis Alberto",
                Surname: "Garcia Mendez",
                Phone: "945678901",
                Email: "luis.garcia@owner.com",
                State: "ACTIVO"
            );

            // When - Actualiza su información personal
            var updatedOwner = existingOwner.Update(updateCommand);

            // Then - El sistema debe actualizar el perfil correctamente
            Assert.That(updatedOwner.Id, Is.EqualTo(1));
            Assert.That(updatedOwner.Name, Is.EqualTo("Luis Alberto"));
            Assert.That(updatedOwner.Surname, Is.EqualTo("Garcia Mendez"));
            Assert.That(updatedOwner.Email, Is.EqualTo("luis.garcia@owner.com"));
            Assert.That(updatedOwner.State, Is.EqualTo("ACTIVO"));
        }

        #endregion
    }
}