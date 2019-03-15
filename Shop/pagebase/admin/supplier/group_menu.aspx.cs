using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class group_menu : AdminPageBase
    {
        protected List<Lebi_Supplier_Menu> models;
        protected Lebi_Supplier_Group group;
        protected int count = 0;
        protected string ids = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            models = GetMenu(0);
            int id = RequestTool.RequestInt("id", 0);
            group = B_Lebi_Supplier_Group.GetModel(id);
            if (group == null)
            {
                PageError();
            }
            ids = "," + group.Menu_ids + ",";
        }

        public List<Lebi_Supplier_Menu> GetMenu(int pid)
        {
            string where = "";
            where = "parentid=" + pid + " and Isshow=1";
            List<Lebi_Supplier_Menu> ls = B_Lebi_Supplier_Menu.GetList(where, "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Supplier_Menu>();
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