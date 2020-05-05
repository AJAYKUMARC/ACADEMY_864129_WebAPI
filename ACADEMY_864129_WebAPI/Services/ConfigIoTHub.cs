using ACADEMY_864129_WebAPI.Models;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public class ConfigIoTHub : IConfigIoTHub
    {
        private readonly AppSettings appSettings;
        public ConfigIoTHub(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<IList<DeviceInfo>> GetConfigurationData()
        {
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(appSettings.IoTHubConnectionString);
            var query = registryManager.CreateQuery(
             "SELECT * FROM devices", 100);
            var twinsInCoimbatore = await query.GetNextAsTwinAsync();
            return twinsInCoimbatore.Select(x => new DeviceInfo
            {
                ConnectedStatus = x.ConnectionState == DeviceConnectionState.Connected,
                DeviceId = x.DeviceId,
                IoTDeviceStatus = x.Status == DeviceStatus.Enabled
            }).ToList();
        }

        public async Task MessageToIoTHub(DeviceData deviceData)
        {
            ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(appSettings.IoTHubConnectionString);
            string message = String.Format("The temperature is {0}°C. The Humidity is {1} and Status is {2} ",
                         deviceData.Temperature, deviceData.Humidity, deviceData.DoorStatus);
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await serviceClient.SendAsync(deviceData.DeviceId, commandMessage);
        }
    }
}
