using ACADEMY_864129_WebAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public class TableStorage : ITableStorage
    {
        private readonly AppSettings appSettings;
        public TableStorage(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<IList<DeviceData>> GetAlertData(int days)
        {
            return await RetriveData("AlertData");
        }

        public async Task<IList<DeviceData>> GetTelemetryData(int days)
        {
            return await RetriveData("TelemetryData");
        }

        private async Task<IList<DeviceData>> RetriveData(string GroupByParameter)
        {
            try
            {
                var deviceData = new List<DeviceData>();
                TableContinuationToken token = null;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(appSettings.StorageAccountConnectionString);
                CloudTableClient client = storageAccount.CreateCloudTableClient();
                CloudTable table = client.GetTableReference(appSettings.TableName);
                var condition = TableQuery.GenerateFilterCondition("GroupData", QueryComparisons.Equal, GroupByParameter);
                var query = new TableQuery().Where(condition);
                do
                {
                    var result = await table.ExecuteQuerySegmentedAsync(query, token);
                    token = result.ContinuationToken;
                    var x=result.Results as List<DeviceData>;
                    deviceData.AddRange(result.Results as List<DeviceData>);
                }
                while (token != null);


                //bool b = table.ExistsAsync().Result;
                //TableOperation retOp = TableOperation.Retrieve("DeviceId", "CaptureTime");
                //TableResult tr = table.ExecuteAsync(retOp).Result;
                //var RESULT = tr.Result as DeviceData;
                return new List<DeviceData>();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
