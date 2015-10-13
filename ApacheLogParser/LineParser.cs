using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApacheLogParser
{
    public class LineParser
    {
        public Log Parse(string line)
        {
            Console.WriteLine(line);
            var reg = new Regex(@"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) (\S*) (\S*) \[(.*)\] (\""(\S*) (\S*) (\S*)\"") (\d*) (\d*) (\d*)");
            var collections = reg.Matches(line);
            if (collections.Count == 0)
                return null;
            var matchs = collections[0];
            return new Log()
            {
                ClientIp = matchs.Groups[1].Value,
                DateTime = ParseDateTime(matchs.Groups[4].Value),
                Url = matchs.Groups[7].Value,
                StatusCode = int.Parse(matchs.Groups[9].Value),
                Bytes = GetBytes(matchs),
                MicroSeconds = int.Parse(matchs.Groups[11].Value),
            };
        }

        private static int? GetBytes(Match matchs)
        {
            string value = matchs.Groups[10].Value;
            if (value == "-") return null;
            return int.Parse(value);
        }

        private DateTime ParseDateTime(string input)
        {
            //06/Oct/2015:17:07:42 +0800
            var input1 = input.Split(' ')[0];
            return DateTime.ParseExact(input1, "dd/MMM/yyyy:HH:mm:ss", new CultureInfo("en-US"));
        }
    }

    public class Log
    {
        public string ClientIp { get; set; }
        public DateTime DateTime { get; set; }
        public string Url { get; set; }

        public int StatusCode { get; set; }
        public int? Bytes { get; set; }

        public int MicroSeconds { get; set; }
    }
}
