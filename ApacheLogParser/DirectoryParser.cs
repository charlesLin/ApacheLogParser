using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApacheLogParser
{
    class DirectoryParser
    {
        public IEnumerable<Log> Parse(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "access-*.log");
            var parser = new FileParser();
            foreach (var file in files)
            {
                var logs = parser.Parse(file);
                foreach (var log in logs)
                {
                    yield return log;
                }
            }
        } 
    }
}
