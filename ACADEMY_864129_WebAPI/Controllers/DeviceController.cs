using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACADEMY_864129_WebAPI.Models;
using ACADEMY_864129_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ACADEMY_864129_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ITableStorage tableStorageService;
        private readonly ICosmosDataBase cosmosDataBaseService;
        private readonly IConfigIoTHub configIoTHubService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DeviceController(ITableStorage tableStorageService, ICosmosDataBase cosmosDataBaseService, IConfigIoTHub configIoTHubService)
        {
            this.tableStorageService = tableStorageService;
            this.cosmosDataBaseService = cosmosDataBaseService;
            this.configIoTHubService = configIoTHubService;
        }
        /// <summary>
        /// Get's the Telemerty Data from Azure Table
        /// </summary>
        /// <param name="days">Optional , Get data based on number of days</param>
        /// <returns></returns>
        [HttpGet("GetTelemetryDataTable")]
        public async Task<IActionResult> GetTelemetryDataFromTable([FromQuery] int days)
        {
            try
            {
                var telemetryData = await tableStorageService.GetTelemetryData(days);
                return Ok(telemetryData);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.InnerException);
                throw;
            }

        }

        /// <summary>
        /// Get's the Error free data from Azure Table (Not Alert Data)
        /// </summary>
        /// <param name="days">Optional , Get data based on number of days</param>
        /// <returns></returns>
        [HttpGet("GetNormalDataTable")]
        public async Task<IActionResult> GetPositiveDataFromTable([FromQuery] int days)
        {
            try
            {
                var telemetryData = await tableStorageService.GetNormalData(days);
                return Ok(telemetryData);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.InnerException);
                throw;
            }
        }
        /// <summary>
        /// Get's the Alert Data
        /// </summary>
        /// <param name="days">Optional , Get data based on number of days</param>
        /// <returns></returns>
        [HttpGet("GetAlertDataTable")]
        public async Task<IActionResult> GetAlertDataFromTable([FromQuery] int days)
        {
            try
            {
                var alertData = await tableStorageService.GetAlertData(days);
                return Ok(alertData);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.InnerException);
                throw;
            }
        }

        /// <summary>
        /// Gets the Telemetry Data from Cosmos Db
        /// </summary>    
        /// <returns></returns>
        [HttpGet("GetTelemetryDataCosmos")]
        public async Task<IActionResult> GetTelemetryDataFromCosmos()
        {
            try
            {
                var telemetryData = await cosmosDataBaseService.GetTelemetryData();
                return Ok(telemetryData);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.InnerException);
                throw;
            }

        }

        [HttpPost("MessageToDevice")]
        public async Task<IActionResult> ConfigureIoTHub([FromBody] DeviceData deviceDate)
        {
            try
            {
                await configIoTHubService.MessageToIoTHub(deviceDate);
                return Ok();
            }
            catch (Exception exception)
            {
                Logger.Error(exception.InnerException);
                throw;
            }
        }

        /// <summary>
        /// Get's the Configuration Information 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetConfigInfo")]
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
