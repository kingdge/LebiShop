using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin.storeConfig
{
    public partial class EmailConfig : AdminPageBase
    {
        protected BaseConfig model;
        protected string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("emailconfig_edit", "编辑邮件设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            B_BaseConfig bconfig = new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
            password = model.MailPassWord;
            if (password != "")
                password = "******";
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