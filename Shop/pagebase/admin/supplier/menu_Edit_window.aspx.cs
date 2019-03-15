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
    public partial class menu_Edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier_Menu model;
        protected Lebi_Supplier_Menu pmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_menu_edit", "编辑菜单"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_Supplier_Menu.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Menu();
                model.parentid = pid;
                model.Isshow = 1;
            }
            else
            {
                pid = model.parentid;
            }
            pmodel = B_Lebi_Supplier_Menu.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_Supplier_Menu();
                pmodel.Name = "-";
            }
        }


        public string GetParent(int sid)
        {
            List<Lebi_Supplier_Menu> ps = B_Lebi_Supplier_Menu.GetList("parentid=0", "Sort desc");
            string str = "";
            foreach (Lebi_Supplier_Menu p in ps)
            {
                string sel = "";
                if (p.id == sid)
                    sel = "selected";
                str += "<option value=" + p.id + "  " + sel + ">" + p.Name + "</option>";

                List<Lebi_Supplier_Menu> ss = B_Lebi_Supplier_Menu.GetList("parentid=" + p.id + "", "Sort desc");
                foreach (Lebi_Supplier_Menu s in ss)
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