using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Logging
{
    public class NLogLogger : ILog
    {
        private static NLog.Logger logger;

        public NLogLogger(Type type)
        {
            logger = NLog.LogManager.GetLogger(type.FullName);
        }

        public void Debug(string message)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(message, exception);
        }

        public void Info(string message)
        {
            if (logger.IsInfoEnabled)
                logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            if (logger.IsInfoEnabled)
                logger.Info(message, exception);
        }

        public void Warn(string message)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(message, exception);
        }

        public void Error(string message)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message, exception);
        }

        public void Trace(string message)
        {
            if (logger.IsTraceEnabled)
                logger.Trace(message);
        }

        public void Trace(string message, Exception exception)
        {
            if (logger.IsTraceEnabled)
                logger.Trace(message, exception);
        }

        public void Fatal(string message)
        {
            if (logger.IsFatalEnabled)
                logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            if (logger.IsFatalEnabled)
                logger.Fatal(message, exception);
        }
    }
}
