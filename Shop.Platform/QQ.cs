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
    public class QQ : LoginBase
    {
        private string url = "";
        private string appid = "";
        private string appkey = "";
        private string reurnurl = "";
        private string platform_image = "";
        private int DT_id = 0;
        public QQ(int _DT_id = 0)
        {
            DT_id = _DT_id;
            url = "https://graph.qq.com/";
            if (DT_id == 0)
            {
                BaseConfig bcf = ShopCache.GetBaseConfig();
                appid = bcf.platform_qq_id;
                appkey = bcf.platform_qq_key;
                platform_image = bcf.platform_qq_image;
            }else{
                BaseConfig_DT bcf = ShopCache.GetBaseConfig_DT(DT_id);
                appid = bcf.platform_qq_id;
                appkey = bcf.platform_qq_key;
                platform_image = bcf.platform_qq_image;
            }
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            reurnurl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/platform/login_qq.aspx";
        }

        #region 静态实例
        private static QQ _Instance;
        public static QQ Instance
        {
            get
            {
                return new QQ();
            }
            set
            {
                _Instance = value;
            }
        }
        public static QQ GetInstance(int DT_id = 0)
        {
            QQ qq = new QQ(DT_id);
            return qq;
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
            sb.Append("&client_id="+ appid);
            sb.Append("&state=qq");
            //string backuri = reurnurl + "?backurl=" + ENBackuri(back);
            string backuri = reurnurl;
            //backuri = System.Web.HttpUtility.UrlEncode(backuri);
            sb.Append("&redirect_uri=" + backuri);
            string url = "?response_type=code&client_id=" + appid + "&state=qq&redirect_uri=" + backuri;
            return APIURL("oauth2.0/authorize", url);

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
        private string API(string action, string para)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append(action);
            sb.Append(para);
            string str = HtmlEngine.Get(sb.ToString());
            return str;
        }

        public string Login(string back, int IsLogin = 1,int DT_id = 0)
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
                    string uri = reurnurl + "?backurl=" + back;
                    uri = System.Web.HttpUtility.UrlEncode(uri);
                    sb.Append("&redirect_uri=" + uri);
                    string res = API("oauth2.0/token", sb.ToString());
                    res = res + "&";
                    string access_token = RegexTool.GetRegValue(res, "access_token=(.*?)&");

                    //获取openid
                    sb = new StringBuilder();
                    sb.Append("?access_token=" + access_token);
                    res = API("oauth2.0/me", sb.ToString());
                    string openid = RegexTool.GetRegValue(res, "openid\":\"(.*?)\"}");

                    //获取用户资料
                    sb = new StringBuilder();
                    sb.Append("?access_token=" + access_token);
                    sb.Append("&oauth_consumer_key=" + appid);
                    sb.Append("&openid=" + openid);
                    res = API("user/get_user_info", sb.ToString());

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Model.QQ.userinfo model = jss.Deserialize<Model.QQ.userinfo>(res);
                    string where = "bind_qq_id='" + openid + "'";
                    //if (DT_id > 0)
                    //{
                    //    where += " and DT_id =" + DT_id + "";
                    //}
                    Lebi_User user = B_Lebi_User.GetModel(where);
                    Lebi_User CurrentUser = EX_User.CurrentUser();
                    if (CurrentUser.id > 0)//已经登录
                    {
                        if (IsLogin == 0){
                            if (user != null)
                            {
                                if (CurrentUser.id != user.id)
                                {
                                    return "已绑定其它帐号";
                                }
                            }
                        }
                        CurrentUser.bind_qq_id = openid;
                        CurrentUser.bind_qq_nickname = model.nickname;
                        CurrentUser.bind_qq_token = access_token;
                        if (CurrentUser.Face == "")
                            CurrentUser.Face = model.figureurl_qq_1;//头像
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
                            user.bind_qq_id = openid;
                            user.bind_qq_nickname = model.nickname;
                            user.bind_qq_token = access_token;
                            user.Face = model.figureurl_qq_1;//头像
                            user.UserName = "qq_" + openid;
                            user.NickName = model.nickname;
                            user.Password = EX_User.MD5(openid);
                            user.Language = Language.CurrentLanguage().Code;
                            user.Sex = model.gender;
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
                            user.bind_qq_id = openid;
                            user.bind_qq_nickname = model.nickname;
                            user.bind_qq_token = access_token;
                            if (user.Face == "")
                                user.Face = model.figureurl_qq_1;//头像
                            //user.Sex = model.gender;
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