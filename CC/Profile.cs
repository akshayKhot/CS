using BenchmarkDotNet.Attributes;
using CC.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC
{
    public class Profile
    {
        [Benchmark]
        public void RunParser()
        {
            var parser = new Parser("1 + 2 + 3");

            var expression = parser.Parse();
        }
    }
}
