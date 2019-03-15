using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.IO;
namespace Shop.Admin
{
    public partial class updatepage : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Shop.Bussiness.SystemTheme.CreateAdmin("admin", "");
            //Shop.Bussiness.SystemTheme.CreateAdmin("supplier", "");
            //Shop.Bussiness.SystemTheme.CreateAdmin("inc", "");
            //Shop.Bussiness.SystemTheme.CreateAdmin("ajax", "");
            Shop.Bussiness.SystemTheme.CreateSystemPage();
            Response.Write("OK");
        }

    }
}