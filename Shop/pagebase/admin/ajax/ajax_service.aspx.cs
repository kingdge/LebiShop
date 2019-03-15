using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading;
namespace Shop.Admin.Ajax
{
    public partial class Ajax_service : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        #region 授权
        /// <summary>
        /// 设置授权
        /// </summary>
        public void Licence_Set()
        {
            if (!EX_Admin.Power("License_set", "账户设置"))
            {
                AjaxNoPower();
                return;
            }
            string LicenseUserName = RequestTool.RequestString("LicenseUserName");
            string LicensePWD = RequestTool.RequestString("LicensePWD");
            if (LicensePWD == "******")
                LicensePWD = SYS.LicensePWD;
            string LicenseDomain = RequestTool.RequestString("LicenseDomain").ToLower();

            BaseConfig cf = new BaseConfig();
            cf.LicensePWD = LicensePWD;
            cf.LicenseUserName = LicenseUserName;
            cf.LicenseDomain = LicenseDomain;
            B_BaseConfig dob = new B_BaseConfig();

            //============================================
            //获取服务端权限
            LBAPI_Licencse api;
            try
            {
                string res = Shop.LebiAPI.Service.Instanse.License_Set("SetUser", "username=" + LicenseUserName + "&pwd=" + LicensePWD + "&domain=" + LicenseDomain + "&Version=" + ShopCache.GetBaseConfig().Version + "&Version_Son=" + ShopCache.GetBaseConfig().Version_Son + "&code=" + cf.InstallCode);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                api = jss.Deserialize<LBAPI_Licencse>(res);
                cf.LicenseMD5 = api.md5 == null ? "" : api.md5;
                cf.LicenseString = api.data == null ? "" : api.data;
                cf.LicenseDomain = api.domain;
                cf.SpreadCode = api.spreadcode == null ? "" : api.spreadcode;
                cf.LicensePackage = api.servicepackage == null ? "" : api.servicepackage;
                dob.SaveConfig(cf);
                ////<-{修改Web.config文件让授权即时生效 by lebi.kingdge 2018.10.12
                //string path = base.Server.MapPath("../../Web.config");
                //try
                //{
                //    string contents = string.Empty;
                //    contents = File.ReadAllText(path) + "\r\n";
                //    File.WriteAllText(path, contents);
                //}
                //catch (Exception e) { }
                ////}->
                ShopCache.SetLicense();
                Response.Write("{\"msg\":\"" + api.msg + "\"}");
            }
            catch (Exception ex)
            {
                Response.Write("{\"msg\":\"" + ex.Message + "\"}");
            }

        }

