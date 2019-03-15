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
    public partial class Transport_Container_Edit_window : AdminAjaxBase
    {
        protected Lebi_Transport_Container model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("transport_add", "添加配送方式"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("transport_edit", "编辑配送方式"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Transport_Container.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Transport_Container();
            }
        }

    }
}