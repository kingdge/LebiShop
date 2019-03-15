using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.IO;
namespace Shop.Admin.Config
{
    public partial class BackUP : AdminPageBase
    {
        protected List<Lebi_Email> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("backup_add", "添加数据备份") || !EX_Admin.Power("backup_delete", "删除数据备份") || !EX_Admin.Power("backup_config", "数据备份配置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
        }
        public string GetList()
        {
            string str = "";
            try
            {
                string backpath = ShopCache.GetBaseConfig().DataBase_BackPath;
                string backname = ShopCache.GetBaseConfig().DataBase_BackName;


                //if (backpath.IndexOf("/") == 0)
                //{
                //    backpath = System.Web.HttpRuntime.AppDomainAppPath + "/" + backpath;
                //}
                //else
                //{
                    backpath = System.Web.HttpRuntime.AppDomainAppPath  + backpath;
                //}
                if (!Directory.Exists(backpath))
                {//目录不存在，创建目录
                    Directory.CreateDirectory(backpath);
                }
                DirectoryInfo mydir = new DirectoryInfo(backpath);
                FileInfo[] files = mydir.GetFiles();
                foreach (FileInfo file in files)
                {
                    str += "<tr class=\"list\">";
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<td><label class=\"custom-control custom-checkbox\"><input type=\"checkbox\" id=\"checkbox" + file.Name + "\" name=\"files\" value=\"" + file.Name + "\" class=\"custom-control-input\" del=\"true\"><span class=\"custom-control-label\"></span></label></td>";
                    }
                    else { 
                        str += "<td class=\"center\"><input type=\"checkbox\" value=\"" + file.Name + "\" name=\"files\" del=\"true\" /></td>";
                    }
                    str += "<td>" + file.Name + "";
                    if (EX_Admin.Power("backup_down", "数据备份下载"))
                    {
                        str += "&nbsp;&nbsp;<a href=\"" + ShopCache.GetBaseConfig().DataBase_BackPath + "/"+ file.Name + "\">"+ Tag("下载") +"</a>";
                    }
                    str += "</td><td>" + Math.Round(Convert.ToDouble(file.Length) / 1024 / 1024,2) + "</td><td>" + file.CreationTime + "</td>";
                    str += "</tr>";
                }
            }
            catch (Exception ex)
            {

            }
            return str;
        }
    }
}