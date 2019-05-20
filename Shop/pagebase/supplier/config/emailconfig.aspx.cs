using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;

namespace Shop.Supplier.Config
{
    public partial class EmailConfig : SupplierPageBase
    {
        protected BaseConfig_Supplier model;
        protected string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_emailconfig", "邮件设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }

            B_BaseConfig_Supplier bconfig = new B_BaseConfig_Supplier();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig_Supplier(CurrentSupplier.id);
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