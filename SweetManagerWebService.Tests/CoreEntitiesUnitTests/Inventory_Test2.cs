using NUnit.Framework;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Commands;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Commands;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SweetManagerIotWebService.Tests.InventoryTests
{
    [TestFixture]
    public class InventoryManagementTests
    {
        #region Scenario: Revisar stock de recursos del hotel

        [Test]
        public void ReviewHotelResourceStock_WhenOwnerAccessesInventoryModule_ShouldDisplayCurrentStockAndState()
        {
            // Given - El propietario del hotel accede al módulo de inventario
            var supplies = new List<Supply>
            {
                new Supply(1, 1, 1, "Toallas", 25.50m, 50, "AVAILABLE"),
                new Supply(2, 1, 1, "Jabón", 5.75m, 100, "AVAILABLE"),
                new Supply(3, 1, 2, "Shampoo", 12.30m, 0, "OUT_OF_STOCK"),
                new Supply(4, 1, 1, "Papel higiénico", 8.90m, 25, "LOW_STOCK")
            };

            // When - Consulta el stock actual de recursos
            var inventoryReport = GetInventoryReport(supplies);

            // Then - Debe visualizar las cantidades disponibles y el estado de cada recurso
            Assert.That(inventoryReport.Count, Is.EqualTo(4));
            
            var towels = inventoryReport.First(s => s.Name == "TOALLAS");
            Assert.That(towels.Stock, Is.EqualTo(50));
            Assert.That(towels.State, Is.EqualTo("AVAILABLE"));
            Assert.That(towels.Price, Is.EqualTo(25.50m));

            var shampoo = inventoryReport.First(s => s.Name == "SHAMPOO");
            Assert.That(shampoo.Stock, Is.EqualTo(0));
            Assert.That(shampoo.State, Is.EqualTo("OUT_OF_STOCK"));

            var toiletPaper = inventoryReport.First(s => s.Name == "PAPEL HIGIÉNICO");
            Assert.That(toiletPaper.Stock, Is.EqualTo(25));
            Assert.That(toiletPaper.State, Is.EqualTo("LOW_STOCK"));
        }

        [Test]
        public void ReviewResourceStock_WhenFilteringByAvailableItems_ShouldShowOnlyAvailableResources()
        {
            // Given - Existe inventario con diferentes estados
            var supplies = new List<Supply>
            {
                new Supply(1, 1, 1, "Producto A", 10.00m, 30, "AVAILABLE"),
                new Supply(2, 1, 1, "Producto B", 15.00m, 0, "OUT_OF_STOCK"),
                new Supply(3, 1, 1, "Producto C", 20.00m, 50, "AVAILABLE")
            };

            // When - Se filtran solo los recursos disponibles
            var availableSupplies = supplies.Where(s => s.State == "AVAILABLE").ToList();

            // Then - Solo debe mostrar recursos disponibles
            Assert.That(availableSupplies.Count, Is.EqualTo(2));
            Assert.That(availableSupplies.All(s => s.State == "AVAILABLE"), Is.True);
            Assert.That(availableSupplies.All(s => s.Stock > 0), Is.True);
        }

        [Test]
        public void ReviewResourceStock_WhenCheckingLowStockItems_ShouldIdentifyItemsNeedingRestock()
        {
            // Given - Inventario con diferentes niveles de stock
            var supplies = new List<Supply>
            {
                new Supply(1, 1, 1, "Item Normal", 10.00m, 100, "AVAILABLE"),
                new Supply(2, 1, 1, "Item Bajo", 15.00m, 5, "LOW_STOCK"),
                new Supply(3, 1, 1, "Item Crítico", 20.00m, 1, "LOW_STOCK")
            };

            // When - Se revisan items con stock bajo
            var lowStockItems = supplies.Where(s => s.State == "LOW_STOCK" || s.Stock <= 10).ToList();

            // Then - Debe identificar items que necesitan reabastecimiento
            Assert.That(lowStockItems.Count, Is.EqualTo(2));
            Assert.That(lowStockItems.Any(s => s.Name == "ITEM BAJO"), Is.True);
            Assert.That(lowStockItems.Any(s => s.Name == "ITEM CRÍTICO"), Is.True);
        }

        #endregion

        #region Scenario: Registrar nuevo proveedor

        [Test]
        public void RegisterNewProvider_WhenAdminEntersProviderInformation_ShouldSaveProviderInSystem()
        {
            // Given - El administrador accede al módulo de proveedores
            var createProviderCommand = new CreateProviderCommand(
                Name: "Distribuidora Hotel Supplies SAC",
                Email: "ventas@hotelsupplies.com",
                Phone: "+51-1-555-0123",
                State: "Active"
            );

            // When - Ingresa la información del proveedor (nombre, contacto, productos)
            var provider = new Provider(createProviderCommand);

            // Then - El proveedor debe guardarse en el sistema para futuras referencias
            Assert.That(provider.Name, Is.EqualTo("Distribuidora Hotel Supplies SAC"));
            Assert.That(provider.Email, Is.EqualTo("ventas@hotelsupplies.com"));
            Assert.That(provider.Phone, Is.EqualTo("+51-1-555-0123"));
            Assert.That(provider.State.ToString(), Is.EqualTo("Active"));
            Assert.That(provider.IsActive(), Is.True);
        }

        [Test]
        public void RegisterProvider_WithContactInformationAndProducts_ShouldCreateProviderWithAssociatedSupplies()
        {
            // Given - Nuevo proveedor con productos asociados
            var provider = new Provider("Proveedor Textil Lima", "contacto@textillima.com", "+51-1-999-8888", "Active");
            
            var supplies = new List<Supply>
            {
                new Supply(new CreateSupplyCommand(ProviderId: 1, HotelId: 1, Name: "Toallas de baño", Price: 35.00m, Stock: 100, State: "AVAILABLE")),
                new Supply(new CreateSupplyCommand(ProviderId: 1, HotelId: 1, Name: "Sábanas", Price: 45.00m, Stock: 80, State: "AVAILABLE")),
                new Supply(new CreateSupplyCommand(ProviderId: 1, HotelId: 1, Name: "Almohadas", Price: 25.00m, Stock: 60, State: "AVAILABLE"))
            };

            // When - Se asocian los productos al proveedor
            foreach (var supply in supplies)
            {
                provider.Supplies.Add(supply);
            }

            // Then - El proveedor debe tener los productos asociados
            Assert.That(provider.Supplies.Count, Is.EqualTo(3));
            Assert.That(provider.Supplies.Any(s => s.Name == "TOALLAS DE BAÑO"), Is.True);
            Assert.That(provider.Supplies.Any(s => s.Name == "SÁBANAS"), Is.True);
            Assert.That(provider.Supplies.Any(s => s.Name == "ALMOHADAS"), Is.True);
        }

        [Test]
        public void RegisterProvider_WhenSavingForFutureReference_ShouldMaintainProviderData()
        {
            // Given - Información completa del proveedor
            var providerData = new
            {
                Name = "Equipos Hoteleros del Perú",
                Email = "info@equiposhoteleros.pe",
                Phone = "+51-1-444-5555",
                ContactPerson = "Juan Pérez",
                Address = "Av. Industrial 456, Lima"
            };

            // When - Se registra el proveedor para futuras referencias
            var provider = new Provider(providerData.Name, providerData.Email, providerData.Phone, "Active");

            // Then - Los datos deben persistir para futuras consultas
            Assert.That(provider.Name, Is.EqualTo(providerData.Name));
            Assert.That(provider.Email, Is.EqualTo(providerData.Email));
            Assert.That(provider.Phone, Is.EqualTo(providerData.Phone));
            Assert.That(provider.IsActive(), Is.True);
            // El proveedor debe estar disponible para futuras órdenes de compra
            Assert.That(provider.State.ToString(), Is.EqualTo("Active"));
        }

        #endregion

        #region Scenario: Solicitar dispositivos IoT

        [Test]
        public void RequestIoTDevices_WhenOwnerIdentifiesNeedForMoreDevices_ShouldRegisterAndSendRequest()
        {
            // Given - El propietario identifica la necesidad de más dispositivos
            var hotelId = 1;
            var deviceRequests = new List<DeviceRequest>
            {
                new DeviceRequest { DeviceType = "RFID_CARD", Quantity = 10, Justification = "Expansión de habitaciones" },
                new DeviceRequest { DeviceType = "SMART_LOCK", Quantity = 5, Justification = "Actualización de seguridad" },
                new DeviceRequest { DeviceType = "TEMPERATURE_SENSOR", Quantity = 15, Justification = "Control de climatización" }
            };

            // When - Realiza una solicitud a través del sistema
            var iotRequest = new IoTDeviceRequest(hotelId, deviceRequests, "Solicitud trimestral de dispositivos");

            // Then - La solicitud debe registrarse y enviarse al departamento correspondiente
            Assert.That(iotRequest.HotelId, Is.EqualTo(hotelId));
            Assert.That(iotRequest.DeviceRequests.Count, Is.EqualTo(3));
            Assert.That(iotRequest.Status, Is.EqualTo("PENDING"));
            Assert.That(iotRequest.Description, Is.EqualTo("Solicitud trimestral de dispositivos"));
            
            // Verificar que se puede enviar al departamento
            Assert.That(iotRequest.CanBeSentToDepartment(), Is.True);
        }

        [Test]
        public void RequestIoTDevices_WhenRequestIsSubmitted_ShouldTrackRequestStatus()
        {
            // Given - Una solicitud de dispositivos IoT
            var deviceRequest = new DeviceRequest { DeviceType = "SMART_SENSOR", Quantity = 8, Justification = "Monitoreo ambiental" };
            var iotRequest = new IoTDeviceRequest(1, new List<DeviceRequest> { deviceRequest }, "Solicitud de sensores");

            // When - La solicitud se procesa
            iotRequest.SubmitRequest();

            // Then - El estado debe actualizarse correctamente
            Assert.That(iotRequest.Status, Is.EqualTo("SUBMITTED"));
            Assert.That(iotRequest.SubmissionDate, Is.Not.Null);
            
            // When - El departamento recibe la solicitud
            iotRequest.MarkAsReceived();
            
            // Then - El estado debe reflejar que fue recibida
            Assert.That(iotRequest.Status, Is.EqualTo("RECEIVED"));
            Assert.That(iotRequest.ReceivedDate, Is.Not.Null);
        }

        #endregion

        #region Additional Command Tests

        [Test]
        public void UpdateSupply_WhenSupplyInformationChanges_ShouldUpdateCorrectly()
        {
            // Given - Existe un suministro registrado
            var originalSupply = new Supply(new CreateSupplyCommand(
                ProviderId: 1, 
                HotelId: 1, 
                Name: "Producto Original", 
                Price: 10.00m, 
                Stock: 50, 
                State: "AVAILABLE"
            ));

            var updateCommand = new UpdateSupplyCommand(
                Id: 1,
                ProviderId: 2,
                HotelId: 1,
                Name: "Producto Actualizado",
                Price: 15.00m,
                Stock: 75,
                State: "AVAILABLE"
            );

            // When - Se actualiza la información del suministro
            originalSupply.Update(updateCommand);

            // Then - Los datos del suministro deben actualizarse correctamente
            Assert.That(originalSupply.ProviderId, Is.EqualTo(2));
            Assert.That(originalSupply.Name, Is.EqualTo("PRODUCTO ACTUALIZADO"));
            Assert.That(originalSupply.Price, Is.EqualTo(15.00m));
            Assert.That(originalSupply.Stock, Is.EqualTo(75));
            Assert.That(originalSupply.State, Is.EqualTo("AVAILABLE"));
        }

        [Test]
        public void CreateSupplyRequest_WhenHotelNeedsSupplies_ShouldCreateRequestCorrectly()
        {
            // Given - El hotel necesita solicitar suministros
            var supply = new Supply(new CreateSupplyCommand(
                ProviderId: 1, 
                HotelId: 1, 
                Name: "Toallas Premium", 
                Price: 30.00m, 
                Stock: 100, 
                State: "AVAILABLE"
            ));

            var requestCommand = new CreateSupplyRequestCommand(
                PaymentOwnerId: 1,
                SupplyId: supply.Id,
                Count: 20,
                Amount: 600.00m
            );

            // When - Se crea la solicitud de suministro
            var supplyRequest = new SupplyRequest(requestCommand);

            // Then - La solicitud debe crearse correctamente
            Assert.That(supplyRequest.PaymentOwnerId, Is.EqualTo(1));
            Assert.That(supplyRequest.SupplyId, Is.EqualTo(supply.Id));
            Assert.That(supplyRequest.Count, Is.EqualTo(20));
            Assert.That(supplyRequest.Amount, Is.EqualTo(600.00m));
        }

        #endregion

        #region Helper Methods and Classes

        private List<Supply> GetInventoryReport(List<Supply> supplies)
        {
            return supplies.OrderBy(s => s.Name).ToList();
        }

        // Clases auxiliares para las pruebas de dispositivos IoT
        public class DeviceRequest
        {
            public string DeviceType { get; set; }
            public int Quantity { get; set; }
            public string Justification { get; set; }
        }

        public class IoTDeviceRequest
        {
            public int HotelId { get; set; }
            public List<DeviceRequest> DeviceRequests { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime? SubmissionDate { get; set; }
            public DateTime? ReceivedDate { get; set; }

            public IoTDeviceRequest(int hotelId, List<DeviceRequest> deviceRequests, string description)
            {
                HotelId = hotelId;
                DeviceRequests = deviceRequests;
                Description = description;
                Status = "PENDING";
                RequestDate = DateTime.Now;
            }

            public bool CanBeSentToDepartment()
            {
                return DeviceRequests.Any() && !string.IsNullOrEmpty(Description);
            }

            public void SubmitRequest()
            {
                Status = "SUBMITTED";
                SubmissionDate = DateTime.Now;
            }

            public void MarkAsReceived()
            {
                Status = "RECEIVED";
                ReceivedDate = DateTime.Now;
            }
        }

        #endregion
    }
}