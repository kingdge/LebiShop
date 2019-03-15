using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class Tag_Edit_window : AdminAjaxBase
    {
        protected Lebi_Pro_Tag model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("pro_tag_add", "添加商品标签"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("pro_tag_edit", "编辑商品标签"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Pro_Tag.GetModel(id);
            if (model == null)
                model = new Lebi_Pro_Tag();

        }
    }
}