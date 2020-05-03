using ACADEMY_864129_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public interface ITableStorage
    {
        Task<IList<DeviceData>> GetTelemetryData(int days);

        Task<IList<DeviceData>> GetAlertData(int days);

        Task<IList<DeviceData>> GetNormalData(int days);
    }
}
