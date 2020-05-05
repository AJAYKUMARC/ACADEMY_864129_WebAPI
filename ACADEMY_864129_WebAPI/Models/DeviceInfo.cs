using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Models
{
    public class DeviceInfo
    {
        public string DeviceId { get; set; }
        public bool ConnectedStatus { get; set; }
        public bool IoTDeviceStatus { get; set; }
    }
}
