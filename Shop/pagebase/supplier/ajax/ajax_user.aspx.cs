using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
using System.Reflection;
using System.Collections.Specialized;


namespace Shop.Supplier.Ajax
{

    public partial class Ajax_user : ShopPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Shop.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 检查用户名是否注册
        /// </summary>
        public void CheckUserName()
        {
            string UserName = RequestTool.RequestSafeString("UserName");
            int count = B_Lebi_Supplier.Counts("UserName=lbsql{'" + UserName + "'}");
            if (count > 0)
                Response.Write("NO");
            else
                Response.Write("OK");
        }
        /// <summary>
        /// 用户这注册
        /// </summary>
        public void User_Reg()
        {
            string verifycode = RequestTool.RequestString("verifycode");
            string code = CurrentCheckCode;
            string UserName = RequestTool.RequestSafeString("UserName");
            //检查用户名存在
            if (CurrentUser.id == 0 || CurrentUser.IsAnonymous == 1)
            {
                Response.Write("{\"msg\":\"" + Tag("请登录") + "\"}");
                return;
            }
            if (SYS.Verifycode_SupplierRegister == "1")
            {
                if (code != verifycode)
                {
                    Response.Write("{\"msg\":\"" + Tag("验证码错误") + "\"}");
                    return;
                }
            }
            //检查用户名存在
            int count = B_Lebi_Supplier.Counts("UserName !='' and UserName=lbsql{'" + CurrentUser.UserName + "'}");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("商家已注册") + "\"}");
                return;
            }
            Lebi_Supplier model = B_Lebi_Supplier.GetModel("User_id=" + CurrentUser.id + "");
            if (model == null)
            {
                model = new Lebi_Supplier();
            }
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(RequestTool.RequestInt("Supplier_Group_id"));
            if (group == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Supplier.SafeBindForm(model);
            model.User_id = CurrentUser.id;
            model.UserName = CurrentUser.UserName;
            model.Email = RequestTool.RequestSafeString("Email");
            model.RealName = RequestTool.RequestSafeString("RealName");
            model.Company = RequestTool.RequestSafeString("Company");
            model.Name = RequestTool.RequestSafeString("Name");
            model.Name = "[{\"L\":\"" + Language.DefaultLanguage().Code + "\",\"C\":\"" + model.Name + "\"}]";
            model.SubName = RequestTool.RequestSafeString("SubName");
            model.MobilePhone = RequestTool.RequestSafeString("MobilePhone");
            model.Phone = RequestTool.RequestSafeString("Phone");
            model.QQ = RequestTool.RequestSafeString("QQ");
            model.Language = CurrentLanguage.Code;
            model.Time_Reg = DateTime.Now;
            model.Time_This = DateTime.Now;
            model.Time_Last = DateTime.Now;
            model.Count_Login = 1;
            model.IP_This = RequestTool.GetClientIP();
            model.IP_Last = RequestTool.GetClientIP();
            model.Type_id_SupplierStatus = 441;
            model.BillingDays = group.BillingDays;
            model.Money_Service = group.ServicePrice;
            model.Money_Margin = group.MarginPrice;
            model.Time_Begin = DateTime.Now;
            model.Time_End = DateTime.Now.AddDays(group.ServiceDays);
            if (model.id == 0)
            {
                B_Lebi_Supplier.Add(model);
                model.id = B_Lebi_Supplier.GetMaxId();
            }
            else
            {
                B_Lebi_Supplier.Update(model);
            }
            if (group.Verified_ids != "")
            {
                List<Lebi_Supplier_Verified> models = B_Lebi_Supplier_Verified.GetList("id in (" + group.Verified_ids + ")", "Sort desc");
                foreach (Lebi_Supplier_Verified m in models)
                {
                    string where = "Verified_id = " + m.id + " and Supplier_id = " + model.id + "";
                    Lebi_Supplier_Verified_Log log = B_Lebi_Supplier_Verified_Log.GetModel(where);
                    if (log == null)
                    {
                        log = new Lebi_Supplier_Verified_Log();
                    }
                    //log = B_Lebi_Supplier_Verified_Log.SafeBindForm(log);
                    log.ImageUrl = RequestTool.RequestSafeString("Image" + m.id);
                    log.Type_id_SupplierVerifiedStatus = 9020;
                    log.Time_Add = DateTime.Now;
                    log.Verified_id = m.id;
                    log.Supplier_id = model.id;
                    if (log.id == 0)
                    {
                        B_Lebi_Supplier_Verified_Log.Add(log);
                    }
                    else
                    {
                        B_Lebi_Supplier_Verified_Log.Update(log);
                    }
                }
            }
            EX_Supplier.SupplierRegister(model);//触发事件
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 供应商登录
        /// </summary>
        public void User_Login()
        {
            string msg = "";
            string userName = RequestTool.RequestSafeString("userName");
            string UserPWD = RequestTool.RequestSafeString("UserPWD");
            string code = RequestTool.RequestString("code");
            string logintype = RequestTool.RequestString("logintype", "supplier");
            int saveusername = RequestTool.RequestInt("saveusername", 0);
            string loginerror = "false";
            string Ststus = "";
            if (SYS.Verifycode_SupplierLogin == "1")
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
            //UserPWD = EX_Supplier.MD5(UserPWD);
            if (EX_User.UserLogin(userName, UserPWD))
            {
                Lebi_User CurrentUser = B_Lebi_User.GetModel("UserName=lbsql{'" + userName + "'}");
                if (EX_Supplier.Login(CurrentUser, logintype, 0, out msg))
                {
                    if (saveusername == 1)
                    {
                        Shop.Tools.CookieTool.SetCookieString("SupplierUserName", userName, 60 * 24);
                        Shop.Tools.CookieTool.SetCookieString("saveusername", "1", 60 * 24);
                    }
                    else
                    {
                        Shop.Tools.CookieTool.SetCookieString("SupplierUserName", "", -1);
                        Shop.Tools.CookieTool.SetCookieString("saveusername", "", -1);
                    }
                    Log.Add("登陆系统", "Login", CurrentUser.id.ToString(), CurrentUser.UserName);
                    Response.Write("OK");
                    return;
                }
                else
                {
                    Ststus = Language.Tag("未审核", CurrentLanguage.Code);
                    Lebi_Supplier_User model_supplier_user = B_Lebi_Supplier_User.GetList("User_id = " + CurrentUser.id + "", "").FirstOrDefault();
                    if (model_supplier_user != null)
                    {
                        switch (model_supplier_user.Type_id_SupplierUserStatus)
                        {
                            case 9010:
                                Ststus = Language.Tag("未审核", CurrentLanguage.Code);
                                break;
                            case 9012:
                                Ststus = Language.Tag("已停用", CurrentLanguage.Code);
                                break;
                        }
                    }
                    msg = Language.Tag("账号状态异常：", CurrentLanguage.Code) + Ststus;
                }
            }
            else
            {
                msg = Language.Tag("用户名或密码错误", CurrentLanguage.Code);
                if (SYS.Verifycode_SupplierLogin == "1")
                {
                    HttpContext.Current.Session["loginerror"] = "true";
                }
                Log.Add("登陆系统", "Login", "", CurrentUser, "[" + userName + "]用户名或密码错误");
            }
            Response.Write(msg);
        }
        /// <summary>
        /// 管理员退出登录
        /// </summary>
        /// <returns></returns>
        public void LoginOut()
        {
            try
            {
                Log.Add("退出系统", "Login");
                CookieTool.DeleteCookie("User");
                Response.Write("OK");
            }
            catch
            {

            }

        }

    }
}