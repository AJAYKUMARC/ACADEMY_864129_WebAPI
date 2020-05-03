using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Models
{
    public class AppSettings
    {
        public string StorageAccountConnectionString { get; set; }
        public string TableName { get; set; }
        public string CosmosDatabaseName { get; set; }
        public string CosmosContainerName { get; set; }
        public string CosmosEndPointURL { get; set; }
        public string CosmosPrimaryKey { get; set; }
    }
}
