using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.Threading;
namespace Shop.Bussiness
{
    public class EX_Admin
    {
        #region EX_Master
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="MasterName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public static bool AdminLogin(string AdminName, string PWD)
        {
            string SafeAdminLogin = RequestTool.GetConfigKey("SafeAdminLogin").Trim();
            if (SafeAdminLogin == "")
            {
                SafeAdminLogin = "1";
            }
            bool ret = false;
            Lebi_Administrator master = B_Lebi_Administrator.GetModel("UserName=lbsql{'" + AdminName + "'} and Type_id_AdminStatus=230");

            if (CheckAdmin(master, PWD, AdminName,false))
            {
                Random Random = new Random();
                int RandNum = Random.Next(100000000, 999999999);
                if (SafeAdminLogin == "1")
                {
                    master.RandNum = RandNum;
                    B_Lebi_Administrator.Update(master);
                }
                else
                {
                    RandNum = master.RandNum;
                }
                NameValueCollection nvs = new NameValueCollection();
                nvs.Add("id", master.id.ToString());
                nvs.Add("hash", MD5(RandNum + PWD));
                nvs.Add("name", AdminName);
                CookieTool.WriteCookie("Master", nvs, 365);
                SetSession(master);
                HttpContext.Current.Session["checkCode"] = null;
                ret = true;
                //开启一个线程提交最后登录记录
                Thread thread = new Thread(new ThreadStart(UserLastUse));
                thread.IsBackground = true;//这样能随主程序一起结束
                thread.Start();
            }
            return ret;
        }
        public static void UserLastUse()
        {
            Shop.LebiAPI.Service.Instanse.UserLastUse();
        }
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="master"></param>
        public static void SetSession(Lebi_Administrator master)
        {
            HttpContext.Current.Session["admin"] = master;
            HttpContext.Current.Session["admin_group"] = B_Lebi_Admin_Group.GetModel("id=" + master.Admin_Group_id + "");
            List<Lebi_Admin_Power> ps = B_Lebi_Admin_Power.GetList("Admin_Group_id=" + master.Admin_Group_id + "", "");
            HttpContext.Current.Session["admin_power"] = (from m in ps
                                                          where m.Url == ""
                                                          select m).ToList();
            HttpContext.Current.Session["admin_power_url"] = (from m in ps
                                                              where m.Url != ""
                                                              select m).ToList();
        }
        /// <summary>
        /// 验证ID、密码
        /// </summary>
        /// <param name="MasterID"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public static bool CheckAdmin(Lebi_Administrator master, string PWD, string NAME,bool ReadyLogin = true)
        {
            if (master == null)
            {
                return false;
            }
            else
            {
                string SafeAdminLogin = RequestTool.GetConfigKey("SafeAdminLogin").Trim();
                if (SafeAdminLogin == "")
                {
                    SafeAdminLogin = "1";
                }
                if (ReadyLogin == true)
                {
                    if (RequestTool.GetConfigKey("DemoSite").Trim() == "1")
                    {
                        return true;
                    }
                    if (PWD == MD5(master.RandNum + master.Password) && NAME == master.UserName)  //增加随机加密串二次MD5加密 增加安全性 by lebi.kingdge 2015-04-15
                    {
                        if (SafeAdminLogin == "1")
                        {
                            if (RequestTool.GetClientIP() != master.IP_This)  //增加IP判断 其他IP要求重新登录 增加安全性 by lebi.kingdge 2015-04-15
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (PWD == master.Password && NAME == master.UserName)
                        return true;
                }
                if (master.Type_id_AdminStatus != 230)
                    return false;
            }
            return false;
        }
        public static bool CheckAdmin(int id, string PWD, string NAME)
        {
            Lebi_Administrator master = B_Lebi_Administrator.GetModel(id);
            return CheckAdmin(master, PWD, NAME);
        }
        /// <summary>
        /// 判断登录状态
        /// </summary>
        /// <returns></returns>
        public static bool LoginStatus()
        {
            if (HttpContext.Current.Session["admin"] == null)
            {

                var nv = CookieTool.GetCookie("Master");
                int uid = 0;
                string uhash = "";
                string uname = "";
                if (nv.Count > 1)
                {
                    if (!string.IsNullOrEmpty(nv.Get("id")) && !string.IsNullOrEmpty(nv.Get("hash")) && !string.IsNullOrEmpty(nv.Get("name")))
                    {
                        //uid = StringUtils.StrToInt(nv.Get("id"), 0);
                        int.TryParse(nv.Get("id"), out uid);
                        uhash = nv.Get("hash");
                        uname = nv.Get("name");
                        Lebi_Administrator master = B_Lebi_Administrator.GetModel(uid);
                        if (CheckAdmin(master, uhash, uname, true))
                        {
                            SetSession(master);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获得当前管理者
        /// </summary>
        /// <returns></returns>
        public static Lebi_Administrator CurrentAdmin()
        {
            try
            {
                if (LoginStatus())
                    return (Lebi_Administrator)HttpContext.Current.Session["admin"];
                else
                    return new Lebi_Administrator();
            }
            catch
            {
                return new Lebi_Administrator();
            }
        }
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="strin"></param>
        /// <returns></returns>
        public static string MD5(string strin)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            strin = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(strin))).Replace("-", "").ToLower();
            return strin;
        }

        #endregion

        #region 管理员权限
        public static bool Power(string code, string name)
        {
            if (RequestTool.GetConfigKey("DemoSite").Trim() == "1" && CurrentAdmin().AdminType != "super")
            {
                return false;
            }
            List<Lebi_Admin_Power> ps;
            if (HttpContext.Current.Session["admin_power"] == null)
                ps = new List<Lebi_Admin_Power>();
            else
                ps = (List<Lebi_Admin_Power>)HttpContext.Current.Session["admin_power"];

            Lebi_Admin_Power model = (from m in ps
                                      where m.Admin_Limit_Code == code
                                      select m).ToList().FirstOrDefault();
            if (model != null)
            {
                return true;
            }
            else
            {
                if (name != "")
                {
                    Lebi_Admin_Limit limit = B_Lebi_Admin_Limit.GetModel("Code='" + code + "'");
                    if (limit == null)
                    {
                        limit = B_Lebi_Admin_Limit.GetModel("Code='default'");
                        if (limit == null)
                        {
                            limit = new Lebi_Admin_Limit();
                            limit.parentid = 0;
                            limit.id = 1;
                            limit.Code = "default";
                            limit.Name = "未分组";
                            B_Lebi_Admin_Limit.Add(limit);
                            limit.id = B_Lebi_Admin_Limit.GetMaxId();
                        }
                        limit.Code = code;
                        limit.Name = name;
                        limit.parentid = limit.id;
                        B_Lebi_Admin_Limit.Add(limit);
                    }
                }
            }
            if (CurrentAdmin().AdminType == "super")
                return true;
            return false;
        }
        public static bool CheckPower(string code)
        {
            if (!Power(code,""))
            {
                return false;
            }
            return true;
        }
        public static bool CheckPower(string code, string name)
        {
            if (!Power(code, name))
            {
                NoPower();
                return false;
            }
            return true;
        }
        public static void NoPower()
        {
            HttpContext.Current.Response.Write("{\"msg\":\"权限不足\"}");
            HttpContext.Current.Response.End();
        }
        public static Lebi_Project Project()
        {
            Lebi_Project project = new Lebi_Project();
            if (!string.IsNullOrEmpty(CurrentAdmin().Project_ids))
            {
                project = B_Lebi_Project.GetModel("id in(" + CurrentAdmin().Project_ids + ")");
                if (project == null)
                {
                    project = new Lebi_Project();
                }
            }else
            {
                project.Pro_Type_ids = CurrentAdmin().Pro_Type_ids;
                project.Site_ids = CurrentAdmin().Site_ids;
            }
            return project;
        }
        #endregion
    }
}
