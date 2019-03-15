using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class OnLinePay_Edit_window : SupplierAjaxBase
    {
        protected Lebi_OnlinePay model;
        protected Lebi_OnlinePay parentmodel;
        protected string username;
        protected string key;
        protected string email;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            
            parentmodel = B_Lebi_OnlinePay.GetModel(id);
            if (parentmodel == null)
            {
                PageError();
                return;
            }
            model = B_Lebi_OnlinePay.GetModel("parentid=" + parentmodel.id + " and Supplier_id=" + CurrentSupplier.id + "");
            if (model == null)
                model = new Lebi_OnlinePay();
            if (model.id == 0)
            {
                if (!Power("onlinepay_add", "添加在线支付"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!Power("onlinepay_edit", "编辑在线支付"))
                {
                    WindowNoPower();
                }
            }
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
                str += "<option value=\"" + m.id + "\">" + m.Name + "</option>";
            }
            return str;
        }
    }
}