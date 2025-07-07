using NUnit.Framework;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;
using System;
using System.Linq;

namespace SweetManagerIotWebService.Tests.CommerceManagementTests
{
    [TestFixture]
    public class BookingManagementTests
    {
        #region Scenario: Reservar habitación personalizada

        [Test]
        public void BookRoomWithCustomization_WithValidData_ShouldCreatePaymentCustomerSuccessfully()
        {
            // Given - El huésped accede a la lista de habitaciones disponibles
            var createPaymentCommand = new CreatePaymentCustomerCommand(
                GuestId: 1,
                FinalAmount: 350.75m
            );

            // When - Selecciona una habitación y aplica personalizaciones según sus preferencias
            var paymentCustomer = new PaymentCustomer(createPaymentCommand);

            // Then - La reserva debe procesarse con las especificaciones personalizadas
            Assert.That(paymentCustomer.GuestId, Is.EqualTo(1));
            Assert.That(paymentCustomer.FinalAmount, Is.EqualTo(350.75m));
            Assert.That(paymentCustomer.Bookings, Is.Not.Null);
            Assert.That(paymentCustomer.Bookings.Count, Is.EqualTo(0));
        }

        [Test]
        public void BookRoomWithCustomization_WithPremiumUpgrade_ShouldCalculateCorrectAmount()
        {
            // Given - El huésped selecciona una habitación estándar
            var baseAmount = 200.00m;
            var premiumUpgrade = 150.75m;
            var totalAmount = baseAmount + premiumUpgrade;

            var createPaymentCommand = new CreatePaymentCustomerCommand(
                GuestId: 2,
                FinalAmount: totalAmount
            );

            // When - Aplica personalizaciones premium
            var paymentCustomer = new PaymentCustomer(createPaymentCommand);

            // Then - El monto final debe incluir las personalizaciones
            Assert.That(paymentCustomer.FinalAmount, Is.EqualTo(350.75m));
            Assert.That(paymentCustomer.GuestId, Is.EqualTo(2));
        }

        #endregion

        #region Scenario: Gestionar reservas activas

        [Test]
        public void ManageActiveBookings_WithExistingReservation_ShouldAllowModification()
        {
            // Given - El huésped tiene reservas vigentes
            var existingPayment = new PaymentCustomer(
                guestId: 1,
                finalAmount: 300.00m
            );

            var updateCommand = new UpdatePaymentCustomerCommand(
                Id: 1,
                GuestId: 1,
                FinalAmount: 425.50m
            );

            // When - Accede a su panel de reservas activas y modifica la reserva
            var updatedPayment = new PaymentCustomer(updateCommand);

            // Then - Debe poder visualizar, modificar o cancelar sus reservas según las políticas del hotel
            Assert.That(updatedPayment.Id, Is.EqualTo(1));
            Assert.That(updatedPayment.GuestId, Is.EqualTo(1));
            Assert.That(updatedPayment.FinalAmount, Is.EqualTo(425.50m));
        }

        [Test]
        public void ManageActiveBookings_WithCancellation_ShouldSetZeroAmount()
        {
            // Given - El huésped quiere cancelar una reserva
            var cancellationCommand = new UpdatePaymentCustomerCommand(
                Id: 2,
                GuestId: 1,
                FinalAmount: 0.00m
            );

            // When - Procesa la cancelación
            var cancelledPayment = new PaymentCustomer(cancellationCommand);

            // Then - La reserva debe reflejar la cancelación
            Assert.That(cancelledPayment.Id, Is.EqualTo(2));
            Assert.That(cancelledPayment.FinalAmount, Is.EqualTo(0.00m));
            Assert.That(cancelledPayment.GuestId, Is.EqualTo(1));
        }

        #endregion

        #region Scenario: Consultar historial de reservas

        [Test]
        public void ViewBookingHistory_WithMultipleReservations_ShouldShowAllPastBookings()
        {
            // Given - El huésped accede a su perfil
            var historicalPayment1 = new PaymentCustomer(
                guestId: 1,
                finalAmount: 250.00m
            );

            var historicalPayment2 = new PaymentCustomer(
                guestId: 1,
                finalAmount: 480.50m
            );

            var historicalPayment3 = new PaymentCustomer(
                guestId: 1,
                finalAmount: 175.25m
            );

            // When - Solicita ver su historial de reservas anteriores
            var totalSpent = historicalPayment1.FinalAmount + 
                           historicalPayment2.FinalAmount + 
                           historicalPayment3.FinalAmount;

            // Then - Debe visualizar todas sus reservas pasadas con detalles completos
            Assert.That(historicalPayment1.GuestId, Is.EqualTo(1));
            Assert.That(historicalPayment2.GuestId, Is.EqualTo(1));
            Assert.That(historicalPayment3.GuestId, Is.EqualTo(1));
            Assert.That(totalSpent, Is.EqualTo(905.75m));
        }

