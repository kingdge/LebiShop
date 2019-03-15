using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Money_Edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier_Money model;
        protected List<Lebi_Supplier> users;
        protected string User_Name;
        protected string user_ids;
        protected string Money;
        protected int id;
        protected int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("supplier_money_add", "添加商家资金"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("supplier_money_edit", "编辑商家资金"))
                {
                    WindowNoPower();
                }
            }
            int user_id = RequestTool.RequestInt("user_id", 0);
            model = B_Lebi_Supplier_Money.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Money();
                model.Type_id_MoneyStatus = 181;//有效资金
                model.Type_id_SupplierMoneyType = 344; //人工调整
                Money = "";
            }
            else{
                Money = FormatMoney(model.Money,"Number");
                user_id = model.Supplier_id;
            }
        }
    }
}