using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Web.Script.Serialization;

namespace Shop.Admin.promotion
{
    public partial class Promotion_Edit : AdminPageBase
    {
        protected Lebi_Promotion model;
        protected Lebi_Promotion_Type pt;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("promotion_add", "添加促销活动"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!EX_Admin.Power("promotion_edit", "编辑促销活动"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            
            model = B_Lebi_Promotion.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Promotion();
                model.Type_id_PromotionStatus = 240;
            }
            else
            {
                tid = model.Promotion_Type_id;
                if (model.Case805 != "")
                {
                    string codes = "";
                    List<Lebi_Product> pros = B_Lebi_Product.GetList("id in (" + model.Case805 + ")", "");
                    foreach (Lebi_Product pro in pros)
                    {
                        if (codes == "")
                            codes = pro.Number;
                        else
                        {
                            if (codes.Contains(pro.Number))
                                continue;
                            else
                                codes = codes + "," + pro.Number;
                        }
                    }
                    model.Case805 = codes;
                }
            }
            pt = B_Lebi_Promotion_Type.GetModel(tid);
            if (pt == null)
            {
                PageError();
                return;
            }


        }


        public string setChecked(int v)
        {
            if (v == 1)
                return "checked";
            return "";
        }

    }
}