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

        public IList<DeviceData> GetAlertData(int days)
        {
            return RetriveData("AlertData");
        }

        public IList<DeviceData> GetTelemetryData(int days)
        {
            return RetriveData("TelemetryData");
        }

        private IList<DeviceData> RetriveData(string GroupByParameter)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(appSettings.StorageAccountConnectionString);

            CloudTableClient client = storageAccount.CreateCloudTableClient();

            CloudTable table = client.GetTableReference(appSettings.TableName);

            bool b = table.ExistsAsync().Result;
            var condition = TableQuery.GenerateFilterCondition("GroupData", QueryComparisons.Equal, GroupByParameter);
            var query = new TableQuery().Where(condition);
            var result = table.ExecuteQuerySegmentedAsync(query, null);

            TableOperation retOp = TableOperation.Retrieve("DeviceId", "CaptureTime");
            TableResult tr = table.ExecuteAsync(retOp).Result;
            var RESULT = tr.Result as DeviceData;
            return new List<DeviceData>();
        }
    }
}
