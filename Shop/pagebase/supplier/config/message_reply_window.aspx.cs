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
    public partial class Message_Reply_window : SupplierAjaxBase
    {
        protected Lebi_Message model;
        protected List<Lebi_Message> models;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_message", "站内信"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Message.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
            {
                model = new Lebi_Message();
            }
            if (model.User_id_From == 0)
            {
                model.IsRead = 1;
                B_Lebi_Message.Update(model);
            }
            models = B_Lebi_Message.GetList("Parentid = " + id, "id desc", PageSize, page);
        }
    }
}