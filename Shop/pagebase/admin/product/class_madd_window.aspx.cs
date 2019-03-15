using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class Class_MAdd_window : AdminAjaxBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("pro_type_add", "添加商品分类"))
            {
                WindowNoPower();
            }
        }
        /// <summary>
        /// 属性规格选择
        /// </summary>
        /// <param name="selid"></param>
        /// <returns></returns>
        public string Property(string selid, int t)
        {
            string str = "";
            List<Lebi_ProPerty> pros = B_Lebi_ProPerty.GetList("parentid=0 and Type_id_ProPertyType=" + t + "", "Sort desc");
            foreach (Lebi_ProPerty pro in pros)
            {
                string sel = "";
                if (("," + selid + ",").Contains("," + pro.id + ""))
                {
                    sel = "checked";
                }
                str += "<label><input " + sel + " type=\"checkbox\" value=\"" + pro.id + "\" shop=\"true\" name=\"ProPerty" + t + "\"/>" + Language.Content(pro.Name, CurrentLanguage.Code) + "</label>&nbsp;";
            }
            return str;
        }
    }
}