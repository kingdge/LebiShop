using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class selectpickupdate : Bussiness.ShopPage
    {
        protected DateTime start;
        protected string cmonth = ",";
        protected Lebi_PickUp pick;
        protected Lebi_Order order;
        protected string selectedday;
        protected int sumdays = 90;
        protected string callback = "";
        public void LoadPage()
        {
            int orderid = RequestTool.RequestInt("orderid");
            int pickupid = RequestTool.RequestInt("pickupid");
            selectedday = RequestTool.RequestString("selectedday");
            callback = RequestTool.RequestString("callback");
            order = B_Lebi_Order.GetModel(orderid);
            if (order == null)
                order = new Lebi_Order();

            start = Convert.ToDateTime(order.Time_Add.ToString("yyyy-MM") + "-1");
            pick = B_Lebi_PickUp.GetModel(pickupid);
            if (pick == null)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            sumdays = sumdays + order.Time_Add.Day;
        }
        /// <summary>
        /// 1配货未完成
        /// 2周末不上班
        /// 3节假日活暂停服务
        /// 4已经选中
        /// 0可选
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int checkday(DateTime day)
        {
            if (day < order.Time_Add.AddDays(pick.BeginDays).Date)
                return 1;
            if (day > order.Time_Add.AddDays(90).Date)
                return 1;
            if (pick.IsCanWeekend == 0)
            {
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                {
                    return 2;
                }
            }
            if (pick.NoServiceDays != "")
            {
                string[] nodays = pick.NoServiceDays.Split(',');
                foreach (string noday in nodays)
                {
                    if (noday.TrimStart('0').Replace(".0", ".") == day.ToString("M.d"))
                        return 3;
                }
            }
            if (day.ToString("yyyy-MM-dd") == selectedday)
                return 4;
            return 0;
        }
        /// <summary>
        /// 补齐最后一个月的日期
        /// </summary>
        /// <returns></returns>
        public string GetLastDays(DateTime day)
        {
            int month = day.Month;
            int days = 30;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                days = 31;
            if (month == 2)
            {
                if (day.Year % 4 == 0)
                    days = 29;
            }

            string res = "";
            for (int i = day.Day + 1; i <= days; i++)
            {
                res += "<div class=\"day day1\">" + i + "</div>";
            }
            return res;
        }
        /// <summary>
        /// 按照星期在前面补齐
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string GetBeginDays(DateTime day)
        {
            int days = 0;
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    days = 0;
                    break;
                case DayOfWeek.Tuesday:
                    days = 1;
                    break;
                case DayOfWeek.Wednesday:
                    days = 2;
                    break;
                case DayOfWeek.Thursday:
                    days = 3;
                    break;
                case DayOfWeek.Friday:
                    days = 4;
                    break;
                case DayOfWeek.Saturday:
                    days = 5;
                    break;
                case DayOfWeek.Sunday:
                    days = 6;
                    break;
            }

            string res = "";
            for (int i = 0; i < days; i++)
            {
                res += "<div class=\"day\"></div>";
            }
            return res;
        }
    }
}