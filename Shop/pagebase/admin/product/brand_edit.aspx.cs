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
    public partial class Brand_Edit : AdminPageBase
    {
        protected Lebi_Brand model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("brand_add", "添加品牌"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!EX_Admin.Power("brand_edit", "编辑品牌"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            model = B_Lebi_Brand.GetModel(id);
            if (model == null)
                model = new Lebi_Brand();

        }
    }
}