using System.Diagnostics;

namespace Tracer
{
    public interface ITracer
    {
        void Log(TraceLevel level, object obj);
    }
}
