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
    public partial class product_Edit_part : AdminAjaxBase
    {
        protected List<Lebi_ProPerty> pros;
        protected Lebi_Product model;
        protected List<KeyValue> ProPerty133;
        protected int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            tid = RequestTool.RequestInt("tid", 0);
            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_Product.GetModel(pid);
            if (model == null)
                model = new Lebi_Product();
            //Lebi_ProPerty_Type ptype = B_Lebi_ProPerty_Type.GetModel(tid);
            string property = EX_Product.ProductType_ProPertystr(tid,model.Supplier_id);
            if (property != "")
            {
                pros = B_Lebi_ProPerty.GetList("Type_id_ProPertyType in (132,133) and id in (" + property + ")", "Sort desc");
            }
            if (pros == null)
            {
                pros = new List<Lebi_ProPerty>();
            }
            ProPerty133 = Common.KeyValueToList(model.ProPerty133);
            if (tid == 0)
                tid = model.Pro_Type_id;
        }

        public string GetproList(Lebi_ProPerty pmodel)
        {
            string str = "<div class=\"input-group\">";
            List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid=" + pmodel.id + "", "Sort desc");
            if (pmodel.Type_id_ProPertyType == 132)
            {
                foreach (Lebi_ProPerty p in ps)
                {
                    string sel = "";
                    if (("," + model.ProPerty132 + ",").Contains("," + p.id + ","))
                        sel = "checked";
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"Property132" + p.id + "\" name=\"Property132\" value=\"" + p.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">";
                        if (p.ImageUrl != "")
                            str += "<img src=\"" + Image(p.ImageUrl, 16, 16) + "\">";
                        str += Language.Content(p.Name, CurrentLanguage.Code) + "</span></label>";
                    }
                    else
                    {
                        str += " <label><input type=\"checkbox\" name=\"Property132\" shop=\"true\" " + sel + " value=\"" + p.id + "\"" + " />";
                        if (p.ImageUrl != "")
                            str += "<img src=\"" + Image(p.ImageUrl, 16, 16) + "\">";
                        str += Language.Content(p.Name, CurrentLanguage.Code) + "</label>";
                    }

                }
            }
            else if (pmodel.Type_id_ProPertyType == 133)
            {
                List<Lebi_Language_Code> langs = Language.Languages();
                KeyValue kv;
                try
                {
                    kv = (from m in ProPerty133
                          where m.K == pmodel.id.ToString()
                          select m).ToList().FirstOrDefault();
                }
                catch
                {
                    kv = new KeyValue();
                }
                if (kv == null)
                    kv = new KeyValue();
                foreach (Lebi_Language_Code lang in langs)
                {
                    string val = "";
                    if (kv != null && kv.V != null)
                        val = kv.V;
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                              
                        str += "<div class=\"input-group-prepend\"><span class=\"input-group-text\">" + lang.Code + "</span></div><input type=\"text\" name=\"Property133_" + pmodel.id + lang.Code + "\" shop=\"true\" value=\"" + Language.Content(val, lang.Code) + "\"" + " class=\"form-control form-contorl-sm m-r-20\" style=\"width:70px\"  /> ";
                    }
                    else
                    {
                        str += lang.Code + " <input type=\"text\" name=\"Property133_" + pmodel.id + lang.Code + "\" shop=\"true\" value=\"" + Language.Content(val, lang.Code) + "\"" + " class=\"input\"  /> ";
                    }
                }


            }
            else if (pmodel.Type_id_ProPertyType == 134)
            {
                foreach (Lebi_ProPerty p in ps)
                {
                    string sel = "";
                    if (("," + model.ProPerty134 + ",").Contains("," + p.id + ","))
                        sel = "checked";
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"Property134" + p.id + "\" name=\"Property134\" value=\"" + p.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">";
                        if (p.ImageUrl != "")
                            str += "<img src=\"" + Image(p.ImageUrl, 16, 16) + "\">";
                        str += Language.Content(p.Name, CurrentLanguage.Code) + "</span></label>";
                    }
                    else
                    {
                        str += " <label><input type=\"checkbox\" name=\"Property134\" shop=\"true\" " + sel + " value=\"" + p.id + "\"" + " />";
                        if (p.ImageUrl != "")
                            str += "<img src=\"" + Image(p.ImageUrl, 16, 16) + "\">";
                        str += Language.Content(p.Name, CurrentLanguage.Code) + "</label>";
                    }

                }
            }
            str += "</div>";
            return str;
        }

        public string Getpro131List(int pid)
        {
            string str = "<div class=\"input-group\">";
            List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid=" + pid + "", "Sort desc");
            foreach (Lebi_ProPerty p in ps)
            {
                string sel = "";
                if (("," + model.ProPerty131 + ",").Contains("," + p.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"Property131" + p.id + "\" name=\"Property131\" value=\"" + p.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">";
                    if (p.Code != "")
                        str += "(" + p.Code + ")";
                    str += "</span></label>";
                }
                else
                {
                    str += " <label><input type=\"checkbox\" name=\"Property131\" shop=\"true\" " + sel + " value=\"" + p.id + "\"" + " />";
                    if (p.ImageUrl != "")
                        str += "<img src=\"" + Image(p.ImageUrl, 16, 16) + "\">";
                    str += Language.Content(p.Name, CurrentLanguage.Code);
                    if (p.Code != "")
                        str += "(" + p.Code + ")";
                    str += "</label>";
                }
            }
            str += "</div>";
            return str;
        }

        /// <summary>
        /// 属性规格选择
        /// </summary>
        /// <param name="selid"></param>
        /// <returns></returns>
        public string Property(string selid, int t)
        {
            string str = "<div class=\"input-group\">";
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
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"Property" + t + "" + pro.id + "\" name=\"Property" + t + "\" value=\"" + pro.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">";
                    str += Language.Content(pro.Name, CurrentLanguage.Code);
                    if (pro.Code != "")
                        str += "(" + pro.Code + ")";
                    str += "</span></label>";
                }
                else
                {
                    str += "<label><input " + sel + " type=\"checkbox\" value=\"" + pro.id + "\" shop=\"true\" name=\"ProPerty" + t + "\"/>";
                    if (pro.ImageUrl != "")
                        str += "<img src=\"" + Image(pro.ImageUrl, 16, 16) + "\">";
                    str += Language.Content(pro.Name, CurrentLanguage.Code);
                    if (pro.Code != "")
                        str += "(" + pro.Code + ")";
                    str += "</label>&nbsp;";
                }
            }
            str += "</div>";
            return str;
        }
    }
}