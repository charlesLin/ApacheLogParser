using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ApacheLogParser
{
    public class LogSaver
    {
        private const string TableName = "apachelogs";
        private readonly string _connectionString;
        private CloudTable _table;

        public LogSaver(string connectionString)
        {
            _connectionString = connectionString;
            var account = CloudStorageAccount.Parse(_connectionString);
            var client = account.CreateCloudTableClient();
            _table = client.GetTableReference(TableName);
            _table.CreateIfNotExists();
        }

        public void Save(Log log)
        {
            var entity = new LogTableEntity(log.DateTime)
            {
               MicroSeconds = log.MicroSeconds,
               Url = log.Url,
               Bytes = log.Bytes,
               ClientIp = log.ClientIp,
               StatusCode = log.StatusCode
            };
            var action = TableOperation.Insert(entity);
            _table.Execute(action);
        }
    }
}