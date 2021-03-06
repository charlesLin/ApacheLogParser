// <copyright file="LineParserTest.cs">Copyright ©  2015</copyright>
using System;
using System.Collections.Generic;
using System.Globalization;
using ApacheLogParser;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Xunit;

namespace ApacheLogParser.Tests
{
    /// <summary>This class contains parameterized unit tests for LineParser</summary>
    [PexClass(typeof(LineParser))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    public partial class LineParserTest
    {
        /// <summary>Test stub for Parse(String)</summary>
        [PexMethod]
        public Log ParseTest([PexAssumeUnderTest]LineParser target, string line)
        {
            var result = target.Parse(line);
            return result;
            // TODO: add assertions to method LineParserTest.ParseTest(LineParser, String)
        }

        [Fact]
        public void Test()
        {
            var target = new LineParser();
            var result = target.Parse(@"202.39.77.14 - - [06/Oct/2015:17:07:43 +0800] ""POST /cvs/ap_interface.php HTTP/1.1"" 200 289 328165");
            Assert.Equal("202.39.77.14", result.ClientIp);
            Assert.Equal("/cvs/ap_interface.php", result.Url);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(289, result.Bytes);
            Assert.Equal(328165, result.MicroSeconds);
        }

    }



}
