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
        public IActionResult GetTelemetryData([FromQuery] int days)
        {
            var telemetryData = tableStorage.GetTelemetryData(days);
            return Ok(telemetryData);
        }

        [HttpGet("GetNormalData")]
        public IActionResult GetPositiveData([FromQuery] int days)
        {
            var telemetryData = tableStorage.GetNormalData(days);
            return Ok(telemetryData);
        }

        [HttpGet("GetAlertData")]
        public IActionResult GetAlertData([FromQuery] int days)
        {
            var alertData = tableStorage.GetAlertData(30);
            return Ok(alertData);
        }

        [HttpGet("GetStarted")]
        public string GetStarted()
        {
            return "started";
        }
    }
}
