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
    public partial class Message_Write_window : SupplierAjaxBase
    {
        protected Lebi_Tab model;
        protected List<Lebi_Supplier> user;
        protected string User_Name;
        protected string ids;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_message", "站内信"))
            {
                WindowNoPower();
            }
            User_Name = RequestTool.RequestString("User_Name");
            ids = RequestTool.RequestString("ids");
            if (ids != "")
            {
                user = B_Lebi_Supplier.GetList("id in (lbsql{" + ids + "})", "id desc");
                if (user != null)
                {

                }
            }
        }
    }
}