using System.Diagnostics;

namespace Interfaces
{
    public interface ITracer
    {
        void Log(TraceLevel level, object obj);
    }
}
