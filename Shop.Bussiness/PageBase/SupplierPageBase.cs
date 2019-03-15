using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using System.Collections.Specialized;
using Shop.Model;
using Shop.Tools;
using Shop.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using Shop.Bussiness;
namespace Shop.Bussiness
{
    public class SupplierPageBase : SupplierBase   {
        protected List<Lebi_Supplier_Menu> TopMenus;
        protected List<Lebi_Supplier_Menu> AllMenus;
        protected Lebi_Supplier_Menu CurrentTopMenu;
        protected Lebi_Supplier_Menu CurrentLeftMenu;
        protected string MenuType = "";
        protected int desk;
        protected string lbmenu = "";//头部授权
        protected string lbcopyright = "";//底部版权
        protected string PageReturnMsg = "";
        protected int LeftNewEventTimes;
        protected override void OnLoad(EventArgs e)
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
            {
                Response.Redirect(WebPath + "/");
                Response.End();
                return;
            }
            PageLoadCheck(); //页面载入检查
            #region 当前用户信息
            int selectsupplierid = RequestTool.RequestInt("selectsupplierid");
            if (selectsupplierid > 0)
            {
                //切换超级账号
                string msg = "";
                EX_Supplier.Login(CurrentUser, "", selectsupplierid, out msg);
                CurrentSupplierUser = B_Lebi_Supplier_User.GetModel("User_id = " + CurrentUser.id + " and Supplier_id=" + selectsupplierid + " and Type_id_SupplierUserStatus=9011");
            }
            if (CurrentSupplierUser == null)
                CurrentSupplierUser = EX_Supplier.CurrentSupplierUser(CurrentUser);
        
            if (CurrentSupplierUser.id == 0)
            {
                Response.Redirect(Shop.Bussiness.Site.Instance.SupplierPath + "/Login.aspx");
                return;
            }
            CurrentSupplier = B_Lebi_Supplier.GetModel(CurrentSupplierUser.Supplier_id);
            if (CurrentSupplier.Type_id_SupplierStatus != 442)
            {
                Response.Redirect(Shop.Bussiness.Site.Instance.SupplierPath + "/Login.aspx");
                return;
            }
            CurrentSupplierUserGroup = B_Lebi_Supplier_UserGroup.GetModel(CurrentSupplierUser.Supplier_UserGroup_id);
            if (CurrentSupplierUserGroup == null)
                CurrentSupplierUserGroup = new Lebi_Supplier_UserGroup();
            if (CurrentSupplier != null)
            {
                CurrentSupplierGroup = B_Lebi_Supplier_Group.GetModel(CurrentSupplier.Supplier_Group_id);

                if (CurrentSupplierGroup == null)
                    CurrentSupplierGroup = new Lebi_Supplier_Group();
            }
            else
            {
                Response.Redirect(Shop.Bussiness.Site.Instance.SupplierPath + "/Login.aspx");
                return;
            }
            DateTime LastDate = CurrentSupplier.Time_This;
            TimeSpan ts = System.DateTime.Now - LastDate;
            int NewEventTimes = 0;
            int.TryParse(SYS.NewEventTimes, out NewEventTimes);
            LeftNewEventTimes = Convert.ToInt32(ts.TotalSeconds % (NewEventTimes / 1000));
            LeftNewEventTimes = NewEventTimes - (LeftNewEventTimes * 1000);
            #endregion
            
            #region 配合前台主题
            string themecode = "";
            int siteid = 0;
            var nv = CookieTool.GetCookie("ThemeStatus");
            if (!string.IsNullOrEmpty(nv.Get("theme")))
            {
                themecode = nv.Get("theme");
            }
            if (!string.IsNullOrEmpty(nv.Get("site")))
            {
                int.TryParse(nv.Get("site"), out siteid);
            }
            if (siteid == 0)
                siteid = ShopCache.GetMainSite().id;
            LoadTheme(themecode, siteid, CurrentLanguage.Code, "", true);
            #endregion

            #region 处理菜单
            MenuType = Shop.Tools.CookieTool.GetCookieString("Menu");
            if (MenuType == "")
                MenuType = "Index";

