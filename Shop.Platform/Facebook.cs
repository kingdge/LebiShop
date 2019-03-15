using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using LB.Tools;
using Shop.Model;
using Shop.Bussiness;
using System.Web;
using DB.LebiShop;
namespace Shop.Platform
{
    public class Facebook : LoginBase
    {
        private string url = "";
        private string appid = "";
        private string appkey = "";
        private string reurnurl = "";
        private string platform_image = "";
        public Facebook(int DT_id = 0)
        {
            url = "https://graph.facebook.com/";
            if (DT_id == 0)
            {
                BaseConfig bcf = ShopCache.GetBaseConfig();
                appid = bcf.platform_facebook_id;
                appkey = bcf.platform_facebook_secret;
                platform_image = bcf.platform_facebook_image;
            }
            else
            {
                BaseConfig_DT bcf = ShopCache.GetBaseConfig_DT(DT_id);
                appid = bcf.platform_facebook_id;
                appkey = bcf.platform_facebook_secret;
                platform_image = bcf.platform_facebook_image;
            }
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            reurnurl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/platform/login_facebook.aspx";

        }

        #region 静态实例
        private static Facebook _Instance;
        public static Facebook Instance
        {
            get
            {
                return new Facebook();
            }
            set
            {
                _Instance = value;
            }
        }
        public static Facebook GetInstance(int DT_id = 0)
        {
            Facebook ins = new Facebook(DT_id);
            return ins;
        }
        #endregion
        /// <summary>
        /// 登录地址
        /// </summary>
        /// <returns></returns>
        public string LoginURL(string back)
        {
            
            StringBuilder sb = new StringBuilder();
            sb.Append("?response_type=code");
            sb.Append("&client_id=" + appid);
            string uri = reurnurl +"?backurl=" + ENBackuri(back);
            uri = System.Web.HttpUtility.UrlEncode(uri);
            sb.Append("&redirect_uri=" + uri);
            return APIURL("oauth/authorize", sb.ToString());

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
        public string Login(string back, int IsLogin = 1, int DT_id = 0)
        {
      
            string code = RequestTool.RequestString("code");
            string res = "";
            StringBuilder sb;
            if (code != "")
            {
                try
                {
                    sb = new StringBuilder();
                    sb.Append("?grant_type=authorization_code");
                    sb.Append("&client_id=" + appid);
                    sb.Append("&client_secret=" + appkey);
                    sb.Append("&code=" + code);
                    string uri = reurnurl +"?backurl=" + ENBackuri(back);
                    uri = System.Web.HttpUtility.UrlEncode(uri);
                    sb.Append("&redirect_uri=" + uri);
                    res = PostAPI("oauth/access_token", sb.ToString());
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
                    string where = "bind_facebook_id='" + uid + "'";
                    //if (DT_id > 0)
                    //{
                    //    where += " and DT_id =" + DT_id + "";
                    //}
                    Lebi_User user = B_Lebi_User.GetModel(where);
                    Lebi_User CurrentUser = EX_User.CurrentUser();
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
                    if (CurrentUser.id > 0)//已经登录
                    {
                        if (IsLogin == 0)
                        {
                            if (user != null)
                            {
                                if (CurrentUser.id != user.id)
                                {
                                    return "已绑定其它帐号";
                                }
                            }
                        }
                        CurrentUser.bind_facebook_id = uid;
                        CurrentUser.bind_facebook_nickname = username;
                        CurrentUser.bind_facebook_token = access_token;
                        if (CurrentUser.Face == "")
                            CurrentUser.Face = model.picture.data.url;//头像
                        B_Lebi_User.Update(CurrentUser);
                    }
                    else
                    {

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
                            user.NickName = username;
                            user.UserName = "facebook_" + uid;
                            user.Password = EX_User.MD5(uid);
                            user.Language = Language.CurrentLanguage().Code;
                            user.Sex = model.gender == "female" ? "女" : "男";
                            user.UserLevel_id = B_Lebi_UserLevel.GetList("Grade>0", "Grade asc").FirstOrDefault().id;
                            user.IsPlatformAccount = 1;
                            if (CurrentSite != null)
                                user.Site_id = CurrentSite.id;
                            B_Lebi_User.Add(user);
                            user.id = B_Lebi_User.GetMaxId();
                            EX_User.LoginOK(user);
                        }
                        else
                        {
                            user.bind_facebook_id = uid;
                            user.bind_facebook_nickname = username;
                            user.bind_facebook_token = access_token;
                            if (user.Face == "")
                                user.Face = model.picture.data.url;
                            //user.Sex = model.gender == "female" ? "女" : "男";
                            B_Lebi_User.Update(user);
                            EX_User.LoginOK(user);
                        }
                        
                    }
                    return "OK";
                }
                catch
                {
                    return "授权失败";
                }
            }
            return "授权失败";
        }
    }
}
