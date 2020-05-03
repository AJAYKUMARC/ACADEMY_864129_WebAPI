using ACADEMY_864129_WebAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public interface ICosmosDataBase
    {
        Task<IList<DeviceData>> GetTelemetryData();
    }
}
