using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Web.UI.HtmlControls;
using System.Linq;

namespace Shop.Bussiness
{
    public class PageBase : System.Web.UI.Page
    {


        protected string reqPage = "";//当前页面的地址
        protected string refPage = "";//来源页面的地址
        protected BaseConfig SYS;
        protected string WebPath;
        protected string LBTITLE = "";
        protected string CurrentDomain = "";
        protected string ImageServer = "";
        protected WebConfig webconfig;
        public PageBase()
        {
            SYS = ShopCache.GetBaseConfig();
            refPage = RequestTool.GetUrlReferrer();
            reqPage = RequestTool.GetRequestUrl();
            WebPath = RequestTool.GetConfigKey("WebPath").Trim().TrimEnd('/');
            WebPath = WebPath == "/" ? "" : WebPath;
            ImageServer = RequestTool.GetConfigKey("ImageServer").Trim();
            CurrentDomain = RequestTool.GetRequestDomain();
            webconfig = Shop.Bussiness.WebConfig.Instrance;
        }
        /// <summary>
        /// 验证码
        /// </summary>
        public string CurrentCheckCode
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session["checkCode"] == null)
                        return "";
                    return (string)HttpContext.Current.Session["checkCode"];
                }
                catch
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool License(string code)
        {
            return Shop.LebiAPI.Service.Instanse.Check(code);
        }

        /// <summary>
        /// 返回图片的显示路径
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public string GetImage(string img)
        {
            img = WebPath + "/" + img;
            img = ThemeUrl.CheckURL(img);
            return img;
        }
        /// <summary>
        /// 将形如 CN,EN 字符串转化为'CN','EN'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string StringToSqlforIN(string str)
        {
            string[] arr = str.Split(',');
            str = "";
            foreach (string s in arr)
            {
                if (str == "")
                    str = "'" + s + "'";
                else
                    str += ",'" + s + "'";
            }
            return str;
        }
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string FormatDate(DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string FormatTime(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 页面载入检查
        /// </summary>
        public void PageLoadCheck()
        {
            if (ShopCache.GetDomainStatus() == "0")
            {
                if (!Shop.LebiAPI.Service.Instanse.Check("lebilicense"))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Powered by <a style=\"font-size:12px;color:#00497f\" href=\"http://www.lebi.cn\" target=\"_blank\" title=\"LebiShop多语言网店系统\">LebiShop</a> ");
                    sb.Append("V" + SYS.Version + "." + SYS.Version_Son);
                    Response.Write("<div style=\"height:100px;padding-top:10px;text-align:left;font-size:12;\">LebiShop发现异常,您可以访问<a style=\"font-size:12px;color:#00497f\" href=\"http://www.lebi.cn\" target=\"_blank\" title=\"LebiShop多语言网店系统\">www.lebi.cn</a>寻求帮助<br>");
                    Response.Write(sb.ToString() + "</div>");
                }
                else
                {
                    Response.Write(SYS.Wornings);
                }
                Response.End();
                return;

            }
            if (SYS.VisitTimeFlag == "1")
            {
                //开启了禁止访问
                Response.Write(SYS.Wornings);
                Response.End();
                return;
            }
            if (SYS.IPLock != "")
            {
                string ip = RequestTool.GetClientIP();
                string[] locks = SYS.IPLock.Split(',');
                string reg;
                foreach (string iplock in locks)
                {
                    if (iplock == ip)
                    {
                        Response.Write(SYS.Wornings);
                        Response.End();
                        return;
                    }
                    reg = iplock.Replace(".", @"\.");
                    reg = iplock.Replace("*", @"\d+");
                    if (RegexTool.Check(ip, reg))
                    {
                        Response.Write(SYS.Wornings);
                        Response.End();
                        return;
                    }
                }
            }
        }


        public bool AjaxLoadCheck()
        {
            if (SYS.IsAllowOutSideAjax == "0")
            {
                if (RequestTool.GetReferrerDomain() != RequestTool.GetRequestDomain())
                {
                    if (!SYS.SafeIPs.Contains(RequestTool.GetClientIP()))
                    {
                        Response.Write("{\"msg\":\"请求无效，请联系管理员\"}");
                        Response.End();
                        return false;
                    }
                }
            }
            return true;
        }
        #region 图片路径处理
        public string Image(string imageold, int width, int height)
        {
            return Image(imageold + "&w=" + width + "&h=" + height);

        }
        public string Image(string imageold, Lebi_Theme theme, string type)
        {


            int width = 0;
            int height = 0;

            switch (type.ToLower())
            {
                case "small":
                    width = theme.ImageSmall_Width;
                    height = theme.ImageSmall_Height;
                    break;
                case "medium":
                    width = theme.ImageMedium_Width;
                    height = theme.ImageMedium_Height;
                    break;
                case "big":
                    width = theme.ImageBig_Width;
                    height = theme.ImageBig_Height;
                    break;
            }

            if (width == 0 && height == 0)
            {
                switch (type.ToLower())
                {
                    case "small":
                        width = SYS.ImageSmallWidth;
                        height = SYS.ImageSmallHeight;
                        break;
                    case "medium":
                        width = SYS.ImageMediumWidth;
                        height = SYS.ImageMediumHeight;
                        break;
                    case "big":
                        width = SYS.ImageBigWidth;
                        height = SYS.ImageBigHeight;
                        break;
                }

            }
            return Image(imageold, width, height);
        }
        public string Image(string imageold, string type)
        {


            int width = 0;
            int height = 0;
            switch (type.ToLower())
            {
                case "small":
                    width = SYS.ImageSmallWidth;
                    height = SYS.ImageSmallHeight;
                    break;
                case "medium":
                    width = SYS.ImageMediumWidth;
                    height = SYS.ImageMediumHeight;
                    break;
                case "big":
                    width = SYS.ImageBigWidth;
                    height = SYS.ImageBigHeight;
                    break;
            }
            return Image(imageold, width, height);
        }
        /// <summary>
        /// 处理图片路径
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public string Image(string img)
        {
            if (!img.Contains("."))
                img += "&.jpg";
            if (img.IndexOf("data:image") == 0)
                return img;
            if (img.ToLower().IndexOf("http") == 0)
            {
                return img;
            }
            if (img.IndexOf("/") == 0)
            {
                if (webconfig.ImageURL == "")
                {
                    if (img.Contains("&"))
                    {
                        return WebPath + "/pic.aspx?p=" + img;
                    }
                    else
                    {
                        return WebPath + img;
                    }
                }
                else
                {
                    if (webconfig.LocalImageURL != "")
                    {
                        return webconfig.LocalImageURL + img;
                    }

                    if (img.Contains("&"))
                    {
                        if (webconfig.MainDomain.ToLower().IndexOf("http") == 0)
                            return webconfig.MainDomain + "/pic.aspx?p=" + img;
                        else
                            return "" + SYS.HTTPServer + "://" + webconfig.MainDomain + "/pic.aspx?p=" + img;
                    }
                    else
                    {
                        if (webconfig.MainDomain.ToLower().IndexOf("http") == 0)
                            return webconfig.MainDomain + img;
                        else
                            return "" + SYS.HTTPServer + "://" + webconfig.MainDomain + img;
                    }
                }
            }
            return webconfig.ImageURL + img;
        }
        /// <summary>
        /// 文字内容里面的图片处理
        /// </summary>
        /// <param name="str"></param>
        /// <param name="IsLazyLoad"></param>
        /// <returns></returns>
        public string ContentImage(string str, int IsLazyLoad = 0)
        {
            string[] imgs = RegexTool.GetSimpleRegResultArray(str, @"(<[Ii][Mm][Gg].*?>)");
            string temp = "";
            foreach (string img in imgs)
            {
                temp = img;
                temp = temp.Replace("'", "\"").Replace(" ", "");
                temp = RegexTool.GetRegValue(img, "[Ss][Rr][Cc]=\"(.*?)\"");
                if (IsLazyLoad == 1)
                {
                    str = str.Replace(img, "<img class=\"contentlazy\" data-original=\"" + Image(temp) + "\"/>");
                }
                else
                {
                    str = str.Replace(img, "<img src=\"" + Image(temp) + "\"/>");
                }
            }
            return str;
        }
        #endregion

        #region 处理引用资源

        public string PageJS(string file)
        {
            return WebPath + "/theme/system/js/" + file.Trim();
        }
        public string PageCSS(string file)
        {
            return WebPath + "/theme/system/css/" + file.Trim();
        }
        public string PageImage(string file)
        {
            return WebPath + "/theme/system/images/" + file.Trim();
        }
        public string AdminJS(string file)
        {
            return WebPath + "/theme/system/systempage/admin/js/" + file.Trim();
        }
        public string AdminCSS(string file)
        {
            return WebPath + "/theme/system/systempage/admin/css/" + file.Trim();
        }
        public string AdminImage(string file)
        {
            return WebPath + "/theme/system/systempage/admin/images/" + file.Trim();
        }
        public string SupplierJS(string file)
        {
            return WebPath + "/theme/system/systempage/supplier/js/" + file.Trim();
        }
        public string SupplierCSS(string file)
        {
            return WebPath + "/theme/system/systempage/supplier/css/" + file.Trim();
        }
        public string SupplierImage(string file)
        {
            return WebPath + "/theme/system/systempage/supplier/images/" + file.Trim();
        }
        #endregion
    }
}