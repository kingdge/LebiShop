using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Tools
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 计算日期间隔天数
        /// 例如 2010-08-09.DiffDay(2010-08-07) 返回 1
        /// </summary>
        /// <param name="date"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int DiffDay(this DateTime date, DateTime toDate)
        {
            return (new DateTime(date.Year, date.Month, date.Day) - new DateTime(toDate.Year, toDate.Month, toDate.Day)).Days;
        }

        public static string ToMMdd(this DateTime date)
        {
            return date.ToString("MM-dd");
        }   
   
        public static string ToMMddHHmm(this DateTime date)
        {
            return date.ToString("MM-dd HH:mm");
        }   
   

    }
}
