using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Platform.Model.weixin
{
    /// <summary>
    /// token
    /// </summary>
    public class token
    {
        public string access_token
        {
            get;
            set;
        }
        public int expires_in
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 错误信息
    /// {"errcode":40013,"errmsg":"invalid appid"}
    /// </summary>
    public class errormsg
    {
        public string errcode
        {
            get;
            set;
        }
        public string errmsg
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 自定义菜单
    /// </summary>
    public class menu
    {
        public string type
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string url
        {
            get;
            set;
        }
        public List<menu> sub_button
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 下载菜单的格式
    /// </summary>
    public class Downmenu
    {
        public sonmenu menu
        {
            get;
            set;
        }
        public class sonmenu
        {
            public List<menu> button
            {
                get;
                set;
            }
        }
    }
    /// <summary>
    /// 用户登录的TOKEN
    /// "access_token":"ACCESS_TOKEN",
    /// "expires_in":7200,
    /// "refresh_token":"REFRESH_TOKEN",
    /// "openid":"OPENID",
    /// "scope":"SCOPE"
    /// </summary>
    public class tokeninfo
    {
        public string access_token
        {
            get;
            set;
        }
        public int expires_in
        {
            get;
            set;
        }
        public string refresh_token
        {
            get;
            set;
        }
        public string openid
        {
            get;
            set;
        }
        public string SCOPE
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 用户资料
    /// "openid":" OPENID",
    ///" nickname": NICKNAME,
    ///"sex":"1",
    ///"province":"PROVINCE"
    ///"city":"CITY",
    ///"country":"COUNTRY",
    /// "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46", 
    /// "privilege":[
    /// "PRIVILEGE1"
    /// "PRIVILEGE2"
    /// ]
    /// </summary>
    public class userinfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }

    }
    /// <summary>
    /// 二维码
    /// </summary>
    public class Ticket
    {
        public string ticket { get; set; }
        public string expire_seconds { get; set; }
        public string url { get; set; }
    }
    public class wxmessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string MsgType { get; set; }
        public string MsgId { get; set; }
        public string Content { get; set; }
        public string EventName { get; set; }
        public string EventKey { get; set; }
    }
}
