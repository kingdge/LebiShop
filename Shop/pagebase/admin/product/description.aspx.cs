using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class Description : AdminPageBase
    {
        protected Lebi_ProDesc model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("prodesc_edit", "编辑商品通用描述"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            model = B_Lebi_ProDesc.GetList("","").FirstOrDefault();
            if (model == null)
                model = new Lebi_ProDesc();
        }
        
    }
}