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
    public partial class ajax_supplier : SupplierAjaxBase
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

        /// <summary>
        /// 编辑等级分组
        /// </summary>
        public void Group_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_UserGroup model = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id=" + CurrentSupplier.id + " and id ="+ id);

            if (model == null)
            {
                model = new Lebi_Supplier_UserGroup();
            }
            B_Lebi_Supplier_UserGroup.SafeBindForm(model);
            model.Supplier_id = CurrentSupplier.id;

            if (model.id == 0)
            {
                if (!Power("supplier_group_add", "添加用户分组"))
                {
                    AjaxNoPower();
                    return;
                }
                model.User_id_Add = CurrentUser.id;
                B_Lebi_Supplier_UserGroup.Add(model);
                id = B_Lebi_Supplier_UserGroup.GetMaxId();
                Log.Add("添加用户分组", "Supplier_Group", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                if (!Power("supplier_group_edit", "编辑用户分组"))
                {
                    AjaxNoPower();
                    return;
                }
                model.User_id_Edit = CurrentUser.id;
                model.Time_Edit = System.DateTime.Now;
                B_Lebi_Supplier_UserGroup.Update(model);
                Log.Add("编辑用户分组", "Supplier_Group", id.ToString(), CurrentSupplier, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除会员分组
        /// </summary>
        public void Group_Del()
        {
            string id = RequestTool.RequestSafeString("id");
            if (!Power("supplier_group_del", "删除用户分组"))
            {
                AjaxNoPower();
                return;
            }
            if (id != "")
            {
                List<Lebi_Supplier_UserGroup> models = B_Lebi_Supplier_UserGroup.GetList("Supplier_id=" + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
                foreach (Lebi_Supplier_UserGroup model in models)
                {
                    int Level_id = 1;
                    Lebi_Supplier_UserGroup tmodel = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id=" + CurrentSupplier.id + " and Sort>" + model.Sort + " order by Sort asc");
                    if (tmodel == null)
                    {
                        Level_id = 0;
                    }
                    else
                    {
                        Level_id = tmodel.id;
                    }
                    Common.ExecuteSql("Update Lebi_Supplier_User set Supplier_UserGroup_id = " + Level_id + " where Supplier_UserGroup_id = " + model.id + "");
                    B_Lebi_Supplier_UserGroup.Delete(model.id);
                }
                Log.Add("删除用户分组", "Supplier_Group", id.ToString(), CurrentSupplier, id.ToString());
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑用户
        /// 20141124 未考虑用户同意（拒绝）的功能
        /// </summary>
        public void User_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_User model = B_Lebi_Supplier_User.GetModel("Supplier_id=" + CurrentSupplier.id + " and id ="+ id);
            if (model == null)
            {
                if (!Power("supplier_user_add", "添加用户"))
                {
                    AjaxNoPower();
                    return;
                }
                model = new Lebi_Supplier_User();
            }
            else
            {
                if (!Power("supplier_user_edit", "编辑用户"))
                {
                    AjaxNoPower();
                    return;
                }
            }
            string UserName = RequestTool.RequestSafeString("UserName");
            B_Lebi_Supplier_User.SafeBindForm(model);
            if (model.id == 0)
            {
                Lebi_User user = B_Lebi_User.GetModel("UserName=lbsql{'" + UserName + "'}");
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                    return;
                }
                int count = B_Lebi_Supplier_User.Counts("User_id=" + user.id + " and Supplier_id=" + CurrentSupplier.id + "");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("不能重复添加") + "\"}");
                    return;
                }
                model.User_id = user.id;
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_Supplier_User.Add(model);
                model.id = B_Lebi_Supplier_User.GetMaxId();
                Log.Add("添加用户", "Supplier_User", model.id.ToString(), CurrentSupplier, "用户:" + CurrentUser.UserName);
            }
            else
            {
                if (model.User_id == CurrentSupplier.User_id)
                    model.Type_id_SupplierUserStatus = 9011;//不能锁定所有者
                B_Lebi_Supplier_User.Update(model);
                Log.Add("编辑用户", "Supplier_User", model.id.ToString(), CurrentSupplier, "用户:" + CurrentUser.UserName);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + model.id + "\"}");

        }
        /// <summary>
        /// 删除用户
        /// </summary>
        public void User_Del()
        {
            string ids = RequestTool.RequestSafeString("ids");
            if (!Power("supplier_user_del", "删除用户分组"))
            {
                AjaxNoPower();
                return;
            }
            if (ids != "")
            {
                //不能删除所有者
                B_Lebi_Supplier_User.Delete("id in (lbsql{" + ids + "}) and Supplier_id=" + CurrentSupplier.id + " and User_id!=" + CurrentSupplier.User_id + "");
                Log.Add("删除用户", "Supplier_User", ids, CurrentSupplier, "用户:" + CurrentUser.UserName);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑分组菜单
        /// </summary>
        public void SaveMenu()
        {
            if (!Power("supplier_menu_edit", "编辑分组菜单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("gid", 0);
            Lebi_Supplier_UserGroup group = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id=" + CurrentSupplier.id + " and id ="+id);
            if (group == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (group.Supplier_id != CurrentSupplier.id)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            group.Menu_ids = RequestTool.RequestSafeString("id");
            B_Lebi_Supplier_UserGroup.Update(group);
            Log.Add("编辑分组菜单", "Supplier_User", id.ToString(), CurrentSupplier, "用户:" + CurrentUser.UserName);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑桌面菜单
        /// </summary>
        public void SaveIndexMenu()
        {
            if (!Power("supplier_menu_edit", "编辑分组菜单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("gid", 0);
            Lebi_Supplier_UserGroup group = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (group == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (group.Supplier_id != CurrentSupplier.id)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            group.Menu_ids_index = RequestTool.RequestSafeString("id");
            B_Lebi_Supplier_UserGroup.Update(group);
            Log.Add("编辑分组桌面菜单", "Supplier_User", id.ToString(), CurrentSupplier, "用户:" + CurrentUser.UserName);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑分组权限
        /// </summary>
        public void SavePower()
        {
            if (!Power("supplier_power_edit", "编辑分组权限"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("gid", 0);
            Lebi_Supplier_UserGroup group = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (group == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (group.Supplier_id != CurrentSupplier.id)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            group.Limit_ids = RequestTool.RequestSafeString("id");
            string codes = "";
            if (group.Limit_ids != "")
            {
                List<Lebi_Supplier_Limit> models = B_Lebi_Supplier_Limit.GetList("id in (" + group.Limit_ids + ")", "");
                foreach (Lebi_Supplier_Limit model in models)
                {
                    if (codes == "")
                        codes = "'" + model.Code + "'";
                    else
                        codes += "," + "'" + model.Code + "'";
                }
            }
            group.Limit_Codes = codes;
            B_Lebi_Supplier_UserGroup.Update(group);
            Log.Add("编辑分组桌面菜单", "Supplier_User", id.ToString(), CurrentSupplier, "用户:" + CurrentUser.UserName);
            Response.Write("{\"msg\":\"OK\"}");
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