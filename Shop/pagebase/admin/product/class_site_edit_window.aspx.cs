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
    public partial class class_site_edit_window : AdminAjaxBase
    {
        protected string ids;
        protected void Page_Load(object sender, EventArgs e)
        {
            ids = RequestTool.RequestString("ids");
            if (!EX_Admin.Power("pro_type_edit", "编辑商品分类"))
            {
                WindowNoPower();
            }
        }
    }
}