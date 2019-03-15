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
    public partial class Admin_Group_power : AdminPageBase
    {
        protected List<Lebi_Admin_Limit> models;
        protected Lebi_Admin_Group group;
        protected List<Lebi_Admin_Power> ps;
        protected List<Lebi_Admin_Power> purls;
        protected int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_group_edit", "编辑权限组"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            models = GetLimit(0);
            int id = RequestTool.RequestInt("id", 0);
            group = B_Lebi_Admin_Group.GetModel(id);
            if (group == null)
            {
                PageReturnMsg = PageErrorMsg();
            }
            ps = B_Lebi_Admin_Power.GetList("Admin_Group_id=" + group.id + " and Url=''", "");
            purls = B_Lebi_Admin_Power.GetList("Admin_Group_id=" + group.id + " and Url!=''", "");
            count = B_Lebi_Admin_Limit.Counts("parentid = 222"); //未分组
        }

        public List<Lebi_Admin_Limit> GetLimit(int pid)
        {
            string where = "";
            where = "parentid=" + pid + "";
            if (pid == 0)
                where += " and Code<>'default'";
            List<Lebi_Admin_Limit> ls = B_Lebi_Admin_Limit.GetList(where, "Sort desc");
            if (ls == null)
                ls = new List<Lebi_Admin_Limit>();
            return ls;
        }

        public bool Check(int lid)
        {

            List<Lebi_Admin_Power> cps = (from m in ps
                      where m.Admin_Limit_id == lid
                      select m).ToList();
            if (cps.Count>0)
                return true;
            return false;
        }

    }
}