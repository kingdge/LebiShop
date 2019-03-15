using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class order_baseinfo_edit_window : AdminAjaxBase
    {
        protected Lebi_Order model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Order.GetModel(id);
            if (model == null)
                model = new Lebi_Order();
        }
        public string paylist(int sid)
        {
            List<Lebi_Pay> models = B_Lebi_Pay.GetList("IsUsed=1", "Sort desc");
            string str = "";
            foreach (Lebi_Pay pay in models)
            {
                string sel = "";
                if (pay.id == sid)
                    sel = "selected";
                str += "<option  code=\"" + pay.Code + "\"  value=\"" + pay.id + "\" " + sel + ">" + Lang(pay.Name) + "</option>";
            }
            return str;
        }
        public string onlinepaylist(int sid)
        {
            List<Lebi_OnlinePay> models = B_Lebi_OnlinePay.GetList("IsUsed=1", "Sort desc");
            string str = "";
            foreach (Lebi_OnlinePay pay in models)
            {
                string sel = "";
                if (pay.id == sid)
                    sel = "selected";
                str += "<option code=\"" + pay.Code + "\" value=\"" + pay.id + "\" " + sel + ">" + Lang(pay.Name) + "</option>";
            }
            return str;
        }
        public string transportlist(int sid)
        {
            List<Lebi_Transport> models = B_Lebi_Transport.GetList("", "Sort desc");
            string str = "";
            foreach (Lebi_Transport m in models)
            {
                string sel = "";
                if (m.id == sid)
                    sel = "selected";
                str += "<option code=\"" + m.Type_id_TransportType + "\" value=\"" + m.id + "\" " + sel + ">" + m.Name + "</option>";
            }
            return str;
        }
        public string pickuplist(int sid)
        {
            List<Lebi_PickUp> models = B_Lebi_PickUp.GetList("Supplier_id=0", "Sort desc");
            string str = "";
            foreach (Lebi_PickUp m in models)
            {
                string sel = "";
                if (m.id == sid)
                    sel = "selected";
                str += "<option value=\"" + m.id + "\" " + sel + ">" + m.Name + "</option>";
            }
            return str;
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