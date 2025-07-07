using NUnit.Framework;
using SweetManagerIotWebService.API;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Commands.Booking;
using System;

namespace SweetManagerIotWebService.Tests.ReservationManagementTests
{
    [TestFixture]
    public class BookingManagementTests
    {
        #region Scenario: Crear una nueva reserva

        [Test]
        public void CreateBooking_WithValidData_ShouldCreateBookingSuccessfully()
        {
            // Given - El administrador accede al módulo de reservas
            var createCommand = new CreateBookingCommand(
                PaymentCustomerId: 1,
                RoomId: 101,
                Description: "Reserva para huésped de negocios",
                StartDate: new DateTime(2025, 8, 15),
                FinalDate: new DateTime(2025, 8, 20),
                PriceRoom: 150.00m,
                NightCount: 5,
                Amount: 750.00m,
                State: "ACTIVA",
                PreferenceId: 1
            );

            // When - Completa el formulario de nueva reserva con los datos del huésped y habitación
            var booking = new Booking(createCommand);

            // Then - La reserva debe guardarse correctamente y mostrarse en la lista de reservas activas
            Assert.That(booking.PaymentCustomerId, Is.EqualTo(1));
            Assert.That(booking.RoomId, Is.EqualTo(101));
            Assert.That(booking.Description, Is.EqualTo("Reserva para huésped de negocios"));
            Assert.That(booking.StartDate, Is.EqualTo(new DateTime(2025, 8, 15)));
            Assert.That(booking.FinalDate, Is.EqualTo(new DateTime(2025, 8, 20)));
            Assert.That(booking.State, Is.EqualTo("ACTIVA"));
        }

        #endregion

        #region Scenario: Cancelar una reserva activa

        [Test]
        public void CancelBooking_WithActiveReservation_ShouldUpdateStateToCancelled()
        {
            // Given - El huésped accede a su lista de reservas
            var updateStateCommand = new UpdateBookingStateCommand(
                Id: 1,
                State: "CANCELADA"
            );

            // When - Selecciona la opción para cancelar una reserva vigente
            var booking = new Booking(updateStateCommand);

            // Then - La reserva debe marcarse como cancelada
            Assert.That(booking.Id, Is.EqualTo(1));
            Assert.That(booking.State, Is.EqualTo("CANCELADA"));
        }

        #endregion

        #region Scenario: Editar fechas de una reserva

        [Test]
        public void EditBookingEndDate_WithValidData_ShouldUpdateEndDateSuccessfully()
        {
            // Given - El administrador selecciona una reserva existente
            var updateEndDateCommand = new UpdateBookingEndDateCommand(
                Id: 1,
                EndDate: new DateTime(2025, 8, 25)
            );

            // When - Modifica la fecha de ingreso y/o salida
            var booking = new Booking(updateEndDateCommand);

            // Then - La reserva debe actualizarse con las nuevas fechas sin perder la información anterior
            Assert.That(booking.Id, Is.EqualTo(1));
            Assert.That(booking.FinalDate, Is.EqualTo(new DateTime(2025, 8, 25)));
        }

        #endregion
    }
}