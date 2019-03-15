using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class CnzzConfig : AdminAjaxBase
    {
        string domain = HttpContext.Current.Request.Url.Host;
        string key = "Jsit7Kd3";
        string cms = "56770";
        protected string returnValue = "";
        protected Lebi_Cnzz model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("cnzz_edit", "CNZZ统计配置"))
            {
                WindowNoPower();
            }
            model = B_Lebi_Cnzz.GetList("", "").FirstOrDefault();
        }
    }
    //public string GetAspMd5(string md5str, int type)
    //{
    //    if (type == 16)
    //    {
    //        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5").Substring(8, 16).ToLower();
    //    }
    //    else if (type == 32)
    //    {
    //        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5").ToLower();
    //    }
    //    return "";
    //}
    //public string getStr(string url)
    //{
    //    //
    //    // TODO: 在此处添加代码以启动应用程序
    //    //
    //    WebClient wc = new WebClient();
    //    byte[] response = wc.DownloadData(url);
    //    System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
    //    string cc = encoding.GetString(response);
    //    return cc;
    //}
}