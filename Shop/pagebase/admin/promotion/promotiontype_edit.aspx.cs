using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.promotion
{
    public partial class PromotionType_Edit : AdminPageBase
    {
        protected Lebi_Promotion_Type model;
        protected List<Lebi_UserLevel> userlevels;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("promotion_add", "添加促销活动"))
                {
                    PageNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("promotion_edit", "编辑促销活动"))
                {
                    PageNoPower();
                }
            }
            model = B_Lebi_Promotion_Type.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Promotion_Type();
                model.Type_id_PromotionStatus = 240;
                model.Type_id_PromotionType = 421;

            }
            userlevels = B_Lebi_UserLevel.GetList("","Grade desc");
        }

        public string CheckStatus(string ids,int id)
        {
            if (("," + ids + ",").Contains("," + id.ToString() + ","))
                return "checked";
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public string GetShops(int sid)
        {
            string str = "";
            List<Lebi_Supplier> shops = B_Lebi_Supplier.GetList("Type_id_SupplierStatus=442 and IsSupplierTransport=1", "");
            foreach (Lebi_Supplier shop in shops)
            {
                string sel = "";
                if (sid == shop.id)
                    sel = "selected";
                str += "<option value=\"" + shop.id + "\" " + sel + ">" + shop.Company + "</option>";
            }
            return str;
        }
    }
}