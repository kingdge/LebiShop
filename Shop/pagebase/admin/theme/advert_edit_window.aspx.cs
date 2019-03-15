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
    public partial class Advert_Edit_window : AdminAjaxBase
    {
        protected Lebi_Theme_Advert model;
        protected Lebi_Theme theme;
        protected int id;
        protected int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("advert_edit", "设置广告位"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestInt("id", 0);
            tid = RequestTool.RequestInt("tid", 0);
            theme = B_Lebi_Theme.GetModel(tid);
            if (theme == null)
            {
                PageError();
                return;
            }
            model = B_Lebi_Theme_Advert.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Advert();
                model.Theme_id = theme.id;
            }


        }

    }
}