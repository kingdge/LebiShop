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
    public class Taobao : LoginBase
    {
        private string url = "";//登录
        private string apiurl = "";//其他功能
        private string appid = "";
        private string appkey = "";
        private string reurnurl = "";
        private string platform_image = "";
        private int DT_id = 0;
        public Taobao(int _DT_id = 0)
        {
            DT_id = _DT_id;
            url = "https://oauth.taobao.com/";
            apiurl = "https://eco.taobao.com/router/rest";
            if (DT_id == 0)
            {
                BaseConfig bcf = ShopCache.GetBaseConfig();
                appid = bcf.platform_taobao_key;
                appkey = bcf.platform_taobao_secret;
                platform_image = bcf.platform_taobao_image;
            }
            else
            {
                BaseConfig_DT bcf = ShopCache.GetBaseConfig_DT(DT_id);
                appid = bcf.platform_taobao_key;
                appkey = bcf.platform_taobao_secret;
                platform_image = bcf.platform_taobao_image;
            }
            appid = Shop.Bussiness.ShopCache.GetBaseConfig().platform_taobao_key;
            appkey = Shop.Bussiness.ShopCache.GetBaseConfig().platform_taobao_secret;
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            reurnurl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/platform/login_taobao.aspx";

        }

        #region 静态实例
        private static Taobao _Instance;
        public static Taobao Instance
        {
            get
            {
                return new Taobao();
            }
            set
            {
                _Instance = value;
            }
        }
        public static Taobao GetInstance(int _DT_id = 0)
        {
            Taobao ins = new Taobao(_DT_id);
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
            return APIURL("authorize", sb.ToString());

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
        private string GetAPI(string action, string para, bool islogin)
        {
            StringBuilder sb = new StringBuilder();
            if (islogin)
            {
                sb.Append(url);
                sb.Append(action);
            }
            else
            {
                sb.Append(apiurl);
                sb.Append("?method=" + action);
                sb.Append("&format=json");
                sb.Append("&v=2.0");
            }

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
                    string res = PostAPI("token", sb.ToString());

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Model.Taobao.token token = jss.Deserialize<Model.Taobao.token>(res);
                    string access_token = token.access_token;
                    string uid = token.taobao_user_id;
                    string username = token.taobao_user_nick;
                    //获取详细个人信息

                    sb = new StringBuilder();
                    sb.Append("&access_token=" + access_token);
                    res = GetAPI("taobao.user.buyer.get", sb.ToString(), false);


                    Model.Taobao.userinfo model = jss.Deserialize<Model.Taobao.userinfo>(res);
                    if (model == null)
                    {
                        model = new Model.Taobao.userinfo();
                    }
                    if (model.user_buyer_get_response == null)
                        model.user_buyer_get_response = new Model.Taobao.userinfo.user_buyer_get_response_();
                    if (model.user_buyer_get_response.user == null)
                    {
                        model.user_buyer_get_response.user = new Model.Taobao.userinfo.user_buyer_get_response_.user_();
                        model.user_buyer_get_response.user.sex = "m";
                        model.user_buyer_get_response.user.avatar = "";
                    }
                    if (model.user_buyer_get_response.user.location == null)
                        model.user_buyer_get_response.user.location = new Model.Taobao.userinfo.user_buyer_get_response_.user_.location_();
                    string where = "bind_taobao_id='" + uid + "'";
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
                        CurrentUser.bind_taobao_id = uid;
                        CurrentUser.bind_taobao_nickname = username;
                        CurrentUser.bind_taobao_token = access_token;
                        if (CurrentUser.Face == "")
                            CurrentUser.Face = model.user_buyer_get_response.user.avatar;//头像
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
                            user.bind_taobao_id = uid;
                            user.bind_taobao_nickname = username;
                            user.bind_taobao_token = access_token;
                            user.UserName = "taobao_" + uid;
                            user.NickName = username;
                            user.Password = EX_User.MD5(uid);
                            user.Language = Language.CurrentLanguage().Code;
                            user.Sex = model.user_buyer_get_response.user.sex == "f" ? "女" : "男";
                            user.Face = model.user_buyer_get_response.user.avatar;//头像
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
                            user.bind_taobao_id = uid;
                            user.bind_taobao_nickname = username;
                            user.bind_taobao_token = access_token;
                            if (user.Face == "")
                                user.Face = model.user_buyer_get_response.user.avatar;//头像
                            //user.Sex = model.user_buyer_get_response.user.sex == "f" ? "女" : "男";
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