            string menu_ids = "";
            if (CurrentSupplierUserGroup != null && CurrentSupplierUser.User_id != CurrentSupplier.User_id)
                menu_ids = CurrentSupplierUserGroup.Menu_ids;
            else
            {
                CurrentSupplierGroup = B_Lebi_Supplier_Group.GetModel(CurrentSupplier.Supplier_Group_id);
                if (CurrentSupplierGroup != null)
                    menu_ids = CurrentSupplierGroup.Menu_ids;
            }
            //lebi菜单
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                lbmenu = "<ul class=\"tool\"><li><span><a href=\"" + site.AdminPath + "/?desk=1\">" + Tag("桌面") + "</a></span></li></ul>";
                string license = "Copyright 2003-" + DateTime.Now.Year + " <a href=\"http://www.lebi.cn/\" target=\"_blank\" class=\"footcopy\">Lebi.cn</a> , All Rights Reserved. Powered by <a href=\"http://www.lebi.cn/support/license/?url=" + Request.ServerVariables["SERVER_NAME"] + "\" target=\"_blank\" title=\"LebiShop\" class=\"footcopy\">LebiShop</a> V<a href=\"" + site.AdminPath + "/config/version.aspx\">" + SYS.Version + "." + SYS.Version_Son + "</a>";
                try
                {
                    Label LBLicense = (Label)this.Page.FindControl("LBLicense");
                    LBLicense.Text = license;
                }
                catch
                {
                    string strscript = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><script language='javascript'>";
                    strscript += "document.onreadystatechange = loadlicense;";
                    strscript += "function loadlicense(){";
                    strscript += "if(document.readyState == \"complete\"){";
                    strscript += "if ($(\"#lebicopy\")[0] == undefined) {alert('页面布局异常')}";
                    strscript += "$('#lebicopy').html('" + license + "')";
                    strscript += "}}</script>";
                    Response.Write(strscript);
                }
            }
            else
            {
                lbmenu = "<ul class=\"tool\"><li><span><a href=\"" + site.AdminPath + "/?desk=1\">" + Tag("桌面") + "</a></ul>";
            }
            //处理菜单
            desk = RequestTool.RequestInt("desk", 0);
            AllMenus = new List<Lebi_Supplier_Menu>();
            List<Lebi_Supplier_Menu> All = B_Lebi_Supplier_Menu.GetList("Isshow=1", "Sort desc");
            string currentgroupmenu = "," + menu_ids + ",";
            foreach (Lebi_Supplier_Menu m in All)
            {
                if (currentgroupmenu.Contains("," + m.id + ","))
                    AllMenus.Add(m);
            }
            TopMenus = (from m in AllMenus
                        where m.parentid == 0
                        select m).ToList();
            if (desk != 1)
            {
                try
                {
                    var temp = from m in AllMenus
                               where reqPage.Contains(m.URL.ToLower()) && m.parentid > 0 && m.URL != ""
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
                    CookieTool.WriteCookie("sMenu", nvs, 365);
                    //}
                }
                catch (NullReferenceException)
                {
                    var menu = CookieTool.GetCookie("sMenu");
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
                CookieTool.WriteCookie("sMenu", nvs, 365);
            }
            if (CurrentTopMenu == null)
            {
                CurrentTopMenu = new Lebi_Supplier_Menu();
                desk = 1;
            }
            #endregion
            Suppliers = GetSuppliers();


            base.OnLoad(e);

        }

      

        #region 菜单
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

        public List<Lebi_Supplier_Menu> GetMenus(int pid)
        {
            List<Lebi_Supplier_Menu> menus = (from m in AllMenus
                                              where m.parentid == pid
                                              select m).ToList();
            return menus;
        }

        public List<Lebi_Supplier_Menu> GetIndexMenus()
        {
            List<Lebi_Supplier_Menu> menus;
            string indexmenu_ids = "";
            if (CurrentSupplierUserGroup != null && CurrentSupplierUser.User_id != CurrentSupplier.User_id)
                indexmenu_ids = CurrentSupplierUserGroup.Menu_ids_index;
            else
                indexmenu_ids = CurrentSupplierGroup.Menu_ids_index;
            if (indexmenu_ids != "")
                menus = B_Lebi_Supplier_Menu.GetList("id in (" + indexmenu_ids + ")", "Sort desc");
            else
                menus = new List<Lebi_Supplier_Menu>();
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
       
    }


}
