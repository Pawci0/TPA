using System;
using System.Diagnostics;

namespace Tracer
{
    public class FileTracer : Tracer
    {
        public FileTracer(string fileName, TraceLevel level = TraceLevel.Error)
            : base(new TextWriterTraceListener(DateTime.Now.ToString("d-m-yyyy_HH-mm-ss") + "_" + fileName), level) { }
    }
}
