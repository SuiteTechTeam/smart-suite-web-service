using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using SweetManagerWebService.IOT.Domain.Model.Queries;
using SweetManagerWebService.IOT.Domain.Services;
using SweetManagerWebService.IOT.Interfaces.REST.Resources;
using SweetManagerWebService.IOT.Interfaces.REST.Transform;

namespace SweetManagerWebService.IOT.Interfaces.REST
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class IoTController(
        IIotDeviceCommandService iotDeviceCommandService,
        IIoTDeviceQueryService ioTDeviceQueryService,
        IRoomDeviceCommandService roomDeviceCommandService,
        IRoomDeviceQueryService roomDeviceQueryService,
        INotificationHistoryCommandService notificationHistoryCommandService,
        INotificationHistoryQueryService notificationHistoryQueryService
        ) : ControllerBase
    {
        [HttpPost("iot-devices")]
        public async Task<IActionResult> CreateIoTDevice([FromBody] CreateIoTDeviceResource resource)
        {
            var result = await iotDeviceCommandService.Handle
                (CreateIoTDeviceCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(true);
        }

        [HttpPut("iot-devices/{id}")]
        public async Task<IActionResult> UpdateIoTDevice(int id, [FromBody] UpdateIoTDeviceResource resource)
        {
            var result = await iotDeviceCommandService.Handle
                (UpdateIoTDeviceCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(true);
        }

        [HttpPost("room-devices")]
        public async Task<IActionResult> CreateRoomDevice([FromBody] CreateRoomDeviceResource resource)
        {
            var result = await roomDeviceCommandService.Handle
                (CreateRoomDeviceCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(true);
        }

        [HttpPut("room-devices/{id}")]
        public async Task<IActionResult> UpdateRoomDevice(int id, [FromBody] UpdateRoomDeviceResource resource)
        {
            var result = await roomDeviceCommandService.Handle
                (UpdateRoomDeviceCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(true);
        }

        [HttpPost("notification-history")]
        public async Task<IActionResult> CreateNotificationHistory([FromBody] CreateNotificationHistoryResource resource)
        {
            var result = await notificationHistoryCommandService.Handle
                (CreateNotificationHistoryCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(true);
        }

        [HttpGet("iot-devices")]
        public async Task<IActionResult> AllIotDevices()
        {
            var ioTDevices = await ioTDeviceQueryService.Handle(new GetAllIoTDevicesQuery());

            var ioTDevicesResource = ioTDevices.Select(IotDeviceResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(ioTDevicesResource);
        }

        [HttpGet("iot-devices/{id}")]
        public async Task<IActionResult> IotDeviceById(int id)
        {
            var ioTDevice = await ioTDeviceQueryService.Handle(new GetIoTDeviceByIdQuery(id));

            if (ioTDevice is null)
                return BadRequest();

            var ioTDeviceResource = IotDeviceResourceFromEntityAssembler.ToResourceFromEntity(ioTDevice);

            return Ok(ioTDeviceResource);
        }

        [HttpGet("room-devices/by-iot-device/{ioTDeviceId}")]
        public async Task<IActionResult> RoomDevicesByIoTDevice(int ioTDeviceId)
        {
            var roomDevices = await roomDeviceQueryService.Handle(new GetRoomDevicesByIoTDeviceIdQuery(ioTDeviceId));

            var roomDevicesResource = roomDevices.Select(RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(roomDevicesResource);
        }

        [HttpGet("room-devices/by-room/{roomId}")]
        public async Task<IActionResult> RoomDevicesByRoom(int roomId)
        {
            var roomDevices = await roomDeviceQueryService.Handle(new GetRoomDevicesByRoomIdQuery(roomId));

            var roomDevicesResource = roomDevices.Select(RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(roomDevicesResource);
        }

        [HttpGet("notification-history/by-room/{roomId}")]
        public async Task<IActionResult> NotificationHistoryByRoomId(int roomId)
        {
            var notificationHistory = await notificationHistoryQueryService
                .Handle(new GetNotificationHistoryByRoomDeviceIdQuery(roomId));

            var notificationHistoryResource = notificationHistory.Select(NotificationHistoryResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(notificationHistoryResource);
        }
    }
}