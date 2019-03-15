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


namespace Shop.Admin.Ajax
{
    public partial class Ajax_admin : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 编辑管理员头像
        /// </summary>
        public void Admin_Avatar_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Administrator model = B_Lebi_Administrator.GetModel(id);
            if (model != null)
            {
                if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Administrator.BindForm(model);
                model.Avatar = RequestTool.RequestSafeString("Avatar");
                B_Lebi_Administrator.Update(model);
                string action = Tag("编辑系统用户");
                string description = model.UserName + " - " + model.Avatar;
                Log.Add(action, "Administrator", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑管理员
        /// </summary>
        public void Admin_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Administrator model = B_Lebi_Administrator.GetModel(id);
            if (model == null)
            {
                if (!EX_Admin.Power("admin_add", "添加系统用户"))
                {
                    AjaxNoPower();
                    return;
                }
                string UserName = RequestTool.RequestSafeString("UserName");
                int count = B_Lebi_Administrator.Counts("UserName=lbsql{'" + UserName + "'}");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"账户已存在\"}");
                    return;
                }
                model = new Lebi_Administrator();
                B_Lebi_Administrator.BindForm(model);
                model.Pro_Type_ids = RequestTool.RequestSafeString("Pro_Type_ids");
                model.Site_ids = RequestTool.RequestSafeString("Site_ids");
                model.Project_ids = RequestTool.RequestSafeString("Project_ids");
                string password = RequestTool.RequestSafeString("Password");
                model.Password = EX_Admin.MD5(password);
                B_Lebi_Administrator.Add(model);
                string action = Tag("添加系统用户");
                string description = model.UserName;
                Log.Add(action, "Administrator", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Administrator.BindForm(model);
                model.Pro_Type_ids = RequestTool.RequestString("Pro_Type_ids");
                model.Site_ids = RequestTool.RequestString("Site_ids");
                B_Lebi_Administrator.Update(model);
                string action = Tag("编辑系统用户");
                string description = model.UserName;
                Log.Add(action, "Administrator", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void Admin_Del()
        {
            if (!EX_Admin.Power("admin_del", "删除系统用户"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            B_Lebi_Administrator.Delete("id in (lbsql{" + ids + "}) and AdminType!='super'");
            string action = Tag("删除系统用户");
            string description = ids;
            Log.Add(action, "Administrator", ids, CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void Admin_PWD()
        {

            int id = RequestTool.RequestInt("id", 0);
            Lebi_Administrator model = B_Lebi_Administrator.GetModel(id);
            if (model == null)
            {
                model = CurrentAdmin;
            }

            string PWD1 = RequestTool.RequestString("PWD1");
            string PWD2 = RequestTool.RequestString("PWD2");
            string PWD = EX_Admin.MD5(PWD1);
            //if (model.id != CurrentAdmin.id)
            //{
            if (!EX_Admin.Power("admin_pwd", "编辑系统用户密码"))
            {
                AjaxNoPower();
                return;
            }
            //}

            if (!EX_Admin.Power("admin_pwd", "编辑系统用户密码"))
            {
                if (PWD != model.Password)
                {
                    Response.Write("{\"msg\":\"原始密码错误\"}");
                    return;
                }
            }

            if (PWD1 != PWD2)
            {
                Response.Write("{\"msg\":\"两次输入的密码不一致\"}");
                return;
            }
            model.Password = PWD;
            B_Lebi_Administrator.Update(model);
            string action = Tag("编辑系统用户密码");
            string description = model.UserName;
            Log.Add(action, "Administrator", model.id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑权限代码
        /// </summary>
        public void AdminLimit_Edit()
        {
            if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Admin_Limit model = B_Lebi_Admin_Limit.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.Name = RequestTool.RequestString("name");
            model.parentid = RequestTool.RequestInt("parentid", 0);
            B_Lebi_Admin_Limit.Update(model);
            string action = Tag("编辑系统用户权限");
            string description = RequestTool.RequestString("name");
            Log.Add(action, "Admin_Limit", model.id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 编辑权限-编辑过滤页面
        /// </summary>
        public void Admin_url_Edit()
        {
            if (!EX_Admin.Power("admin_url_edit", "编辑过滤页面"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int gid = RequestTool.RequestInt("gid", 0);
            string url = RequestTool.RequestString("Url");
            Lebi_Admin_Group group = B_Lebi_Admin_Group.GetModel(gid);
            if (url == "")
            {
                Response.Write("{\"msg\":\"" + Tag("地址不能为空") + "\"}");
                return;
            }
            if (group == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Admin_Power p = B_Lebi_Admin_Power.GetModel(id);
            if (p == null)
            {
                p = B_Lebi_Admin_Power.GetList("Admin_Group_id=" + group.id + " and Url=lbsql{'" + url + "'}", "").FirstOrDefault();
                if (p != null)
                {
                    Response.Write("{\"msg\":\"" + Tag("地址已经存在") + "\"}");
                    return;
                }
                p = new Lebi_Admin_Power();
                p.Admin_Group_id = group.id;
                p.Url = url;
                B_Lebi_Admin_Power.Add(p);
                string action = Tag("添加过滤页面");
                string description = url;
                Log.Add(action, "Admin_Power", p.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                p.Url = url;
                B_Lebi_Admin_Power.Update(p);
                string action = Tag("编辑过滤页面");
                string description = url;
                Log.Add(action, "Admin_Power", p.id.ToString(), CurrentAdmin, description);
            }

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑权限-删除过滤页面
        /// </summary>
        public void Admin_url_Del()
        {
            if (!EX_Admin.Power("admin_url_edit", "编辑过滤页面"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("uid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Admin_Power.Delete("id in (lbsql{" + id + "})");
            string action = Tag("删除过滤页面");
            string description = id;
            Log.Add(action, "Admin_Power", id, CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑管理员权限组
        /// </summary>
        public void Admin_Group_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Admin_Group model = B_Lebi_Admin_Group.GetModel(id);
            if (model == null)
            {
                if (!EX_Admin.Power("admin_group_add", "添加权限组"))
                {
                    AjaxNoPower();
                    return;
                }
                model = new Lebi_Admin_Group();
                model = B_Lebi_Admin_Group.BindForm(model);
                model.Name = Language.RequestString("Name");
                B_Lebi_Admin_Group.Add(model);
                string action = Tag("添加权限组");
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(action, "Admin_Group", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("admin_group_edit", "编辑权限组"))
                {
                    AjaxNoPower();
                    return;
                }
                model = B_Lebi_Admin_Group.BindForm(model);
                model.Name = Language.RequestString("Name");
                B_Lebi_Admin_Group.Update(model);
                string action = Tag("添加权限组");
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(action, "Admin_Group", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除权限组
        /// </summary>
        public void Admin_Group_Del()
        {
            if (!EX_Admin.Power("admin_group_del", "删除权限组"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Admin_Group.Delete("id in (lbsql{" + ids + "})");
            string action = Tag("删除权限组");
            string description = ids;
            Log.Add(action, "Admin_Group", ids, CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        public void Admin_limit_Edit()
        {
            if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Admin_Limit model = B_Lebi_Admin_Limit.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Admin_Limit();
                model = B_Lebi_Admin_Limit.BindForm(model);
                B_Lebi_Admin_Limit.Add(model);
                string action = Tag("添加权限分组");
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(action, "Admin_Limit", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                model = B_Lebi_Admin_Limit.BindForm(model);
                B_Lebi_Admin_Limit.Update(model);
                string action = Tag("编辑权限分组");
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(action, "Admin_Limit", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除一个权限代码
        /// </summary>
        public void Admin_limit_Del()
        {
            if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Admin_Limit model = B_Lebi_Admin_Limit.GetModel(id);
            string action = Tag("删除权限分组");
            string description = Shop.Bussiness.Language.Content(model.Name, "CN");
            Log.Add(action, "Admin_Limit", id.ToString(), CurrentAdmin, description);
            B_Lebi_Admin_Limit.Delete(id);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        public void SavePower()
        {
            if (!EX_Admin.Power("admin_limit_edit", "编辑权限"))
            {
                EX_Admin.NoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            int gid = RequestTool.RequestInt("gid", 0);
            Lebi_Admin_Group group = B_Lebi_Admin_Group.GetModel(gid);
            if (group == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Admin_Power.Delete("Admin_Group_id=" + group.id + "");
            List<Lebi_Admin_Limit> models = B_Lebi_Admin_Limit.GetList("id in (lbsql{" + ids + "})", "");
            if (models != null)
            {
                Lebi_Admin_Power p = new Lebi_Admin_Power();
                foreach (Lebi_Admin_Limit model in models)
                {
                    p.Admin_Group_id = group.id;
                    p.Admin_Limit_Code = model.Code;
                    p.Admin_Limit_id = model.id;
                    B_Lebi_Admin_Power.Add(p);
                }
            }
            string action = Tag("编辑权限");
            string description = Shop.Bussiness.Language.Content(group.Name, "CN");
            Log.Add(action, "Admin_Power", gid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除操作日志
        /// </summary>
        public void Log_Del()
        {
            if (!EX_Admin.Power("log_del", "删除操作日志"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Log.Delete("id in (lbsql{" + ids + "})");
            string action = Tag("删除操作日志");
            string description = ids;
            Log.Add(action, "Log", ids, CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 编辑后台菜单
        /// </summary>
        public void Menu_Edit()
        {
            if (!EX_Admin.Power("admin_menu_edit", "编辑菜单"))
            {
                EX_Admin.NoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int pid = RequestTool.RequestInt("pid", 0);
            int parentid = RequestTool.RequestInt("parentid", 0);
            string parentCode = "";
            if (parentid > 0)
            {
                Lebi_Menu parent = B_Lebi_Menu.GetModel(parentid);
                parentCode = parent.Code;
            }
            Lebi_Menu model = B_Lebi_Menu.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Menu();
                B_Lebi_Menu.BindForm(model);
                model.Isshow = 1;
                model.parentCode = parentCode;
                B_Lebi_Menu.Add(model);
            }
            else
            {
                model.parentCode = parentCode;
                B_Lebi_Menu.BindForm(model);
                B_Lebi_Menu.Update(model);
            }
            ImageHelper.LebiImagesUsed(model.Image, "menu", id);
            string action = Tag("编辑菜单");
            Log.Add(action, "Admin_Power", id.ToString(), CurrentAdmin, model.Name);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除一个菜单
        /// </summary>
        public void Menu_Del()
        {
            if (!EX_Admin.Power("admin_menu_edit", "编辑菜单"))
            {
                EX_Admin.NoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Menu model = B_Lebi_Menu.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            else
            {
                //if (model.IsSYS == 0)
                    B_Lebi_Menu.Delete(id);
            }
            string action = Tag("删除菜单");
            Log.Add(action, "Admin_Power", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑管理员菜单
        /// </summary>
        public void SaveMenu()
        {
            if (!EX_Admin.Power("admin_menu_edit", "编辑菜单"))
            {
                EX_Admin.NoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            int gid = RequestTool.RequestInt("gid", 0);
            Lebi_Admin_Group group = B_Lebi_Admin_Group.GetModel(gid);
            if (group == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            group.Menu_ids = ids;
            B_Lebi_Admin_Group.Update(group);
            string action = Tag("编辑菜单");
            string description = Shop.Bussiness.Language.Content(group.Name, "CN");
            Log.Add(action, "Admin_Power", gid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑管理员桌面菜单
        /// </summary>
        public void SaveIndexMenu()
        {
            if (!EX_Admin.Power("admin_menu_edit", "编辑菜单"))
            {
                EX_Admin.NoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            int gid = RequestTool.RequestInt("gid", 0);
            Lebi_Admin_Group group = B_Lebi_Admin_Group.GetModel(gid);
            if (group == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            group.Menu_ids_index = ids;
            B_Lebi_Admin_Group.Update(group);
            string action = Tag("编辑桌面菜单");
            string description = Shop.Bussiness.Language.Content(group.Name, "CN");
            Log.Add(action, "Admin_Power", gid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
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
                //前往快捷桌面菜单
                if (EX_Admin.Power("admin_data", "系统桌面") || RequestTool.GetConfigKey("DemoSite").Trim() == "1")
                {
                    Response.Redirect(AdminMenuURL("default.aspx?desk=1"));
                }
                if (CurrentAdminGroup.Menu_ids_index != "")
                {
                    List<Lebi_Menu> ims = B_Lebi_Menu.GetList("id in (" + CurrentAdminGroup.Menu_ids_index + ")", "Sort desc");
                    foreach (Lebi_Menu im in ims)
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
            string currentgroupmenu = "," + CurrentAdminGroup.Menu_ids + ",";
            List<Lebi_Menu> ms = B_Lebi_Menu.GetList("parentid=" + pid + "", "Sort desc");
            foreach (Lebi_Menu m in ms)
            {
                if (CurrentAdmin.AdminType == "super")
                {
                    List<Lebi_Menu> models = B_Lebi_Menu.GetList("parentid=" + m.id + "", "Sort desc");
                    foreach (Lebi_Menu model in models)
                    {
                        Response.Redirect(AdminMenuURL(model.URL));
                        return;
                    }
                }
                else
                {
                    if (currentgroupmenu.Contains("," + m.id + ","))
                    {
                        List<Lebi_Menu> models = B_Lebi_Menu.GetList("parentid=" + m.id + "", "Sort desc");
                        foreach (Lebi_Menu model in models)
                        {
                            if (currentgroupmenu.Contains("," + model.id + ","))
                            {
                                Response.Redirect(AdminMenuURL(model.URL));
                                return;
                            }
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
        public List<Lebi_Menu> GetMenus(int pid)
        {
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
            List<Lebi_Menu> menus = (from m in AllMenus
                                     where m.parentid == pid
                                     select m).ToList();
            return menus;
        }
        public List<Lebi_Menu> GetMenus(string parentCode)
        {
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
            List<Lebi_Menu> menus = (from m in AllMenus
                                     where m.parentCode == parentCode
                                     select m).ToList();
            return menus;
        }
        public void GetMenu()
        {
            string code = RequestTool.RequestSafeString("code");
            int id = RequestTool.RequestInt("id",0);
            string str = "";
            //if (LB.Tools.CookieTool.GetCookieString("ShopMenu" + code) == ""){
                foreach (Lebi_Menu pmenu in GetMenus(id)){
                    str += "<h2><span><img mid=\"" + pmenu.id + "\" src=\"" + site.AdminImagePath + "/2015/minus.png\" id=\"imgStatis\" /> " + Tag(pmenu.Name) + "</span></h2>";
                    str +="<ul class=\"clear\">";
                    if (pmenu.Code != "")
                    {
                        foreach (Lebi_Menu menu in GetMenus(pmenu.Code))
                        {
                            str += "<li><a href=\"" + MenuUrl(menu.URL, 0) + "\"><span>" + Tag(menu.Name) + "</span></a></li>";
                        }
                    }
                    else
                    {
                        foreach (Lebi_Menu menu in GetMenus(pmenu.id))
                        {
                            str += "<li><a href=\"" + MenuUrl(menu.URL, 0) + "\"><span>" + Tag(menu.Name) + "</span></a></li>";
                        }
                    }
                    str +="</ul>";
                }
            //    Response.Cookies.Add(new HttpCookie("ShopMenu" + code, str));
            //}else{
            //    str = LB.Tools.CookieTool.GetCookieString("ShopMenu" + code);
            //}
            Response.Write(str);
        }
        /// <summary>
        /// 编辑项目
        /// </summary>
        public void Project_Edit()
        {
            if (!EX_Admin.Power("admin_project", "项目列表"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Project model = B_Lebi_Project.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Project();
                B_Lebi_Project.SafeBindForm(model);
                model.Supplier_ids = RequestTool.RequestSafeString("Supplier_ids");
                model.Site_ids = RequestTool.RequestSafeString("Site_ids");
                model.Pro_Type_ids = RequestTool.RequestSafeString("Pro_Type_ids");
                B_Lebi_Project.Add(model);
                Log.Add("添加项目", "Project", model.id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                B_Lebi_Project.SafeBindForm(model);
                model.Supplier_ids = RequestTool.RequestString("Supplier_ids");
                model.Site_ids = RequestTool.RequestString("Site_ids");
                model.Pro_Type_ids = RequestTool.RequestSafeString("Pro_Type_ids");
                B_Lebi_Project.Update(model);
                Log.Add("编辑项目", "Project", model.id.ToString(), CurrentAdmin, model.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void Project_Del()
        {
            if (!EX_Admin.Power("admin_project", "项目列表"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            B_Lebi_Project.Delete("id in (lbsql{" + ids + "})");
            Log.Add("删除项目", "Project", ids, CurrentAdmin, ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}