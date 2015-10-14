using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace ApacheLogParser
{
    public class LogTableEntity : TableEntity
    {
        public LogTableEntity()
        {
        }

        public LogTableEntity(DateTime dateTime, string resourcePath)
        {
            this.DateTime = dateTime;
            this.PartitionKey = resourcePath.Replace('/', '|');
            this.RowKey = dateTime.ToString("yyyyMMdd HHmmss ") + Guid.NewGuid().ToString();
        }

        public string ClientIp { get; set; }
        public DateTime DateTime { get; set; }
        public string Url { get; set; }

        public int StatusCode { get; set; }

        public int? Bytes { get; set; }

        public int MicroSeconds { get; set; }
    }
}
