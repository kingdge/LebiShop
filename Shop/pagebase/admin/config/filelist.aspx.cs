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

namespace Shop.Admin.config
{
    public partial class filelist : AdminPageBase
    {

        protected string file = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("fileedit", "文件编辑"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            file = RequestTool.RequestString("file");
            if (file.IndexOf("/") == 0)
                file = file.Substring(1, file.Length - 1);
        }

        public string GetList()
        {
            string str = "";
            try
            {
                string path = "";
                if (file != "")
                    path += "/" + file;
                //if (backpath.IndexOf("/") == 0)
                //{
                //    backpath = System.Web.HttpRuntime.AppDomainAppPath + "/" + backpath;
                //}
                //else
                //{
                path = System.Web.HttpRuntime.AppDomainAppPath + path;
                //}
                if (!Directory.Exists(path))
                {//目录不存在，创建目录
                    Directory.CreateDirectory(path);
                }
                DirectoryInfo mydir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = mydir.GetDirectories();
                string filename = "";
                foreach (DirectoryInfo dir in dirs)
                {
                    filename = dir.Name;
                    if (file != "")
                        filename = file + "/" + dir.Name;

                    if (dir.Name.ToLower().Contains("image"))
                        continue;
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<tr class=\"list\" ondblclick=\"Load('" + filename + "');\">";
                        str += "<td><i class=\"ti-folder\"> <a href=\"javascript:void(0);\" onclick=\"Load('" + filename + "');\">" + filename + "</a></td>";
                        str += "<td></td><td></td>";
                        str += "<td>" + dir.LastWriteTime + "</td>";
                        str += "<td><a href=\"javascript:void(0);\" onclick=\"Load('" + filename + "');\">" + Tag("打开") + "</a></td>";
                        str += "</tr>";
                    }
                    else { 
                        str += "<tr class=\"list\" ondblclick=\"Load('" + filename + "');\">";
                        str += "<td><img src=\"" + site.AdminImagePath + "/Ext/folder.gif\" /> <a href=\"javascript:void(0);\" onclick=\"Load('" + filename + "');\">" + filename + "</a></td>";
                        str += "<td></td><td></td>";
                        str += "<td>" + dir.LastWriteTime + "</td>";
                        str += "<td><a href=\"javascript:void(0);\" onclick=\"Load('" + filename + "');\">" + Tag("打开") + "</a></td>";
                        str += "</tr>";
                    }
                }


                FileInfo[] files = mydir.GetFiles();
                string remark = "";
                foreach (FileInfo f in files)
                {
                    filename = f.Name;
                    if (filename.ToLower().Contains("web.config"))
                        continue;
                    if (file != "")
                        filename = file + "/" + f.Name;
                    remark = GetRemark(filename);
                    str += "<tr class=\"list\" ondblclick=\"Edit('" + filename + "');\">";
                    str += "<td>" + filename + "</td>";

                    str += "<td>" + GetName(remark) + "</td>";
                    str += "<td>" + GetPage(remark) + "</td>";
                    str += "<td>" + f.LastWriteTime + "</td>";
                    str += "<td><a href=\"javascript:void(0);\" onclick=\"Edit('" + filename + "');\">" + Tag("编辑") + "</a></td>";
                    str += "</tr>";
                }
            }
            catch (Exception ex)
            {

            }
            return str;
        }
        /// <summary>
        /// 取得一个文件内的备注
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="skin"></param>
        /// <returns></returns>
        private string GetRemark(string filename)
        {
            string str = "";
            string path = "/theme/system/" + filename;
            str = HtmlEngine.ReadTxt(path);
            if (str.IndexOf("<!--") == 0)
            {
                str = RegexTool.GetRegValue(str, @"<!--(.*?)-->");
                str = str.Trim() + "\r\n";
                //str = str.Replace("\r\n","");
            }
            else
                str = "";
            return str;

        }
        /// <summary>
        /// 提取名称
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private string GetName(string strIn)
        {
            string str = RegexTool.GetRegValue(strIn, @"[Nn][Aa][Mm][Ee]:(.*?)\r\n");
            return str;
        }
        /// <summary>
        /// 提取限制页面
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        private string GetPage(string strIn)
        {
            string str = RegexTool.GetRegValue(strIn, @"[Pp][Aa][Gg][Ee]:(.*?)\r\n");
            return str;
        }
    }
}