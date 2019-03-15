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


namespace Shop.Supplier.Ajax
{
    public partial class ajax_admin : SupplierAjaxBase
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
        /// 头部菜单跳转
        /// </summary>
        /// <param name="pid"></param>
        public void MenuJump()
        {
            int pid = RequestTool.RequestInt("pid", 0);
            if (pid == 0)
            {
                ////前往快捷桌面菜单
                //if (EX_Admin.Power("admin_data", "系统桌面"))
                //{
                //    Response.Redirect(AdminMenuURL("default.aspx?desk=1"));
                //}
                if (CurrentSupplierGroup.Menu_ids_index != "")
                {
                    List<Lebi_Supplier_Menu> ims = B_Lebi_Supplier_Menu.GetList("id in (" + CurrentSupplierGroup.Menu_ids_index + ")", "Sort desc");
                    foreach (Lebi_Supplier_Menu im in ims)
                    {
                        if (im.URL.Contains("?"))
                        {
                            im.URL = im.URL + "&desk=1";
                        }
                        else
                        {
                            im.URL = im.URL + "?desk=1";
                        }
                        Response.Redirect(AdminMenuURL(im.URL));
                        return;
                    }
                }
            }
            string currentgroupmenu = "," + CurrentSupplierUserGroup.Menu_ids + ",";
            string andwhere = "";
            if (CurrentSupplierGroup.Menu_ids != "")
                andwhere = " and id in (" + CurrentSupplierGroup.Menu_ids + ")";
            bool ismaster = false;
            if (CurrentSupplier.User_id == CurrentUser.id)
                ismaster = true;

            List<Lebi_Supplier_Menu> ms = B_Lebi_Supplier_Menu.GetList("parentid=" + pid + andwhere, "Sort desc");
            foreach (Lebi_Supplier_Menu m in ms)
            {
                if (currentgroupmenu.Contains("," + m.id + ",") || ismaster)
                {
                    List<Lebi_Supplier_Menu> models = B_Lebi_Supplier_Menu.GetList("parentid=" + m.id + andwhere, "Sort desc");
                    foreach (Lebi_Supplier_Menu model in models)
                    {
                        if (currentgroupmenu.Contains("," + model.id + ",") || ismaster)
                        {
                            Response.Redirect(AdminMenuURL(model.URL));
                            return;
                        }
                    }
                }
            }
            Response.Write(Tag("菜单设置错误"));
        }

        public string AdminMenuURL(string url)
        {
            if (url.IndexOf("http") != 0)
            {
                url = site.AdminPath + "/" + url;
                url = ThemeUrl.CheckURL(url);
            }
            return url;
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
        public List<Lebi_Supplier_Menu> Supplier_GetMenus(int pid)
        {
            string menu_ids = "";
            if (CurrentSupplierUserGroup != null && CurrentSupplierUser.User_id != CurrentSupplier.User_id)
                menu_ids = CurrentSupplierUserGroup.Menu_ids;
            else
            {
                CurrentSupplierGroup = B_Lebi_Supplier_Group.GetModel(CurrentSupplier.Supplier_Group_id);
                if (CurrentSupplierGroup != null)
                    menu_ids = CurrentSupplierGroup.Menu_ids;
            }
            AllMenus = new List<Lebi_Supplier_Menu>();
            List<Lebi_Supplier_Menu> All = B_Lebi_Supplier_Menu.GetList("Isshow=1", "Sort desc");
            string currentgroupmenu = "," + menu_ids + ",";
            foreach (Lebi_Supplier_Menu m in All)
            {
                if (currentgroupmenu.Contains("," + m.id + ","))
                    AllMenus.Add(m);
            }
            List<Lebi_Supplier_Menu> menus = (from m in AllMenus
                                              where m.parentid == pid
                                              select m).ToList();
            return menus;
        }
        public void GetMenu()
        {
            string code = RequestTool.RequestSafeString("code");
            int id = RequestTool.RequestInt("id", 0);
            string str = "";
            //if (Shop.Tools.CookieTool.GetCookieString("ShopMenu" + code) == ""){
            foreach (Shop.Model.Lebi_Supplier_Menu pmenu in Supplier_GetMenus(id))
            {
                str += "<h2><span><img mid=\"" + pmenu.id + "\" src=\"" + site.AdminImagePath + "/2015/minus.png\" id=\"imgStatis\" /> " + Tag(pmenu.Name) + "</span></h2>";
                str += "<ul class=\"clear\">";
                foreach (Shop.Model.Lebi_Supplier_Menu menu in Supplier_GetMenus(pmenu.id))
                {
                    str += "<li><a href=\"" + MenuUrl(menu.URL, 0) + "\"><span>" + Tag(menu.Name) + "</span></a></li>";
                }
                str += "</ul>";
            }
            //    Response.Cookies.Add(new HttpCookie("ShopMenu" + code, str));
            //}else{
            //    str = Shop.Tools.CookieTool.GetCookieString("ShopMenu" + code);
            //}
            Response.Write(str);
        }
    }
}