using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public interface ITracer
    {
        void Log(TraceLevel level, object obj);
    }
}
