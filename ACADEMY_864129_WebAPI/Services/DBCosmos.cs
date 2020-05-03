using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Services
{
    public class DBCosmos
    {
        /// <summary>
        /// Run a query (using Azure Cosmos DB SQL syntax) against the container
        /// </summary>
        private async Task QueryItemsAsync()
        {
            //var sqlQueryText = "SELECT * FROM c WHERE c.LastName = 'Andersen'";

            //Console.WriteLine("Running query: {0}\n", sqlQueryText);

            //QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            //FeedIterator<Family> queryResultSetIterator = this.container.GetItemQueryIterator<Family>(queryDefinition);

            //List<Family> families = new List<Family>();

            //while (queryResultSetIterator.HasMoreResults)
            //{
            //    FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
            //    foreach (Family family in currentResultSet)
            //    {
            //        families.Add(family);
            //        Console.WriteLine("\tRead {0}\n", family);
            //    }
            //}
        }

        //public static List<TelemeteryData> DisplayTableRecords(CloudTable table)
        //{
        //    List<TelemeteryData> teleCount = new List<TelemeteryData>();
        //    TableQuery<TelemeteryData> tableQuery = new TableQuery<TelemeteryData>();
        //    foreach (TelemeteryData customerEntity in table.ExecuteQuerySegmentedAsync(tableQuery, null).Result)
        //    {
        //        teleCount.Add(customerEntity);
        //    }
        //    return teleCount;
        //}
    }
}
