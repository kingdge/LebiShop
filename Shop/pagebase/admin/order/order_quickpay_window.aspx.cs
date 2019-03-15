using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
using System.Security.Cryptography;
namespace Shop.Admin.order
{
    public partial class Order_QuickPay_window : AdminAjaxBase
    {
        protected string QuickPayURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int orderid = RequestTool.RequestInt("orderid", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(orderid);
            if (order != null)
            {
                order.KeyCode = GetMd5Str(order.Code);
                B_Lebi_Order.Update(order);
            }
            QuickPayURL = "http://" + RequestTool.GetRequestDomain() + WebPath.TrimEnd('/') + "/OrderQuickPay/?k="+ order.KeyCode;
        }
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
}