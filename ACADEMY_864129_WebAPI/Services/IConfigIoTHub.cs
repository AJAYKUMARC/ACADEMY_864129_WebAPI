using ACADEMY_864129_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public interface IConfigIoTHub
    {
        Task<IList<DeviceInfo>> GetConfigurationData();

        Task MessageToIoTHub(DeviceData deviceData);
    }
}