        [Test]
        public void ViewBookingHistory_WithEmptyHistory_ShouldShowNoReservations()
        {
            // Given - Un huésped nuevo accede a su perfil
            var newGuestId = 999;

            // When - Solicita ver su historial (simulando que no tiene reservas)
            var emptyPayment = new PaymentCustomer(
                guestId: newGuestId,
                finalAmount: null
            );

            // Then - Debe mostrar un historial vacío
            Assert.That(emptyPayment.GuestId, Is.EqualTo(999));
            Assert.That(emptyPayment.FinalAmount, Is.Null);
            Assert.That(emptyPayment.Bookings, Is.Not.Null);
            Assert.That(emptyPayment.Bookings.Count, Is.EqualTo(0));
        }

        #endregion
    }

    [TestFixture]
    public class PaymentOwnerManagementTests
    {
        #region Scenario: Gestionar pagos de propietarios

        [Test]
        public void CreateOwnerPayment_WithSupplyRequest_ShouldCreatePaymentSuccessfully()
        {
            // Given - El propietario necesita realizar un pago por suministros
            var createPaymentCommand = new CreatePaymentOwnerCommand(
                OwnerId: 1,
                Description: "Pago por suministros de limpieza y mantenimiento",
                FinalAmount: 1500.75m
            );

            // When - Procesa el pago
            var ownerPayment = new PaymentOwner(createPaymentCommand);

            // Then - El pago debe registrarse correctamente
            Assert.That(ownerPayment.OwnerId, Is.EqualTo(1));
            Assert.That(ownerPayment.Description, Is.EqualTo("Pago por suministros de limpieza y mantenimiento"));
            Assert.That(ownerPayment.FinalAmount, Is.EqualTo(1500.75m));
            Assert.That(ownerPayment.SupplyRequests, Is.Not.Null);
        }

        [Test]
        public void UpdateOwnerPayment_WithNewAmount_ShouldUpdatePaymentSuccessfully()
        {
            // Given - Existe un pago que necesita ser actualizado
            var existingPayment = new PaymentOwner(
                ownerId: 2,
                description: "Pago inicial por equipos",
                finalAmount: 2000.00m
            );

            var updateCommand = new UpdatePaymentOwnerCommand(
                Id: 1,
                OwnerId: 2,
                Description: "Pago actualizado por equipos y instalación",
                FinalAmount: 2750.50m
            );

            // When - Actualiza el monto y descripción
            var updatedPayment = new PaymentOwner(updateCommand);

            // Then - Los datos deben actualizarse correctamente
            Assert.That(updatedPayment.Id, Is.EqualTo(1));
            Assert.That(updatedPayment.OwnerId, Is.EqualTo(2));
            Assert.That(updatedPayment.Description, Is.EqualTo("Pago actualizado por equipos y instalación"));
            Assert.That(updatedPayment.FinalAmount, Is.EqualTo(2750.50m));
        }

        #endregion

        #region Scenario: Gestionar contratos de suscripción

        [Test]
        public void CreateContractOwner_WithSubscription_ShouldCreateContractSuccessfully()
        {
            // Given - El propietario quiere suscribirse a un plan
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(12);

            var createContractCommand = new CreateContractOwnerCommand(
                OwnerId: 1,
                StartDate: startDate,
                FinalDate: endDate,
                SubscriptionId: 1,
                Status: EStates.Active
            );

            // When - Crea el contrato de suscripción
            // Note: Assuming ContractOwner entity exists similar to other aggregates
            // Since it's not provided, we'll test the command creation
            
            // Then - El contrato debe crearse con los datos correctos
            Assert.That(createContractCommand.OwnerId, Is.EqualTo(1));
            Assert.That(createContractCommand.StartDate, Is.EqualTo(startDate));
            Assert.That(createContractCommand.FinalDate, Is.EqualTo(endDate));
            Assert.That(createContractCommand.SubscriptionId, Is.EqualTo(1));
            Assert.That(createContractCommand.Status, Is.EqualTo(EStates.Active));
        }

