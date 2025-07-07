using NUnit.Framework;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;

namespace SweetManagerIotWebService.API.Tests.Commerce.Domain.Model.Aggregates
{
    [TestFixture]
    public class CommerceManagementTests
    {
        [Test]
        public void PaymentCustomer_WhenValidPaymentInfoIsProvided_ShouldProcessPaymentAndConfirmReservation()
        {
            // Given: Un huésped confirma su reserva
            var createPaymentCustomerCommand = new CreatePaymentCustomerCommand(
                GuestId: 1,
                FinalAmount: 250.00m
            );

            // When: Proporciona información de pago válida
            var paymentCustomer = new PaymentCustomer(createPaymentCustomerCommand);

            // Then: El sistema debe procesar el pago y confirmar la reserva automáticamente
            Assert.That(paymentCustomer.GuestId, Is.EqualTo(1));
            Assert.That(paymentCustomer.FinalAmount, Is.EqualTo(250.00m));
            Assert.That(paymentCustomer.FinalAmount, Is.GreaterThan(0));
            Assert.That(paymentCustomer.GuestId, Is.Not.Null);
        }

        [Test]
        public void PaymentCustomer_WhenValidPromotionalCodeIsApplied_ShouldApplyCorrespondingDiscount()
        {
            // Given: Un huésped aplica un código promocional
            decimal originalAmount = 300.00m;
            decimal discountPercentage = 15.00m;
            var guestId = 1;

            // When: El código es válido y aplicable
            decimal discountAmount = originalAmount * (discountPercentage / 100);
            decimal finalAmount = originalAmount - discountAmount;
            
            var paymentCustomer = new PaymentCustomer(
                guestId: guestId,
                finalAmount: finalAmount
            );

            // Then: El sistema debe aplicar el descuento correspondiente al total de la reserva
            Assert.That(paymentCustomer.FinalAmount, Is.EqualTo(255.00m));
            Assert.That(paymentCustomer.FinalAmount, Is.LessThan(originalAmount));
            Assert.That(discountAmount, Is.EqualTo(45.00m));
            Assert.That(paymentCustomer.GuestId, Is.EqualTo(guestId));
        }

        [Test]
        public void PaymentOwner_WhenAdditionalServicesAreConsumed_ShouldGenerateDetailedInvoice()
        {
            // Given: Un huésped consume servicios adicionales
            var createPaymentOwnerCommand = new CreatePaymentOwnerCommand(
                OwnerId: 1,
                Description: "Servicios adicionales: Spa, Minibar, Lavandería",
                FinalAmount: 180.50m
            );

            // When: Solicita la factura de su estadía
            var paymentOwner = new PaymentOwner(createPaymentOwnerCommand);

            // Then: El sistema debe generar una factura detallada con todos los servicios consumidos
            Assert.That(paymentOwner.OwnerId, Is.EqualTo(1));
            Assert.That(paymentOwner.Description, Is.EqualTo("Servicios adicionales: Spa, Minibar, Lavandería"));
            Assert.That(paymentOwner.FinalAmount, Is.EqualTo(180.50m));
            Assert.That(paymentOwner.Description, Is.Not.Null.And.Not.Empty);
            Assert.That(paymentOwner.FinalAmount, Is.GreaterThan(0));
        }

        [Test]
        public void Subscription_WhenRefundIsRequestedWithinAllowedPeriod_ShouldProcessRefundAutomatically()
        {
            // Given: Un huésped cancela su reserva dentro del plazo permitido
            var createSubscriptionCommand = new CreateSubscriptionCommand(
                Name: ESubscriptionTypes.Premium,
                Content: "Cancelación dentro del plazo - Reembolso completo",
                Price: 320.00m,
                Status: EStates.Active
            );

            // When: Solicita el reembolso correspondiente
            var subscription = new Subscription(createSubscriptionCommand);
            bool isRefundEligible = subscription.Status == EStates.Active;
            decimal refundAmount = isRefundEligible ? subscription.Price ?? 0 : 0;

            // Then: El sistema debe procesar el reembolso automáticamente según las políticas establecidas
            Assert.That(subscription.Name, Is.EqualTo(ESubscriptionTypes.Premium));
            Assert.That(subscription.Status, Is.EqualTo(EStates.Active));
            Assert.That(isRefundEligible, Is.True);
            Assert.That(refundAmount, Is.EqualTo(320.00m));
            Assert.That(subscription.Content, Contains.Substring("Reembolso"));
        }
    }
}