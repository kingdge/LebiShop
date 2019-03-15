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
    public partial class BillType_Edit_window : AdminAjaxBase
    {
        protected Lebi_BillType model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("billtype_add", "添加发票类型"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("billtype_edit", "编辑发票类型"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_BillType.GetModel(id);
            if (model == null)
            {
                model = new Lebi_BillType();
                model.Type_id_BillType = 151;
            }
        }
    }
}