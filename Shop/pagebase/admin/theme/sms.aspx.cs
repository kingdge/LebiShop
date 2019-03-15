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

namespace Shop.Admin.theme
{
    public partial class SMSTPL : AdminPageBase
    {
        protected string type;
        protected BaseConfig model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_sms_edit", "编辑短信模板"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            type = RequestTool.RequestString("type");
            if (type == "") type = "user";
            model = ShopCache.GetBaseConfig();
        }

    }
}