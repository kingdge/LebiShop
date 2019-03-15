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

namespace Shop.Admin.config
{
    public partial class lebionline : AdminPageBase
    {
        public string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            url = "username=" + SYS.LicenseUserName + "&password=" + EX_User.MD5(SYS.LicenseUserName + System.DateTime.Now.Day) + "&vc=" + EX_User.MD5(SYS.LicensePWD);
        }


    }
}