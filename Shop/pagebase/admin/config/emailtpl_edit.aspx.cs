using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.Config
{
    public partial class EmailTPL_Edit : AdminPageBase
    {
        protected string content;
        protected string type;
        protected string typename;
        protected string title;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_email_edit", "编辑邮件模板"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            type = RequestTool.RequestString("type");
            if (type == "") type = "EmailTPL_newuser";
            BaseConfig model = ShopCache.GetBaseConfig();
            switch (type)
            {
                case "EmailTPL_getpwd":
                    content = model.EmailTPL_getpwd;
                    title = model.EmailTPL_getpwd_title;
                    typename = Tag("找回密码");
                    break;
                case "EmailTPL_newuser":
                    content = model.EmailTPL_newuser;
                    title = model.EmailTPL_newuser_title;
                    typename = Tag("新用户注册");
                    break;
                case "EmailTPL_ordershipping":
                    content = model.EmailTPL_ordershipping;
                    title = model.EmailTPL_ordershipping_title;
                    typename = Tag("订单发货");
                    break;
                case "EmailTPL_ordersubmit":
                    content = model.EmailTPL_ordersubmit;
                    title = model.EmailTPL_ordersubmit_title;
                    typename = Tag("订单提交");
                    break;
                case "EmailTPL_orderpaid":
                    content = model.EmailTPL_orderpaid;
                    title = model.EmailTPL_orderpaid_title;
                    typename = Tag("订单付款");
                    break;
                case "EmailTPL_Admin_newuser":
                    content = model.EmailTPL_Admin_newuser;
                    title = model.EmailTPL_Admin_newuser_title;
                    typename = Tag("新用户注册");
                    break;
                case "EmailTPL_Admin_ordersubmit":
                    content = model.EmailTPL_Admin_ordersubmit;
                    title = model.EmailTPL_Admin_ordersubmit_title;
                    typename = Tag("订单提交");
                    break;
                case "EmailTPL_Admin_orderpaid":
                    content = model.EmailTPL_Admin_orderpaid;
                    title = model.EmailTPL_Admin_orderpaid_title;
                    typename = Tag("订单付款");
                    break;
                case "EmailTPL_Admin_ordercomment":
                    content = model.EmailTPL_Admin_ordercomment;
                    title = model.EmailTPL_Admin_ordercomment_title;
                    typename = Tag("订单留言");
                    break;
                case "EmailTPL_Admin_inquiry":
                    content = model.EmailTPL_Admin_inquiry;
                    title = model.EmailTPL_Admin_inquiry_title;
                    typename = Tag("留言反馈");
                    break;
                case "EmailTPL_Admin_comment":
                    content = model.EmailTPL_Admin_comment;
                    title = model.EmailTPL_Admin_comment_title;
                    typename = Tag("商品评论");
                    break;
                case "EmailTPL_Admin_ask":
                    content = model.EmailTPL_Admin_ask;
                    title = model.EmailTPL_Admin_ask_title;
                    typename = Tag("商品咨询");
                    break;
                case "EmailTPL_Admin_message":
                    content = model.EmailTPL_Admin_message;
                    title = model.EmailTPL_Admin_message_title;
                    typename = Tag("站内信");
                    break;
                case "EmailTPL_changgouqingdan":
                    content = model.EmailTPL_changgouqingdan;
                    title = model.EmailTPL_changgouqingdan_title;
                    typename = Tag("常购清单");
                    break;
                case "EmailTPL_checkcode":
                    content = model.EmailTPL_checkcode;
                    title = model.EmailTPL_checkcode_title;
                    typename = Tag("常购清单");
                    break;
                case "EmailTPL_sendfriend":
                    content = model.EmailTPL_sendfriend;
                    title = model.EmailTPL_sendfriend_title;
                    typename = Tag("邮件分享");
                    break;
                case "EmailTPL_reserveok":
                    content = model.EmailTPL_reserveok;
                    title = model.EmailTPL_reserveok_title;
                    typename = Tag("预定到货提醒");
                    break;
                default:
                    PageError();
                    Response.End();
                    break;
            }

        }

    }
}