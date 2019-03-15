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

namespace Shop.Admin.theme
{
    public partial class part : AdminPageBase
    {

        protected string file = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_syspart_list", "系统块列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            file = RequestTool.RequestString("file");

        }

        public string GetList()
        {
            string str = "";
            try
            {
                string backpath = "/theme/system";
                if (file != "")
                    backpath += "/" + file;
                //if (backpath.IndexOf("/") == 0)
                //{
                //    backpath = System.Web.HttpRuntime.AppDomainAppPath + "/" + backpath;
                //}
                //else
                //{
                backpath = System.Web.HttpRuntime.AppDomainAppPath + backpath;
                //}
                if (!Directory.Exists(backpath))
                {//目录不存在，创建目录
                    Directory.CreateDirectory(backpath);
                }
                DirectoryInfo mydir = new DirectoryInfo(backpath);
                DirectoryInfo[] dirs = mydir.GetDirectories();
                string filename = "";
                foreach (DirectoryInfo dir in dirs)
                {
                    filename = dir.Name;
                    if (file != "")
                        filename = file + "/" + dir.Name;

                    if (dir.Name.ToLower().Contains("image"))
                        continue;
                    str += "<tr ondblclick=\"Load('" + filename + "');\">";
                    str += "<td><i class=\"ti-folder\"></i> <a href=\"javascript:void(0);\" onclick=\"Load('" + filename + "');\">" + filename + "</a></td>";
                    str += "<td></td><td></td>";
                    str += "<td>" + FormatTime(dir.LastWriteTime) + "</td>";
                    str += "<td><a href=\"javascript:void(0);\" onclick=\"Load('" + filename + "');\">" + Tag("打开") + "</a></td>";
                    str += "</tr>";
                }


                FileInfo[] files = mydir.GetFiles();
                string remark = "";
                foreach (FileInfo f in files)
                {
                    filename = f.Name;
                    if (file != "")
                        filename = file + "/" + f.Name;
                    remark = GetRemark(filename);
                    str += "<tr ondblclick=\"Edit('" + filename + "');\">";
                    str += "<td>" + filename + "</td>";
                    str += "<td>" + GetName(remark) + "</td>";
                    str += "<td>" + GetPage(remark) + "</td>";
                    str += "<td>" + FormatTime(f.LastWriteTime) + "</td>";
                    str += "<td><a href=\"part_edit.aspx?file=" + filename + "\">" + Tag("编辑") + "</a></td>";
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