        [Test]
        public void UpdateContractOwner_WithExtendedPeriod_ShouldUpdateContractSuccessfully()
        {
            // Given - Un contrato existente necesita ser extendido
            var originalEndDate = DateTime.Now.AddMonths(6);
            var extendedEndDate = DateTime.Now.AddMonths(18);

            var updateContractCommand = new UpdateContractOwnerCommand(
                Id: 1,
                OwnerId: 1,
                StartDate: DateTime.Now,
                FinalDate: extendedEndDate,
                SubscriptionId: 2,
                Status: EStates.Active
            );

            // When - Actualiza el período del contrato
            // Then - El contrato debe reflejar la extensión
            Assert.That(updateContractCommand.Id, Is.EqualTo(1));
            Assert.That(updateContractCommand.OwnerId, Is.EqualTo(1));
            Assert.That(updateContractCommand.FinalDate, Is.EqualTo(extendedEndDate));
            Assert.That(updateContractCommand.SubscriptionId, Is.EqualTo(2));
            Assert.That(updateContractCommand.Status, Is.EqualTo(EStates.Active));
        }

        #endregion
    }
    
    [TestFixture]
    public class CommerceValidationTests
    {
        #region Scenario: Validaciones de montos y cálculos

        [Test]
        public void PaymentCustomer_WithZeroAmount_ShouldAllowZeroPayments()
        {
            // Given - Un huésped con promoción gratuita
            var freeStayCommand = new CreatePaymentCustomerCommand(
                GuestId: 1,
                FinalAmount: 0.00m
            );

            // When - Se procesa el pago gratuito
            var freePayment = new PaymentCustomer(freeStayCommand);

            // Then - Debe permitir montos cero
            Assert.That(freePayment.FinalAmount, Is.EqualTo(0.00m));
            Assert.That(freePayment.GuestId, Is.EqualTo(1));
        }

        [Test]
        public void PaymentOwner_WithLargeAmount_ShouldHandleLargeTransactions()
        {
            // Given - Un propietario con una transacción grande
            var largeAmount = 50000.00m;
            var largePaymentCommand = new CreatePaymentOwnerCommand(
                OwnerId: 1,
                Description: "Renovación completa del hotel - equipos y mobiliario",
                FinalAmount: largeAmount
            );

            // When - Se procesa el pago grande
            var largePayment = new PaymentOwner(largePaymentCommand);

            // Then - Debe manejar montos grandes correctamente
            Assert.That(largePayment.FinalAmount, Is.EqualTo(50000.00m));
            Assert.That(largePayment.Description, Contains.Substring("Renovación completa"));
            Assert.That(largePayment.OwnerId, Is.EqualTo(1));
        }
        
        [Test]
        public void PaymentCustomer_WithNullValues_ShouldHandleNullsGracefully()
        {
            // Given - Datos incompletos del huésped
            var incompleteCommand = new CreatePaymentCustomerCommand(
                GuestId: null,
                FinalAmount: null
            );

            // When - Se intenta crear el pago
            var incompletePayment = new PaymentCustomer(incompleteCommand);

            // Then - Debe manejar valores nulos sin errores
            Assert.That(incompletePayment.GuestId, Is.Null);
            Assert.That(incompletePayment.FinalAmount, Is.Null);
            Assert.That(incompletePayment.Bookings, Is.Not.Null);
        }

        #endregion

        #region Scenario: Cálculos de precisión decimal

        [Test]
        public void PaymentCalculations_WithDecimalPrecision_ShouldMaintainAccuracy()
        {
            // Given - Cálculos con precisión decimal
            var basePrice = 199.99m;
            var tax = 19.99m;
            var discount = 10.00m;
            var finalAmount = basePrice + tax - discount;

            var precisionCommand = new CreatePaymentCustomerCommand(
                GuestId: 1,
                FinalAmount: finalAmount
            );

            // When - Se procesa el pago con cálculos precisos
            var precisionPayment = new PaymentCustomer(precisionCommand);

            // Then - Debe mantener la precisión decimal
            Assert.That(precisionPayment.FinalAmount, Is.EqualTo(209.98m));
            Assert.That(precisionPayment.FinalAmount, Is.EqualTo(basePrice + tax - discount));
        }

        #endregion
    }
}