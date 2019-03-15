using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
namespace Shop.Admin.order
{
    public partial class Bill_Edit_window : AdminAjaxBase
    {
        protected Lebi_Bill model;
        protected Lebi_Order order;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("bill_edit", "修改发票"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int orderid = RequestTool.RequestInt("orderid", 0);
            model = B_Lebi_Bill.GetModel(id);

            Lebi_Order order = B_Lebi_Order.GetModel(orderid);
            if (order == null)
                order = new Lebi_Order();
            if (order.id > 0 && model == null)
            {
                model = B_Lebi_Bill.GetModel("Order_id=" + order.id + "");
                if (model == null)
                {
                    model = new Lebi_Bill();
                    model.Order_Code = order.Code;
                    model.Order_id = order.id;
                    model.User_id = order.User_id;
                    model.User_UserName = order.User_UserName;
                    model.Type_id_BillStatus = 160;
                }
            }
            if (model == null)
            {
                model = new Lebi_Bill();
                model.Type_id_BillStatus = 160;
            }

        }
        public Lebi_BillType BillType(int id)
        {
            Lebi_BillType m = B_Lebi_BillType.GetModel(id);
            if (m == null)
                m = new Lebi_BillType();
            return m;
        }
        public Lebi_User GetUser(int id)
        {
            Lebi_User User = B_Lebi_User.GetModel(id);
            if (User == null)
                User = new Lebi_User();
            return User;
        }
        public string billlist(int sid)
        {
            List<Lebi_BillType> models = B_Lebi_BillType.GetList("", "Sort desc");
            string str = "";
            foreach (Lebi_BillType m in models)
            {
                string sel = "";
                if (m.id == sid)
                    sel = "selected";
                str += "<option code=\"" + m.Type_id_BillType + "\" value=\"" + m.id + "\" " + sel + ">" + Lang(m.Name) + "</option>";
            }
            return str;
        }
    }
}