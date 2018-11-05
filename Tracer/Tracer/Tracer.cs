using System;
using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        static TraceSwitch traceSwitch = new TraceSwitch("GeneralSwitch", "Entire Application");

        public Tracer(TraceListener listener, TraceLevel level = TraceLevel.Error)
        {
            traceSwitch.Level = level;
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
        }


        public void Log(TraceLevel level, object obj)
        {
            Trace.WriteLineIf(level <= traceSwitch.Level,"[" + level + "] \t" + DateTime.Now + "\t" + obj);
        }
    }
}
