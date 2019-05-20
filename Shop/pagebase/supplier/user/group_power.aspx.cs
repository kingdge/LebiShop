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
    public partial class Group_power : SupplierPageBase
    {
        protected List<Lebi_Supplier_Limit> models;
        protected Lebi_Supplier_UserGroup group;
        protected List<Lebi_Supplier_Power> ps;
        protected List<Lebi_Supplier_Power> purls;
        protected int count = 0;
        protected Lebi_Supplier_Limit defaultparent;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_power_edit", "编辑分组权限"))
            {
                AjaxNoPower();
                return;
            }
            models = GetLimit(0);
            int id = RequestTool.RequestInt("id", 0);
            group = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (group == null)
            {
                PageError();
            }
            ps = B_Lebi_Supplier_Power.GetList("Supplier_Group_id=" + group.id + " and Url=''", "");
            defaultparent = B_Lebi_Supplier_Limit.GetModel("Code='default'");
            if (defaultparent != null)
                count = B_Lebi_Supplier_Limit.Counts("parentid = " + defaultparent.id + ""); //未分组
        }

        public List<Lebi_Supplier_Limit> GetLimit(int pid, string pcode = "")
        {
            string where = "";
            where = "parentid=" + pid + "";
            if (pcode != "")
                where = "parentid in (select id from [Lebi_Supplier_Limit] where Code='" + pcode + "')";
            if (pid == 0)
                where += " and Code<>'default'";
            else
                where += " and id in (select Supplier_Limit_id from Lebi_Supplier_Power where Supplier_Group_id=" + CurrentSupplierGroup.id + ")";
            List<Lebi_Supplier_Limit> ls = B_Lebi_Supplier_Limit.GetList(where, "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Supplier_Limit>();
            return ls;
        }

        public bool Check(int lid)
        {

            if (("," + group.Limit_ids + ",").Contains("," + lid + ","))
                return true;
            return false;
        }

    }
}