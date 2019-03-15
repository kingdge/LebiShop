using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
namespace LB.Tools
{
    public class FileTool
    {
        /// <summary>
        /// 生成CSV文件
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="fname"></param>
        public static void StringToCSV(string dv, string fname)
        {

            fname = fname + "_" + System.DateTime.Now.ToString("yyyy-MM-dd");
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            //System.Web.HttpContext.Current.Response.Charset = "GB2312";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fname + ".csv");
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //dv.RenderControl(oHtmlTextWriter);
            System.Web.HttpContext.Current.Response.Output.Write(dv);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

        }
        public static void StringTofile(string dv, string fname, string ext)
        {

            fname = fname + "_" + System.DateTime.Now.ToString("yyyy-MM-dd");
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            //System.Web.HttpContext.Current.Response.Charset = "GB2312";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fname + ext);
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //dv.RenderControl(oHtmlTextWriter);
            System.Web.HttpContext.Current.Response.Output.Write(dv);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

        }
        /// <summary>
        /// 复制文件夹，
        /// </summary>
        /// <param name="varFromDirectory">绝对路径</param>
        /// <param name="varToDirectory">绝对路径</param>
        public static void CopyFiles(string varFromDirectory, string varToDirectory, bool overwrite = false)
        {
            Directory.CreateDirectory(varToDirectory);
            if (!Directory.Exists(varFromDirectory)) return;
            string[] directories = Directory.GetDirectories(varFromDirectory);
            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    if (d.ToLower() == "_[copy]")
                        continue;
                    CopyFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")), overwrite);
                }
            }
            string[] files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")), overwrite);
                }
            }
            
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="varFromFile"></param>
        /// <param name="varToFile"></param>
        public static void CopyFile(string varFromFile, string varToFile, bool overwrite)
        {
            varFromFile = SetPath(varFromFile);
            varToFile = SetPath(varToFile);
            try
            {
                File.Copy(varFromFile, varToFile, overwrite);
            }
            catch
            {
            }
            //throw new Exception(varFromFile + "----" + varToFile);
        }
        private static string SetPath(string path)
        {
            path = path.Replace(@"\", "/").Replace(@"//", @"/");
            return path;
        }
    }
}
