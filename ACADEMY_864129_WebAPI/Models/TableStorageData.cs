using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebAPI.Models
{
    public class TableStorageData
    {
        public string ETag { get; set; }
        public string PartitionKey { get; set; }
        public IList<DeviceData> Properties { get; set; }
        public string RowKey { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

