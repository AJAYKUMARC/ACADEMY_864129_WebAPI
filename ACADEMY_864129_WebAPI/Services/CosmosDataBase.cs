using ACADEMY_864129_WebAPI.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ACADEMY_864129_WebAPI.Services
{
    public class CosmosDataBase : ICosmosDataBase
    {
        private readonly AppSettings appSettings;

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The container we will create.
        private Container container;

        public CosmosDataBase(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        private async Task ConfigDetails()
        {
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(appSettings.CosmosEndPointURL, appSettings.CosmosPrimaryKey);
            this.cosmosClient = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();
            DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(appSettings.CosmosDatabaseName);
            this.container = await database.Database.CreateContainerIfNotExistsAsync(appSettings.CosmosContainerName, "/CaptureTime");
        }

        public async Task<IList<DeviceData>> GetTelemetryData()
        {
            await ConfigDetails();
            var sqlQueryText = "SELECT * FROM c";
            var query = this.container.GetItemQueryIterator<DeviceData>(new QueryDefinition(sqlQueryText));
            List<DeviceData> results = new List<DeviceData>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