        /// <summary>
        /// 更新授权
        /// </summary>
        public void Lincense_Update()
        {
            Shop.LebiAPI.Service.Instanse.License_Update(false);
            Response.Write("{\"msg\":\"OK\"}");
        }
        #endregion
        #region 系统升级
        /// <summary>
        /// 更新系统类型表
        /// </summary>
        public void CheckType()
        {
            Shop.LebiAPI.Service.Instanse.UpdateType();
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 检查新版本
        /// </summary>
        public void CheckVersion()
        {
            int count = Shop.LebiAPI.Service.Instanse.UpdateVersion();

            Response.Write("{\"msg\":\"OK\",\"count\":\"" + count + "\"}");

        }
        /// <summary>
        /// 删除系统版本
        /// </summary>
        public void Version_Del()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id != "")
                B_Lebi_Version.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 下载一个版本的升级文件
        /// </summary>
        public void Version_DownLoad()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            //if (!Shop.LebiAPI.Service.Instanse.Check("zaixianshengji"))
            //{
            //    Response.Write("{\"msg\":\"" + Tag("权限不足") + "\"}");
            //    return;
            //}
            int id = RequestTool.RequestInt("id");
            Lebi_Version model = B_Lebi_Version.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (model.Path_rar == "")
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            string remoteUri = Shop.LebiAPI.Service.Instanse.downurl;
            string fileName = model.Path_rar;
            string myStringWebResource = null;
            try
            {
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myStringWebResource = remoteUri + fileName;
                string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                string Path = ServerPath + "/update/";
                if (!System.IO.File.Exists(Path))   //如果路径不存在，则创建
                {
                    System.IO.Directory.CreateDirectory(Path);
                }

                myWebClient.DownloadFile(myStringWebResource, (Path + model.Version + "_" + model.Version_Son.ToString("0.00").Replace(".", "_") + ".rar"));

            }
            catch (System.Net.WebException)
            { }
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 下载一个文件
        /// </summary>
        public void FileDownLoad()
        {
            string fileName = RequestTool.RequestString("file");

            string remoteUri = Shop.LebiAPI.Service.Instanse.downurl;
            string myStringWebResource = null;
            try
            {

                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myStringWebResource = remoteUri + fileName;
                string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                string Path = ServerPath + fileName.Substring(0, fileName.LastIndexOf("/")) + "/";
                if (!System.IO.File.Exists(Path))   //如果路径不存在，则创建
                {
                    System.IO.Directory.CreateDirectory(Path);
                }

                myWebClient.DownloadFile(myStringWebResource, ServerPath + fileName);

            }
            catch (System.Net.WebException)
            { }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获得一个版本的文件
        /// </summary>
        public void Version_Files()
        {
            int Version = RequestTool.RequestInt("Version");
            decimal Version_Son = RequestTool.RequestDecimal("Version_Son");
            string res = Shop.LebiAPI.Service.Instanse.API("VersionFiles", "Version=" + Version + "&Version_Son=" + Version_Son + "");
            Response.Write(res);
        }

        /// <summary>
        /// 解压部署文件
        /// </summary>
        public void Version_FileUpdate()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id");
            int IsUpdated = RequestTool.RequestInt("IsUpdated", 0);
            Lebi_Version model = B_Lebi_Version.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (!Shop.LebiAPI.Service.Instanse.ISRightVersion(SYS, model))
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            //if (IsUpdated == 0)
            //{
            if (model.Path_rar != "")
            {
                //文件解包+部署文件
                string nistring = HtmlEngine.ReadTxt("/config/noupdate.txt");
                UpDate.DecompressFile(model.Path_rar, "/", nistring);
                //执行更新程序
                try
                {
                    string url = "http://" + HttpContext.Current.Request.Url.Authority + "/update/update.aspx";
                    string res = HtmlEngine.CetHtml(url);
                    if (res.Contains("OK"))
                    {

                        string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                        string fileUrl = ServerPath + "/update/update.aspx";
                        if (File.Exists(fileUrl))
                        {
                            File.Delete(fileUrl);
                        }
                        //fileUrl = ServerPath + "/update.aspx.cs";
                        //if (File.Exists(fileUrl))
                        //{
                        //    File.Delete(fileUrl);
                        //}

                    }
                }
                catch
                {

                }
                //执行sql
                string sqlfile = WebPath + "/update/update.sql";
                if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "access")
                    sqlfile = WebPath + "/update/update_access.sql";
                //string sql = HtmlEngine.ReadTxt(sqlfile);
                //if (sql != null)
                //{
                //    if (sql != "")
                //        Common.ExecuteSql(sql);
                //}
                string fileName = HttpContext.Current.Server.MapPath(@"~/" + sqlfile);
                if (File.Exists(fileName))
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            string text = string.Empty;
                            while (!reader.EndOfStream)
                            {
                                try
                                {
                                    text = reader.ReadLine();
                                    if (text != "")
                                        Common.ExecuteSql(text);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            //}
            //if (model.IsTypeUpdate == 1)
            //{
            //    //更新type表
            //    Thread thread = new Thread(new ThreadStart(Shop.LebiAPI.Service.Instanse.UpdateType));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (model.IsDBStructUpdate == 1)
            //{
            //    //更新数据库结构
            //    Thread thread = new Thread(new ThreadStart(Shop.LebiAPI.Service.Instanse.UpdateDBBody));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (model.IsSystemMenuUpdate == 1)
            //{
            //    //更新系统菜单
            //    Thread thread = new Thread(new ThreadStart(Shop.LebiAPI.Service.Instanse.UpdateMenu));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (model.IsThemePageUpdate == 1)
            //{
            //    //更新系统页面
            //    Thread thread = new Thread(new ThreadStart(Shop.LebiAPI.Service.Instanse.UpdateThemePage));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (model.IsNodeUpdate == 1)
            //{
            //    //更新系统结点
            //    Thread thread = new Thread(new ThreadStart(Shop.LebiAPI.Service.Instanse.UpdateNode));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (model.IsSystemPageUpdate == 1)
            //{
            //    //更新系统页面
            //    Thread thread = new Thread(new ThreadStart(SystemTheme.CreateSystemPage));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (model.IsPageUpdate == 1)
            //{
            //    //更新所有前台页面
            //    Thread thread = new Thread(new ThreadStart(Shop.Bussiness.Theme.CreateThemeALL));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //更新版本号
            BaseConfig cf = new BaseConfig();
            cf.Version_Son = model.Version_Son.ToString();
            cf.Version = model.Version.ToString();
            B_BaseConfig bcf = new B_BaseConfig();
            bcf.SaveConfig(cf);
            model.IsUpdate = 1;
            model.Time_Update = System.DateTime.Now;
            B_Lebi_Version.Update(model);
            //同步版本号
            Shop.LebiAPI.Service.Instanse.UpdateVersionCode();
            Response.Write("{\"msg\":\"OK\"}");

        }
        public void Version_DBUpdate()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id");
            Lebi_Version model = B_Lebi_Version.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (model.IsTypeUpdate == 1)
            {
                //更新type表
                Shop.LebiAPI.Service.Instanse.UpdateType();
            }
            if (model.IsDBStructUpdate == 1)
            {
                //更新数据库结构
                Shop.LebiAPI.Service.Instanse.UpdateDBBody();
            }
            if (model.IsSystemMenuUpdate == 1)
            {
                //更新系统菜单
                Shop.LebiAPI.Service.Instanse.UpdateMenu();
            }
            if (model.IsThemePageUpdate == 1)
            {
                //更新系统页面
                Shop.LebiAPI.Service.Instanse.UpdateThemePage();
            }
            if (model.IsNodeUpdate == 1)
            {
                //更新系统结点
                Shop.LebiAPI.Service.Instanse.UpdateNode();
            }
            Response.Write("{\"msg\":\"OK\"}");

        }
        public void Version_AdminUpdate()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            //更新系统页面
            SystemTheme.CreateSystemPage();
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void Version_WebUpdate()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            //更新系统页面
            Shop.Bussiness.Theme.CreateThemeALL();
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 已经手动更新时调用
        /// </summary>
        public void Version_UpdateOK()
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id");
            Lebi_Version model = B_Lebi_Version.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            //更新版本号
            BaseConfig cf = new BaseConfig();
            cf.Version_Son = model.Version_Son.ToString();
            cf.Version = model.Version.ToString();
            B_BaseConfig bcf = new B_BaseConfig();
            bcf.SaveConfig(cf);
            model.IsUpdate = 1;
            model.Time_Update = System.DateTime.Now;
            B_Lebi_Version.Update(model);
            //同步版本号
            Shop.LebiAPI.Service.Instanse.UpdateVersionCode();
            Response.Write("{\"msg\":\"OK\"}");

        }
        #endregion
        #region 词条
        /// <summary>
        /// 检测词条库最新版本
        /// </summary>
        public void LanguageTagVersion()
        {
            string lang = RequestTool.RequestString("lang");
            Lebi_Language_Tag_Version model = Shop.LebiAPI.Service.Instanse.LanguageTagVersion(lang);
            //model.Content = model.Content.Replace("<br>","\r\n");
            string b = "<div class=\\\"tools tools-m\\\"><ul>";
            b += "<li class=\\\"submit\\\"><a href=\\\"javascript:void(0);\\\" onclick=\\\"TagUpdateDO('" + lang + "');\\\"><b></b><span>" + Tag("立即更新") + "</span></a></li>";
            b += "</ul></div>";
            model.Content += b;
            Response.Write("{\"msg\":\"OK\",\"version\":\"" + model.Version + "\",\"content\":\"" + model.Content + "\"}");
        }
        /// <summary>
        /// 更新一个语种的词条
        /// </summary>
        public void LanguageTagUpdate()
        {
            string lang = RequestTool.RequestString("lang");
            int version = RequestTool.RequestInt("version");
            Shop.LebiAPI.Service.Instanse.LanguageTagUpdate(lang);
            //更新本地词条版本号
            Lebi_Language_Tag_Version ver = new Lebi_Language_Tag_Version();
            ver.Version = version;
            ver.Language_Code = lang;
            B_Lebi_Language_Tag_Version.Add(ver);
            Response.Write("{\"msg\":\"OK\"}");
        }

        #endregion
        #region 获取官方公告
        /// <summary>
        /// 获取官方公告
        /// </summary>
        public void GetNotice()
        {
            string res = HtmlEngine.CetHtml("http://www.lebi.cn/support/notice/index.html");
            Response.Write(res);
        }
        #endregion
    }
}