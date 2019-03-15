using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using System.Web.Script.Serialization;
namespace Shop.Admin.Config
{
    public partial class License : AdminPageBase
    {
        protected BaseConfig model;
        protected string password = "";
        protected bool IsBussiness = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_BaseConfig bconfig=new B_BaseConfig();
            model = bconfig.LoadConfig();
            
            if (!EX_Admin.Power("License_set", "账户设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            password = model.LicensePWD;
            if (password != "")
                password = "******";
            
            string[] arr = model.LicenseDomain.Split(',');
            string res = "";
            foreach (string str in arr)
            {
                if (str == "")
                    continue;
                if (res == "")
                    res = str;
                else
                    res += ","+str;
            }
            model.LicenseDomain = res;


            string ps = SYS.LicensePackage;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<UserService> services;
            try
            {
                services = jss.Deserialize<List<UserService>>(ps);
            }
            catch
            {
                services = new List<UserService>();
            }
            if (services == null)
                services = new List<UserService>();
            if (services.Count > 0)
                IsBussiness = true;
        }
    }
}