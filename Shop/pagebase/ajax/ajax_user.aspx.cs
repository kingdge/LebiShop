using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Collections.Specialized;
using System.Drawing;

namespace Shop.Ajax
{
    /// 这个文件放置
    /// 登录前进行的动作
    public partial class Ajax_user : ShopPage
    {

        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            string action = LB.Tools.RequestTool.RequestString("__Action");
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
            int count = B_Lebi_User.Counts("UserName=lbsql{'" + UserName + "'}");
            if (count > 0)
                Response.Write("NO");
            else
                Response.Write("OK");
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        public void User_Reg()
        {
            string url = "";
            bool Checkmobilephone = false;
            bool Checkemail = false;
            string MobilePhone = RequestTool.RequestSafeString("MobilePhone");
            string Email = RequestTool.RequestSafeString("Email");
            try
            {
                Lebi_UserLevel defaultlevel = B_Lebi_UserLevel.GetModel("Grade>0 order by Grade asc");
                if (defaultlevel == null)
                {
                    defaultlevel = new Lebi_UserLevel();
                }
                if (defaultlevel.RegisterType == 0) //关闭注册
                {
                    Response.Write("{\"msg\":\"" + Tag("会员注册已关闭") + "\"}");
                    return;
                }
                if (SYS.UserRegCheckedType.Contains("mobilephone"))
                {
                    Checkmobilephone = true;
                    string MobilePhone_checkcode = RequestTool.RequestSafeString("MobilePhone_checkcode");
                    try
                    {
                        string phonecheckcode = (string)Session["phonecheckcode"];//phonecheckcode
                        if (phonecheckcode != (MobilePhone + MobilePhone_checkcode))
                        {
                            Response.Write("{\"msg\":\"" + Tag("手机验证码错误") + phonecheckcode + "(" + MobilePhone + MobilePhone_checkcode + ")\"}");
                            return;
                        }
                        if (SYS.IsMobilePhoneMutiReg == "0")
                        {
                            int phonecount = B_Lebi_User.Counts("MobilePhone=lbsql{'" + MobilePhone + "'} and IsCheckedMobilePhone=1");
                            if (phonecount > 0)
                            {
                                Response.Write("{\"msg\":\"" + Tag("此手机号已经注册") + "\"}");
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("{\"msg\":\"" + Tag("手机验证码错误") + "" + ex.ToString() + "\"}");
                        return;
                    }
                }
                if (SYS.UserRegCheckedType.Contains("email"))
                {
                    Checkemail = true;
                    string Email_checkcode = RequestTool.RequestSafeString("Email_checkcode");
                    try
                    {
                        string emailcheckcode = (string)Session["emailcheckcode"];
                        if (emailcheckcode != (Email + Email_checkcode))
                        {
                            Response.Write("{\"msg\":\"" + Tag("邮件验证码错误") + "\"}");
                            return;
                        }
                        int emailcount = B_Lebi_User.Counts("Email=lbsql{'" + Email + "'} and IsCheckedEmail=1");
                        if (emailcount > 0)
                        {
                            Response.Write("{\"msg\":\"" + Tag("此邮箱已经注册") + "\"}");
                            return;
                        }
                    }
                    catch
                    {
                        Response.Write("{\"msg\":\"" + Tag("邮件验证码错误") + "\"}");
                        return;
                    }
                }
                if (SYS.Verifycode_UserRegister == "1")
                {
                    if (Checkemail == false && Checkmobilephone == false)
                    {
                        string verifycode = RequestTool.RequestString("verifycode");
                        if (CurrentCheckCode != verifycode)
                        {
                            Response.Write("{\"msg\":\"" + Tag("验证码错误") + "\"}");
                            return;
                        }
                    }
                }
                string UserName = RequestTool.RequestSafeString("UserName");
                string PWD = RequestTool.RequestSafeString("Password");
                string token = RequestTool.RequestString("token");
                if (UserName == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请输入用户名") + "\"}");
                    return;
                }
                if (PWD == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请输入密码") + "\"}");
                    return;
                }
                //检查用户名存在
                int count = B_Lebi_User.Counts("UserName=lbsql{'" + UserName + "'}");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("用户名已注册") + "\"}");
                    return;
                }
                var nv = CookieTool.GetCookie("parentuser");
                int parentuserid = 0;
                if (!string.IsNullOrEmpty(nv.Get("id")))
                {
                    string parentuserid_ = nv.Get("id");
                    int.TryParse(parentuserid_, out parentuserid);
                }
                if (parentuserid == 0)
                {
                    parentuserid = RequestTool.RequestInt("parentuserid", 0);
                }
                if (parentuserid != 0)
                {
                    Lebi_User puser = B_Lebi_User.GetModel("id=" + parentuserid + "");
                    if (puser != null)
                    {
                        if (defaultlevel.RegisterType == 2)  //邀请注册
                        {
                            Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(puser.UserLevel_id);
                            if (userlevel != null)
                            {
                                if (userlevel.RegisterType == 0) //关闭注册
                                {
                                    Response.Write("{\"msg\":\"" + Tag("会员注册已关闭") + "\"}");
                                    return;
                                }
                            }
                        }
                        if (SYS.IsUsedAgent == "1")
                        {
                            if (Shop.LebiAPI.Service.Instanse.Check("plugin_agent"))
                            {
                                parentuserid = puser.id;
                                puser.Count_sonuser++;
                                B_Lebi_User.Update(puser);
                            }
                        }
                    }
                }else
                {
                    if (defaultlevel.RegisterType == 2)  //邀请注册
                    {
                        Response.Write("{\"msg\":\"" + Tag("请输入邀请码") + "\"}");
                        return;
                    }
                }
                Lebi_User model = new Lebi_User();
                B_Lebi_User.SafeBindForm(model);
                model.NickName = model.UserName;
                model.Password = EX_User.MD5(PWD);
                model.UserLevel_id = defaultlevel.id;
                model.Time_Reg = DateTime.Now;
                model.Time_This = DateTime.Now;
                model.Time_Last = DateTime.Now;
                model.Count_Login = 1;
                model.IP_This = RequestTool.GetClientIP();
                model.IP_Last = RequestTool.GetClientIP();
                if (Checkemail)
                    model.IsCheckedEmail = 1;
                if (Checkmobilephone)
                    model.IsCheckedMobilePhone = 1;
                model.User_id_parent = parentuserid;
                if (CurrentSite != null)
                    model.Site_id = CurrentSite.id;
                if (CurrentLanguage != null)
                    model.Language = CurrentLanguage.Code;
                model.Area_id = RequestTool.RequestInt("Area_id", 0);
                int DefaultUserEndDays = 0;
                int.TryParse(SYS.DefaultUserEndDays, out DefaultUserEndDays);
                model.Time_End = System.DateTime.Now.AddDays(DefaultUserEndDays);
                Lebi_User user = EX_User.UserReg(model, PWD);
                try
                {
                    //发送邮件
                    if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("zhuce") || ShopCache.GetBaseConfig().AdminMailSign.ToLower().Contains("register"))
                    {
                        Shop.Bussiness.Email.SendEmail_newuser(user);
                    }
                    //发送短信
                    if (ShopCache.GetBaseConfig().SMS_sendmode.Contains("SMSTPL_newuser") || ShopCache.GetBaseConfig().SMS_sendmode.Contains("SMSTPL_Admin_newuser"))
                    {
                        SMS.SendSMS_newuser(user);
                    }
                }
                catch { }
                url = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
                if (EX_User.MD5(SYS.InstallCode + url) != token)
                    url = URL("P_Index", "");
                if (url.ToLower().IndexOf("http") > -1 || url.ToLower().IndexOf(URL("P_Register", "").ToLower()) > -1 || url.ToLower().IndexOf(URL("P_Login", "").ToLower()) > -1 || url == "")
                {
                    url = URL("P_Index", "");
                }
                Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
            }
            catch (Exception e)
            {
                SystemLog.Add("User_Reg-" + e.ToString());
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        public void User_Login()
        {
            string verifycode = RequestTool.RequestString("verifycode");

            string UserName = RequestTool.RequestSafeString("UserName");
            string PWD = RequestTool.RequestSafeString("Password");
            string token = RequestTool.RequestString("token");
            string url = "";
            if (SYS.Verifycode_UserLogin == "1")
            {
                string loginerror = "false";
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
                    if (CurrentCheckCode != verifycode && CurrentCheckCode != "")
                    {
                        Response.Write("{\"msg\":\"" + Tag("验证码错误") + "\"}");
                        return;
                    }
                }
            }
            if (EX_User.UserLogin(UserName, PWD))
            {
                CurrentUser = EX_User.CurrentUser();
                if (ShopCache.GetBaseConfig().IsOpenUserEnd == "1")
                {
                    if (CurrentUser.Time_End < System.DateTime.Now)
                    {
                        Response.Write("{\"msg\":\"" + Tag("账号已过期") + "\"}");
                        CookieTool.DeleteCookie("User");
                        return;
                    }
                }
                url = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
                if (EX_User.MD5(SYS.InstallCode + url) != token)
                    url = URL("P_Index", "");
                if (url.ToLower().IndexOf("http") > -1 || url.ToLower().IndexOf(URL("P_Register", "").ToLower()) > -1 || url.ToLower().IndexOf(URL("P_Login", "").ToLower()) > -1 || url == "")
                {
                    url = URL("P_Index", "");
                }
                if (SYS.Verifycode_UserLogin == "1")
                {
                    HttpContext.Current.Session["loginerror"] = "false";
                }
                Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
            }
            else
            {
                if (SYS.Verifycode_UserLogin == "1")
                {
                    HttpContext.Current.Session["loginerror"] = "true";
                }
                Response.Write("{\"msg\":\"" + Tag("用户名或密码错误") + "\"}");
            }

        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public void User_LoginOut()
        {
            CookieTool.DeleteCookie("User");
            string url = URL("P_Index", "");
            if (IsWechat())
            {
                if (url.Contains("&weixinlogin=1"))
                    url = url.Replace("&weixinlogin=1","");
                else
                    url = url.Replace("?weixinlogin=1", "");
            }
            Response.Write("{\"msg\":\"OK\",\"mes\":\"" + Tag("已退出登录") + "\",\"url\":\"" + url + "\"}");
        }
        /// <summary>
        /// 忘记密码的处理
        /// </summary>
        public void User_forgetpwd()
        {
            int type = RequestTool.RequestInt("type", 0);
            string Email_ = RequestTool.RequestSafeString("Email");
            string UserName = RequestTool.RequestSafeString("UserName");
            string verifycode = RequestTool.RequestString("verifycode");
            if (SYS.Verifycode_ForgetPassword == "1")
            {
                if (CurrentCheckCode != verifycode)
                {
                    Response.Write("{\"msg\":\"" + Tag("验证码错误") + "\"}");
                    return;
                }
            }
            if (type == 0)
            {
                //发送邮件
                Lebi_User user = B_Lebi_User.GetModel("Email=lbsql{'" + Email_ + "'}");
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                    return;
                }
                try
                {
                    //发送邮件
                    Email.SendEmail_forgetpwd(user, CurrentTheme);
                    //发送短信
                    SMS.SendSMS_forgetpwd(user);
                }
                catch (Exception ex)
                {
                    Response.Write("{\"msg\":\"" + ex.Message + "\"}");
                }
                Response.Write("{\"msg\":\"OK\"}");
            }
            else if (type == 1)
            {
                Lebi_User user = B_Lebi_User.GetModel("UserName=lbsql{'" + UserName + "'}");
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                    return;
                }
                Lebi_User_Answer user_answer = B_Lebi_User_Answer.GetModel("User_id= " + user.id);
                if (user_answer == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("未设置安全问题") + "\"}");
                    return;
                }
                BaseConfig conf = ShopCache.GetBaseConfig();
                user.CheckCode = EX_User.MD5(System.DateTime.Now.ToString() + conf.InstallCode);
                B_Lebi_User.Update(user);
                Response.Write("{\"msg\":\"OK\",\"url\":\"" + ThemeUrl.GetURL("P_FindPassword", "", "", user.Language) + "?id=" + user.id + "&v=" + user.CheckCode + "&type=1\"}");
            }
            else if (type == 2)
            {
                Lebi_User user = B_Lebi_User.GetModel("UserName=lbsql{'" + UserName + "'}");
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                    return;
                }
                if (user.MobilePhone == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("未设置手机号码") + "\"}");
                    return;
                }
                Random Random = new Random();
                int RandNum = Random.Next(100000, 999999);
                user.Password = EX_User.MD5(RandNum.ToString());
                B_Lebi_User.Update(user);
                //发送短信
                SMS.SendSMS_newpwd(user, RandNum.ToString());
                Response.Write("{\"msg\":\"OK\",\"url\":\"" + ThemeUrl.GetURL("P_Login", "", "", user.Language) + "\"}");
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        public void User_resetpwd()
        {
            int type = RequestTool.RequestInt("type", 0);
            string checkcode = RequestTool.RequestString("checkcode");
            string email = RequestTool.RequestSafeString("email");
            string PWD = RequestTool.RequestSafeString("PWD");
            string RPWD = RequestTool.RequestSafeString("RPWD");
            string v = RequestTool.RequestSafeString("v");
            int id = RequestTool.RequestInt("id", 0);
            string Answer1 = EX_User.MD5(RequestTool.RequestSafeString("Answer1"));
            string Answer2 = EX_User.MD5(RequestTool.RequestSafeString("Answer2"));
            Lebi_User user = B_Lebi_User.GetModel("id=" + id + " and CheckCode = lbsql{'" + v + "'}");
            bool flag = true;
            if (user == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (type == 0 && user != null)
            {
                if (user.CheckCode != checkcode)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
            }
            if (type == 1)
            {
                int i = 0;
                List<Lebi_User_Answer> modellists = B_Lebi_User_Answer.GetList("User_id = " + id + "", "id asc");
                if (modellists.Count == 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("没有设置安全问题") + "\"}");
                    return;
                }
                foreach (Lebi_User_Answer modellist in modellists)
                {
                    if (i == 0 && modellist.Answer != Answer1)
                    {
                        Response.Write("{\"msg\":\"" + Tag("问题答案回答不正确") + "\"}");
                        return;
                    }
                    if (i == 1 && modellist.Answer != Answer2)
                    {
                        Response.Write("{\"msg\":\"" + Tag("问题答案回答不正确") + "\"}");
                        return;
                    }
                    i++;
                }
            }
            if (PWD == "")
            {
                Response.Write("{\"msg\":\"" + Tag("密码不能为空") + "\"}");
                return;
            }
            if (PWD != RPWD)
            {
                Response.Write("{\"msg\":\"" + Tag("两次输入的密码不一致，请检查") + "\"}");
                return;
            }
            user.Password = EX_User.MD5(PWD);
            user.CheckCode = "";
            B_Lebi_User.Update(user);
            Response.Write("{\"msg\":\"OK\"}");
        }
        #region 购物车收藏夹相关
        /// <summary>
        /// 添加或修改
        /// 收藏141或
        /// 购物车142
        /// 浏览历史143
        /// </summary>
        public void UserProduct_Edit()
        {
            int t = RequestTool.RequestInt("type", 141);//默认收藏
            int num = RequestTool.RequestInt("num", 1);//默认数量1
            int pid = RequestTool.RequestInt("pid", 0);
            if (t != 142)
                num = 1;
            if (num <= 0)
                num = 1;
            string property = RequestTool.RequestSafeString("property");
            string propertypriceids = RequestTool.RequestSafeString("propertypriceids");
            int warndays = RequestTool.RequestInt("warndays", 0);
            string mes = "";
            if (t != 141 && t != 142 && t != 143 && t != 144)
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            if ((t == 141 || t == 144) && CurrentUser.id == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("请先登陆") + "\",\"url\":\"" + URL("P_Login", "") + "\"}");
                return;
            }
            //<-{ 判断是否上架状态 by lebi.kingdge 2015-02-10
            Lebi_Product pro = GetProduct(pid);
            if (pro == null)
            {
                return;
            }
            if (pro.id == 0)
            {
                return;
            }
            if (t == 142)
            {
                int levelcount = ProductLevelCount(pro);
                if (num < levelcount)
                {
                    num = levelcount;
                    Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("起订量") + " " + levelcount + "\"}");
                    return;
                }
            }
            if (pro.Type_id_ProductStatus != 101)
            {
                Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("该商品已经下架") + "\"}");
                return;
            }
            if (ProductStock(pro) < 1 && pro.Type_id_ProductType != 324 && SYS.IsNullStockSale != "1")
            {
                Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("该商品已经售罄") + "\"}");
                return;
            }
            if ((pro.Type_id_ProductType == 321 || pro.Type_id_ProductType == 322) & (System.DateTime.Now < pro.Time_Start))
            {
                Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("尚未开始") + "\"}");
                return;
            }
            if ((pro.Type_id_ProductType == 321 || pro.Type_id_ProductType == 322) & (System.DateTime.Now > pro.Time_Expired))
            {
                Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("已结束") + "\"}");
                return;
            }
            if (CurrentUserLevel.BuyRight == 0 && t == 142)
            {
                Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("无权购买") + "\"}");
                return;
            }
            if (EX_Product.ProductPrice(pro, CurrentUserLevel, CurrentUser) < 0 && t == 142)
            {
                Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("无权购买") + "\"}");
                return;
            }
            //用户的商品权限
            if (Shop.LebiAPI.Service.Instanse.Check("plugin_productlimit") && t == 142)
            {
                if (SYS.ProductLimitType == "1")//选择表允许
                {
                    int lc = B_Lebi_Product_Limit.Counts("(Product_id=" + pro.id + " or Product_id=" + pro.Product_id + ") and User_id=" + CurrentUser.id + " and IsShow=1 and IsPriceShow=1 and IsBuy=1");
                    if (lc == 0)
                    {
                        lc = B_Lebi_Product_Limit.Counts("(Product_id=" + pro.id + " or Product_id=" + pro.Product_id + ") and UserLevel_id=" + CurrentUserLevel.id + " and IsShow=1 and IsPriceShow=1 and IsBuy=1");
                        if (lc > 0)
                        {
                            Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("无权购买") + "\"}");
                            return;
                        }
                    }
                }
                else
                {
                    int lc = B_Lebi_Product_Limit.Counts("(Product_id=" + pro.id + " or Product_id=" + pro.Product_id + ") and User_id=" + CurrentUser.id + " and IsShow=1 and IsPriceShow=1 and IsBuy=1");
                    if (lc == 0)
                    {
                        lc = B_Lebi_Product_Limit.Counts("(Product_id=" + pro.id + " or Product_id=" + pro.Product_id + ") and UserLevel_id=" + CurrentUserLevel.id + " and IsShow=1 and IsPriceShow=1 and IsBuy=1");
                        if (lc > 0)
                        {
                            Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("无权购买") + "\"}");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("无权购买") + "\"}");
                        return;
                    }
                }

            }
            //if (!("," + pro.UserLevel_ids_buy + ",").Contains("," + CurrentUserLevel.id + ",") && pro.UserLevel_ids_buy != "" && t == 142)
            //{
            //    Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("无权购买") + "\"}");
            //    return;
            //}
            //}->
            EX_User.UserProduct_Edit(CurrentUser, pid, num, t, property, warndays, propertypriceids);
            if (t == 141)
            {
                mes = Tag("商品已加入收藏夹");
            }
            else if (t == 144)
            {
                mes = Tag("商品已加入常购清单");
            }
            else
            {
                Basket basket = new Basket(0);
                mes = Tag("商品已加入购物车") + "<div>" + Tag("数量") + " <span>" + basket.Count + "</span> " + Tag("件") + " <span>" + FormatMoney(basket.Money_Product) + "</span></div><div><a href='" + URL("P_Basket", "") + "' class='btn btn-7'><s></s>" + Tag("查看购物车") + "</a>&nbsp;&nbsp;<a href='javascript:void(0)' onclick='cloesedialog();' class='btn btn-11'><s></s>" + Tag("关闭") + "</a></div>";
            }
            Response.Write("{\"msg\":\"OK\",\"count\":\"" + Basket_Product_Count() + "\",\"amount\":\"" + FormatMoney(Basket_Product_Price()) + "\",\"mes\":\"" + mes + "\",\"url\":\"" + URL("P_AddToBasket", "") + "\"}");

        }
        /// <summary>
        /// 购物车或收藏夹中删除一个商品
        /// 141购物车
        /// 142收藏
        /// 143浏览记录
        /// </summary>
        public void UserProduct_Del()
        {
            int t = RequestTool.RequestInt("type", 141);//默认收藏
            if (t != 141 && t != 142 && t != 143)
            {
                Response.Write("OK");
                return;
            }
            string productid = RequestTool.RequestSafeString("pid");
            if (productid == "all")
            {
                //清空购物车/收藏夹
                Basket.Clear(CurrentUser, t);
            }
            else
            {
                int pid = RequestTool.RequestInt("pid", 0);
                if (EX_User.LoginStatus(CurrentUser))//已经登录
                {
                    B_Lebi_User_Product.Delete("user_id=" + CurrentUser.id + " and product_id=" + pid + " and type_id_UserProductType=" + t + "");
                }
                else//未登录
                {
                    string CookieName = "UserProduct" + t;
                    NameValueCollection nv = CookieTool.GetCookie(CookieName);
                    string key = "p" + pid.ToString();
                    string userproduct = nv.Get(key);
                    if (!string.IsNullOrEmpty(userproduct))
                    {
                        nv.Remove(key);
                    }
                    CookieTool.WriteCookie(CookieName, nv, 365);
                }
            }
            Response.Write("{\"msg\":\"OK\",\"mes\":\"" + Tag("操作成功") + "\"}");
        }
        /// <summary>
        /// 修改购物车产品
        /// </summary>
        public void UserBasket_Edit()
        {
            int t = 142;
            //int num = RequestTool.RequestInt("num", 1);//默认数量1
            string property = RequestTool.RequestSafeString("property");
            string CookieName = "UserProduct" + t;
            List<Lebi_User_Product> pros = Basket.UserProduct(CurrentUser, t);
            foreach (Lebi_User_Product pro in pros)
            {
                pro.count = RequestTool.RequestInt("BasketCount" + pro.Product_id, 1);//默认数量1
                pro.count = pro.count < 1 ? 1 : pro.count;
            }
            if (CurrentUser.id > 0 && CurrentUser.IsAnonymous == 0)//已经登录
            {
                foreach (Lebi_User_Product pro in pros)
                {
                    B_Lebi_User_Product.Update(pro);
                }
            }
            else//未登录
            {
                NameValueCollection nv = CookieTool.GetCookie(CookieName);
                string key, userproduct;
                foreach (Lebi_User_Product pro in pros)
                {
                    key = "p" + pro.Product_id.ToString();
                    userproduct = nv.Get(key);
                    if (string.IsNullOrEmpty(userproduct))
                    {
                        //nv.Add(key, pro.count.ToString());
                        nv.Add(key, pro.count.ToString() + "|" + property);
                    }
                    else
                    {
                        nv.Set(key, pro.count.ToString() + "|" + property);
                    }
                }

                CookieTool.WriteCookie(CookieName, nv, 365);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        #endregion
        /// <summary>
        /// 留言反馈
        /// </summary>
        public void Inquiry()
        {
            Lebi_Inquiry model = new Lebi_Inquiry();
            model = B_Lebi_Inquiry.BindForm(model);
            model.Time_Add = System.DateTime.Now;
            model.Type_id_InquiryStatus = 411;
            model.Language = CurrentLanguage.Code;
            B_Lebi_Inquiry.Add(model);
            //发送邮件
            Email.SendEmail_inquiry(model);
            //发送短信
            SMS.SendSMS_inquiry(model);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 生成登录二维码
        /// </summary>
        public void GetQrCode()
        {
            string cid = CookieTool.GetCookieString("qrcodeid");
            int id = 0;
            string url = "";
            int DT_id = ShopPage.GetDT();
            Lebi_weixin_qrcode model;
            int.TryParse(cid, out id);
            string where = "id='" + id + "'";
            if (DT_id > 0)
            {
                where += " and DT_id =" + DT_id + "";
            }
            model = B_Lebi_weixin_qrcode.GetModel(where);
            if (model == null)
            {
                model = new Lebi_weixin_qrcode();
                model.User_id = CurrentUser.id;
                model.DT_id = DT_id;
                B_Lebi_weixin_qrcode.Add(model);
                id = B_Lebi_weixin_qrcode.GetMaxId();
                CookieTool.SetCookieString("qrcodeid", id.ToString(), 60);
            }
            string token = RequestTool.RequestString("token");
            string backurl = RequestTool.RequestString("url");
            backurl = HttpUtility.UrlDecode(backurl);
            if (EX_User.MD5(SYS.InstallCode + backurl) != token)
                backurl = URL("P_Index", "");

            if (backurl.Contains("?"))
                backurl += "&qrcodeid=" + id;
            else
                backurl += "?qrcodeid=" + id;
            backurl = HttpUtility.UrlEncode(backurl);
            url = Shop.Platform.weixin.GetInstance(DT_id,CurrentSite).LoginURL(backurl);
            //QRCode.Instance.CreateImage(url, id.ToString());
            Bitmap image = QRCode.Instance.CreateImage(url);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            //删除60分钟前的二维码
            //string where = "datediff(n,Time_add,'" + System.DateTime.Now + "')>60";
            //List<Lebi_weixin_qrcode> qs = B_Lebi_weixin_qrcode.GetList(where, "");
            //foreach (Lebi_weixin_qrcode q in qs)
            //{
            //    ImageHelper.DeleteImage("/qrcode/" + q.id + ".png");
            //}
            //B_Lebi_weixin_qrcode.Delete(where);
            ////================
            //Response.Write("{\"msg\":\"OK\",\"img\":\"" + WebPath + "/qrcode/" + id + ".png\"}");
        }
        /// <summary>
        /// 验证微信是否已经授权-登陆账号
        /// </summary>
        public void wechatlogin()
        {
            string cid = CookieTool.GetCookieString("qrcodeid");
            int id = 0;
            if (cid != "")
            {
                int.TryParse(cid, out id);
                Lebi_weixin_qrcode model = B_Lebi_weixin_qrcode.GetModel(id);
                if (model != null)
                {
                    if (model.User_id > 0)
                    {
                        //CookieTool.SetCookieString("qrcodeid", "0", -1);
                        CookieTool.DeleteCookie("qrcodeid");
                        Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                        EX_User.LoginOK(user);
                        Response.Write("{\"msg\":\"OK\"}");
                        return;
                    }
                }
            }
            Response.Write("{\"msg\":\"NO\"}");
        }
        /// <summary>
        /// 验证微信是否已经授权-绑定账号
        /// </summary>
        public void wechatbind()
        {
            if (CurrentUser.bind_weixin_id != "")
            {
                CookieTool.DeleteCookie("qrcodeid");
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            Response.Write("{\"msg\":\"NO\"}");
        }
        /// <summary>
        /// 邮件分享朋友
        /// </summary>
        public void SendFriend()
        {
            int id = RequestTool.RequestInt("id", 0);
            string FromUserName = RequestTool.RequestSafeString("UserName");
            string FromEmail = RequestTool.RequestSafeString("Email");
            string Content = RequestTool.RequestSafeString("Content");
            string ToUserName = RequestTool.RequestSafeString("ToUserName");
            string ToEmail = RequestTool.RequestSafeString("ToEmail");
            Lebi_Product product = B_Lebi_Product.GetModel(id);
            if (product == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            //发送邮件
            try
            {
                if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("sendfriend"))
                {
                    Email.SendEmail_sendfriend(FromUserName + "|" + FromEmail, ToUserName + "|" + ToEmail, Content, product, CurrentLanguage.Code);
                }
            }
            catch (Exception ex)
            {
                Response.Write("{\"msg\":\"" + ex.Message + "\"}");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}