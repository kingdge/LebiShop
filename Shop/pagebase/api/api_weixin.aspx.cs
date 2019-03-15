using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.Security;
namespace Shop.api
{
    public class api_weixin : System.Web.UI.Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            SystemLog.Add("api_weixin");
            if (!Check())
                return;
            try
            {
                //微信推送消息处理
                Shop.Platform.Model.weixin.wxmessage wx = Shop.Platform.weixin.Instance.GetWxMessage();
                string platform_weixin_subscribe_automsg = "";
                string parentuserid = wx.EventKey.Replace("qrscene_", "");
                Lebi_User parentuser = B_Lebi_User.GetModel("id=" + parentuserid + "");
                if (parentuser == null)
                    parentuser = new Lebi_User();
                int DT_id = ShopPage.GetDT();
                if (DT_id == 0)
                {
                    BaseConfig bcf = ShopCache.GetBaseConfig();
                    platform_weixin_subscribe_automsg = bcf.platform_weixin_subscribe_automsg;
                    if (parentuser != null)
                    {
                        DT_id = parentuser.DT_id;
                    }
                }
                else
                {
                    BaseConfig_DT bcf = ShopCache.GetBaseConfig_DT(DT_id);
                    platform_weixin_subscribe_automsg = bcf.platform_weixin_subscribe_automsg;
                }
                string res = "";
                if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "subscribe")//未关注情况下扫码的事件推送
                {
                    string content = "";
                    if (!wx.EventKey.Contains("qrscene_"))
                    {
                        content = platform_weixin_subscribe_automsg;
                        res = Shop.Platform.weixin.Instance.sendTextMessage(wx, content);
                    }
                    else
                    {
                        //content = "二维码参数：\n" + wx.EventKey.Replace("qrscene_", "");
                        //res = Shop.Platform.weixin.Instance.sendTextMessage(wx, content);
                        content = platform_weixin_subscribe_automsg;
                        res = Shop.Platform.weixin.Instance.sendTextMessage(wx, content);

                        parentuserid = wx.EventKey.Replace("qrscene_", "");
                        Lebi_User user = Shop.Platform.weixin.Instance.GetUserByopenid(wx.FromUserName, DT_id);
                        if (parentuser != null)
                        {
                            if (parentuser.id != user.id && user.User_id_parent == 0)
                            {
                                //生成上下级关系
                                user = B_Lebi_User.GetModel(user.id);
                                user.User_id_parent = parentuser.id;
                                user.DT_id = DT_id;
                                B_Lebi_User.Update(user);
                            }
                        }

                    }
                    Shop.Platform.weixin.GetInstance(DT_id, null).GetUserByopenid(wx.FromUserName, DT_id);
                }
                else if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.ToLower() == "scan")//已经关注情况下扫码的事件推送
                {
                    Lebi_User user = Shop.Platform.weixin.Instance.GetUserByopenid(wx.FromUserName);
                    if (parentuser != null)
                    {
                        if (parentuser.id != user.id && user.User_id_parent == 0)
                        {
                            //生成上下级关系
                            user = B_Lebi_User.GetModel(user.id);
                            user.User_id_parent = parentuser.id;
                            user.DT_id = DT_id;
                            B_Lebi_User.Update(user);
                        }
                    }

                    //string str = "二维码参数：\n" + wx.EventKey;
                    //res = Shop.Platform.weixin.Instance.sendTextMessage(wx, str);
                }
                else if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "CLICK")
                {
                    if (wx.EventKey == "HELLO")
                        res = Shop.Platform.weixin.Instance.sendTextMessage(wx, "你好,欢迎使用公共微信平台!");
                }
                else
                {
                    if (wx.MsgType == "text" && wx.Content == "你好")
                    {
                        res = Shop.Platform.weixin.Instance.sendTextMessage(wx, "你好,欢迎使用公共微信平台!");
                    }
                    //else if (wx.MsgType == "voice")
                    //{
                    //    res = Shop.Platform.weixin.Instance.sendTextMessage(wx, wx.Recognition);
                    //}
                    else
                    {
                        res = Shop.Platform.weixin.Instance.sendTextMessage(wx, "你好,未能识别消息!");
                    }
                }

                Response.Write(res);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 验证消息真实性
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            BaseConfig bcf = ShopCache.GetBaseConfig();
            string signature = RequestTool.RequestString("signature");
            string timestamp = RequestTool.RequestString("timestamp");
            string nonce = RequestTool.RequestString("nonce");
            string echostr = RequestTool.RequestString("echostr");
            string token = bcf.platform_weixin_custemtoken;
            string[] arr = { token, timestamp, nonce };
            Array.Sort(arr);
            string temp = arr[0] + arr[1] + arr[2];
            string sha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(temp, "SHA1").ToLower();
            //SystemLog.Add(sha1 + "---------" + signature + "------------" + echostr);
            if (sha1 == signature)
            {
                if (echostr != "")
                    Response.Write(echostr);
                return true;
            }
            else
                return false;
        }

    }
}