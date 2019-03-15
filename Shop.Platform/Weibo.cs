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
    public class Weibo : LoginBase
    {
        private string url = "";
        private string appid = "";
        private string appkey = "";
        private string reurnurl = "";
        private string platform_image = "";
        private int DT_id = 0;
        public Weibo(int _DT_id = 0)
        {
            DT_id = _DT_id;
            url = "https://api.weibo.com/";
            if (DT_id == 0)
            {
                BaseConfig bcf = ShopCache.GetBaseConfig();
                appid = bcf.platform_weibo_id;
                appkey = bcf.platform_weibo_key;
                platform_image = bcf.platform_weibo_image;
            }
            else
            {
                BaseConfig_DT bcf = ShopCache.GetBaseConfig_DT(DT_id);
                appid = bcf.platform_weibo_id;
                appkey = bcf.platform_weibo_key;
                platform_image = bcf.platform_weibo_image;
            }
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            reurnurl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/platform/login_weibo.aspx";

        }

        #region 静态实例
        private static Weibo _Instance;
        public static Weibo Instance
        {
            get
            {
                return new Weibo();
            }
            set
            {
                _Instance = value;
            }
        }
        public static Weibo GetInstance(int _DT_id = 0)
        {
            Weibo ins = new Weibo(_DT_id);
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
            string uri = reurnurl + "?backurl=" + ENBackuri(back);
            uri = System.Web.HttpUtility.UrlEncode(uri);
            sb.Append("&redirect_uri=" + uri);
            return APIURL("oauth2/authorize", sb.ToString());

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
            if (code != "")
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("?grant_type=authorization_code");
                    sb.Append("&client_id=" + appid);
                    sb.Append("&client_secret=" + appkey);
                    sb.Append("&code=" + code);
                    string uri = reurnurl + "?backurl=" + ENBackuri(back);
                    uri = System.Web.HttpUtility.UrlEncode(uri);
                    sb.Append("&redirect_uri=" + uri);
                    string res = PostAPI("oauth2/access_token", sb.ToString());

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Model.Weibo.token token = jss.Deserialize<Model.Weibo.token>(res);
                    string access_token = token.access_token;
                    //获取uid
                    sb = new StringBuilder();
                    sb.Append("?access_token=" + access_token);
                    res = PostAPI("oauth2/get_token_info", sb.ToString());
                    Model.Weibo.tokeninfo tokeninfo = jss.Deserialize<Model.Weibo.tokeninfo>(res);
                    string uid = tokeninfo.uid;

                    //获取用户资料
                    sb = new StringBuilder();
                    sb.Append("?access_token=" + access_token);
                    sb.Append("&uid=" + uid);

                    res = GetAPI("2/users/show.json", sb.ToString());


                    Model.Weibo.userinfo model = jss.Deserialize<Model.Weibo.userinfo>(res);
                    string where = "bind_weibo_id='" + uid + "'";
                    //if (DT_id > 0)
                    //{
                    //    where += " and DT_id =" + DT_id + "";
                    //}
                    Lebi_User user = B_Lebi_User.GetModel(where);
                    Lebi_User CurrentUser = EX_User.CurrentUser();
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
                        CurrentUser.bind_weibo_id = uid;
                        CurrentUser.bind_weibo_nickname = model.screen_name;
                        CurrentUser.bind_weibo_token = access_token;
                        if (CurrentUser.Face == "")
                            CurrentUser.Face = model.profile_image_url;//头像 50*50
                        CurrentUser.DT_id = DT_id;
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
                            user.bind_weibo_id = uid;
                            user.bind_weibo_nickname = model.screen_name;
                            user.bind_weibo_token = access_token;
                            user.Face = model.profile_image_url;//头像 50*50
                            user.NickName = model.screen_name;
                            user.UserName = "weibo_" + uid;
                            user.Password = EX_User.MD5(uid);
                            user.Language = Language.CurrentLanguage().Code;
                            user.Sex = model.gender == "f" ? "女" : "男";
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
                            user.bind_weibo_id = uid;
                            user.bind_weibo_nickname = model.screen_name;
                            user.bind_weibo_token = access_token;
                            if (user.Face == "")
                                user.Face = model.profile_image_url;//头像
                            //user.Sex = model.gender == "f" ? "女" : "男";
                            user.DT_id = DT_id;
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
