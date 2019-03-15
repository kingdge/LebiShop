using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class upload : AdminAjaxBase
    {
        protected string inputname = "";
        protected string i = "";
        protected BaseConfig model;
        protected void Page_Load(object sender, EventArgs e)
        {
            inputname = RequestTool.RequestString("n");
            i = RequestTool.RequestString("i");
            
            if (inputname == "")
                inputname = "Images";
            B_BaseConfig bconfig = new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
        }
    }
}