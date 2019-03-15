using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using System.Collections.Specialized;
namespace Shop.Bussiness
{
    public class AdminPageBase : AdminBase
    {
        protected List<Lebi_Menu> TopMenus;
        protected List<Lebi_Menu> AllMenus;
        protected Lebi_Menu CurrentTopMenu;
        protected Lebi_Menu CurrentLeftMenu;
        protected string Version = "";
        protected string LicenseString = "";
        protected int desk;
        protected string lbmenu = "";//头部授权
        protected string lbcopyright = "";//底部版权
        protected string MenuType = "";
        protected string PageReturnMsg = "";
        protected int LeftNewEventTimes;
        protected override void OnLoad(EventArgs e)
        {
            if (!EX_Admin.LoginStatus() && RequestTool.GetRequestUrlNonDomain().ToLower().IndexOf("login.aspx") < 0)
            {
                //Response.Redirect(site.AdminPath + "/login.aspx?url=" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "");
                string url = site.AdminPath + "/login.aspx?url=" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "&token=" + EX_Admin.MD5(SYS.InstallCode + RequestTool.GetRequestUrlNonDomain()) + "";
                url = "<script type=\"text/javascript\">window.location='" + url + "';</script>";
                Response.Write(url);
                Response.End();
                return;
            }
            CurrentAdmin = EX_Admin.CurrentAdmin();
            //CurrentAdminGroup = (Lebi_Admin_Group)Session["admin_group"];
            CurrentAdminGroup = B_Lebi_Admin_Group.GetModel(CurrentAdmin.Admin_Group_id);
            DateTime LastDate = CurrentAdmin.Time_This;
            TimeSpan ts = System.DateTime.Now - LastDate;
            int NewEventTimes = 0;
            int.TryParse(SYS.NewEventTimes, out NewEventTimes);
            LeftNewEventTimes = Convert.ToInt32(ts.TotalSeconds % (NewEventTimes / 1000));
            LeftNewEventTimes = NewEventTimes - (LeftNewEventTimes * 1000);
            MenuType = LB.Tools.CookieTool.GetCookieString("Menu");
            if (MenuType == "")
                MenuType = "Index";

            if (Shop.LebiAPI.Service.Instanse.ServicepackName(SYS.LicensePackage) == "免费版")
            {
                LicenseString = "<div class=\"licensecheck\" style=\"display:block;float:left;margin:7px 0 0 0;padding:0;height:25px;line-height:25px;color:#ffffff;\"><span><a href=\"http://www.lebi.cn/license/\" target=\"_blank\" style=\"color:#fff;font-size:12px;\">" + Tag("免费版") + "</a></span></div>";
            }
            if (Shop.Bussiness.EX_Admin.CheckPower("version"))
            {
                int vers = B_Lebi_Version.Counts("IsUpdate=0 and Version_Check like '%" + SYS.Version + "." + SYS.Version_Son + "%'");
                if (vers > 0)
                    Version = "<div id=\"version\"><a href=\"" + site.AdminPath + "/config/Version.aspx\" style=\"color:red\" >" + Tag("发现新版本") + " " + Tag("点击此处更新") + " </a></div>";
            }
            //lebi菜单
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                var licenseuserurl = "username=" + SYS.LicenseUserName + "&password=" + EX_User.MD5(SYS.LicenseUserName + System.DateTime.Now.Day) + "&vc=" + EX_User.MD5(SYS.LicensePWD);
                lbmenu = "<ul class=\"tool\"><li><span><a href=\"" + site.AdminPath + "/Ajax/ajax_admin.aspx?__Action=MenuJump&pid=0\">" + Tag("桌面") + "</a> | <a href=\"javascript:void(0);\" onclick=\"Copyright();\">" + Tag("关于") + "</a></span></li></ul>";
                lbmenu += "<ul class=\"faq\"><li><a href=\"http://www.lebi.cn/support/checkuser.aspx?" + licenseuserurl + "\" title=\"" + Tag("客服中心") + "\" target=\"_blank\"><b></b><span>" + Tag("客服中心") + "</span></a></li></ul>";
                string license = "Copyright 2003-" + DateTime.Now.Year + " <a href=\"http://www.lebi.cn/\" target=\"_blank\" class=\"footcopy\">Lebi.cn</a> , All Rights Reserved. Powered by <a href=\"http://www.lebi.cn/support/license/?url=" + Request.ServerVariables["SERVER_NAME"] + "\" target=\"_blank\" title=\"LebiShop\" class=\"footcopy\">LebiShop</a> V<a href=\"" + site.AdminPath + "/config/version.aspx\">" + SYS.Version + "." + SYS.Version_Son + "</a>";
                try
                {
                    Label LBLicense = (Label)this.Page.FindControl("LBLicense");
                    LBLicense.Text = license;
                }
                catch
                {
                    string strscript = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                    //strscript = "<script language='javascript'>";
                    //strscript += "document.onreadystatechange = loadlicense;";
                    //strscript += "function loadlicense(){";
                    //strscript += "if(document.readyState == \"complete\"){";
                    //strscript += "if ($(\"#lebicopy\")[0] == undefined) {alert('页面布局异常')}else{";
                    //strscript += "$('#lebicopy').html('" + license + "')}";
                    //strscript += "}}</script>";
                    strscript += license;
                    Response.Write(strscript);
                }
            }
            else
            {
                lbmenu = "<ul class=\"tool\"><li><span><a href=\"" + site.AdminPath + "/Ajax/ajax_admin.aspx?__Action=MenuJump&pid=0\">" + Tag("桌面") + "</a> | <a href=\"javascript:void(0);\" onclick=\"Copyright();\">" + Tag("关于") + "</a></span></li></ul>";
            }
            //处理菜单
            desk = RequestTool.RequestInt("desk", 0);
            AllMenus = new List<Lebi_Menu>();
            List<Lebi_Menu> All = B_Lebi_Menu.GetList("Isshow=1", "Sort desc");
            string currentgroupmenu = "," + CurrentAdminGroup.Menu_ids + ",";
            if (CurrentAdmin.AdminType == "super")
            {
                AllMenus = All;
            }
            else
            {
                foreach (Lebi_Menu m in All)
                {
                    if (currentgroupmenu.Contains("," + m.id + ","))
                        AllMenus.Add(m);
                }
            }
            TopMenus = (from m in AllMenus
                        where m.parentid == 0
                        select m).ToList();
            string currentPage = RequestTool.GetRequestUrl().ToLower();
            int apathindex = currentPage.IndexOf(site.AdminPath.ToLower());
            currentPage = currentPage.Substring(apathindex, currentPage.Length - apathindex);
            currentPage = currentPage.Substring(site.AdminPath.Length, currentPage.Length - site.AdminPath.Length).TrimStart('/');
            if (desk != 1)
            {
                try
                {
                    var temp = from m in AllMenus
                               where currentPage.IndexOf(m.URL.ToLower().TrimStart('/')) == 0 && m.parentid > 0 && m.URL != ""
                               select m;
                    //if (temp == null)
                    //    CurrentLeftMenu = new Lebi_Menu();
                    //else
                    //{
                    CurrentLeftMenu = temp.ToList().FirstOrDefault();
                    CurrentTopMenu = (from m in AllMenus
                                      where m.id == CurrentLeftMenu.parentid
                                      select m).ToList().FirstOrDefault();
                    CurrentTopMenu = (from m in TopMenus
                                      where m.id == CurrentTopMenu.parentid
                                      select m).ToList().FirstOrDefault();
                    ////写入cookie
                    NameValueCollection nvs = new NameValueCollection();
                    nvs.Add("top", CurrentTopMenu.id.ToString());
                    nvs.Add("left", CurrentLeftMenu.id.ToString());
                    CookieTool.WriteCookie("Menu", nvs, 30);
                    //}
                }
                catch (NullReferenceException)
                {
                    var menu = CookieTool.GetCookie("Menu");
                    int tid = 0;
                    int lid = 0;
                    int.TryParse(menu.Get("top"), out tid);
                    int.TryParse(menu.Get("left"), out lid);
                    CurrentTopMenu = (from m in TopMenus
                                      where m.id == tid
                                      select m).ToList().FirstOrDefault();
                    CurrentLeftMenu = (from m in AllMenus
                                       where m.id == lid
                                       select m).ToList().FirstOrDefault();

                }

            }
            else
            {
                //写入cookie
                NameValueCollection nvs = new NameValueCollection();
                nvs.Add("top", "0");
                nvs.Add("left", "0");
                CookieTool.WriteCookie("Menu", nvs, 365);
            }
            if (CurrentTopMenu == null)
            {
                CurrentTopMenu = new Lebi_Menu();
                desk = 1;
            }

