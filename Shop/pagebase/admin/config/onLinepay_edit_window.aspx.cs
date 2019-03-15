using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class OnLinePay_Edit_window : AdminAjaxBase
    {
        protected Lebi_OnlinePay model;
        protected string username;
        protected string key;
        protected string email;

        protected List<Lebi_OnlinePay> models;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("onlinepay_add", "添加在线支付"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("onlinepay_edit", "编辑在线支付"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_OnlinePay.GetModel(id);
            if (model == null)
                model = new Lebi_OnlinePay();
            string where = "parentid=0";
            models = B_Lebi_OnlinePay.GetList(where, "IsUsed desc,Sort desc", PageSize, page);
            switch (model.Code)
            {
                case "molpay":
                    username = "Merchant ID";
                    key = "Verify Key";
                    email = Tag("邮箱");
                    break;
                case "wangyinzaixian":
                    username = "商户号";
                    key = "MD5密钥";
                    email = Tag("邮箱");
                    break;
                case "alipayJSDZ":
                    username = "合作者身份(PID)";
                    key = "安全校验码(Key)";
                    email = "支付宝账号";
                    break;
                case "alipayDBJY":
                    username = "合作者身份(PID)";
                    key = "安全校验码(Key)";
                    email = "支付宝账号";
                    break;
                case "alipaySJK":
                    username = "合作者身份(PID)";
                    key = "安全校验码(Key)";
                    email = "支付宝账号";
                    break;
                case "alipayWAP":
                    username = "合作者身份(PID)";
                    key = "安全校验码(Key)";
                    email = "支付宝账号";
                    break;
                case "alipaybatch":
                    username = "合作者身份(PID)";
                    key = "安全校验码(Key)";
                    email = "支付宝账号";
                    break;
                case "chinapnr":
                    username = "商户ID";
                    key = "公钥相对路径";
                    email = "私钥相对路径";
                    break;
                default:
                    username = Tag("帐号");
                    key = Tag("KEY");
                    email = Tag("邮箱");
                    break;
            }
        }

        public string Currency(int id)
        {
            string str = "";
            List<Lebi_Currency> models = B_Lebi_Currency.GetList("", "Sort desc");
            foreach (Lebi_Currency m in models)
            {
                string sel = "";
                if (m.id == id)
                    sel = "selected";
                str += "<option value=\"" + m.id + "\" " + sel + ">" + m.Name + "</option>";
            }
            return str;
        }
    }
}