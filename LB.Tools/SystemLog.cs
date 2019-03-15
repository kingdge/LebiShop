using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace LB.Tools
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class TxtLog
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tablename"></param>
        /// <param name="keyid"></param>
        public static void Add(string content)
        {
            string res = "time:" + System.DateTime.Now + "\r\n";
            res += "url:" + RequestTool.GetRequestUrlNonDomain() + "\r\n";
            res += "refererurl:" + RequestTool.GetUrlReferrerNonDomain() + "\r\n";
            

            res += "content:" + content + "\r\n\r\n";
            string Path = "/systemlog/" + System.DateTime.Now.Year + "/";
            string sp = "";
            try
            {
                sp = HttpRuntime.AppDomainAppPath;

            }
            catch
            {
                sp = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            }
            string PhysicsPath = sp + Path;

            if (!Directory.Exists(PhysicsPath))
            {
                Directory.CreateDirectory(PhysicsPath);
            }
            try
            {
                string PhysicsFileName = PhysicsPath + System.DateTime.Now.ToString("MMdd") + ".txt";
                if (System.IO.File.Exists(PhysicsFileName))
                {
                    FileStream fs3 = new FileStream(PhysicsFileName, FileMode.Append);
                    StreamWriter sw3 = new StreamWriter(fs3, System.Text.Encoding.GetEncoding("utf-8"));
                    //sw3.WriteLine(""+labeltop.Text.ToString()+"\r\n"+labelmessage.Text.ToString()+"\r\n");
                    StringBuilder sb3 = new StringBuilder();
                    sb3.Append(res);
                    sw3.Write(sb3.ToString());
                    sw3.Close();
                    fs3.Close();
                }
                else
                {
                    HtmlEngine.Instance.WriteFile(PhysicsFileName, res);
                }
            }
            catch (IOException)
            {

            }

        }



    }
}
