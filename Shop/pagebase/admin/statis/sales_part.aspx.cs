using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.statis
{
    public partial class sales_part : AdminAjaxBase
    {
        protected int dateType;
        protected string key;
        protected DateTime dateFrom;
        protected DateTime dateTo;
        protected int Pay_id;
        protected int Transport_id;
        protected int Y_dateFrom;
        protected int M_dateFrom;
        protected int D_dateFrom;
        protected int Y_dateTo;
        protected int M_dateTo;
        protected int D_dateTo;
        protected int year;
        protected List<Lebi_Order> orders;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("statis_sales", "销售报表"))
            {
                PageNoPower();
            }
            dateType = RequestTool.RequestInt("dateType",0);
            Pay_id = RequestTool.RequestInt("Pay_id",0);
            Transport_id = RequestTool.RequestInt("Transport_id", 0);
            dateFrom = Convert.ToDateTime(RequestTool.RequestString("dateFrom"));
            dateTo = Convert.ToDateTime(RequestTool.RequestString("dateTo"));
            Y_dateFrom = dateFrom.Year;
            M_dateFrom = dateFrom.Month;
            D_dateFrom = dateFrom.Day;
            Y_dateTo = dateTo.Year;
            M_dateTo = dateTo.Month;
            D_dateTo = dateTo.Day;
            var year = 1; for (year = Y_dateFrom; year <= Y_dateTo; year++){
            int M_From = 0; int M_To = 0; int D_From = 0; int D_To = 0;
            if (year == Y_dateFrom)
            {
                M_From = M_dateFrom;
            }
            else
            {
                M_From = 1;
            }
            if (year == Y_dateTo)
            {
                M_To = M_dateTo;
                D_dateTo = dateTo.Day;
            }
            else
            {
                M_To = 12;
                DateTime get_lastday;
                get_lastday = Convert.ToDateTime((year+1) + "-1-1");
                D_dateTo = get_lastday.AddDays(-1).Day;
            }
            string where_year = "Type_id_OrderType=211 and IsPaid = 1";
            if (dateType == 0) { 
                where_year += " and Time_Add>='" + year + "-" + M_From + "-" + D_dateFrom + "'and Time_Add<='" + year + "-" + M_To + "-" + D_dateTo + " 23:59:59'";
            }
            else if (dateType == 1) { 
                where_year += " and Time_Paid>='" + year + "-" + M_From + "-" + D_dateFrom + "'and Time_Paid<='" + year + "-" + M_To + "-" + D_dateTo + " 23:59:59'";
            }
            else
            {
                where_year += " and Time_Shipped>='" + year + "-" + M_From + "-" + D_dateFrom + "'and Time_Shipped<='" + year + "-" + M_To + "-" + D_dateTo + " 23:59:59'";
            }
            if (Pay_id > 0)
                where_year += " and Pay_id = " + Pay_id;
            if (Transport_id > 0)
                where_year += " and Transport_id = " + Transport_id;
            decimal Order_Money = Shop.Bussiness.Statis.MoneyCount(where_year);
            decimal Money_Product = Shop.Bussiness.Statis.Money_ProductCount(where_year);
            decimal Money_Transport = Shop.Bussiness.Statis.Money_TransportCount(where_year);
            decimal Money_Bill = Shop.Bussiness.Statis.Money_BillCount(where_year);
            decimal Money_Cost = Shop.Bussiness.Statis.Money_CostCount(where_year);
            Response.Write("<tr class=\"list\">");
            Response.Write("<td><strong>" + year + " "+Tag("年")+ "</strong></td>");
            Response.Write("<td><strong>" + Shop.Bussiness.Statis.ProductCount(where_year) + "</strong></td>");
            Response.Write("<td><strong>" + FormatMoney(Order_Money) + "</strong></td>");
            Response.Write("<td><strong>" + FormatMoney(Money_Product) + "</strong></td>");
            Response.Write("<td><strong>" + FormatMoney(Money_Transport) + "</strong></td>");
            Response.Write("<td><strong>" + FormatMoney(Money_Bill) + "</strong></td>");
            Response.Write("<td><strong>" + FormatMoney(Money_Cost) + "</strong></td>");
            Response.Write("<td><strong>" + FormatMoney(Order_Money - Money_Transport - Money_Bill - Money_Cost) + "</strong></td>");
            Response.Write("</tr>");
            var m_i = 1; for (m_i = M_From; m_i <= M_To; m_i++)
            {
                if (m_i == M_dateFrom && year == Y_dateFrom)
                {
                    D_From = D_dateFrom;
                }
                else
                {
                    D_From = 1;
                }
                if (m_i == M_dateTo && year == Y_dateTo)
                {
                    D_To = D_dateTo;
                }
                else
                {
                    int day_i = 1; int str_y_i = 0; int str_m_i = 0;
                    DateTime get_lastday;
                    str_m_i = m_i + 1;
                    str_y_i = year;
                    if (str_m_i == 13)
                    {
                        str_m_i = 1;
                        str_y_i = year + 1;
                    }
                    get_lastday = Convert.ToDateTime(str_y_i + "-" + str_m_i + "-" + day_i);
                    D_To = get_lastday.AddDays(-1).Day;
                }
                string m_i_DateFrom; string m_i_DateTo;
                m_i_DateFrom = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + D_From));
                m_i_DateTo = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + D_To));
                string where = "Type_id_OrderType=211 and IsPaid = 1";
                if (dateType == 0) { 
                    where += " and Time_Add>='" + m_i_DateFrom + "' and Time_Add<='" + m_i_DateTo + " 23:59:59'";
                }
                else if (dateType == 1) { 
                    where += " and Time_Paid>='" + m_i_DateFrom + "' and Time_Paid<='" + m_i_DateTo + " 23:59:59'";
                }
                else
                {
                    where += " and Time_Shipped>='" + m_i_DateFrom + "' and Time_Shipped<='" + m_i_DateTo + " 23:59:59'";
                }
                if (Pay_id > 0)
                    where += " and Pay_id = " + Pay_id;
                if (Transport_id > 0)
                    where += " and Transport_id = " + Transport_id;
                Order_Money = Shop.Bussiness.Statis.MoneyCount(where);
                Money_Product = Shop.Bussiness.Statis.Money_ProductCount(where);
                Money_Transport = Shop.Bussiness.Statis.Money_TransportCount(where);
                Money_Bill = Shop.Bussiness.Statis.Money_BillCount(where);
                Money_Cost = Shop.Bussiness.Statis.Money_CostCount(where);
                Response.Write("<tr class=\"list\" name=\"tr" + year +"_"+ m_i + "\">");
                Response.Write("<td><img src=\"" + AdminImage("plus.gif") + "\" name=\"img" + year + "_" + m_i + "\" id=\"img" + year + "_" + m_i + "\" style=\"cursor: pointer; text-align: center\" onclick=\"ShowChild('" + findpath(D_From, D_To, "" + year + "_" + m_i + "") + "','" + year + "_" + m_i + "')\" />&nbsp;&nbsp;<strong>" + m_i + " " + Tag("月") + "</strong></td>");
                Response.Write("<td><strong>" + Shop.Bussiness.Statis.ProductCount(where) + "</strong></td>");
                Response.Write("<td><strong>" + FormatMoney(Order_Money) + "</strong></td>");
                Response.Write("<td><strong>" + FormatMoney(Money_Product) + "</strong></td>");
                Response.Write("<td><strong>" + FormatMoney(Money_Transport) + "</strong></td>");
                Response.Write("<td><strong>" + FormatMoney(Money_Bill) + "</strong></td>");
                Response.Write("<td><strong>" + FormatMoney(Money_Cost) + "</strong></td>");
                Response.Write("<td><strong>" + FormatMoney(Order_Money - Money_Transport - Money_Bill - Money_Cost) + "</strong></td>");
                Response.Write("</tr>");
                var d_i = 1; for (d_i = D_From; d_i <= D_To; d_i++)
                {
                    string d_datefrom = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + d_i));
                    string d_dateto = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + d_i));
                    string where_day = "Type_id_OrderType=211 and IsPaid = 1";
                    if (dateType == 0) { 
                        where_day += " and Time_Add>='" + d_datefrom + "' and Time_Add<='" + d_dateto + " 23:59:59'";
                    }
                    else if (dateType == 1) { 
                        where_day += " and Time_Paid>='" + d_datefrom + "' and Time_Paid<='" + d_dateto + " 23:59:59'";
                    }
                    else
                    {
                        where_day += " and Time_Shipped>='" + d_datefrom + "' and Time_Shipped<='" + d_dateto + " 23:59:59'";
                    }
                    if (Pay_id > 0)
                        where_day += " and Pay_id = " + Pay_id;
                    if (Transport_id > 0)
                        where_day += " and Transport_id = " + Transport_id;
                    Order_Money = Shop.Bussiness.Statis.MoneyCount(where_day);
                    Money_Product = Shop.Bussiness.Statis.Money_ProductCount(where_day);
                    Money_Transport = Shop.Bussiness.Statis.Money_TransportCount(where_day);
                    Money_Bill = Shop.Bussiness.Statis.Money_BillCount(where_day);
                    Money_Cost = Shop.Bussiness.Statis.Money_CostCount(where_day);
                    Response.Write("<tr class=\"list\" name=\"tr" + year + "_" + m_i + "\" id=\"tr" + year + "_" + m_i + "_" + d_i + "\" style=\"display:none;\">");
                    Response.Write("<td>&nbsp;&nbsp;&nbsp;&nbsp;" + d_i + " " + Tag("日") + "&nbsp;<a target=\"_blank\" href=\"sales_list.aspx?dateType=" + dateType + "&dateFrom=" + d_datefrom + "&dateTo=" + d_dateto + "&Pay_id=" + Pay_id + "&Transport_id=" + Transport_id + "\"><img src=\"" + PageImage("icon/newWindow.png") + "\" /></a></td>");
                    Response.Write("<td>" + Shop.Bussiness.Statis.ProductCount(where_day) + "</td>");
                    Response.Write("<td><strong>" + FormatMoney(Order_Money) + "</strong></td>");
                    Response.Write("<td><strong>" + FormatMoney(Money_Product) + "</strong></td>");
                    Response.Write("<td><strong>" + FormatMoney(Money_Transport) + "</strong></td>");
                    Response.Write("<td><strong>" + FormatMoney(Money_Bill) + "</strong></td>");
                    Response.Write("<td><strong>" + FormatMoney(Money_Cost) + "</strong></td>");
                    Response.Write("<td><strong>" + FormatMoney(Order_Money - Money_Transport - Money_Bill - Money_Cost) + "</strong></td>");
                    Response.Write("</tr>");
                }
            }
            }
        }
        private string findpath(int D_From, int D_To,string Name)
        {
            string str = "";
            var d_i = 1; for (d_i = D_From; d_i <= D_To; d_i++)
            {
                str += ","+ Name +"_" + d_i;
            }
            return str;

        }
    }
}