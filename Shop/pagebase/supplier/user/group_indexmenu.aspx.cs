using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.user
{
    public partial class group_indexmenu : SupplierPageBase
    {
        protected List<Lebi_Supplier_Menu> models;
        protected Lebi_Supplier_UserGroup group;
        protected int count = 0;
        protected string ids = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_menu_edit", "编辑分组菜单"))
            {
                AjaxNoPower();
                return;
            }
            models = GetMenu(0);
            int id = RequestTool.RequestInt("id", 0);
            group = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (group == null)
            {
                PageError();
            }
            ids = "," + group.Menu_ids_index + ",";
        }

        public List<Lebi_Supplier_Menu> GetMenu(int pid)
        {
            CurrentSupplierGroup.Menu_ids = CurrentSupplierGroup.Menu_ids == "" ? "0" : CurrentSupplierGroup.Menu_ids;
            string where = "";
            where = "parentid=" + pid + " and Isshow=1 and id in (" + CurrentSupplierGroup.Menu_ids + ")";
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