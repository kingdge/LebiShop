using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class Area_Edit_window : AdminAjaxBase
    {
        protected Lebi_Area model;
        protected int pid;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            pid = RequestTool.RequestInt("pid", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("area_add", "添加地名"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("area_edit", "编辑地名"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Area.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Area();
                model.Parentid = pid;
            }
        }

        protected Lebi_Area Getarea(int id)
        {
            Lebi_Area area = B_Lebi_Area.GetModel(id);
            if (area == null)
                area = new Lebi_Area();
            return area;
        }
    }
}