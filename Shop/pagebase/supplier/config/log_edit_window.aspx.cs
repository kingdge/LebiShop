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
    public partial class Log_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Log model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_log_list", "操作日志"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Log.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
            {
                model = new Lebi_Log();
            }
        }
    }
}