using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACADEMY_864129_WebAPI.Models;
using ACADEMY_864129_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACADEMY_864129_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ITableStorage tableStorageService;
        private readonly ICosmosDataBase cosmosDataBaseService;
        private readonly IConfigIoTHub configIoTHubService;
        public DeviceController(ITableStorage tableStorageService, ICosmosDataBase cosmosDataBaseService, IConfigIoTHub configIoTHubService)
        {
            this.tableStorageService = tableStorageService;
            this.cosmosDataBaseService = cosmosDataBaseService;
            this.configIoTHubService = configIoTHubService;
        }
        [HttpGet("GetTelemetryDataTable")]
        public async Task<IActionResult> GetTelemetryDataFromTable([FromQuery] int days)
        {
            var telemetryData = await tableStorageService.GetTelemetryData(days);
            return Ok(telemetryData);
        }

        [HttpGet("GetNormalDataTable")]
        public async Task<IActionResult> GetPositiveDataFromTable([FromQuery] int days)
        {
            var telemetryData = await tableStorageService.GetNormalData(days);
            return Ok(telemetryData);
        }

        [HttpGet("GetAlertDataTable")]
        public async Task<IActionResult> GetAlertDataFromTable([FromQuery] int days)
        {
            var alertData = await tableStorageService.GetAlertData(days);
            return Ok(alertData);
        }

        [HttpGet("GetTelemetryDataCosmos")]
        public async Task<IActionResult> GetTelemetryDataFromCosmos([FromQuery] int days)
        {
            var telemetryData = await cosmosDataBaseService.GetTelemetryData();
            return Ok(telemetryData);
        }

        [HttpPost("ConfigureIoTHub")]
        public async Task<IActionResult> ConfigureIoTHub([FromBody] DeviceData deviceDate)
        {
            await configIoTHubService.MessageToIoTHub(deviceDate);
            return Ok();
        }

        [HttpGet("GetIoTHubInfo")]
        public async Task<IActionResult> GetConnectedDevice()
        {
            var deviceInfo = await configIoTHubService.GetConfigurationData();
            return Ok(deviceInfo);
        }

        [HttpGet("GetStarted")]
        public string GetStarted()
        {
            return "started";
        }
    }
}
