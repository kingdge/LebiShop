using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
namespace Shop.Admin.theme
{
    public partial class ThemeOnline : AdminPageBase
    {
        protected List<LBAPI_ThemeOnline> models;
        protected string PageString;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_onlist", "在线模板"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            models = Shop.LebiAPI.Service.Instanse.Theme_Online();
        }

        public bool Ishave(string code)
        {
            int count = B_Lebi_Theme.Counts("Code='"+code+"'");
            if (count == 0)
                return false;
            return true;
        }
    }
}