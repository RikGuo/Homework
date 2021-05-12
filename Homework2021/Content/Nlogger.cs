using NLog;
using System;

namespace Homework2021.Content
{
    public class Nlogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public enum NType { Info, Error, Debug }
        public static void WriteLog(NType LogType, string msg, Exception ex = null)
        {
            switch (LogType)
            {
                case NType.Info:
                    logger.Info(msg);
                    break;
                case NType.Error:
                    logger.Error(ex, msg);
                    break;
                case NType.Debug:
                    logger.Debug(ex, msg);
                    break;
            }
        }
    }
}
