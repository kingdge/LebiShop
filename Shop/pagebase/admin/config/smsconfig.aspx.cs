using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using System.Web.Configuration;
using com.todaynic.ScpClient;
using cn.eibei.xml;

namespace Shop.Admin.storeConfig
{
    public partial class SMSConfig : AdminPageBase
    {
        protected BaseConfig model;
        protected SMSClient smsClient;
        protected string account;
        protected string SMS_password;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("smsconfig_edit", "手机短信设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            B_BaseConfig bconfig=new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
            SMS_password = model.SMS_password;
            try
            {
                if (model.SMS_server == "0" && model.SMS_state == "1")
                {
                    smsClient = new SMSClient("sms.todaynic.com", Convert.ToInt32(model.SMS_serverport), model.SMS_user, model.SMS_password);
                    account = smsClient.getBalance();
                }
            }
            catch
            {
 
            }
            if (SMS_password != "")
                SMS_password = "******";
        }
    }
}