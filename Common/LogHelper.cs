using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LogHelper
    {
        public static readonly ILog log = LogManager.GetLogger("RollingLogFileAppender");
        public static void Debug(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            if (log.IsDebugEnabled)
            {

                //log4net.LogicalThreadContext.Properties["CustomColumn"] = "Custom value";
                log.Debug(message);
            }
            log = null;
        }

        public static void Error(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
            log = null;
        }

        public static void Fatal(string message)
        {

            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
            log = null;
        }
        public static void Info(string message, string username)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            if (log.IsInfoEnabled)
            {
                log4net.LogicalThreadContext.Properties["UserName"] = username;
                log.Info(message);
            }
            log = null;
        }

        public static void Warn(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
            log = null;
        }
    }
}
