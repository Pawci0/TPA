using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using DBData.Entities;
using Tracer;

namespace DBData
{
    [Export(typeof(ITracer))]
    public class DatabaseTracer : ITracer
    {
        public void Log(TraceLevel level, object obj)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Log.Add(new DatabaseLog
                {
                    Message = (string)obj,
                    TraceLevel = level.ToString(),
                    Timestamp = DateTime.Now
                });
            }
        }
    }
}
