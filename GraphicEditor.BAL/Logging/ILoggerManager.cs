using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace GraphicEditor.BAL
{
    public interface ILoggerManager
    {
        void LogError(string ErrorMessage);
        void LogError(Exception ex, string ErrorMessage);
        void LogTrace(string ErrorMessage);
        void LogDebug(string ErrorMessage);
        void LogInfo(string ErrorMessage);
    }

    public class LoggerManager : ILoggerManager
    {
        Logger log = LogManager.GetCurrentClassLogger(); //  GetLogger("ErrorLogFile");
        public void LogError(string ErrorMessage)
        {
            log.Error(ErrorMessage);
        }
        public void LogError(Exception ex, string ErrorMessage)
        {
            log.Error(ex, ErrorMessage);
        }
        public void LogInfo(string ErrorMessage)
        {
            log.Info(ErrorMessage);
        }

        public void LogDebug(string ErrorMessage)
        {
            log.Debug(ErrorMessage);
        }
        public void LogTrace(string ErrorMessage)
        {
            log.Trace(ErrorMessage);
        }
    }
}
