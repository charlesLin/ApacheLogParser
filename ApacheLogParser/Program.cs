using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApacheLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var folderPath = args.Length > 0 ? args[0] : Environment.CurrentDirectory;
            var sw = Stopwatch.StartNew();
            sw.Start();

            var parser = new DirectoryParser();
            var logs = parser.Parse(folderPath);
            
            SaveToStorage(logs);

            //var ap = (from l in logs
            //    where l?.Url != null && l.Url.Contains("ap_interface.php")
            //    select l).ToList();

            //Console.WriteLine("Max: " + ap.Max(x => x.MicroSeconds) /1000000.0);
            //Console.WriteLine("Avg: " + ap.Average(x => x.MicroSeconds) /1000000.0);
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            
        }

        private static void SaveToStorage(IEnumerable<Log> logs)
        {
            var logSaver = new LogSaver("UseDevelopmentStorage=true");

            var dic = new Dictionary<string, List<Log>>();
            foreach (var log in logs)
            {
                List<Log> list = null;
                if (!dic.ContainsKey(log.ResourcePath))
                {
                    list = new List<Log>(100);
                    dic.Add(log.ResourcePath, list);
                }
                else
                {
                    list = dic[log.ResourcePath];
                }

                if (list.Count < 100)
                    list.Add(log);
                else
                {
                    logSaver.SaveToStorageInBatch(list);
                    list.Clear();
                }
            }

            foreach (var key in dic.Keys)
            {
                var list = dic[key];
                if (list.Count > 0)
                    logSaver.SaveToStorageInBatch(list);
            }
            
        }
    }
}
