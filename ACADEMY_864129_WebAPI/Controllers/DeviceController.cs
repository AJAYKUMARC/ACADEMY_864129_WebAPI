using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACADEMY_864129_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACADEMY_864129_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ITableStorage tableStorage;
        public DeviceController(ITableStorage tableStorage)
        {
            this.tableStorage = tableStorage;
        }
        [HttpGet("GetTelemetryData")]
        public async Task<IActionResult> GetTelemetryData([FromQuery] int days)
        {
            var telemetryData = await tableStorage.GetTelemetryData(days);
            return Ok(telemetryData);
        }

        [HttpGet("GetNormalData")]
        public async Task<IActionResult> GetPositiveData([FromQuery] int days)
        {
            var telemetryData = await tableStorage.GetNormalData(days);
            return Ok(telemetryData);
        }

        [HttpGet("GetAlertData")]
        public async Task<IActionResult> GetAlertData([FromQuery] int days)
        {
            var alertData = await tableStorage.GetAlertData(30);
            return Ok(alertData);
        }

        [HttpGet("GetStarted")]
        public string GetStarted()
        {
            return "started";
        }
    }
}
