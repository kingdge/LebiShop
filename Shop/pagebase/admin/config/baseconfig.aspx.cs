using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin.storeConfig
{
    public partial class BaseConfig_edit : AdminPageBase
    {
        protected BaseConfig model;
        protected List<BaseConfigStepR> stepRs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("baseconfig_edit", "基本设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            B_BaseConfig bconfig = new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
            stepRs = B_BaseConfig.StepR(model.Refund_StepR);
            if (stepRs == null)
                stepRs = new List<BaseConfigStepR>();
        }
        public string areas(string sid)
        {
            List<Lebi_Area> areas = B_Lebi_Area.GetList("Parentid=0", "Sort desc");
            string str = "";
            foreach (Lebi_Area area in areas)
            {
                string sel = "";
                if (sid == area.id.ToString())
                    sel = "selected";
                str += "<option " + sel + " value=\"" + area.id + "\">" + area.Name + "</option>";
            }
            return str;
        }

    }
}