// <copyright file="LineParserTest.cs">Copyright ©  2015</copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using ApacheLogParser;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Xunit;

namespace ApacheLogParser.Tests
{
    public partial class DirectoryParserTest
    {
        [Fact]
        public void Test()
        {
            var target = new DirectoryParser();
            var result = target.Parse(Environment.CurrentDirectory);
            Assert.Equal(576, result.Count());
        }

    }



}
