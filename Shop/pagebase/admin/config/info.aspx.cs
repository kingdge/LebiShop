using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin
{
    public partial class Info : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("weixin", "系统信息"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
        }
        protected bool chkobj(string obj)
        {
            try
            {
                object meobj = Server.CreateObject(obj);
                return (true);
            }
            catch (Exception objex)
            {
                return (false);
            }
        }
    }
}