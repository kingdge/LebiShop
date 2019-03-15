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
    public partial class Admin_group_indexmenu : AdminPageBase
    {
        protected List<Lebi_Menu> models;
        protected Lebi_Admin_Group group;
        protected int count = 0;
        protected string ids = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_group_edit", "编辑权限组"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            models = GetMenu(0);
            int id = RequestTool.RequestInt("id", 0);
            group = B_Lebi_Admin_Group.GetModel(id);
            if (group == null)
            {
                PageReturnMsg = PageErrorMsg();
            }
            ids = "," + group.Menu_ids_index + ",";
        }

        public List<Lebi_Menu> GetMenu(int pid)
        {
            string where = "";
            where = "parentid=" + pid + " and Isshow=1";
            List<Lebi_Menu> ls = B_Lebi_Menu.GetList(where, "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Menu>();
            return ls;
        }

        public bool Check(int lid)
        {
            if (ids.Contains("," + lid + ","))
                return true;
            return false;
        }

    }
}