            CheckPagePower();
            if (CurrentAdmin.Avatar == "")
            {
                CurrentAdmin.Avatar = site.AdminImagePath + "/Avatar.jpg";
            }
            base.OnLoad(e);
        }
        #region 菜单相关
        public string CMSMenu(string code)
        {
            string str = "";
            Lebi_Node node = B_Lebi_Node.GetModel("Code='" + code + "'");
            if (node == null)
                return "";
            List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + node.id + "", "Sort desc");
            int i = 1;
            foreach (Lebi_Node n in nodes)
            {
                str += "<li name=\"News\" id=\"News" + i + "\"><a href=\"" + NodePage.AdminIndexPage(n) + "\"><span>" + Tag(n.Name) + "</span></a> </li>";
                i++;
            }
            return str;
        }

        public string MenuUrl(string url, int t)
        {
            if (url.IndexOf("http") == 0)
                return url;
            url = site.AdminPath + "/" + url;
            if (t == 1)
            {
                if (url.Contains("?"))
                    url = url + "&desk=1";
                else
                    url = url + "?desk=1";
            }
            url = ThemeUrl.CheckURL(url);
            return url;
        }

        public List<Lebi_Menu> GetMenus(int pid)
        {
            List<Lebi_Menu> menus = (from m in AllMenus
                                     where m.parentid == pid
                                     select m).ToList();
            return menus;
        }
        public List<Lebi_Menu> GetMenus(string parentCode)
        {
            List<Lebi_Menu> menus = (from m in AllMenus
                                     where m.parentCode == parentCode
                                     select m).ToList();
            return menus;
        }
        public List<Lebi_Menu> GetIndexMenus()
        {
            List<Lebi_Menu> menus;
            if (CurrentAdminGroup.Menu_ids_index != "")
                menus = B_Lebi_Menu.GetList("id in (" + CurrentAdminGroup.Menu_ids_index + ")", "Sort desc");
            else
                menus = new List<Lebi_Menu>();
            return menus;
        }

        /// <summary>
        /// 根据COOKIE检查菜单是否显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MenuShow(int id)
        {
            string menuids = RequestTool.GetCookieString("menus", "");
            if (menuids.Contains("|" + id + "|"))
                return false;
            return true;
        }
        #endregion
        /// <summary>
        /// 返回每日箴言
        /// </summary>
        /// <returns></returns>
        public string Tips()
        {
            string sqlwhere = "";
            string order = "";
            sqlwhere = " Time_Update>='" + FormatTime(System.DateTime.Now.Date) + "' and Time_Update<'" + FormatTime(System.DateTime.Now.Date.AddDays(1)) + "'";
            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                order = "newid()";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
              
                order = "id desc";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                order = "rand()";
            }
            Lebi_Tips tips = B_Lebi_Tips.GetModel(sqlwhere);
            if (tips != null)
            {
                return Lang(tips.Content);
            }
            else
            {
                List<Lebi_Tips> tipslist = B_Lebi_Tips.GetList("", order);
                tips = tipslist.FirstOrDefault();
                if (tips == null)
                {
                    tips = new Lebi_Tips();
                }
                else
                {
                    tips.Time_Update = System.DateTime.Now;
                    B_Lebi_Tips.Update(tips);
                    return Lang(tips.Content);
                }
            }
            return "";
        }
    }


}
