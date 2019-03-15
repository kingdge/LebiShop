using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
namespace Shop.Admin.theme
{
    public partial class SEO_Edit_window : AdminAjaxBase
    {
        protected Lebi_Theme_Page model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("seo_edit", "SEO设置"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);

            model = B_Lebi_Theme_Page.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Page();
                model.Type_id_PublishType = 120;
            }
            
        }
    }
}