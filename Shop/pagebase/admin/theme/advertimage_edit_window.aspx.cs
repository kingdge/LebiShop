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
    public partial class AdvertImage_Edit_window : AdminAjaxBase
    {
        protected Lebi_Advert model;
        protected Lebi_Theme theme;
        protected Lebi_Theme_Advert advert;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("advertimage_edit", "编辑广告"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int aid = RequestTool.RequestInt("aid");
            advert = B_Lebi_Theme_Advert.GetModel(aid);
            if (advert == null)
            {
                PageError();
                return;
            }
            theme = B_Lebi_Theme.GetModel(advert.Theme_id);
            model = B_Lebi_Advert.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Advert();
                model.Theme_Advert_Code = advert.Code;
                model.Theme_Advert_id = advert.id;
            
            }


        }

    }
}