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
    public partial class ServicePanel_Config_window : SupplierAjaxBase
    {
        protected Lebi_Supplier model;
        protected Shop.Model.ServicePanel sp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                WindowNoPower();
            }
            model = B_Lebi_Supplier.GetModel(CurrentSupplier.id);
            if (model == null)
                model = new Lebi_Supplier();
            string con = model.ServicePanel;
            sp = B_ServicePanel.GetModel(con);

            //===================================
            //这么取数据
            //string con = "[{\"x\":\"111\",\"y\":\"112\".....}]";
            //Shop.Model.ServicePanel sp = B_ServicePanel.GetModel(con);

            //这个sp里面就有你要的数据了
            //例如   sp.x


            //保存的话,可以把以上的sp直接转化为json格式的字符串
            //如
            //Model.ServicePanel sp = new Model.ServicePanel();
            //sp.x = "";
            //sp.y = "";
            //sp.theme = "";

            //string json = B_ServicePanel.ToJson(sp);
            //=====================================
        }
    }
}