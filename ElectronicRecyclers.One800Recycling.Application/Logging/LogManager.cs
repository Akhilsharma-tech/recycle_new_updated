using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Logging
{
    public class LogManager
    {
        public static ILog GetLogger()
        {
            var stackFrame = new StackFrame(1, false);
            return new NLogLogger(stackFrame.GetMethod().DeclaringType);
        }
    }
}
