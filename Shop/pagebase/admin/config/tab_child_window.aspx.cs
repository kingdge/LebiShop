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
    public partial class Tab_Child_window : AdminAjaxBase
    {
        protected int id;
        protected List<Lebi_Pro_Type> models;
        protected Lebi_Tab tab;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestInt("id", 0);
            tab = B_Lebi_Tab.GetModel(id);
            if (tab == null)
            {
                Response.Write("参数错误");
                Response.End();
                return;
            }
            //string where = "Parentid=0";
            //models = B_Lebi_Pro_Type.GetList(where, "sort desc");

        }

        public bool ISselected(int id, int tabid)
        {
            int count = B_Lebi_TabChild.Counts("tabid=" + tabid + " and protypeid="+id);
            if (count > 0)
                return true;
            return false;
        }
    }
}