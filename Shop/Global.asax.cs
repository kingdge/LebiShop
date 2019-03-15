using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Shop.Bussiness;
using System.Text;
namespace Shop
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            try
            {
                // 在应用程序启动时运行的代码
                TimeWork tw = new TimeWork();//启动定时器
                tw.work_databackup_start();
                tw.work_email_start();
                Event.Initialization();
            }
            catch (Exception ex)
            {
                SystemLog.Add(ex.ToString());
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            //Exception ex = Server.GetLastError();
            //StringBuilder sb = new StringBuilder();
            //string dt = DateTime.Now.ToString();
            //sb.Append(dt).Append("内部错误:").Append(ex.InnerException.ToString())
            //   .Append("<br/>堆栈:").Append(ex.StackTrace).Append("<br/>内容:").Append(ex.Message)
            //  .Append("<br/>来源:").Append(ex.Source);
            //SystemLog.Add(sb.ToString());
            //Server.ClearError();
            //System.Web.HttpContext.Current.Response.Write("<script>window.location='/'</script>");

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}
