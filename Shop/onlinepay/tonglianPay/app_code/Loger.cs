using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace tonglianPay
{
    public static class Loger
    {
        //<logger name="errorLog">
        static log4net.ILog log = log4net.LogManager.GetLogger("errorLog");

        public static void WriteEvent(
            Exception objExp, string eventTitle)
        {
            WriteEvent(objExp, eventTitle, LogLevelCode.Info);
        }

        public static void WriteEvent(string eventTitle)
        {
            WriteEvent(eventTitle, LogLevelCode.Info);
        }

        public static void WriteEvent(string eventTitle, LogLevelCode LogLevel)
        {
            StringBuilder result = new StringBuilder(1000);
            result.Append("—" + eventTitle + "—").Append("\r\n");
            result.Append("时间：").Append(DateTime.Now).Append("\r\n");
            switch (LogLevel)
            {
                case LogLevelCode.Debug:
                    log.Debug(result);
                    break;
                case LogLevelCode.Info:
                    log.Info(result);
                    break;
                case LogLevelCode.Error:
                    log.Error(result);
                    break;
                case LogLevelCode.Fatal:
                    log.Fatal(result);
                    break;
            }
        }

        public static void WriteEvent(
            Exception objExp, string EventTitle, LogLevelCode LogLevel)
        {
            StringBuilder result = new StringBuilder(1000);
            result.Append("\r\n==========" + EventTitle + "==========").Append("\r\n");
            result.Append("时间：").Append(DateTime.Now).Append("\r\n");
            switch (LogLevel)
            {
                case LogLevelCode.Debug:
                    log.Debug(result, objExp);
                    break;
                case LogLevelCode.Info:
                    log.Info(result, objExp);
                    break;
                case LogLevelCode.Error:
                    log.Error(result, objExp);
                    break;
                case LogLevelCode.Fatal:
                    log.Fatal(result, objExp);
                    break;
            }
        }
    }
}