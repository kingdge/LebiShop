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
    public partial class ServicePanel_Config_window : AdminAjaxBase
    {
        protected BaseConfig model;
        protected Shop.Model.ServicePanel sp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("servicepanel_config", "编辑客服面板配置"))
            {
                WindowNoPower();
            }
            B_ServicePanel config = new B_ServicePanel();
            model = ShopCache.GetBaseConfig();

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