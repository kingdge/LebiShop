using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public delegate void UserRegisterEventHandler(Lebi_User user);
    public delegate void UserLoginEventHandler(Lebi_User user);
    public delegate void UserInfoEditEventHandler(Lebi_User user);
    public class EX_User : System.Web.UI.Page
    {
        public static event UserRegisterEventHandler UserRegisterEvent;
        public static event UserLoginEventHandler UserLoginEvent;
        public static event UserInfoEditEventHandler UserInfoEditEvent;
        public static void UserRegister(Lebi_User user)
        {
            if (UserRegisterEvent != null)
            {
                UserRegisterEvent(user);
            }
        }
        public static void UserLogin(Lebi_User user)
        {
            if (UserLoginEvent != null)
            {
                UserLoginEvent(user);
            }
        }
        public static void UserInfoEdit(Lebi_User user)
        {
            if (UserInfoEditEvent != null)
            {
                UserInfoEditEvent(user);
            }
        }
        #region EX_User
        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Lebi_User GetUser(int id)
        {
            Lebi_User user = B_Lebi_User.GetModel(id);
            if (user == null)
            {
                user = new Lebi_User();
            }
            return user;
        }
        /// <summary>
        /// 新会员注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Lebi_User UserReg(Lebi_User user, string pwd = "")
        {
            try
            {
                Random Random = new Random();
                user.RandNum = Random.Next(100000, 999999);
                B_Lebi_User.Add(user);
                user = B_Lebi_User.GetModel(B_Lebi_User.GetMaxId());
                LoginOK(user);
                user.JYpwd = pwd;//向触发事件传递明文密码，但不存入数据库
                UserRegister(user);//触发事件
                return user;
            } 
            catch (Exception e){
                string json = "";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                json = jss.Serialize(user);
                SystemLog.Add("EX_USER-UserReg-" + json);
                return null;
            }

        }
        /// <summary>
        /// 返回分组下会员数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UserCount(int id)
        {
            int sum = B_Lebi_User.Counts("(IsDel!=1 or IsDel is null) and UserLevel_id = " + id + "");
            return Convert.ToInt32(sum);
        }
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="MasterName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public static bool UserLogin(string UserName, string PWD, bool md5flag = true)
        {

            if (md5flag)
                PWD = MD5(PWD);
            string shortpwd = PWD;
            if (shortpwd.Length > 24)
                shortpwd = PWD.Substring(8, 16);
            Lebi_User user = B_Lebi_User.GetModel("(IsDel!=1 or IsDel is null) and (UserName=lbsql{'" + UserName + "'} or (Email=lbsql{'" + UserName + "'} and IsCheckedEmail=1) or (MobilePhone=lbsql{'" + UserName + "'} and IsCheckedMobilePhone=1)) and (Password='" + PWD + "' or Password='" + shortpwd + "')");
            if (user == null)
                return false;

            LoginOK(user);
            UserLogin(user);
            HttpContext.Current.Session["checkCode"] = null;

            return true;
        }
        public static bool UserLogin(string UserName, string PWD, int DT_id, bool md5flag = true)
        {

            if (md5flag)
                PWD = MD5(PWD);
            string shortpwd = PWD;
            if (shortpwd.Length > 24)
                shortpwd = PWD.Substring(8, 16);
            string where = "(UserName=lbsql{'" + UserName + "'} or (Email=lbsql{'" + UserName + "'} and IsCheckedEmail=1) or (MobilePhone=lbsql{'" + UserName + "'} and IsCheckedMobilePhone=1)) and (Password='" + PWD + "' or Password='" + shortpwd + "')";
            if (DT_id > 0)
            {
                where += " and DT_id = " + DT_id + "";
            }
            Lebi_User user = B_Lebi_User.GetModel(where);
            if (user == null)
                return false;

            LoginOK(user);
            UserLogin(user);
            HttpContext.Current.Session["checkCode"] = null;

            return true;
        }
        public static bool UserLogin(string UserName, string PWD, int DT_id, bool md5flag = true, int AdminLogin = 0)
        {

            if (md5flag)
                PWD = MD5(PWD);
            string shortpwd = PWD;
            if (shortpwd.Length > 24)
                shortpwd = PWD.Substring(8, 16);
            string where = "(UserName=lbsql{'" + UserName + "'} or (Email=lbsql{'" + UserName + "'} and IsCheckedEmail=1) or (MobilePhone=lbsql{'" + UserName + "'} and IsCheckedMobilePhone=1)) and (Password='" + PWD + "' or Password='" + shortpwd + "')";
            if (DT_id > 0)
            {
                where += " and DT_id = " + DT_id + "";
            }
            Lebi_User user = B_Lebi_User.GetModel(where);
            if (user == null)
                return false;
            LoginOK(user, AdminLogin);
            if (AdminLogin == 0)
            {
                UserLogin(user);
            }
            HttpContext.Current.Session["checkCode"] = null;

            return true;
        }
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="user"></param>
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="user"></param>
        public static void LoginOK(Lebi_User user)
        {
            LoginOK(user, 0);
        }        
        public static void LoginOK(Lebi_User user, int AdminLogin = 0)
        {
            int RandNum = user.id;
            string hash = "";
            if (!IsWap())
            {
                if (AdminLogin == 0)
                {
                    Random Random = new Random();
                    RandNum = Random.Next(100000000, 999999999);
                }else
                {
                    RandNum = user.RandNum;
                }
                hash = MD5(RandNum.ToString() + user.Password.ToString());
            }
            else
            {
                //登录过程中，如果其它方法也在修改USER表，user.RandNum将被复原，造成登录失败
                hash = MD5(user.id + user.Password.ToString());
            }
            NameValueCollection nvs = new NameValueCollection();
            nvs.Add("id", user.id.ToString());
            nvs.Add("hash", hash);
            // CookieTool.DeleteCookie("User");
            CookieTool.WriteCookie("User", nvs, 365);
            if (AdminLogin == 0)
            {
                user.Count_Login++;
                user.Time_Last = user.Time_This;
                user.Time_This = System.DateTime.Now;
                user.IP_Last = user.IP_This;
                user.IP_This = RequestTool.GetClientIP();
                user.RandNum = RandNum;
                if (user.Site_id == 0)
                    user.Site_id = 1;
                string Device_id = RequestTool.RequestSafeString("Device_id");
                string Device_system = RequestTool.RequestSafeString("Device_system");
                if (Device_id != "" && (Device_system == "ios" || Device_system == "android"))
                {
                    Common.ExecuteSql("update [Lebi_User] set Device_id='' where Device_id='" + Device_id + "' and id !=" + user.id + "");
                    user.Device_id = Device_id;
                    user.Device_system = Device_system;
                }
                B_Lebi_User.Update(user);
                DoUserProduct(user);
            }
        }
        public static bool IsWap()
        {
            string agent = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
            if (agent == "" || agent.Contains("weixin") || agent.Contains("mobile") || agent.Contains("mobi") || agent.Contains("nokia") || agent.Contains("samsung") || agent.Contains("sonyericsson") || agent.Contains("mot") || agent.Contains("blackberry") || agent.Contains("lg") || agent.Contains("htc") || agent.Contains("j2me") || agent.Contains("ucweb") || agent.Contains("opera mini") || agent.Contains("mobi"))
            {
                //终端可能是手机 
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证ID、密码
        /// </summary>
        /// <param name="MasterID"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public static bool CheckUser(int MasterID, string PWD, bool md5flag = true)
        {
            Lebi_User master = B_Lebi_User.GetModel(MasterID);
            return CheckUser(master, PWD, md5flag);
        }
        public static bool CheckUser(Lebi_User master, string PWD, bool md5flag = true)
        {
            if (md5flag)
                PWD = MD5(PWD);
            //        7a57a5a743894a0e
            //21232f297a57a5a743894a0e4a801fc3
            if (master != null)
            {
                if (PWD == MD5(master.RandNum.ToString() + master.Password.ToString()))
                    return true;
                if (master.Password.Length > 15)
                    if (PWD.Substring(8, 16) == MD5(master.RandNum.ToString() + master.Password.ToString()))
                        return true;
            }
            return false;
        }
        public static string MD5(string PWD)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            PWD = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(PWD))).Replace("-", "").ToLower();
            return PWD;
        }
        /// <summary>
        /// 判断登录状态
        /// </summary>
        /// <returns></returns>
        public static bool LoginStatus()
        {
            bool ret = false;
            var nv = CookieTool.GetCookie("User");
            int uid = 0;
            string uhash = "";
            if (nv.Count > 1)
            {
                if (!string.IsNullOrEmpty(nv.Get("id")) && !string.IsNullOrEmpty(nv.Get("hash")))
                {
                    //uid = StringUtils.StrToInt(nv.Get("id"), 0);
                    int.TryParse(nv.Get("id"), out uid);
                    uhash = nv.Get("hash");
                    Lebi_User user = B_Lebi_User.GetModel(uid);
                    if (user != null)
                    {
                        if (user.IsAnonymous == 1)
                            ret = false;
                        else
                            ret = CheckUser(user, uhash, false);
                    }
                }
            }
            return ret;
        }
        public static bool LoginStatus(Lebi_User user)
        {
            if (user.id > 0 && user.IsAnonymous == 0)
                return true;
            return false;
        }
        /// <summary>
        /// 获得当前会员
        /// </summary>
        /// <returns></returns>
        public static Lebi_User CurrentUser()
        {
            Lebi_User user = null;
            var nv = CookieTool.GetCookie("User");
            int uid = 0;
            string uhash = "";
            if (nv.Count > 1)
            {
                if (!string.IsNullOrEmpty(nv.Get("id")) && !string.IsNullOrEmpty(nv.Get("hash")))
                {
                    int.TryParse(nv.Get("id"), out uid);
                    uhash = nv.Get("hash");
                    user = B_Lebi_User.GetModel(uid);
                    if (user != null)
                    {
                        if (RequestTool.GetConfigKey("DemoSite").Trim() == "1")
                        {
                            return user;
                        }
                        if (uhash == MD5(user.RandNum.ToString() + user.Password.ToString()))  //增加随机加密串二次MD5加密 增加安全性 by lebi.kingdge 2015-04-15
                        //登录过程中，如果其它方法也在修改USER表，user.RandNum将被复原，造成登录失败
                        //if (uhash == MD5(user.id + user.Password.ToString()))
                        {
                            //if (RequestTool.GetClientIP() != user.IP_This)  //增加IP判断 其他IP要求重新登录 增加安全性 by lebi.kingdge 2015-04-15
                            //{
                            //    user = null;
                            //}
                            //else
                            //{
                            return user;
                            //}
                        }
                        else
                        {
                            user = null;
                        }
                    }
                }
            }
            if (user == null)
                user = new Lebi_User();
            return user;
        }
        /// <summary>
        /// 自动添加一个帐号
        /// </summary>
        public static Lebi_User CreateAnonymous()
        {
            Random Random = new Random();
            Lebi_User model = new Lebi_User();
            model.NickName = "Anonymous";
            model.UserName = "Anonymous";
            model.IsAnonymous = 1;
            model.Password = EX_User.MD5("lebishop");
            model.Language = Language.CurrentLanguage().Code;
            Lebi_UserLevel ul = B_Lebi_UserLevel.GetModel("Grade=0");
            if (ul == null)
                ul = new Lebi_UserLevel();
            model.UserLevel_id = ul.id;
            model.RandNum = Random.Next(100000000, 999999999);
            B_Lebi_User.Add(model);
            model.id = B_Lebi_User.GetMaxId();
            LoginOK(model);//登录
            return model;
        }
        #endregion
        /// <summary>
        /// 将用户登录前收藏夹或购物车中的商品写入数据库
        /// </summary>
        private static void DoUserProduct(Lebi_User CurrentUser)
        {
            if (!LoginStatus(CurrentUser))//排除匿名登录状况
                return;
            Lebi_User olduser = new Lebi_User();
            List<Lebi_User_Product> pros = EX_User.UserProduct(CurrentUser, 142);
            List<Lebi_User_Product> lpros = Basket.UserProduct(olduser, 142);//本地购物车
            List<Lebi_User_Product> lvpros = Basket.UserProduct(olduser, 143);//本地浏览历史
            foreach (Lebi_User_Product lpro in lpros)
            {
                bool flag = true;
                foreach (Lebi_User_Product pro in pros)
                {
                    if (pro.Product_id == lpro.Product_id)
                    {
                        //数据库购物车中cookie购物车中的商品，修改数据库数量
                        pro.count = lpro.count;
                        B_Lebi_User_Product.Update(pro);
                        flag = false;
                    }
                }
                if (flag)
                {
                    //数据库购物车中cookie没有购物车中的商品，修改添加数据库
                    lpro.User_id = CurrentUser.id;
                    Lebi_Product product = B_Lebi_Product.GetModel(lpro.Product_id);
                    if (product != null)
                    {
                        lpro.Pro_Type_id = product.id;
                        B_Lebi_User_Product.Add(lpro);
                    }
                }
            }
            //本地浏览历史的商品添加到数据库
            List<Lebi_User_Product> vpros = EX_User.UserProduct(CurrentUser, 143);
            foreach (Lebi_User_Product lvpro in lvpros)
            {
                bool flag = true;
                foreach (Lebi_User_Product pro in vpros)
                {
                    if (pro.Product_id == lvpro.Product_id)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    //数据库购物车中cookie没有购物车中的商品，修改添加数据库
                    lvpro.User_id = CurrentUser.id;
                    Lebi_Product product = B_Lebi_Product.GetModel(lvpro.Product_id);
                    if (product != null)
                    {
                        lvpro.Pro_Type_id = product.id;
                        B_Lebi_User_Product.Add(lvpro);
                    }
                }
            }
            //清空本地购物车
            string CookieName = "UserProduct142";
            NameValueCollection nv = new NameValueCollection();
            CookieTool.WriteCookie(CookieName, nv, 0);

            //清空本地浏览记录
            CookieName = "UserProduct143";
            CookieTool.WriteCookie(CookieName, nv, 0);



            //Lebi_User user = CurrentUser();
            //string[] ps = UserProduct(141);
            //for (int i = 0; i < ps.Length; i++)
            //{
            //    int c = B_Lebi_User_Product.Counts("user_id=" + user.id + " and product_id=" + ps[i] + " and type_id_UserProductType=141");
            //    if (c == 0)
            //    {
            //        Lebi_User_Product upro = new Lebi_User_Product();
            //        upro.User_id = user.id;
            //        upro.Product_id = Convert.ToInt32(ps[i]);
            //        upro.Type_id_UserProductType = 141;
            //        B_Lebi_User_Product.Add(upro);

            //    }
            //}
            //ps = UserProduct(142);
            //for (int i = 0; i < ps.Length; i++)
            //{
            //    int c = B_Lebi_User_Product.Counts("user_id=" + user.id + " and product_id=" + ps[i] + " and type_id_UserProductType=142");
            //    if (c == 0)
            //    {
            //        Lebi_User_Product upro = new Lebi_User_Product();
            //        upro.User_id = user.id;
            //        upro.Product_id = Convert.ToInt32(ps[i]);
            //        upro.Type_id_UserProductType = 142;
            //        upro.count = 1;
            //        B_Lebi_User_Product.Add(upro);

            //    }
            //}
        }
        /// <summary>
        /// 返回用户产品id数组
        /// 141收藏夹142购物车
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] UserProduct(int type)
        {
            var nv = CookieTool.GetCookie("UserProduct");
            if (string.IsNullOrEmpty(nv.Get("p" + type.ToString())))
            {
                return new string[0];
            }
            return (nv.Get("p" + type.ToString())).Split(',');
        }


        public static string TypeName(int id, string lang)
        {
            Lebi_UserLevel model = B_Lebi_UserLevel.GetModel(id);
            if (model != null)
                return Language.Content(model.Name, lang);
            return "";
        }
        /// <summary>
        /// 返回类型下拉框内容
        /// </summary>
        /// <param name="where"></param>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string TypeOption(string where, int id, string lang)
        {
            List<Lebi_UserLevel> models = B_Lebi_UserLevel.GetList(where, "grade asc");
            string str = "";
            foreach (Lebi_UserLevel model in models)
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + Language.Content(model.Name, lang) + "</option>";
            }
            return str;

        }
        /// <summary>
        /// 返回复选框内容
        /// </summary>
        /// <param name="where"></param>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string TypeCheckbox(string where, string name, string id, string ext, string lang)
        {
            List<Lebi_UserLevel> models = B_Lebi_UserLevel.GetList(where, "grade asc");
            string str = "";
            foreach (Lebi_UserLevel model in models)
            {
                string sel = "";
                if (("," + id + ",").Contains("," + model.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + Language.Content(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"checkbox\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Language.Content(model.Name, lang) + "</label>";
                }
            }
            return str;

        }
        /// <summary>
        /// 返回单选内容
        /// </summary>
        /// <param name="where"></param>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string TypeRadio(string where, string name, int id, string ext, string lang)
        {
            List<Lebi_UserLevel> models = B_Lebi_UserLevel.GetList(where, "grade asc");
            string str = "";
            foreach (Lebi_UserLevel model in models)
            {
                string sel = "";
                if (id == model.id)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-radio m-r-20\"><input type=\"radio\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + Language.Content(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Language.Content(model.Name, lang) + "</label>";
                }
            }
            return str;

        }
        /// <summary>
        /// 验证找回里面链接
        /// </summary>
        /// 
        public static void CheckForgetPWD(Lebi_Theme CurrentTheme, Lebi_Language lang, int type)
        {
            int id = RequestTool.RequestInt("id", 0);
            string email = RequestTool.RequestString("email");
            string v = RequestTool.RequestString("v");
            if (type == 0)
            {
                Lebi_User user = B_Lebi_User.GetModel(id);
                bool flag = true;
                if (user == null)
                    flag = false;
                else
                {
                    if (user.CheckCode != v)
                        flag = false;
                }
                if (!flag)
                {
                    //验证失败,跳转至404页面
                    HttpContext.Current.Response.Redirect(ThemeUrl.GetURL("P_404", "", "", lang));
                }
            }

        }
        /// <summary>
        /// 计算指定日期及状态的会员数量
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetCount_User(string dateFrom, string dateTo, string status, int type)
        {
            int count = 0;
            string where = "";
            if (dateFrom != "" && dateTo != "")
            {
                if (type == 0)
                {
                    where = "Time_reg>='" + dateFrom + "' and Time_reg<='" + dateTo + "'";
                }
                else
                {
                    where = "Birthday>='" + dateFrom + "' and Birthday<='" + dateTo + "'";
                }
            }
            if (status != "")
            {
                if (dateFrom != "" && dateTo != "")
                {
                    where += " and ";
                }
                where += status;
            }
            count = B_Lebi_User.Counts(where);
            return count;
        }

        /// <summary>
        /// 添加或修改
        /// 收藏t=141
        /// 购物车t=142 
        /// 浏览记录t=143
        /// </summary>
        /// <param name="CurrentUser"></param>
        /// <param name="pid"></param>
        /// <param name="num"></param>
        /// <param name="t"></param>
        public static void UserProduct_Edit(Lebi_User CurrentUser, int pid, int num, int t, string property, int warndays, string propertypriceids)
        {
            string CookieName = "UserProduct" + t;
            Lebi_Product pro = B_Lebi_Product.GetModel(pid);
            if (pro == null)
            {
                return;
            }
            if (CurrentUser.id > 0 && CurrentUser.IsAnonymous == 0)//已经登录
            {
                Lebi_User_Product upro = B_Lebi_User_Product.GetModel("user_id=" + CurrentUser.id + " and product_id=" + pid + " and type_id_UserProductType=" + t + "");
                decimal propertyprice = 0;
                if (propertypriceids != "")
                {
                    List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("id in (" + propertypriceids + ")", "");
                    foreach (Lebi_ProPerty p in ps)
                    {
                        propertyprice = propertyprice + p.Price;
                    }
                }
                if (upro == null)
                {
                    upro = new Lebi_User_Product();
                    upro.User_id = CurrentUser.id;
                    upro.Product_id = pid;
                    upro.Type_id_UserProductType = t;
                    upro.count = num;
                    upro.Product_Number = pro.Number;
                    upro.Pro_Type_id = pro.Pro_Type_id;
                    upro.ProPerty134 = property;
                    upro.WarnDays = warndays;
                    upro.Time_Add = DateTime.Now;
                    upro.Time_addemail = upro.Time_Add.Date.AddDays(upro.WarnDays);
                    upro.ProPerty_Price = propertyprice;
                    B_Lebi_User_Product.Add(upro);

                }
                else
                {
                    if (num > 0)
                    {
                        upro.ProPerty134 = property;
                        upro.count = num;
                        upro.ProPerty_Price = propertyprice;
                        B_Lebi_User_Product.Update(upro);
                    }
                    else
                    {
                        B_Lebi_User_Product.Delete(pro.id);
                    }
                }
            }
            else//未登录
            {
                NameValueCollection nv = CookieTool.GetCookie(CookieName);
                string key = "p" + pro.id.ToString();
                property = HttpUtility.UrlEncode(propertypriceids);
                string userproduct = nv.Get(key);
                if (num > 0)
                {
                    if (string.IsNullOrEmpty(userproduct))
                    {
                        nv.Add(key, num.ToString() + "|" + property);
                        //nv.Add(key_t, property.ToString());
                    }
                    else
                    {
                        nv.Set(key, num.ToString() + "|" + property);
                        // nv.Set(key_t, property.ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(userproduct))
                    {
                        nv.Remove(key);
                    }
                }
                CookieTool.WriteCookie(CookieName, nv, 365);
            }
        }
        /// <summary>
        /// 用户产品
        /// 收藏t=141
        /// 购物车t=142 
        /// 浏览记录t=143
        /// </summary>
        /// <param name="CurrentUser"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<Lebi_User_Product> UserProduct(Lebi_User CurrentUser, int t, int count = 9999)
        {
            List<Lebi_User_Product> models = new List<Lebi_User_Product>();
            try
            {
                if (LoginStatus(CurrentUser))//已经登录
                {
                    models = B_Lebi_User_Product.GetList("user_id=" + CurrentUser.id + " and type_id_UserProductType=" + t + "", "id desc", count, 1);
                }
                else
                {
                    string CookieName = "UserProduct" + t;
                    NameValueCollection nv = CookieTool.GetCookie(CookieName);
                    for (int i = 0; i < nv.Count; i++)
                    {
                        if (i == count)
                            break;
                        string key = nv.GetKey(i);
                        if (key == null)
                            continue;
                        key = key.Replace("p", "");
                        string con = nv.Get(i).ToString();
                        string count_ = "";
                        string p134_ = "";
                        decimal propertyprice = 0;
                        con = HttpUtility.UrlDecode(con);
                        if (con.Contains("|"))
                        {
                            count_ = con.Substring(0, con.IndexOf("|"));
                            string propertyids = con.Substring(con.IndexOf("|") + 1, (con.Length - con.IndexOf("|") - 1));
                            if (propertyids != "")
                            {
                                List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("id in (" + propertyids + ")", "");
                                foreach (Lebi_ProPerty p in ps)
                                {
                                    propertyprice = propertyprice + p.Price;
                                    Lebi_ProPerty pp = B_Lebi_ProPerty.GetModel(p.parentid);
                                    if (pp != null)
                                        p134_ += "," + Language.Content(pp.Name, CurrentUser.Language) + ":" + Language.Content(p.Name, CurrentUser.Language);
                                    else
                                        p134_ += "," + Language.Content(p.Name, CurrentUser.Language);
                                }
                                p134_ = p134_.TrimStart(',');
                            }
                        }
                        else
                        {
                            count_ = con;
                        }
                        Lebi_Product product = B_Lebi_Product.GetModel(Convert.ToInt32(key));
                        if (product != null)
                        {
                            Lebi_User_Product upro = new Lebi_User_Product();
                            upro.User_id = CurrentUser.id;
                            upro.Product_id = Convert.ToInt32(key);
                            upro.Type_id_UserProductType = t;
                            upro.count = Convert.ToInt32(count_);
                            upro.ProPerty134 = p134_;
                            upro.Pro_Type_id = product.Pro_Type_id;
                            upro.Product_Number = product.Number;
                            upro.ProPerty_Price = propertyprice;
                            models.Add(upro);
                        }
                    }
                }
                return models;
            }
            catch (Exception)
            {
                return models;
            }

        }

        /// <summary>
        /// 赠送卡券
        /// </summary>
        public static void GiveUserCard(Lebi_User user, int orderid)
        {

            Lebi_CardOrder co = B_Lebi_CardOrder.GetModel(orderid);
            if (co == null)
            {
                Log.Add("自动分配卡券失败", "Alert", "");
            }

            Lebi_Card c = B_Lebi_Card.GetModel("CardOrder_id=" + orderid + " and User_id=0");
            if (c == null)
            {
                Log.Add("自动分配卡券失败", "Alert", "");
                return;
            }
            c.User_id = user.id;
            c.Type_id_CardStatus = 201;//已发放
            c.User_UserName = user.UserName;
            B_Lebi_Card.Update(c);

            //Log.Add("发送" + EX_Type.TypeName(co.Type_id_CardType) + "", "card", "", CurrentAdmin, su.Description + User_Name_To + User_ids);
        }
        /// <summary>
        /// 计算指定日期及类型的资金金额
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static decimal GetMoney_UserMoney(string dateFrom, string dateTo, string status)
        {
            decimal money = 0;
            string where = "";
            if (dateFrom != "" && dateTo != "")
            {
                where = "Time_Add>='" + dateFrom + "' and Time_Add<='" + dateTo + "'";
            }
            if (status != "")
            {
                if (dateFrom != "" && dateTo != "")
                {
                    where += " and ";
                }
                where += status;
            }
            List<Lebi_User_Money> models = B_Lebi_User_Money.GetList(where, "");
            foreach (Lebi_User_Money model in models)
            {
                money = money + model.Money;
            }
            return money;
        }
        public static decimal GetMoney_UserMoney(string dateFrom, string dateTo, string status, int hour)
        {
            decimal money = 0;
            string where = "";
            if (dateFrom != "" && dateTo != "")
            {
                where = "Time_Add>='" + dateFrom + " " + hour + ":0:0' and Time_Add<='" + dateTo + " " + hour + ":59:59'";
            }
            if (status != "")
            {
                if (dateFrom != "" && dateTo != "")
                {
                    where += " and ";
                }
                where += status;
            }
            List<Lebi_User_Money> models = B_Lebi_User_Money.GetList(where, "");
            foreach (Lebi_User_Money model in models)
            {
                money = money + model.Money;
            }
            return money;
        }
        /// <summary>
        /// 计算指定日期及类型的资金笔数
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int GetCount_UserMoney(string dateFrom, string dateTo, string status)
        {
            int count = 0;
            string where = "";
            if (dateFrom != "" && dateTo != "")
            {
                where = "Time_Add>='" + dateFrom + "' and Time_Add<='" + dateTo + "'";
            }
            if (status != "")
            {
                if (dateFrom != "" && dateTo != "")
                {
                    where += " and ";
                }
                where += status;
            }
            count = B_Lebi_User_Money.Counts(where);
            return count;
        }
        public static int GetCount_UserMoney(string dateFrom, string dateTo, string status, int hour)
        {
            int count = 0;
            string where = "";
            if (dateFrom != "" && dateTo != "")
            {
                where = "Time_Add>='" + dateFrom + " " + hour + ":0:0' and Time_Add<='" + dateTo + " " + hour + ":59:59'";
            }
            if (status != "")
            {
                if (dateFrom != "" && dateTo != "")
                {
                    where += " and ";
                }
                where += status;
            }
            count = B_Lebi_User_Money.Counts(where);
            return count;
        }
        /// <summary>
        /// 当前站点
        /// </summary>
        Lebi_Site CurrentSite_ = null;
        protected Lebi_Site CurrentSite
        {
            get
            {
                var nv = CookieTool.GetCookie("ThemeStatus");
                int siteid = 0;
                if (!string.IsNullOrEmpty(nv.Get("site")))
                {
                    int.TryParse(nv.Get("site"), out siteid);
                }
                if (siteid == 0)
                    return ShopCache.GetMainSite();
                CurrentSite_ = B_Lebi_Site.GetModel(siteid);
                if (CurrentSite_ == null)
                    return ShopCache.GetMainSite();
                return CurrentSite_;
            }
            set
            {
                CurrentSite_ = value;
            }
        }
        /// <summary>
        /// 返回分组下会员余额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string MoneyCount(int id)
        {
            string sum = B_Lebi_User.GetValue("sum(Money)", "UserLevel_id = " + id + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return sum;
        }
        /// <summary>
        /// 返回分组下会员积分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string PointCount(int id)
        {
            string sum = B_Lebi_User.GetValue("sum(Point)", "UserLevel_id = " + id + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return sum;
        }
        /// <summary>
        /// 返回分组下会员订单数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string OrderCount(int id)
        {
            string sum = B_Lebi_User.GetValue("sum(Count_Order)", "UserLevel_id = " + id + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return sum;
        }
        /// <summary>
        /// 返回分组下会员消费数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Money_xiaofeiCount(int id)
        {
            string sum = B_Lebi_User.GetValue("sum(Money_xiaofei)", "UserLevel_id = " + id + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return sum;
        }
        /// <summary>
        /// 会员合并
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DelUser"></param>
        /// <returns></returns>
        public static bool UserBind(Lebi_User User,Lebi_User DelUser)
        {
            if (DelUser == null || User == null)
            {
                return false;
            }
            //<-{如果主账号和合并账号都有商家 不能合并
            int UserSupplierCount = B_Lebi_Supplier.Counts("User_id = " + User.id + "");
            int DelUserSupplierCount = B_Lebi_Supplier.Counts("User_id = " + DelUser.id + "");
            if (UserSupplierCount > 0 && DelUserSupplierCount > 0)
            {
                return false;
            }
            //}->
            //<-{如果合并账号有分销商账号 不能合并
            int DelUserDTCount = B_Lebi_DT.Counts("User_id = " + DelUser.id + "");
            if (DelUserDTCount > 0)
            {
                return false;
            }
            //}->
            if (User.MobilePhone == "" && DelUser.MobilePhone != "")
            {
                User.MobilePhone = DelUser.MobilePhone;
                if (DelUser.IsCheckedMobilePhone == 1)
                {
                    User.IsCheckedMobilePhone = 1;
                }
            }
            if (User.Email == "" && DelUser.Email != "")
            {
                User.Email = DelUser.Email;
                if (DelUser.IsCheckedEmail == 1)
                {
                    User.IsCheckedEmail = 1;
                }
            }
            if (User.Address == "" && DelUser.Address != "")
            {
                User.Address = DelUser.Address;
            }
            if (User.alipay == "" && DelUser.alipay != "")
            {
                User.alipay = DelUser.alipay;
            }
            if (User.aliwangwang == "" && DelUser.aliwangwang != "")
            {
                User.aliwangwang = DelUser.aliwangwang;
            }
            if (User.Face == "" && DelUser.Face != "")
            {
                User.Face = DelUser.Face;
            }
            if (User.Fax == "" && DelUser.Fax != "")
            {
                User.Fax = DelUser.Fax;
            }
            if (User.Phone == "" && DelUser.Phone != "")
            {
                User.Phone = DelUser.Phone;
            }
            if (User.Postalcode == "" && DelUser.Postalcode != "")
            {
                User.Postalcode = DelUser.Postalcode;
            }
            if (User.City == "" && DelUser.City != "")
            {
                User.City = DelUser.City;
            }
            if (User.Introduce == "" && DelUser.Introduce != "")
            {
                User.Introduce = DelUser.Introduce;
            }
            if (User.QQ == "" && DelUser.QQ != "")
            {
                User.QQ = DelUser.QQ;
            }
            if (User.Sex == "" && DelUser.Sex != "")
            {
                User.Sex = DelUser.Sex;
            }
            if (User.RealName == "" && DelUser.RealName != "")
            {
                User.RealName = DelUser.RealName;
            }
            if (User.UserLevel_id < DelUser.UserLevel_id)
            {
                User.UserLevel_id = DelUser.UserLevel_id;
            }
            if (User.Uavatar == "" && DelUser.Uavatar != "")
            {
                User.Uavatar = DelUser.Uavatar;
            }
            if (User.momo == "" && DelUser.momo != "")
            {
                User.momo = DelUser.momo;
            }
            if (User.weixin == "" && DelUser.weixin != "")
            {
                User.weixin = DelUser.weixin;
            }
            if (User.bind_facebook_id == "" && DelUser.bind_facebook_id != "")
            {
                User.bind_facebook_id = DelUser.bind_facebook_id;
            }
            if (User.bind_facebook_nickname == "" && DelUser.bind_facebook_nickname != "")
            {
                User.bind_facebook_nickname = DelUser.bind_facebook_nickname;
            }
            if (User.bind_facebook_token == "" && DelUser.bind_facebook_token != "")
            {
                User.bind_facebook_token = DelUser.bind_facebook_token;
            }
            if (User.bind_qq_id == "" && DelUser.bind_qq_id != "")
            {
                User.bind_qq_id = DelUser.bind_qq_id;
            }
            if (User.bind_qq_nickname == "" && DelUser.bind_qq_nickname != "")
            {
                User.bind_qq_nickname = DelUser.bind_qq_nickname;
            }
            if (User.bind_qq_token == "" && DelUser.bind_qq_token != "")
            {
                User.bind_qq_token = DelUser.bind_qq_token;
            }
            if (User.bind_taobao_id == "" && DelUser.bind_taobao_id != "")
            {
                User.bind_taobao_id = DelUser.bind_taobao_id;
            }
            if (User.bind_taobao_nickname == "" && DelUser.bind_taobao_nickname != "")
            {
                User.bind_taobao_nickname = DelUser.bind_taobao_nickname;
            }
            if (User.bind_taobao_token == "" && DelUser.bind_taobao_token != "")
            {
                User.bind_taobao_token = DelUser.bind_taobao_token;
            }
            if (User.bind_weixin_id == "" && DelUser.bind_weixin_id !="")
            {
                User.bind_weixin_id = DelUser.bind_weixin_id;
            }
            if (User.bind_weibo_id == "" && DelUser.bind_weibo_id != "")
            {
                User.bind_weibo_id = DelUser.bind_weibo_id;
            }
            if (User.bind_weibo_nickname == "" && DelUser.bind_weibo_nickname != "")
            {
                User.bind_weibo_nickname = DelUser.bind_weibo_nickname;
            }
            if (User.bind_weibo_token == "" && DelUser.bind_weibo_token != "")
            {
                User.bind_weibo_token = DelUser.bind_weibo_token;
            }
            if (User.bind_weixin_id == "" && DelUser.bind_weixin_id != "")
            {
                User.bind_weixin_id = DelUser.bind_weixin_id;
            }
            if (User.bind_weixin_nickname == "" && DelUser.bind_weixin_nickname != "")
            {
                User.bind_weixin_nickname = DelUser.bind_weixin_nickname;
            }
            if (User.bind_weixin_token == "" && DelUser.bind_weixin_token != "")
            {
                User.bind_weixin_token = DelUser.bind_weixin_token;
            }
            if ((User.UserName.Contains(User.bind_facebook_id) && User.bind_facebook_id != "") || (User.UserName.Contains(User.bind_qq_id) && User.bind_qq_id != "") || (User.UserName.Contains(User.bind_taobao_id) && User.bind_taobao_id != "") || (User.UserName.Contains(User.bind_weibo_id) && User.bind_weibo_id != "") || (User.UserName.Contains(User.bind_weixin_id) && User.bind_weixin_id != ""))
            {
                User.UserName = DelUser.UserName;
                User.Password = DelUser.Password;
                User.RandNum = DelUser.RandNum;
            }
            User.Money += DelUser.Money;
            User.Point += DelUser.Point;
            User.Money_Bill += DelUser.Money_Bill;
            User.Money_Order += DelUser.Money_Order;
            User.Money_Product += DelUser.Money_Product;
            User.Money_Transport += DelUser.Money_Transport;
            User.Money_xiaofei += DelUser.Money_xiaofei;
            User.Count_Order += DelUser.Count_Order;
            User.Count_Order_OK += DelUser.Count_Order_OK;
            Common.ExecuteSql("update [Lebi_User] set User_id_parent = " + User.id + " where User_id_parent=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Message] set User_id_To = " + User.id + ",User_Name_To='" + User.UserName + "' where User_id_To=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Message] set User_id_From = " + User.id + ",User_Name_From='" + User.UserName + "' where User_id_From=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Order] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Order_Log] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Order_Product] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_User_BuyMoney] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_User_Card] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_User_Money] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_User_Point] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_User_Product] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_User_Address] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Supplier] set User_id = " + User.id + ",UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Supplier_User] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Agent_Area] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Agent_Money] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Agent_Product] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Agent_Product_request] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Agent_Product_User] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Bill] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Cash] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Card] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_Comment] set User_id = " + User.id + ",User_UserName='" + User.UserName + "' where User_id=" + DelUser.id + "");
            Common.ExecuteSql("update [Lebi_weixin_qrcode] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            if (User.User_Address_id == 0)   //如果没有默认地址ID 使用合并账号的地址ID
            {   
                Lebi_User_Address address = B_Lebi_User_Address.GetModel("User_id = " + User.id + "");
                if (address != null)
                {
                    User.User_Address_id = address.id;
                }
            }
            Lebi_User_Answer answer = B_Lebi_User_Answer.GetModel("User_id = " + User.id + "");   //如果没有设置安全问题 使用合并账号的安全问题
            if (answer == null)
            {
                Common.ExecuteSql("update [Lebi_User_Answer] set User_id = " + User.id + " where User_id=" + DelUser.id + "");
            }
            B_Lebi_User.Update(User);
            B_Lebi_User.Delete(DelUser.id);
            Money.UpdateUserMoney(User);
            Point.UpdateUserPoint(User);
            UserInfoEdit(User);//触发编辑用户资料事件
            return true;
        }
    }
}
