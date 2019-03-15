using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.inc
{
    public partial class pay_window : ShopPage
    {
        public decimal money = 0;
        public Lebi_Currency DefaultCurrency;
        public int keyid = 0;
        public string tablename = "";
        public void LoadPage()
        {
            
            if (CurrentUser.id == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("请先登陆") + "\",\"url\":\"" + URL("P_Login", "") + "\"}");
                return;
            }
            money = RequestTool.RequestDecimal("money");
            DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (DefaultCurrency == null)
                DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();
            keyid = RequestTool.RequestInt("keyid");
            tablename = RequestTool.RequestString("tablename");
        }
    }
}