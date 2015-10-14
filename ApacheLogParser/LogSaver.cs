using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ApacheLogParser
{
    public class LogSaver
    {
        private const string TableName = "apachelogs";
        private readonly CloudTable _table;

        public LogSaver(string connectionString)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            _table = client.GetTableReference(TableName);
            _table.CreateIfNotExists();
        }

        public void Save(Log log)
        {
            var entity = GetLogTableEntity(log);
            var action = TableOperation.Insert(entity);
            _table.Execute(action);
        }

        private static LogTableEntity GetLogTableEntity(Log log)
        {
            var entity = new LogTableEntity(log.DateTime, log.ResourcePath)
            {
                MicroSeconds = log.MicroSeconds,
                Url = log.Url,
                Bytes = log.Bytes,
                ClientIp = log.ClientIp,
                StatusCode = log.StatusCode
            };
            return entity;
        }

        public void SaveToStorageInBatch(List<Log> list)
        {
            var batchOperation = new TableBatchOperation();
            foreach (var log in list)
            {
                var entity = GetLogTableEntity(log);
                batchOperation.Insert(entity);
            }

            var result = _table.ExecuteBatch(batchOperation);

        }
    }
}