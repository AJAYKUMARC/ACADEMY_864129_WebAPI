using ACADEMY_864129_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public interface ITableStorage
    {
        IList<DeviceData> GetTelemetryData(int days);

        IList<DeviceData> GetAlertData(int days);
    }
}
