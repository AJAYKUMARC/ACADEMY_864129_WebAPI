using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Models
{
    public class DeviceData : TableEntity
    {
        public String DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public bool DoorStatus { get; set; }
        public string CaptureTime { get; set; }
    }
}
