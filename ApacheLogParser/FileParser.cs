﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApacheLogParser
{
    public class FileParser
    {
        public IEnumerable<Log> Parse(string filePath)
        {
            var lineParser = new LineParser();
            foreach (var line in File.ReadLines(filePath))
            {
                yield return lineParser.Parse(line);
            }
        } 
    }
}
