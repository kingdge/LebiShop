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
using System.IO;
using System.Web.Script.Serialization;


namespace Shop.Admin.Ajax
{
    public partial class Ajax_theme : AdminAjaxBase
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
        /// 编辑模板
        /// </summary>
        public void Theme_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme model = B_Lebi_Theme.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme();
            }
            B_Lebi_Theme.BindForm(model);
            model.ImageSmallUrl = model.ImageUrl.Replace("icon", "icon_small");
            if (model.LebiUser == "")
                model.LebiUser = SYS.LicenseUserName;
            model.Language = RequestTool.RequestSafeString("Language");
            model.ImageBig_Height = SYS.ImageBigHeight;
            model.ImageBig_Width = SYS.ImageBigWidth;
            model.ImageSmall_Height = SYS.ImageSmallHeight;
            model.ImageSmall_Width = SYS.ImageSmallWidth;
            model.Path_Files = RequestTool.RequestString("Path_Files");
            model.Path_CSS = RequestTool.RequestString("Path_CSS");
            model.Path_Image = RequestTool.RequestString("Path_Image");
            model.Path_JS = RequestTool.RequestString("Path_JS");
            model.Path_Advert = model.Path_Files + "/advertment/";
            model.Path_Files = "/" + model.Path_Files.Trim('/');
            model.Path_CSS = "/" + model.Path_CSS.Trim('/');
            model.Path_Image = "/" + model.Path_Image.Trim('/');
            model.Path_JS = "/" + model.Path_JS.Trim('/');
            model.IsUpdate = RequestTool.RequestInt("IsUpdate");
            if (model.id == 0)
            {
                if (!EX_Admin.Power("theme_add", "添加模板"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Theme.Add(model);
                id = B_Lebi_Theme.GetMaxId();
                //model.Code = SYS.LicenseUserName + "_" + id;
                model.Code = DateTime.Now.ToString("yyMMddssfff");
                model.id = id;
                B_Lebi_Theme.Update(model);
                Log.Add("添加模板", "Theme", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("theme_edit", "编辑模板"))
                {
                    AjaxNoPower();
                    return;
                }
                if (model.Code == "")
                    model.Code = DateTime.Now.ToString("yyMMddssfff");
                //model.Code = SYS.LicenseUserName + "_" + id;
                B_Lebi_Theme.Update(model);
                Log.Add("编辑模板", "Theme", id.ToString(), CurrentAdmin, model.Name);
            }
            string Language_ids = RequestTool.RequestString("Language_ids");
            if (Language_ids != "")
            {
                List<Lebi_Language> langs = B_Lebi_Language.GetList("id in (lbsql{" + Language_ids + "})", "");
                foreach (Lebi_Language lang in langs)
                {
                    lang.Theme_id = id;
                    B_Lebi_Language.Update(lang);
                }
            }
            Language.UpdteImageSize();
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        public void Theme_Del()
        {
            if (!EX_Admin.Power("theme_del", "删除模板"))
            {
                EX_Admin.NoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme model = B_Lebi_Theme.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            int count = B_Lebi_Language.Counts("Theme_id=" + model.id + "");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("模板正在使用中，不能删除") + "\"}");
                return;
            }
            Log.Add("删除模板", "Theme", id.ToString(), CurrentAdmin, model.Name);
            B_Lebi_Theme.Delete(id);
            B_Lebi_Theme_Advert.Delete("Theme_id=" + id + "");
            B_Lebi_Theme_Skin.Delete("Theme_id=" + id + "");
            B_Lebi_Advert.Delete("Theme_id=" + id + "");

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除模板-页面
        /// </summary>
        public void Skin_Del()
        {
            if (!EX_Admin.Power("theme_skin_del", "删除模板页面"))
            {
                AjaxNoPower();
                return;
            }
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme_Skin model = B_Lebi_Theme_Skin.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Log.Add("删除模板-页面", "Theme_Skin", id.ToString(), CurrentAdmin, model.Name);
            B_Lebi_Theme_Skin.Delete(id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑模板-页面
        /// </summary>
        public void Skin_Edit()
        {
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme_Skin model = B_Lebi_Theme_Skin.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Skin();
            }
            B_Lebi_Theme_Skin.BindForm(model);
            //if (model.IsPage == 1)
            //    model.Code = "";
            if (model.id == 0)
            {
                if (!EX_Admin.Power("theme_skin_add", "添加模板页面"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Theme_Skin.Add(model);
                id = B_Lebi_Theme_Skin.GetMaxId();
                Log.Add("添加模板-页面", "Theme_Skin", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("theme_skin_edit", "编辑模板页面"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Theme_Skin.Update(model);
                Log.Add("编辑模板-页面", "Theme_Skin", id.ToString(), CurrentAdmin, model.Name);
            }
            //生成页面
            string SkinContent = Request["SkinContent"];
            if (SkinContent != "" && model.Path_Skin != "") //如果模板内容为空则不生成页面 by lebi.kingdge 2015-02-25
            {
                Lebi_Theme theme = B_Lebi_Theme.GetModel(model.Theme_id);
                string SkinPath = theme.Path_Files + "/" + model.Path_Skin;
                GreatSkin(SkinPath, SkinContent);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }


        /// <summary>
        /// 生成页面文件
        /// </summary>
        /// <param name="FileName">生成路径</param>
        /// <param name="SkinContent">内容</param>
        private void GreatSkin(string FileName, string SkinContent)
        {
            FileName = ThemeUrl.GetFullPath(FileName);
            string PhysicsPath = HttpContext.Current.Server.MapPath(@"~/" + ThemeUrl.GetPath(FileName));
            if (!Directory.Exists(PhysicsPath))
            {
                Directory.CreateDirectory(PhysicsPath);
            }
            string PhysicsFileName = HttpContext.Current.Server.MapPath(FileName);
            if (System.IO.File.Exists(PhysicsFileName))
            {
                System.IO.File.Delete(PhysicsFileName);
            }
            HtmlEngine.Instance.WriteFile(PhysicsFileName, SkinContent);
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        public void ThemePage_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme_Page model = B_Lebi_Theme_Page.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Page();
            }
            B_Lebi_Theme_Page.BindForm(model);
            //model.Name = Language.RequestString("Name");
            model.StaticPath = "/" + model.StaticPath;
            model.StaticPath = ThemeUrl.CheckURL(model.StaticPath).TrimEnd('/');
            if (model.id == 0)
            {
                if (!EX_Admin.Power("themepage_add", "添加页面"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Theme_Page.Add(model);
                id = B_Lebi_Theme_Page.GetMaxId();
                string action = Tag("添加页面");
                Log.Add(action, "ThemePage", model.id.ToString(), CurrentAdmin);
            }
            else
            {
                if (!EX_Admin.Power("themepage_edit", "编辑页面"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Theme_Page.Update(model);
                string action = Tag("编辑页面");
                Log.Add(action, "ThemePage", model.id.ToString(), CurrentAdmin);
            }
            //更新模板中的生成路径
            List<Lebi_Theme_Skin> pages = B_Lebi_Theme_Skin.GetList("Code='" + model.Code + "'", "");
            foreach (Lebi_Theme_Skin page in pages)
            {
                page.PageName = model.PageName;
                page.PageParameter = model.PageParameter;
                page.StaticPageName = model.StaticPageName;
                B_Lebi_Theme_Skin.Update(page);
            }
            //处理静态
            ThemeUrl.CreateURLRewrite();
            ShopCache.SetThemePage();
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }

        /// <summary>
        /// 编辑页面,SEO
        /// </summary>
        public void ThemeSEO_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme_Page model = B_Lebi_Theme_Page.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }

            model.SEO_Description = Language.RequestString("SEO_Description");
            model.SEO_Keywords = Language.RequestString("SEO_Keywords");
            model.SEO_Title = Language.RequestString("SEO_Title");

            if (!EX_Admin.Power("seo_edit", "SEO设置"))
            {
                AjaxNoPower();
                return;
            }
            B_Lebi_Theme_Page.Update(model);
            string action = Tag("SEO设置");
            Log.Add(action, "ThemePage", model.id.ToString(), CurrentAdmin);

            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 编辑广告位
        /// </summary>
        public void Advert_Edit()
        {
            if (!EX_Admin.Power("advert_edit", "设置广告位"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Theme_id = RequestTool.RequestInt("Theme_id", 0);
            string code = RequestTool.RequestString("Code");
            int count = B_Lebi_Theme_Advert.Counts("Theme_id=" + Theme_id + " and Code=lbsql{'" + code + "'} and id!=" + id + "");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"代码已存在\"}");
                return;
            }
            Lebi_Theme theme = B_Lebi_Theme.GetModel(Theme_id);
            Lebi_Theme_Advert model = B_Lebi_Theme_Advert.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Advert();
                model = B_Lebi_Theme_Advert.BindForm(model);
                B_Lebi_Theme_Advert.Add(model);
            }
            else
            {
                model = B_Lebi_Theme_Advert.BindForm(model);
                B_Lebi_Theme_Advert.Update(model);
            }
            string action = Tag("设置广告位");
            Log.Add(action, "Theme_Advert", model.id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 编辑添加一个广告
        /// </summary>
        public void AdvertImage_Edit()
        {
            if (!EX_Admin.Power("advertimage_edit", "编辑广告"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Advert model = B_Lebi_Advert.GetModel(id);

            if (model == null)
            {
                model = new Lebi_Advert();
                model = B_Lebi_Advert.BindForm(model);
                model.Language_Codes = Language.LanuageidsToCodes(model.Language_ids);
                B_Lebi_Advert.Add(model);
                model.id = B_Lebi_Advert.GetMaxId();
            }
            else
            {
                model = B_Lebi_Advert.BindForm(model);
                model.Language_Codes = Language.LanuageidsToCodes(model.Language_ids);
                B_Lebi_Advert.Update(model);
            }
            string action = Tag("编辑广告");
            Lebi_Theme theme = B_Lebi_Theme.GetModel(model.Theme_id);
            if (theme != null)
                ImageHelper.LebiImagesUsed(theme.Path_Files + "/advertment/" + model.Image, "advertment", model.id);
            Log.Add(action, "AdvertImage", model.id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 批量更新广告信息
        /// </summary>
        public void AdvertImage_Update()
        {
            if (!EX_Admin.Power("advertimage_edit", "编辑广告"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Uid");
            List<Lebi_Advert> models = B_Lebi_Advert.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Advert model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                model.Title = RequestTool.RequestString("Title" + model.id);
                model.URL = RequestTool.RequestString("URL" + model.id);
                B_Lebi_Advert.Update(model);
            }
            Log.Add("编辑广告", "AdvertImage", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除一个广告
        /// </summary>
        public void AdvertImage_Del()
        {
            if (!EX_Admin.Power("advertimage_del", "删除广告"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            List<Lebi_Advert> models = B_Lebi_Advert.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Advert model in models)
            {
                B_Lebi_Advert.Delete(model.id);
                //处理图片
                ImageHelper.LebiImagesDelete("advertment", model.id);
            }
            string action = Tag("删除广告");
            Log.Add(action, "AdvertImage", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除一个广告位
        /// </summary>
        public void Advert_Del()
        {
            if (!EX_Admin.Power("advert_del", "删除广告位"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);

            B_Lebi_Theme_Advert.Delete(id);
            B_Lebi_Advert.Delete("Theme_Advert_id=" + id + "");
            string action = Tag("删除广告位");
            Log.Add(action, "Advert", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 编辑系统块
        /// </summary>
        public void part_Edit()
        {
            if (!EX_Admin.Power("theme_syspart_edit", "编辑系统块"))
            {
                AjaxNoPower();
                return;
            }
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                AjaxNoPower();
                return;
            }
            string FileName = RequestTool.RequestString("file");
            if (FileName.Contains("."))
            {
                if (!FileName.Contains(".layout") && !FileName.Contains(".html") && !FileName.Contains(".css") && !FileName.Contains(".js"))
                {
                    FileName = "";
                }
            }
            Log.Add("编辑模板-系统块", "Theme_syspart", FileName);

            //生成页面
            string SkinContent = Request["SkinContent"];
            string SkinPath = "/theme/system/" + FileName;
            GreatSkin(SkinPath, SkinContent);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 发布主题
        /// </summary>
        public void Theme_publish()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme model = B_Lebi_Theme.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Theme.BindForm(model);
            model.Path_Advert = model.Path_Files + "/advertment/";
            model.Path_Advert = model.Path_Advert.Replace("//", "/");
            string res = Shop.LebiAPI.Service.Instanse.Theme_Upload(model);

            Response.Write("{\"msg\":\"" + res + "\"}");


        }

        /// <summary>
        /// 下载主题
        /// </summary>
        public void Theme_DownLoad()
        {
            if (!EX_Admin.Power("theme_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string res = Shop.LebiAPI.Service.Instanse.Theme_DownLoad(id, null);
            Response.Write("{\"msg\":\"" + res + "\"}");
        }
        /// <summary>
        /// 升级一个主题
        /// </summary>
        public void Theme_Update()
        {
            if (!EX_Admin.Power("theme_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme theme = B_Lebi_Theme.GetModel(id);

            string res = Shop.LebiAPI.Service.Instanse.Theme_DownLoad(0, theme);
            Response.Write("{\"msg\":\"" + res + "\"}");
        }
        /// <summary>
        /// 检测主题更新
        /// </summary>
        public void Theme_CheckVersion()
        {
            if (!EX_Admin.Power("theme_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            bool flag = Shop.LebiAPI.Service.Instanse.Theme_CheckVersion();
            if (flag)
                Response.Write("{\"msg\":\"OK\"}");
            else
                Response.Write("{\"msg\":\"" + Tag("没有发现更新") + "\"}");
        }
        /// <summary>
        /// 复制一个主题
        /// </summary>
        public void Theme_Copy()
        {
            if (!EX_Admin.Power("theme_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Theme theme = B_Lebi_Theme.GetModel(id);
            if (theme == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string oldfilepath = theme.Path_Files.Clone().ToString();
            theme.Code = "";
            theme.LebiUser = SYS.LicenseUserName;
            theme.LebiUser_id = 0;
            theme.Path_Advert = theme.Path_Advert.Replace(theme.Path_Files, theme.Path_Files + "_[copy]");
            theme.Path_CSS = theme.Path_CSS.Replace(theme.Path_Files, theme.Path_Files + "_[copy]");
            theme.Path_Image = theme.Path_Image.Replace(theme.Path_Files, theme.Path_Files + "_[copy]");
            theme.Path_JS = theme.Path_JS.Replace(theme.Path_Files, theme.Path_Files + "_[copy]");
            theme.Path_Files = theme.Path_Files + "_[copy]";
            theme.Name = theme.Name + "_[copy]";
            theme.Time_Add = System.DateTime.Now;
            B_Lebi_Theme.Add(theme);
            theme.id = B_Lebi_Theme.GetMaxId();
            theme.Code = SYS.LicenseUserName + "_" + theme.id;
            B_Lebi_Theme.Update(theme);
            //复制皮肤
            List<Lebi_Theme_Skin> skins = B_Lebi_Theme_Skin.GetList("Theme_id=" + id + "", "");
            foreach (Lebi_Theme_Skin skin in skins)
            {
                skin.Theme_id = theme.id;
                B_Lebi_Theme_Skin.Add(skin);
            }
            //复制广告位
            List<Lebi_Theme_Advert> adverts = B_Lebi_Theme_Advert.GetList("Theme_id=" + id + "", "");
            foreach (Lebi_Theme_Advert advert in adverts)
            {
                advert.Theme_id = theme.id;
                B_Lebi_Theme_Advert.Add(advert);
                int adid = B_Lebi_Theme_Advert.GetMaxId();
                //复制广告
                List<Lebi_Advert> ads = B_Lebi_Advert.GetList("Theme_Advert_id=" + advert.id + "", "");
                foreach (Lebi_Advert ad in ads)
                {
                    ad.Theme_id = theme.id;
                    ad.Theme_Advert_id = adid;
                    B_Lebi_Advert.Add(ad);
                }
            }
            //==============================================
            //复制文件
            string varFromDirectory = HttpContext.Current.Server.MapPath(@"~/" + oldfilepath);
            string varToDirectory = HttpContext.Current.Server.MapPath(@"~/" + theme.Path_Files);
            FileTool.CopyFiles(varFromDirectory, varToDirectory);
            Response.Write("{\"msg\":\"" + Tag("OK") + "\"}");
        }
        /// <summary>
        /// 编辑模板-页面
        /// </summary>
        public void AdminSkin_Edit()
        {
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string Code = RequestTool.RequestString("Code");
            int count = B_Lebi_AdminSkin.Counts("Code=lbsql{'" + Code + "'} and id!=" + id + "");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("代码重复") + "\"}");
                return;
            }
            Lebi_AdminSkin model = B_Lebi_AdminSkin.GetModel(id);
            if (model == null)
            {
                model = new Lebi_AdminSkin();
            }
            B_Lebi_AdminSkin.BindForm(model);

            if (model.id == 0)
            {
                if (!EX_Admin.Power("adminskin_add", "添加自定义页面"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_AdminSkin.Add(model);
                id = B_Lebi_AdminSkin.GetMaxId();
                Log.Add("添加自定义页面", "AdminSkin", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("adminskin_edit", "编辑自定义页面"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_AdminSkin.Update(model);
                Log.Add("编辑自定义页面", "AdminSkin", id.ToString(), CurrentAdmin, model.Name);
            }
            //生成页面
            string SkinContent = Request["SkinContent"];

            string SkinPath = site.AdminPath + "/custom/skin/" + model.Code + ".html";

            CreatAdminSkin(SkinPath, SkinContent);
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 生成自定义页面文件
        /// </summary>
        /// <param name="FileName">生成路径</param>
        /// <param name="SkinContent">内容</param>
        private void CreatAdminSkin(string FileName, string SkinContent)
        {
            FileName = ThemeUrl.GetFullPath(FileName);
            string PhysicsPath = HttpContext.Current.Server.MapPath(@"~/" + ThemeUrl.GetPath(FileName));
            if (!Directory.Exists(PhysicsPath))
            {
                Directory.CreateDirectory(PhysicsPath);
            }
            string PhysicsFileName = HttpContext.Current.Server.MapPath(FileName);
            if (System.IO.File.Exists(PhysicsFileName))
            {
                System.IO.File.Delete(PhysicsFileName);
            }
            HtmlEngine.Instance.WriteFile(PhysicsFileName, SkinContent);
        }
        /// <summary>
        /// 生成单个自定义页面
        /// </summary>
        public void AdminSkin_Create()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_AdminSkin model = B_Lebi_AdminSkin.GetModel(id);
            if (model == null)
            {
                model = new Lebi_AdminSkin();
            }
            string FileName = site.AdminPath + "/custom/" + model.Code + ".aspx";
            FileName = ThemeUrl.GetFullPath(FileName);
            //string PhysicsPath = HttpContext.Current.Server.MapPath(@"~/" + ThemeUrl.GetPath(FileName));
            //if (!Directory.Exists(PhysicsPath))
            //{
            //    Directory.CreateDirectory(PhysicsPath);
            //}
            string PhysicsFileName = HttpContext.Current.Server.MapPath(FileName);
            if (System.IO.File.Exists(PhysicsFileName))
            {
                System.IO.File.Delete(PhysicsFileName);
            }
            string SkinContent = Request["SkinContent"];
            SkinContent = Shop.Bussiness.Theme.AdminSkin_DoCodeConvert(SkinContent);
            string top = "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"Shop.Bussiness.PageBase.AdminCustomPageBase.cs\" Inherits=\"Shop.Bussiness.AdminCustomPageBase\" %>\r\n";
            top += "<%@ Import Namespace=\"DB.LebiShop\" %>\r\n";
            top += "<%@ Import Namespace=\"Shop.Bussiness\" %>\r\n";
            top += "<%@ Import Namespace=\"Shop.Model\" %>\r\n";
            top += "<%@ Import Namespace=\"System.Collections.Generic\" %>\r\n";
            SkinContent = top + SkinContent;
            HtmlEngine.Instance.WriteFile(PhysicsFileName, SkinContent);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除自定义页面
        /// </summary>
        public void AdminSkin_Del()
        {
            if (!EX_Admin.Power("theme_skin_del", "删除模板页面"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_AdminSkin.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除自定义页面", "AdminSkin", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获取模板页面内容
        /// </summary>
        public void GetSkinContent()
        {
            if (!EX_Admin.Power("theme_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            string filename = RequestTool.RequestString("filename");
            string str = "";
            string path = site.WebPath + "/theme/system/" + filename;
            path = path.ToLower().Replace(".aspx", ".html");
            string FileName = HttpContext.Current.Server.MapPath(@"~/" + path);
            if (File.Exists(FileName))
            {
                str = HtmlEngine.ReadTxt(path);
            }
            else
            {
                str = "";
            }
            Response.Write(str);
        }
    }
}