using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class admin_limit_edit_window : AdminAjaxBase
    {
        protected Lebi_Admin_Limit model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_limit_edit", "编辑权限"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Admin_Limit.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Admin_Limit();
            }
        }

        public List<Lebi_Admin_Limit> GetLimit(int pid)
        {
            List<Lebi_Admin_Limit> ls = B_Lebi_Admin_Limit.GetList("parentid=" + pid + "", "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Admin_Limit>();
            return ls;
        }
    }
}