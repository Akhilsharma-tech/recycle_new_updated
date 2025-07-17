using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application
{
    public class OperationStatistics
    {
        private long outputtedRows;
        public long OutputtedRows => outputtedRows;
        public int RowsProcessed { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public void MarkStarted() => StartTime = DateTime.UtcNow;
        public void MarkRowProcessed() => RowsProcessed++;
        public void MarkFinished() => EndTime = DateTime.UtcNow;
    }
}
