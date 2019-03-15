using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class Tab_Child : AdminPageBase
    {
        protected int id;
        protected List<Lebi_TabChild> models;
        protected Lebi_Tab tab;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            id = RequestTool.RequestInt("id", 0);
            tab = B_Lebi_Tab.GetModel(id);
            if (tab == null)
            {
                Response.Write("参数错误");
                Response.End();
                return;
            }
            string where = "tabid=" + id;
            models = B_Lebi_TabChild.GetList(where, "sort desc");

        }

        public string tabname(int id)
        {
            Lebi_Pro_Type t = B_Lebi_Pro_Type.GetModel(id);
            if (t == null)
            {
                return "";
            }
            return Language.Content(t.Name, CurrentLanguage.Code);
        }
    }
}