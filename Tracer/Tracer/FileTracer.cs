﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Tracer
{
    [Export(typeof(ITracer))]
    public class FileTracer : Tracer
    {
        [ImportingConstructor]
        public FileTracer(/*string fileName="log", TraceLevel level = TraceLevel.Error*/)
            : base(new TextWriterTraceListener(DateTime.Now.ToString("d-m-yyyy_HH-mm-ss") + "_" + "log.log"), TraceLevel.Info) { }
    }
}
