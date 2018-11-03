﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Tracer.Tests
{
    [TestClass()]
    public class TracerTests
    {
        [TestMethod()]
        public void LogTest()
        {
            using(StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ITracer tracer = new Tracer(new ConsoleTraceListener(), TraceLevel.Error);
                tracer.Log(TraceLevel.Info, "should not be logged");
                Assert.AreEqual("", sw.ToString());

                tracer.Log(TraceLevel.Error, "should be logged");
                Assert.AreNotEqual("", sw.ToString());
            }
        }
    }
}