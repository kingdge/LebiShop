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
    public partial class Class_Edit_window : AdminAjaxBase
    {
        protected Lebi_Pro_Type model;
        protected Lebi_Pro_Type pmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("pro_type_add", "添加商品分类"))
                {
                    PageNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("pro_type_edit", "编辑商品分类"))
                {
                    PageNoPower();
                }
            }
            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_Pro_Type.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Pro_Type();
                model.IsIndexShow = 1;
                model.IsShow = 1;
                model.Site_ids = site.Sitesid();
            }
            else
            {
                pid = model.Parentid;
            }
            pmodel = B_Lebi_Pro_Type.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_Pro_Type();
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
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"ProPerty" + t + "" + pro.id + "\" name=\"ProPerty" + t + "\" value=\"" + pro.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + Language.Content(pro.Name, CurrentLanguage.Code) + "</span></label>";
                }
                else
                {
                    str += "<label><input " + sel + " type=\"checkbox\" value=\"" + pro.id + "\" shop=\"true\" name=\"ProPerty" + t + "\"/>" + Language.Content(pro.Name, CurrentLanguage.Code) + "</label>&nbsp;";
                }
            }
            return str;
        }
    }
}