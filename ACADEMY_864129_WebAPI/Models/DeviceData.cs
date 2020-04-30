using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Models
{
    public class DeviceData : TableEntity
    {
        public string DeviceId { get; set; }
        public string CaptureTime { get; set; }
        public decimal DeviceLong { get; set; }
        public decimal DeviceLat { get; set; }
        public decimal Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public decimal Humidity { get; set; }
        public string HumidityUnit { get; set; }
        public bool DoorStatus { get; set; }
    }
}
