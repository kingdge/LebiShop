using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Shop.Bussiness
{
    public class AdminCustomPageBase : AdminBase
    {
        protected string Table = "";
        protected string Where = "";
        protected string Order = "";
        //protected int PageSize = 1;
        protected int pageindex = 1;
        protected int RecordCount = 0;
        protected string id;
        protected BaseConfig config;
        protected void Page_Load(object sender, EventArgs e)
        {
            config = ShopCache.GetBaseConfig();
        }
        public int Rint(string KeyName)
        {
            return RequestTool.RequestInt(KeyName, 0);
        }
        public string Rstring(string KeyName)
        {
            return StringTool.HtmlFiltrate(RequestTool.RequestString(KeyName));
        }

    }


}
