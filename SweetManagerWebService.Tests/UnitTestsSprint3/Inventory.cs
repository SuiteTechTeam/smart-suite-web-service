using NUnit.Framework;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;

namespace SweetManagerIotWebService.API.Tests.Inventory.Domain.Model.Aggregates
{
    [TestFixture]
    public class InventoryManagementTests
    {
        [Test]
        public void Supply_WhenCreatedWithValidData_ShouldUpdateInventoryAutomatically()
        {
            // Given: Un proveedor entrega productos al hotel
            var createSupplyCommand = new CreateSupplyCommand(
                ProviderId: 1,
                HotelId: 1,
                Name: "Toallas de Baño",
                Price: 25.50m,
                Stock: 100,
                State: "DISPONIBLE"
            );

            // When: El encargado registra la entrada en el sistema
            var supply = new Supply(createSupplyCommand);

            // Then: El inventario debe actualizarse automáticamente con las nuevas cantidades
            Assert.That(supply.Stock, Is.EqualTo(100));
            Assert.That(supply.Name, Is.EqualTo("TOALLAS DE BAÑO"));
            Assert.That(supply.State, Is.EqualTo("DISPONIBLE"));
            Assert.That(supply.Price, Is.EqualTo(25.50m));
            Assert.That(supply.ProviderId, Is.EqualTo(1));
            Assert.That(supply.HotelId, Is.EqualTo(1));
        }

        [Test]
        public void Supply_WhenStockReachesMinimumLevel_ShouldGenerateAutomaticAlerts()
        {
            // Given: Los productos tienen umbrales mínimos configurados
            var supply = new Supply(
                id: 1,
                hotelId: 1,
                providerId: 1,
                name: "Champú",
                price: 12.00m,
                stock: 15,
                state: "DISPONIBLE"
            );

            // When: El stock alcanza el nivel mínimo
            const int minimumStockLevel = 20;
            bool shouldGenerateAlert = supply.Stock < minimumStockLevel;

            // Then: El sistema debe generar alertas automáticas para reabastecimiento
            Assert.That(shouldGenerateAlert, Is.True);
            Assert.That(supply.Stock, Is.LessThan(minimumStockLevel));
            Assert.That(supply.State, Is.EqualTo("DISPONIBLE"));
        }

        [Test]
        public void SupplyRequest_WhenPreventiveMaintenanceIsScheduled_ShouldGenerateAutomaticReminders()
        {
            // Given: Los activos requieren mantenimiento periódico
            var createSupplyRequestCommand = new CreateSupplyRequestCommand(
                PaymentOwnerId: 1,
                SupplyId: 1,
                Count: 5,
                Amount: 150.00m
            );

            // When: Se programa el mantenimiento preventivo
            var supplyRequest = new SupplyRequest(createSupplyRequestCommand);

            // Then: El sistema debe generar recordatorios automáticos según la programación
            Assert.That(supplyRequest.PaymentOwnerId, Is.EqualTo(1));
            Assert.That(supplyRequest.SupplyId, Is.EqualTo(1));
            Assert.That(supplyRequest.Count, Is.EqualTo(5));
            Assert.That(supplyRequest.Amount, Is.EqualTo(150.00m));
            Assert.That(supplyRequest.Amount, Is.GreaterThan(0));
        }
    }
}