using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using LB.Tools;
using Shop.Model;
using DB.LebiShop;
using Shop.Bussiness;
using System.Web;

namespace Shop.Platform
{
    public class Twitter : LoginBase
    {
        private string url = "";
        private string appid = "";
        private string appkey = "";
        private string reurnurl = "";
        private string platform_image = "";
        private int DT_id = 0;
        public Twitter(int _DT_id = 0)
        {
            DT_id = _DT_id;
            url = "https://api.twitter.com/";
            if (DT_id == 0)
            {
                BaseConfig bcf = ShopCache.GetBaseConfig();
                appid = bcf.platform_twitter_key;
                appkey = bcf.platform_twitter_secret;
                platform_image = bcf.platform_twitter_image;
            }
            else
            {
                BaseConfig_DT bcf = ShopCache.GetBaseConfig_DT(DT_id);
                appid = bcf.platform_twitter_key;
                appkey = bcf.platform_twitter_secret;
                platform_image = bcf.platform_twitter_image;
            }
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            reurnurl = "http://" + RequestTool.GetRequestDomain() + ":8001" + site.WebPath + "/platform/login_twitter.aspx";
            //reurnurl = "http://demo.lebi.cn" + site.WebPath + "/platform/login.aspx";
            reurnurl = System.Web.HttpUtility.UrlEncode(reurnurl);

        }

        #region 静态实例
        private static Twitter _Instance;
        public static Twitter Instance
        {
            get
            {
                if (_Instance == null)
                {

                    _Instance = new Twitter();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        public static Twitter GetInstance(int _DT_id = 0)
        {
            Twitter ins = new Twitter(_DT_id);
            return ins;
        }
        #endregion
        /// <summary>
        /// 登录地址
        /// </summary>
        /// <returns></returns>
        public string LoginURL
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("?response_type=code");
                sb.Append("&Consumer key=" + appid);
                sb.Append("&Consumer secret=" + appkey);
                sb.Append("&redirect_uri=" + reurnurl);
                return APIURL("oauth/request_token", sb.ToString());
            }
        }
        public string ImageURL
        {
            get
            {
                return platform_image;
            }
        }
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <param name="action"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        private string APIURL(string action, string para)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append(action);
            sb.Append(para);
            return sb.ToString();
        }
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <param name="action"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        private string GetAPI(string action, string para)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append(action);
            sb.Append(para);
            string str = Get(sb.ToString());
            return str;
        }
        private string PostAPI(string action, string para)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append(action);
            sb.Append(para);
            string str = Post(sb.ToString());
            return str;
        }
        public string Login(string back, int IsLogin = 1)
        {
            string code = RequestTool.RequestString("code");
            string res = "";
            StringBuilder sb;
            if (code != "")
            {
                sb = new StringBuilder();
                sb.Append("?grant_type=authorization_code");
                sb.Append("&client_id=" + appid);
                sb.Append("&client_secret=" + appkey);
                sb.Append("&code=" + code);
                sb.Append("&redirect_uri=" + reurnurl);
                //res = APIURL("oauth/access_token", sb.ToString());
                res = PostAPI("oauth/access_token", sb.ToString());
                //access_token=
                //CAAGUzrOw7
                //AMBADkwUNj
                //HkI9FwLPHD
                //fUamZCSBH1
                //NbSFsKVVjF
                //D2QfmP0aXT
                //FtMTY8e8d5
                //yse93demVg
                //YwCfEWXApj
                //ZAsGqt3ugg
                //YiHSmFQeN2
                //EOwFindWuw
                //R9QHiTlAwr
                //kmuTmr1Qs4
                //mwtSAZC15m
                //wTpLWvZCaj
                //OkVC1zlTZB
                //B5gjRZBmCm
                //C2j&expires=5183999
                //HttpContext.Current.Response.Redirect(res);
                res = res + "&";
                string access_token = RegexTool.GetRegValue(res, "access_token=(.*?)&");

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //获取uid
                sb = new StringBuilder();
                sb.Append("?access_token=" + access_token);
                res = GetAPI("me", sb.ToString());

                Model.Facebook.userinfo model = jss.Deserialize<Model.Facebook.userinfo>(res);
                string uid = model.id;
                //return uid;
                Lebi_User user = B_Lebi_User.GetModel("bind_facebook_id='" + uid + "'");

                if (model == null)
                    model = new Model.Facebook.userinfo();
                if (model.picture == null)
                    model.picture = new Model.Facebook.userinfo.picture_();
                if (model.picture.data == null)
                {
                    model.picture.data = new Model.Facebook.userinfo.picture_.data_();
                    model.picture.data.url = "";
                }
                string username = model.last_name + model.first_name;
                if (user == null)
                {
                    Lebi_UserLevel defaultlevel = B_Lebi_UserLevel.GetModel("Grade>0 order by Grade asc");
                    if (defaultlevel == null)
                    {
                        defaultlevel = new Lebi_UserLevel();
                    }
                    if (defaultlevel.RegisterType == 0) //关闭注册
                    {
                        return "会员注册已关闭";
                    }
                    user = new Lebi_User();
                    user.bind_facebook_id = uid;
                    user.bind_facebook_nickname = username;
                    user.bind_facebook_token = access_token;
                    user.Face = model.picture.data.url;
                    user.UserName = username;
                    user.Password = EX_User.MD5(uid);
                    user.Language = Language.CurrentLanguage().Code;
                    user.Sex = model.gender == "female" ? "女" : "男";
                    user.UserLevel_id = B_Lebi_UserLevel.GetList("Grade>0", "Grade asc").FirstOrDefault().id;
                    user.IsPlatformAccount = 1;
                    if (CurrentSite != null)
                        user.Site_id = CurrentSite.id;
                    user.DT_id = DT_id;
                    B_Lebi_User.Add(user);
                    user.id = B_Lebi_User.GetMaxId();
                    EX_User.LoginOK(user);
                }
                else
                {
                    user.bind_facebook_id = uid;
                    user.bind_facebook_nickname = username;
                    user.bind_facebook_token = access_token;
                    user.Face = model.picture.data.url;
                    user.Sex = model.gender == "female" ? "女" : "男";
                    user.UserName = username;
                    user.DT_id = DT_id;
                    B_Lebi_User.Update(user);
                    EX_User.LoginOK(user);
                }
                return "OK";
            }
            return "授权失败";
        }
    }
}
