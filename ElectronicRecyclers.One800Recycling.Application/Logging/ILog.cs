﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Logging
{
    public interface ILog
    {
        void Info(string message);

        void Info(string message, Exception exception);

        void Debug(string message);

        void Debug(string message, Exception exception);

        void Warn(string message);

        void Warn(string message, Exception exception);

        void Error(string message);

        void Error(string message, Exception exception);

        void Trace(string message);

        void Trace(string message, Exception exception);

        void Fatal(string message);

        void Fatal(string message, Exception exception);
    }
}
