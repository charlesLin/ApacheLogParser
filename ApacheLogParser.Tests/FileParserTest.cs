using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApacheLogParser.Tests
{
    public class FileParserTest
    {
        [Fact]
        public void Test()
        {
            var parser = new FileParser();
            var logs = parser.Parse("access-201510060907.log");
            var log = logs.First();
            Assert.Equal(288, logs.Count());
        }
    }
}
