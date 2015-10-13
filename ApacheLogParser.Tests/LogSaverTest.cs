using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApacheLogParser.Tests
{
    public class LogSaverTest
    {
        [Fact]
        public async Task SaveAsyncTest()
        {
            var saver = new LogSaver("UseDevelopmentStorage=true");
            var log = new Log()
            {
                DateTime = DateTime.Now,
                ClientIp = "122.22.22.22",
                Url = "/aaa/aaa/bb.asp",
                Bytes = 222,
                MicroSeconds = 333333,
                StatusCode = 200
            };
            saver.Save(log);
        }
    }
}
