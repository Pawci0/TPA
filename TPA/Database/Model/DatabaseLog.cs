using System;

namespace Database.Model
{
    public class DatabaseLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string TraceLevel { get; set; }
    }
}

