using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Shop.Admin.Ajax
{
    public partial class Ajax_login : PageBase
    {
        protected Site site;
        protected Lebi_Language_Code CurrentLanguage;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentLanguage = Language.CurrentLanguage();
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        public void AdminLogin()
        {
            string userName = RequestTool.RequestString("userName");
            string UserPWD = RequestTool.RequestString("UserPWD");
            string code = RequestTool.RequestString("code");
            int saveusername = RequestTool.RequestInt("saveusername", 0);
            int type = RequestTool.RequestInt("type", 0);
            string loginerror = "false";
            if (SYS.Verifycode_AdminLogin == "1")
            {
                try
                {
                    loginerror = (string)HttpContext.Current.Session["loginerror"];
                }
                catch
                {
                    loginerror = "false";
                }
                if (loginerror == "true")
                {
                    if (CurrentCheckCode != code)
                    {
                        Response.Write(Language.Tag("验证码错误", CurrentLanguage.Code));
                        return;
                    }
                }
            }
            UserPWD = EX_Admin.MD5(UserPWD);
            if (EX_Admin.AdminLogin(userName, UserPWD))
            {
                Lebi_Administrator admin = EX_Admin.CurrentAdmin();
                admin.Time_Last = admin.Time_This;
                admin.IP_Last = admin.IP_This;
                admin.Time_This = DateTime.Now;
                admin.Count_Login++;
                admin.IP_This = RequestTool.GetClientIP();
                B_Lebi_Administrator.Update(admin);
                Log.Add("登陆系统", "Login", admin.id.ToString(), EX_Admin.CurrentAdmin());
                HttpContext.Current.Session["loginerror"] = "false";
                Response.Cookies.Add(new HttpCookie("AdminLoginError", ""));
                if (saveusername == 1)
                {
                    Response.Cookies.Add(new HttpCookie("saveusername", "1"));
                    Response.Cookies.Add(new HttpCookie("AdminUserName", userName));
                }
                else
                {
                    Response.Cookies.Add(new HttpCookie("saveusername", ""));
                    Response.Cookies.Add(new HttpCookie("AdminUserName", ""));
                }
                if (type == 1)
                {
                    site = new Site();
                    string url = "<script type=\"text/javascript\">window.location='" + site.AdminPath + "/default.aspx';</script>";
                    Response.Write(url);
                    Response.End();
                }
                else
                {
                    Response.Write("OK");
                }
                return;
            }
            if (SYS.Verifycode_AdminLogin == "1")
            {
                HttpContext.Current.Session["loginerror"] = "true";
            }
            Log.Add("登陆系统", "Login", "", EX_Admin.CurrentAdmin(), "[" + userName + "]用户名或密码错误");
            Response.Write(Language.Tag("用户名或密码错误", CurrentLanguage.Code));
            return;
        }
        /// <summary>
        /// 管理员退出登录
        /// </summary>
        /// <returns></returns>
        public void LoginOut()
        {
            try
            {
                Log.Add("退出系统", "Login", EX_Admin.CurrentAdmin().id.ToString(), EX_Admin.CurrentAdmin());
                CookieTool.DeleteCookie("Master");
                Response.Write("OK");
            }
            catch
            {

            }

        }

        /// <summary>
        /// 设置授权
        /// 未登录系统时
        /// 为保证安全，此方法只能在未设置时才能使用
        /// </summary>
        public void Licence_Set()
        {
            BaseConfig bcf = ShopCache.GetBaseConfig();
            if (bcf.LicenseUserName != "" && bcf.LicensePWD != "")
            {
                Response.Write("{\"msg\":\"" + Language.Tag("请登录后操作", CurrentLanguage.Code) + "\"}");
                return;
            }
            string LicenseUserName = RequestTool.RequestString("LicenseUserName");
            string LicensePWD = RequestTool.RequestString("LicensePWD");
            string LicenseDomain = RequestTool.RequestString("LicenseDomain").ToLower();
            BaseConfig cf = new BaseConfig();
            cf.LicensePWD = LicensePWD;
            cf.LicenseUserName = LicenseUserName;
            cf.LicenseDomain = LicenseDomain;
            B_BaseConfig dob = new B_BaseConfig();

            //============================================
            //获取服务端权限
            LBAPI_Licencse api;
            try
            {
                string res = Shop.LebiAPI.Service.Instanse.License_Set("SetUser", "username=" + LicenseUserName + "&pwd=" + LicensePWD + "&domain=" + LicenseDomain + "&Version=" + ShopCache.GetBaseConfig().Version + "&Version_Son=" + ShopCache.GetBaseConfig().Version_Son);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                api = jss.Deserialize<LBAPI_Licencse>(res);

            }
            catch (Exception)
            {
                api = new LBAPI_Licencse();
            }
            if (api.msg == "OK")
            {
                cf.LicenseMD5 = api.md5 == null ? "" : api.md5;
                cf.LicenseString = api.data == null ? "" : api.data;
                cf.LicensePackage = api.servicepackage == null ? "" : api.servicepackage;
                dob.SaveConfig(cf);
            }
            Response.Write("{\"msg\":\"" + api.msg + "\"}");
        }
    }
}