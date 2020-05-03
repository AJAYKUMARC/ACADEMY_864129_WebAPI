using ACADEMY_864129_WebAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
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
            return await RetriveData("Alert", days);
        }

        public async Task<IList<DeviceData>> GetNormalData(int days)
        {
            return await RetriveData(null, days);
        }

        /// <summary>
        /// Gets the TelemetryData
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<IList<DeviceData>> GetTelemetryData(int days)
        {
            return await RetriveData("OK", days);
        }
        /// <summary>
        /// Query the Azure Table and Gets the Data
        /// </summary>
        /// <param name="GroupByParameter">PartitionKey Value</param>
        /// <returns>List of Device Data</returns>
        private async Task<IList<DeviceData>> RetriveData(string GroupByParameter, int days)
        {
            try
            {
                var deviceData = new List<DeviceData>();
                TableContinuationToken token = null;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(appSettings.StorageAccountConnectionString);
                CloudTableClient client = storageAccount.CreateCloudTableClient();
                CloudTable table = client.GetTableReference(appSettings.TableName);
                var partitionCondition = string.Empty;
                if (GroupByParameter != null)
                {
                    partitionCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, GroupByParameter);
                }
                else
                {
                    var cd1 = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "OK");
                    var cd2 = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Alert");
                    partitionCondition = TableQuery.CombineFilters(cd1, TableOperators.Or, cd2);

                }
                var dateCondition = TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, DateTime.UtcNow.AddDays(-days));
                var condition = TableQuery.CombineFilters(partitionCondition, TableOperators.And, dateCondition);
                TableQuery<DeviceData> query = new TableQuery<DeviceData>().Where(condition);
                var tableData = await table.ExecuteQuerySegmentedAsync(query, token);
                foreach (DeviceData customerEntity in tableData)
                {
                    deviceData.Add(customerEntity);
                }
                return deviceData;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
