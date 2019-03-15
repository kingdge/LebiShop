using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using LB.Tools;
using Shop.Model;
using Shop.Bussiness;
using DB.LebiShop;
using System.Web;
using System.Xml;
using System.IO;
namespace Shop.Platform
{
    public class weixin : LoginBase
    {
        private string url = "";
        private string appid = "";
        private string appkey = "";
        private string reurnurl = "";
        private string number = "";
        public string Token;
        private string platform_image = "";
        public Model.weixin.token tokenmodel = null;
        public DateTime EndTime;
        private int DT_id = 0;
        #region 静态实例
        private static weixin _Instance;
        public static weixin Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new weixin();
                }
                if (_Instance.EndTime < System.DateTime.Now)
                {
                    _Instance = new weixin();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        public static weixin GetInstance(int _DT_id, Lebi_Site CurrentSite = null)
        {
            weixin wx = new weixin(_DT_id, CurrentSite);
            return wx;
        }
        #endregion
        public weixin(Lebi_Site CurrentSite = null)
        {
            if (CurrentSite != null)
            {
                if (CurrentSite.platform_weixin_id != "" && CurrentSite.platform_weixin_secret != "")
                {
                    appid = CurrentSite.platform_weixin_id;
                    appkey = CurrentSite.platform_weixin_secret;
                    number = CurrentSite.platform_weixin_number;
                }
                if (appid == "")
                    appid = ShopCache.GetBaseConfig().platform_weixin_id;
                if (appkey == "")
                    appkey = ShopCache.GetBaseConfig().platform_weixin_secret;
                if (number == "")
                    number = ShopCache.GetBaseConfig().platform_weixin_number;
                platform_image = ShopCache.GetBaseConfig().platform_weixin_image;
            }
            else
            {
                BaseConfig bcf = ShopCache.GetBaseConfig();
                appid = bcf.platform_weixin_id;
                appkey = bcf.platform_weixin_secret;
                number = bcf.platform_weixin_number;
                platform_image = bcf.platform_weixin_image;
            }
            Model.weixin.token t = GetToken();
            Token = t.access_token;

            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            url = "https://api.weixin.qq.com/";
            reurnurl = ShopCache.GetBaseConfig().HTTPServer + "://" + RequestTool.GetRequestDomain() + site.WebPath + "/platform/login_weixin.aspx";

        }
        public weixin(int _DT_id, Lebi_Site CurrentSite = null)
        {
            DT_id = _DT_id;
            BaseConfig_DT dtbcf = null;
            if (DT_id > 0)
            {
                dtbcf = ShopCache.GetBaseConfig_DT(DT_id);
                if (dtbcf != null)
                {
                    appid = dtbcf.platform_weixin_id;
                    appkey = dtbcf.platform_weixin_secret;
                    number = dtbcf.platform_weixin_number;
                    platform_image = dtbcf.platform_weixin_image;
                }
            }
            if (DT_id == 0 || dtbcf == null)
            {
                if (CurrentSite != null)
                {
                    if (CurrentSite.platform_weixin_id != "" && CurrentSite.platform_weixin_secret != "")
                    {
                        appid = CurrentSite.platform_weixin_id;
                        appkey = CurrentSite.platform_weixin_secret;
                        number = CurrentSite.platform_weixin_number;
                    }
                    if (appid == "")
                        appid = ShopCache.GetBaseConfig().platform_weixin_id;
                    if (appkey == "")
                        appkey = ShopCache.GetBaseConfig().platform_weixin_secret;
                    if (number == "")
                        number = ShopCache.GetBaseConfig().platform_weixin_number;
                    platform_image = ShopCache.GetBaseConfig().platform_weixin_image;
                }
                else
                {
                    BaseConfig bcf = ShopCache.GetBaseConfig();
                    appid = bcf.platform_weixin_id;
                    appkey = bcf.platform_weixin_secret;
                    number = bcf.platform_weixin_number;
                    platform_image = bcf.platform_weixin_image;
                }
            }
            Model.weixin.token t = GetToken();
            Token = t.access_token;

            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            url = "https://api.weixin.qq.com/";
            reurnurl = ShopCache.GetBaseConfig().HTTPServer + "://" + RequestTool.GetRequestDomain() + site.WebPath + "/platform/login_weixin.aspx";

        }
        public string ImageURL
        {
            get
            {
                return platform_image;
            }
        }
        /// <summary>
        /// 获得TOKEN
        /// https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx7374191e442b14ef&secret=c2f9f7d4a6985b2dd0194a86b2254d8c
        /// </summary>
        /// <returns></returns>
        public Model.weixin.token GetToken()
        {
            //if (EndTime > DateTime.Now)
            //    return tokenmodel;
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + appkey;
            StringBuilder sb = new StringBuilder();
            string str = Get(url);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                tokenmodel = jss.Deserialize<Model.weixin.token>(str);
                EndTime = System.DateTime.Now.AddSeconds(tokenmodel.expires_in);
                return tokenmodel;
            }
            catch (Exception ex)
            {
                TxtLog.Add("微信获得TOKEN异常：" + ex.ToString());
                return new Model.weixin.token();
            }
        }
        /// <summary>
        /// 解析错误代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Model.weixin.errormsg GetErrorMsg(string str)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Model.weixin.errormsg msg;
            try
            {
                msg = jss.Deserialize<Model.weixin.errormsg>(str);
                if (msg.errmsg == "ok")
                    msg.errmsg = "OK";
            }
            catch
            {
                msg = new Model.weixin.errormsg();
            }
            return msg;
        }
        #region 微信菜单
        /// <summary>
        /// 生成菜单
        /// </summary>
        /// <returns></returns>
        public Model.weixin.errormsg CreateMenu()
        {
            List<Lebi_weixin_menu> pmodels = B_Lebi_weixin_menu.GetList("parentid=0", "Sort desc", 3, 1);
            string json = "";
            List<Model.weixin.menu> menus = new List<Model.weixin.menu>();
            foreach (Lebi_weixin_menu pmodel in pmodels)
            {
                Model.weixin.menu menu = new Model.weixin.menu();
                menu.name = pmodel.name;
                menu.type = "view";
                List<Lebi_weixin_menu> models = B_Lebi_weixin_menu.GetList("parentid=" + pmodel.id + "", "Sort desc", 5, 1);
                if (models.Count > 0)
                {
                    List<Model.weixin.menu> smenus = new List<Model.weixin.menu>();
                    foreach (Lebi_weixin_menu model in models)
                    {
                        Model.weixin.menu smenu = new Model.weixin.menu();
                        smenu.name = model.name;
                        smenu.url = model.url;
                        smenu.type = "view";
                        smenus.Add(smenu);
                    }
                    menu.sub_button = smenus;
                    menu.url = "";
                }
                else
                {
                    menu.url = pmodel.url;
                    menu.sub_button = new List<Model.weixin.menu>();
                }
                menus.Add(menu);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            json = jss.Serialize(menus);
            json = "{\"button\":" + json + "}";
            json = json.Replace(",\"sub_button\":null", "");
            json = json.Replace("\"type\":\"\",", "");
            json = json.Replace("\"url\":\"\",", "");
            string res = Post("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + Token + "", json);
            return GetErrorMsg(res);
            //return json;
        }
        /// <summary>
        /// 下载菜单
        /// </summary>
        public string DownMenu()
        {
            try
            {
                string res = Get("https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + Token + "");
                JavaScriptSerializer jss = new JavaScriptSerializer();
                //KeyValue menu = jss.Deserialize<KeyValue>(res);
                //KeyValue button = jss.Deserialize<KeyValue>(menu.V);
                Model.weixin.Downmenu dmenu = jss.Deserialize<Model.weixin.Downmenu>(res);

                //List<Model.weixin.menu> menus = jss.Deserialize<List<Model.weixin.menu>>(button.V);
                if (dmenu.menu.button != null) {
                    B_Lebi_weixin_menu.Delete("id>0");
                    int i = 100;
                    foreach (Model.weixin.menu m in dmenu.menu.button)
                    {
                        Lebi_weixin_menu model = new Lebi_weixin_menu();
                        model.name = m.name;
                        model.Sort = i;
                        model.url = m.url;
                        model.type = m.type;
                        B_Lebi_weixin_menu.Add(model);
                        model.id = B_Lebi_weixin_menu.GetMaxId();
                        i--;
                        foreach (Model.weixin.menu sm in m.sub_button)
                        {
                            Lebi_weixin_menu smodel = new Lebi_weixin_menu();
                            smodel.name = sm.name;
                            smodel.Sort = i;
                            smodel.url = sm.url;
                            smodel.type = sm.type;
                            smodel.parentid = model.id;
                            B_Lebi_weixin_menu.Add(smodel);
                            i--;
                        }
                    }
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region 微信登陆
        /// <summary>
        /// 登录地址
        /// https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxf0e81c3bee622d60&redirect_uri=http%3A%2F%2Fnba.bluewebgame.com%2Foauth_response.php&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public string LoginURL(string back)
        {
            string uri = reurnurl + "?backurl=" + ENBackuri(back);
            uri = System.Web.HttpUtility.UrlEncode(uri);
            string res = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + uri + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
            return res;

        }
        /// <summary>
        /// 微信登录
        /// https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public string Login(string back, int IsLogin = 1)
        {
            string code = RequestTool.RequestString("code");
            if (code != "")
            {
                try
                {

                    string uri = reurnurl + "?backurl=" + ENBackuri(back);
                    int userid = 0;
                    uri = System.Web.HttpUtility.UrlEncode(uri);
                    string res = Post("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + appkey + "&code=" + code + "&grant_type=authorization_code");
                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    Model.weixin.tokeninfo tokeninfo = jss.Deserialize<Model.weixin.tokeninfo>(res);
                    string uid = tokeninfo.openid;
                    //SystemLog.Add(uid);
                    //获取用户资料
                    //https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
                    res = Post("https://api.weixin.qq.com/sns/userinfo?access_token=" + tokeninfo.access_token + "&openid=" + uid + "&lang=zh_CN");

                    Model.weixin.userinfo model = jss.Deserialize<Model.weixin.userinfo>(res);
                    string where = "bind_weixin_id='" + uid + "' and bind_weixin_id!=''";
                    //if (DT_id > 0)
                    //{
                    //    where += " and DT_id =" + DT_id + "";
                    //}
                    Lebi_User user = B_Lebi_User.GetModel(where);
                    Lebi_User CurrentUser = null;
                    string qrcodeid_ = RegexTool.GetRegValue((back + "&"), @"qrcodeid=(\d*)&");
                    int qrid = 0;
                    int.TryParse(qrcodeid_, out qrid);
                    if (qrid > 0)
                    {
                        //扫描二维码授权绑定当前账号
                        Lebi_weixin_qrcode qcode = B_Lebi_weixin_qrcode.GetModel(qrid);
                        CurrentUser = B_Lebi_User.GetModel(qcode.User_id);
                        if (IsLogin == 0)
                        {
                            if (user != null && CurrentUser != null)
                            {
                                return "已绑定其它帐号";
                            }
                        }
                    }

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
                        if (CurrentUser == null)
                        {
                            user = new Lebi_User();
                            user.bind_weixin_id = uid;
                            user.bind_weixin_nickname = model.nickname;
                            user.bind_weixin_token = tokeninfo.access_token;
                            try
                            {
                                user.Face = DownLoadImage(model.headimgurl);
                            }
                            catch
                            {
                                user.Face = model.headimgurl;
                            }
                            user.NickName = model.nickname;
                            user.UserName = "weixin_" + uid;
                            user.Password = EX_User.MD5(uid);
                            user.Language = Language.CurrentLanguage().Code;
                            user.Sex = model.sex == "2" ? "女" : "男";
                            user.UserLevel_id = B_Lebi_UserLevel.GetList("Grade>0", "Grade asc").FirstOrDefault().id;
                            user.IsPlatformAccount = 1;
                            //B_Lebi_User.Add(user);
                            //user.id = B_Lebi_User.GetMaxId();
                            //userid = user.id;
                            user.DT_id = DT_id;
                            EX_User.UserReg(user);
                        }
                        else
                        {
                            CurrentUser.bind_weixin_id = uid;
                            CurrentUser.bind_weixin_nickname = model.nickname;
                            CurrentUser.bind_weixin_token = tokeninfo.access_token;

                            try
                            {
                                CurrentUser.Face = DownLoadImage(model.headimgurl);
                            }
                            catch
                            {
                                CurrentUser.Face = model.headimgurl;
                            }
                            //CurrentUser.Face = model.headimgurl;//头像 50*50

                            if (CurrentUser.NickName == "")
                                CurrentUser.NickName = model.nickname;
                            CurrentUser.DT_id = DT_id;
                            //B_Lebi_User.Update(CurrentUser);
                            EX_User.LoginOK(CurrentUser, 0);
                            userid = CurrentUser.id;
                        }
                    }
                    else
                    {
                        user.bind_weixin_id = uid;
                        user.bind_weixin_nickname = model.nickname;
                        user.bind_weixin_token = tokeninfo.access_token; ;

                        try
                        {
                            user.Face = DownLoadImage(model.headimgurl);
                        }
                        catch (Exception ex)
                        {
                            user.Face = "";
                            LB.Tools.TxtLog.Add(ex.Message);

                        }

                        if (user.NickName == "")
                            user.NickName = model.nickname;
                        user.DT_id = DT_id;
                        //user.Sex = model.gender == "f" ? "女" : "男";
                        //B_Lebi_User.Update(user);
                        EX_User.LoginOK(user, 0);
                        userid = user.id;
                    }


                    if (qrid > 0)
                    {
                        //扫描二维码登录
                        Lebi_weixin_qrcode qcode = B_Lebi_weixin_qrcode.GetModel(qrid);
                        qcode.User_id = userid;
                        B_Lebi_weixin_qrcode.Update(qcode);
                        //Log.Add(back);
                    }
                    return "OK";
                }
                catch (Exception ex)
                {
                    LB.Tools.TxtLog.Add(ex.Message);
                    return "授权失败" + ex.Message;
                }
            }
            return "授权失败";
        }
        /// <summary>
        /// 通过关注者openid获得LEBI用户信息
        /// 如不存在则添加
        /// </summary>
        /// <param name="openid"></param>
        public Lebi_User GetUserByopenid(string openid)
        {
            return GetUserByopenid(openid, 0);
        }
        public Lebi_User GetUserByopenid(string openid, int DT_id = 0)
        {
            string res = Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Token + "&openid=" + openid + "&lang=zh_CN");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Model.weixin.userinfo model = jss.Deserialize<Model.weixin.userinfo>(res);
            Lebi_User user = B_Lebi_User.GetModel("UserName='weixin_" + openid + "'");
            if (user == null)
            {

                user = new Lebi_User();
                user.bind_weixin_id = openid;
                user.bind_weixin_nickname = model.nickname;
                //user.bind_weixin_token = tokeninfo.access_token;
                try
                {
                    user.Face = DownLoadImage(model.headimgurl);
                }
                catch
                {
                    user.Face = model.headimgurl;
                }
                user.NickName = model.nickname;
                user.UserName = "weixin_" + openid;
                user.Password = EX_User.MD5(openid);
                user.Language = Language.CurrentLanguage().Code;
                user.Sex = model.sex == "2" ? "女" : "男";
                user.UserLevel_id = B_Lebi_UserLevel.GetList("Grade>0", "Grade asc").FirstOrDefault().id;
                user.IsPlatformAccount = 1;
                if (CurrentSite != null)
                    user.Site_id = CurrentSite.id;
                user.DT_id = DT_id;
                //B_Lebi_User.Add(user);
                //user.id = B_Lebi_User.GetMaxId();
                //userid = user.id;
                return EX_User.UserReg(user);

            }
            else
            {
                user.bind_weixin_id = openid;
                user.bind_weixin_nickname = model.nickname;
                //user.bind_weixin_token = tokeninfo.access_token; ;
                //if (user.Face == "")
                //{
                try
                {
                    user.Face = DownLoadImage(model.headimgurl);
                }
                catch
                {
                    user.Face = model.headimgurl;
                }
                //}
                if (user.NickName == "")
                    user.NickName = model.nickname;
                //user.Sex = model.gender == "f" ? "女" : "男";
                user.DT_id = DT_id;
                B_Lebi_User.Update(user);
                EX_User.LoginOK(user, 0);
                return user;
            }

        }
        /// <summary>
        /// 下载头像
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string DownLoadImage(string url)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            string ServerPath = AppDomain.CurrentDomain.BaseDirectory;
            string savepath = ShopCache.GetBaseConfig().UpLoadPath + "/userface/" + DateTime.Now.ToString("yyyy") + "/user/";
            savepath = ThemeUrl.CheckURL(savepath);
            if (!Directory.Exists(ServerPath + savepath))   //如果路径不存在，则创建
            {
                Directory.CreateDirectory(ServerPath + savepath);
            }
            string name = DateTime.Now.ToString("yyMMddssfff") + "_w$h_.jpg";
            string OldImage = savepath + name;
            if (File.Exists(OldImage))
            {
                File.Delete(OldImage);
            }
            myWebClient.DownloadFile(url, ServerPath + OldImage);
            return OldImage;
        }
        #endregion
        #region 二维码
        /// <summary>
        /// 创建二维码ticket  
        /// </summary>
        /// <param name="user"></param>
        /// <param name="codetype">0永久1临时二维码</param>
        /// <returns></returns>
        public string QrCode(int codetype, Lebi_User user = null)
        {
            string result = "";
            //QR_SCENE临时二维码   QR_LIMIT_SCENE永久二维码
            string codetypestr = "QR_LIMIT_SCENE";
            if (codetype == 1)
                codetypestr = "QR_SCENE";
            if (user == null)
                user = new Lebi_User();
            string strJson = "{\"action_name\": \"" + codetypestr + "\", \"action_info\": {\"scene\": {\"scene_id\":" + user.id + "}}}";
            string res = Post("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + Token + "", strJson);
            //content:{"errcode":40001,"errmsg":"invalid credential, access_token is invalid or not latest hint: [vbHGEa0238vr32!]"}
            if (res.Contains("invalid credential"))
            {
                _Instance = new weixin();
                //return QrCode(codetype, user);
                return "error";
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Model.weixin.Ticket ticket = jss.Deserialize<Model.weixin.Ticket>(res);

            LB.Tools.TxtLog.Add(res);
            result = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + ticket.ticket;
            return result;
        }

        #endregion
        /// <summary>
        /// 获得微信消息
        /// </summary>
        /// <returns></returns>
        public Model.weixin.wxmessage GetWxMessage()
        {
            Model.weixin.wxmessage wx = new Model.weixin.wxmessage();
            StreamReader str = new StreamReader(System.Web.HttpContext.Current.Request.InputStream, System.Text.Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
            wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
            wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
            if (wx.MsgType.Trim() == "text")
            {
                wx.MsgId = xml.SelectSingleNode("xml").SelectSingleNode("MsgId").InnerText;
                wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
            }
            if (wx.MsgType.Trim() == "event")
            {
                wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
                wx.EventKey = xml.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;
            }
            return wx;
        }
        /// <summary>    
        /// 发送文字消息(被动)   
        /// </summary>    
        /// <param name="wx">获取的收发者信息    
        /// <param name="content">内容    
        /// <returns></returns>    
        public string sendTextMessage(Model.weixin.wxmessage wx, string content)
        {
            if (content == "")
                return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + wx.FromUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + number + "]]></FromUserName>");
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string time = Convert.ToInt64(ts.TotalSeconds).ToString();
            sb.Append("<CreateTime>" + time + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[text]]></MsgType>");
            sb.Append("<Content><![CDATA[" + content + "]]></Content>");
            sb.Append("</xml>");
            return sb.ToString();
        }
        /// <summary>
        /// 发送文字消息(主动)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string sendTextMessage1(Model.weixin.wxmessage wx, string content)
        {
            string strJson = "{\"touser\": \"" + wx + "\",\"msgtype\": \"text\", \"text\": {\"content\":" + content + "}}";
            string res = Post("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Token + "", strJson);
            return GetErrorMsg(res).errmsg;
        }
    }
}
