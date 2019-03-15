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
    public partial class limit_edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier_Limit model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Supplier_Limit.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Limit();
            }
        }

        public List<Lebi_Supplier_Limit> GetLimit(int pid)
        {
            List<Lebi_Supplier_Limit> ls = B_Lebi_Supplier_Limit.GetList("parentid=" + pid + "", "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Supplier_Limit>();
            return ls;
        }
    }
}