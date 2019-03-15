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
namespace Shop.Admin.config
{
    public partial class SetLanguage_Edit_window : AdminAjaxBase
    {
        protected string table;
        protected string ids;
        protected string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            table = RequestTool.RequestString("table");
            ids = RequestTool.RequestString("ids");
            id = RequestTool.RequestString("id");
            //if (table == "Theme_Advert")
            //{
            //    if (!EX_Admin.Power("advertimage_edit", "编辑广告"))
            //    {
            //        PageNoPower();
            //    }
            //}
        }

    }
}