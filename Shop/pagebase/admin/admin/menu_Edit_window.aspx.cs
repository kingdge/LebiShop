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
    public partial class menu_Edit_window : AdminAjaxBase
    {
        protected Lebi_Menu model;
        protected Lebi_Menu pmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_menu_edit", "编辑菜单"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_Menu.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Menu();
                model.parentid = pid;
                model.Isshow = 1;
            }
            else
            {
                pid = model.parentid;
            }
            pmodel = B_Lebi_Menu.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_Menu();
                pmodel.Name = "-";
            }
        }


        public string GetParent(int sid)
        {
            List<Lebi_Menu> ps = B_Lebi_Menu.GetList("parentid=0", "Sort desc");
            string str = "";
            foreach (Lebi_Menu p in ps)
            {
                string sel = "";
                if (p.id == sid)
                    sel = "selected";
                str += "<option value=" + p.id + "  " + sel + ">" + p.Name + "</option>";

                List<Lebi_Menu> ss = B_Lebi_Menu.GetList("parentid=" + p.id + "", "Sort desc");
                foreach (Lebi_Menu s in ss)
                {
                    sel = "";
                    if (s.id == sid)
                        sel = "selected";
                    str += "<option value=" + s.id + "  " + sel + "> - " + s.Name + "</option>";
                }
            }
            return str;
        }


        public string GetURL()
        {
            if (model.IsSYS == 1)
                return model.URL;
            else
                return "<input type=\"text\" class=\"input\" shop=\"true\" name=\"URL\" id=\"URL\" value=\""+model.URL+"\" style=\"width: 400px;\" />";
        }
    }
}