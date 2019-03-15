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
    public partial class menu : AdminPageBase
    {
        protected List<Lebi_Supplier_Menu> models;

        protected int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_menu_edit", "编辑菜单"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            models = GetMenu(0);

        }

        public List<Lebi_Supplier_Menu> GetMenu(int pid)
        {
            string where = "";
            where = "parentid=" + pid + "";
            List<Lebi_Supplier_Menu> ls = B_Lebi_Supplier_Menu.GetList(where, "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Supplier_Menu>();
            return ls;
        }

        public bool Check(int lid)
        {
            /*
            List<Lebi_Admin_Power> cps = (from m in ps
                      where m.Admin_Limit_id == lid
                      select m).ToList();
            if (cps.Count>0)
                return true;
             * */
            return false;
        }

    }
}