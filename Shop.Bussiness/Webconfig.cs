using System;
using System.Collections.Generic;
using System.Text;
using LB.Tools;
namespace Shop.Bussiness
{
    public class WebConfig
    {


        private static WebConfig Instrance_;
        public static WebConfig Instrance
        {
            get
            {
                if (Instrance_ == null)
                {
                    Instrance_ = new WebConfig();
                    Instrance_.ImageServer = RequestTool.GetConfigKey("ImageServer");
                    Instrance_.UpLoadURL = RequestTool.GetConfigKey("UpLoadURL");
                    Instrance_.DownLoadURL = RequestTool.GetConfigKey("DownLoadURL");
                    Instrance_.MainDomain = RequestTool.GetConfigKey("MainDomain");
                    Instrance_.ImageURL = RequestTool.GetConfigKey("ImageURL");
                    Instrance_.LocalImageURL = RequestTool.GetConfigKey("LocalImageURL");
                    if (Instrance_.ImageURL == "")
                        Instrance_.ImageURL = "/pic.aspx?p=";
                    if (Instrance_.MainDomain == "")
                    {

                        if (RequestTool.GetRequestPort() != 80)
                            Instrance_.MainDomain = RequestTool.GetRequestDomainPort();
                        else
                            Instrance_.MainDomain = RequestTool.GetRequestDomain();
                    }
                    Instrance_.ImageServerKey = RequestTool.GetConfigKey("ImageServerKey");
                    Instrance_.ThemeDomain = RequestTool.GetConfigKey("ThemeDomain");

                }
                return Instrance_;
            }
            set
            {
                Instrance_ = value;
            }
        }
        private static int Toint(string val, int defaultval)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return defaultval;
            }
        }
        /// <summary>
        /// 主域名
        /// </summary>
        public string MainDomain { get; set; }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string ImageServer { get; set; }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string UpLoadURL { get; set; }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string DownLoadURL { get; set; }
        /// <summary>
        /// 图片服务器图片路径
        /// </summary>
        public string ImageURL { get; set; }
        /// <summary>
        /// 本地图片路径
        /// </summary>
        public string LocalImageURL { get; set; }
        /// <summary>
        /// 图片服务器密钥
        /// </summary>
        public string ImageServerKey { get; set; }
        /// <summary>
        /// 模板资源域名
        /// </summary>
        public string ThemeDomain { get; set; }
    }
}