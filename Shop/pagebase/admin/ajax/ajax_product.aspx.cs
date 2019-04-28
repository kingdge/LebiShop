using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Threading;
namespace Shop.Admin.Ajax
{
    public partial class Ajax_product : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }

        List<Lebi_Pro_Type> proCategorys = new List<Lebi_Pro_Type>();
        /// <summary>
        /// 生成商品分类选择框
        /// </summary>
        public void GetProductTypeList()
        {
            int id = RequestTool.RequestInt("id", 0);
            string classname = RequestTool.RequestString("classname");
            int Parentid = 0;
            if (proCategorys.Count == 0)
                proCategorys = B_Lebi_Pro_Type.GetList("", "Sort desc");
            List<Lebi_Pro_Type> models = proCategorys.FindAll(o => o.Parentid == id);
            Lebi_Pro_Type area = proCategorys.Find(o => o.id == id);
            if (models.Count == 0)
            {
                if (area == null)
                    Parentid = 0;
                else
                    Parentid = area.Parentid;
                models = proCategorys.FindAll(o => o.Parentid == Parentid);
            }
            else
            {
                Parentid = id;
            }
            string str = "<select id=\"Pro_Type_id\" name=\"Pro_Type_id\" class=\"" + classname + "\" shop=\"true\" onchange=\"SelectProductType('Pro_Type_id');\">";
            str += "<option value=\"0\" selected>" + Tag(" 请选择 ") + "</option>";
            foreach (Lebi_Pro_Type model in models)
            {
                bool show = true;
                if (model.Parentid == 0)
                {
                    if (!string.IsNullOrEmpty(EX_Admin.Project().Pro_Type_ids))
                    {
                        string Pro_Type_ids = "," + EX_Admin.Project().Pro_Type_ids + ",";
                        if (!Pro_Type_ids.Contains("," + model.id + ","))
                            show = false;
                    }
                }
                if (show) { 
                    if (id == model.id)
                        str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                    else
                        str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
                }
            }
            str += "</select>";
            str = CreateProductTypeSelect(Parentid, classname) + str;
            Response.Write(str);
        }

        private string CreateProductTypeSelect(int id, string classname)
        {
            if (proCategorys.Count == 0)
                proCategorys = B_Lebi_Pro_Type.GetList("", "Sort desc");
            string str = "<select id=\"ProductType_" + id + "\" name=\"ProductType_" + id + "\" class=\"" + classname + "\" shop=\"true\" onchange=\"SelectProductType('ProductType_" + id + "');\">";
            Lebi_Pro_Type area = proCategorys.Find(o => o.id == id);
            if (area == null)
                return "";
            List<Lebi_Pro_Type> models = proCategorys.FindAll(o => o.Parentid == area.Parentid);
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Pro_Type model in models)
            {
                bool show = true;
                if (model.Parentid == 0)
                {
                    if (!string.IsNullOrEmpty(EX_Admin.Project().Pro_Type_ids))
                    {
                        string Pro_Type_ids = "," + EX_Admin.Project().Pro_Type_ids + ",";
                        if (!Pro_Type_ids.Contains("," + model.id + ","))
                            show = false;
                    }
                }
                if (show)
                {
                    if (id == model.id)
                        str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                    else
                        str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
                }
            }
            str += "</select> ";
            str = CreateProductTypeSelect(area.Parentid, classname) + str;
            return str;
        }
        /// <summary>
        /// 编辑商品分类
        /// </summary>
        public void Class_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Pro_Type();
            }
            model = B_Lebi_Pro_Type.SafeBindForm(model);
            model.ProPerty131 = RequestTool.RequestSafeString("ProPerty131");
            model.ProPerty132 = RequestTool.RequestSafeString("ProPerty132");
            model.ProPerty133 = RequestTool.RequestSafeString("ProPerty133");
            model.ProPerty134 = RequestTool.RequestSafeString("ProPerty134");
            model.Name = Language.RequestSafeString("Name");
            model.Url = Language.RequestSafeString("Url");
            model.IsUrlrewrite = Language.RequestSafeString("IsUrlrewrite");
            model.SEO_Title = Language.RequestSafeString("SEO_Title");
            model.SEO_Keywords = Language.RequestSafeString("SEO_Keywords");
            model.SEO_Description = Language.RequestSafeString("SEO_Description");
            model.ImageUrl = RequestTool.RequestSafeString("ImageUrl");
            model.Site_ids = RequestTool.RequestSafeString("Site_ids");
            //model.Path = EX_Product.TypePath(model, "");
            if (EX_Product.PathIsOK(model, "") == false)
            {
                Response.Write("{\"msg\":\"" + Tag("上级分类设置错误") + "\"}");
                return;
            }
            if (model.Parentid > 0 && model.id == model.Parentid)
            {
                Response.Write("{\"msg\":\"" + Tag("上级分类设置错误") + "\"}");
                return;
            }
            if (addflag)
            {
                if (!EX_Admin.Power("pro_type_add", "添加商品分类"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Pro_Type.Add(model);
                model.id = B_Lebi_Pro_Type.GetMaxId();
                Log.Add("添加商品分类", "Pro_Type", model.id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("pro_type_edit", "编辑商品分类"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Pro_Type.Update(model);
                Log.Add("编辑商品分类", "Pro_Type", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            if (model.ImageUrl != "")
            {
                ImageHelper.LebiImagesUsed(model.ImageUrl, "producttype", model.id);
            }
            EX_Product.UpdateTypePath(model);//更新当前目录下的所有路径
            ShopCache.SetProductType();//更新缓存
            //====================================
            //处理静态页
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_AllProductCategories'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_OnePage(themepage);
            //处理静态规则
            ThemeUrl.CreateURLRewrite();
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        public void Type_Del()
        {
            if (!EX_Admin.Power("pro_type_del", "删除商品分类"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            List<Lebi_Pro_Type> ps = B_Lebi_Pro_Type.GetList("id in (lbsql{" + id + "})", "");
            foreach (var p in ps)
            {
                int count = B_Lebi_Product.Counts("Product_id = 0 and Pro_Type_id in (" + EX_Product.TypeIds(p.id) + ") and (IsDel!=1 or IsDel is null)");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("分类下包含商品，不能删除") + "\"}");
                    return;
                }
            }


            B_Lebi_Pro_Type.Delete("id in (lbsql{" + id + "})");
            ShopCache.SetProductType();//更新缓存
                                       //处理图片
            ImageHelper.LebiImagesDelete("producttype", id);


            Log.Add("删除商品分类", "Pro_Type", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 合并分类
        /// </summary>
        public void Class_Unite()
        {
            if (!EX_Admin.Power("pro_type_unite", "合并商品分类"))
            {
                AjaxNoPower();
                return;
            }
            int fromid = RequestTool.RequestInt("fromid", 0);
            int toid = RequestTool.RequestInt("toid", 0);
            if (fromid == toid)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Pro_Type fmodel = B_Lebi_Pro_Type.GetModel(fromid);
            Lebi_Pro_Type tmodel = B_Lebi_Pro_Type.GetModel(toid);
            if (fmodel == null || tmodel == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            int fcount = B_Lebi_Pro_Type.Counts("Parentid=" + fmodel.id + "");
            int tcount = B_Lebi_Pro_Type.Counts("Parentid=" + tmodel.id + "");
            if (fcount > 0 || tcount > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("不能对父级分类进行此操作") + "\"}");
                return;
            }
            try
            {
                string sql = "update [Lebi_Product] set Pro_Type_id=" + tmodel.id + " where Pro_Type_id=" + fmodel.id + "";
                Common.ExecuteSql(sql);
                B_Lebi_Pro_Type.Delete(fmodel.id);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return;
            }
            EX_Product.UpdateTypePath();//更新分类路径
            ShopCache.SetProductType();//更新缓存
            Log.Add("合并商品分类", "Pro_Type", toid.ToString(), CurrentAdmin, fromid.ToString() + "->" + toid.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量添加分类
        /// </summary>
        public void Class_MAdd()
        {
            if (!EX_Admin.Power("pro_type_add", "添加商品分类"))
            {
                AjaxNoPower();
                return;
            }
            int Parentid = RequestTool.RequestInt("Parentid", 0);
            int IsShow = RequestTool.RequestInt("IsShow", 0);
            int IsIndexShow = RequestTool.RequestInt("IsIndexShow", 0);
            List<Lebi_Language_Code> langs = Language.Languages();
            //定义DT
            DataTable dt = new DataTable();
            foreach (Lebi_Language_Code lang in langs)
            {
                dt.Columns.Add(lang.Code);
            }
            //向DT填充数据
            Lebi_Language_Code dlang = langs.FirstOrDefault();
            string rname = RequestTool.RequestSafeString("Name" + dlang.Code);
            rname = rname.Replace("\r\n", "\n");
            //Regex re = new Regex(@"\n*\n", RegexOptions.Singleline);
            //rname = re.Replace(rname, "\n");
            if (rname.LastIndexOf("\n") == rname.Length - 1)
                rname = rname.Remove(rname.Length - 1);
            string[] rs = rname.Split('\n');
            int Dlen = rs.Length;
            foreach (string r in rs)
            {
                DataRow dr = dt.NewRow();
                dr[dlang.Code] = r;
                dt.Rows.Add(dr);
            }

            foreach (Lebi_Language_Code lang in langs)
            {
                if (lang.id == dlang.id)
                    continue;
                rname = RequestTool.RequestSafeString("Name" + lang.Code);
                rname = rname.Replace("\r\n", "\n");
                //rname = re.Replace(rname, "\n");
                if (rname.LastIndexOf("\n") == rname.Length - 1)
                    rname = rname.Remove(rname.Length - 1);
                rs = rname.Split('\n');
                if (rs.Length != Dlen)
                {
                    Response.Write("{\"msg\":\"" + Tag("数据行数不一致，请检查") + "\"}");
                    return;
                }
                int j = 0;
                foreach (string r in rs)
                {
                    dt.Rows[j][lang.Code] = r;
                    j++;
                }

            }
            //插入数据库
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Lebi_Pro_Type model = new Lebi_Pro_Type();
                List<LanguageContent> list = new List<LanguageContent>();

                LanguageContent con = new LanguageContent();
                foreach (Lebi_Language_Code lang in langs)
                {
                    con = new LanguageContent();
                    con.L = lang.Code;
                    con.C = dt.Rows[i][lang.Code].ToString();
                    list.Add(con);
                }
                string json = Language.ToJson(list);
                model.Name = json;
                //model.SEO_Keywords = json;
                //model.SEO_Title = json;
                //model.SEO_Description = json;
                if (Parentid > 0 && Parentid == model.id)
                {
                    Parentid = 0;
                }
                model.Parentid = Parentid;
                model.IsShow = IsShow;
                model.IsIndexShow = IsIndexShow;
                model.ProPerty131 = RequestTool.RequestSafeString("ProPerty131");
                model.ProPerty132 = RequestTool.RequestSafeString("ProPerty132");
                model.ProPerty133 = RequestTool.RequestSafeString("ProPerty133");
                model.ProPerty134 = RequestTool.RequestSafeString("ProPerty134");
                model.Site_ids = RequestTool.RequestSafeString("Site_ids");
                B_Lebi_Pro_Type.Add(model);
                model.id = B_Lebi_Pro_Type.GetMaxId();
                EX_Product.UpdateTypePath(model);//更新分类路径
            }
            ShopCache.SetProductType();//更新缓存
            Log.Add("批量添加商品分类", "Pro_Type", "", CurrentAdmin, rname);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改类别站点
        /// </summary>
        public void Class_Site_Edit()
        {
            if (!EX_Admin.Power("pro_type_edit", "编辑商品分类"))
            {
                AjaxNoPower();
                return;
            }
            int DataType = RequestTool.RequestInt("DataType", 0);
            int UpdateType = RequestTool.RequestInt("UpdateType", 0);
            string ids = RequestTool.RequestSafeString("ids");
            if (DataType == 0)
            {
                if (ids == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请选择要修改的类别") + "\"}");
                    return;
                }
            }
            string where = "id in (lbsql{" + ids + "})";
            if (DataType == 1)
            {
                where = "1=1";
            }
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList(where, "");
            string Site_ids = RequestTool.RequestSafeString("Site_ids");
            foreach (Lebi_Pro_Type model in models)
            {
                if (UpdateType == 0)
                {
                    model.Site_ids = Site_ids;
                }
                else
                {
                    model.Site_ids += "," + Site_ids;
                }
                B_Lebi_Pro_Type.Update(model);
            }
            Log.Add("编辑商品分类", "Pro_Type", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品标签
        /// </summary>
        public void ProTag_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Pro_Tag model = B_Lebi_Pro_Tag.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Pro_Tag();
            }
            model = B_Lebi_Pro_Tag.SafeBindForm(model);
            model.Name = Language.RequestString("Name");
            if (addflag)
            {
                if (!EX_Admin.Power("pro_tag_add", "添加商品标签"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Pro_Tag.Add(model);
                id = B_Lebi_Pro_Tag.GetMaxId();
                Log.Add("添加商品标签", "Pro_Tag", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("pro_tag_edit", "编辑商品标签"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Pro_Tag.Update(model);
                Log.Add("编辑商品标签", "Pro_Tag", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 删除商品标签
        /// </summary>
        public void Tag_Del()
        {
            if (!EX_Admin.Power("pro_tag_del", "删除商品标签"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Pro_Tag.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除商品标签", "Pro_Tag", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品单位
        /// </summary>
        public void Unit_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Units model = B_Lebi_Units.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Units();
            }
            model = B_Lebi_Units.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            if (addflag)
            {
                if (!EX_Admin.Power("pro_units_add", "添加商品单位"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Units.Add(model);
                id = B_Lebi_Units.GetMaxId();
                Log.Add("添加商品单位", "Units", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("pro_units_edit", "编辑商品单位"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Units.Update(model);
                Log.Add("编辑商品单位", "Units", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 删除商品单位
        /// </summary>
        public void Unit_Del()
        {
            if (!EX_Admin.Power("pro_units_del", "删除商品单位"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Units.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除商品单位", "Units", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 保存商品通用描述
        /// </summary>
        public void ProDescription_Edit()
        {
            if (!EX_Admin.Power("prodesc_edit", "编辑商品通用描述"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ProDesc model = B_Lebi_ProDesc.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ProDesc();
            }
            model.Description = Language.RequestStringForUserEditor("Description");
            model = B_Lebi_ProDesc.SafeBindForm(model);
            if (addflag)
            {
                B_Lebi_ProDesc.Add(model);
            }
            else
            {
                B_Lebi_ProDesc.Update(model);
            }
            Log.Add("编辑商品通用描述", "ProDesc", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑品牌
        /// </summary>
        public void Brand_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Brand model = B_Lebi_Brand.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Brand();
            }
            model = B_Lebi_Brand.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            model.Description = Language.RequestStringForUserEditor("Description");
            model.SEO_Title = Language.RequestSafeString("SEO_Title");
            model.SEO_Keywords = Language.RequestSafeString("SEO_Keywords");
            model.SEO_Description = Language.RequestSafeString("SEO_Description");
            if (addflag)
            {
                if (!EX_Admin.Power("brand_add", "添加品牌"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Brand.Add(model);
                id = B_Lebi_Brand.GetMaxId();
                Log.Add("添加品牌", "Brand", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("brand_edit", "编辑品牌"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Brand.Update(model);
                Log.Add("编辑品牌", "Brand", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            ImageHelper.LebiImagesUsed(model.ImageUrl, "productbrand", id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品品牌
        /// </summary>
        public void Brand_Del()
        {
            if (!EX_Admin.Power("brand_del", "删除品牌"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Brand.Delete("id in (lbsql{" + id + "})");
            //处理图片
            ImageHelper.LebiImagesDelete("productbrand", id);
            Log.Add("删除品牌", "Brand", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑商品品牌
        /// </summary>
        public void Brands_Edit()
        {
            if (!EX_Admin.Power("brand_edit", "编辑品牌"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            List<Lebi_Brand> models = B_Lebi_Brand.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Brand model in models)
            {
                model.FirstLetter = RequestTool.RequestSafeString("FirstLetter" + model.id);
                model.Sort = RequestTool.RequestInt("Sort" + model.id, 0);
                model.IsRecommend = RequestTool.RequestInt("IsRecommend" + model.id, 0);
                B_Lebi_Brand.Update(model);
            }
            Log.Add("更新品牌", "Brand", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑属性
        /// </summary>
        public void Property_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            string TagName = RequestTool.RequestSafeString("Tag");
            Lebi_ProPerty model = B_Lebi_ProPerty.GetModel(id);
            if (model == null)
            {
                model = new Lebi_ProPerty();
            }
            B_Lebi_ProPerty.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            //model.Value = Language.RequestString("Value");

            if (model.id == 0)
            {
                if (!EX_Admin.Power("property_add", "添加属性规格"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ProPerty.Add(model);
                id = B_Lebi_ProPerty.GetMaxId();
                Log.Add("添加属性规格", "ProPerty", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("property_edit", "编辑属性规格"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ProPerty.Update(model);
                List<Lebi_ProPerty> sons = B_Lebi_ProPerty.GetList("parentid=" + model.id + "", "");
                foreach (Lebi_ProPerty son in sons)
                {
                    son.parentSort = model.Sort;
                    B_Lebi_ProPerty.Update(son);
                }
                Log.Add("编辑属性规格", "ProPerty", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            if (TagName != "")  //写入标签 by lebi.kingdge 20150722
            {
                Lebi_ProPerty_Tag tagmodel = B_Lebi_ProPerty_Tag.GetModel("Name = lbsql{'" + TagName + "'}");
                if (tagmodel == null)
                {
                    tagmodel = new Lebi_ProPerty_Tag();
                    tagmodel.Type_id_ProPertyType = model.Type_id_ProPertyType;
                    tagmodel.Supplier_id = 0;
                    tagmodel.Name = TagName;
                    tagmodel.Sort = 0;
                    B_Lebi_ProPerty_Tag.Add(tagmodel);
                }
            }
            ImageHelper.LebiImagesUsed(model.ImageUrl, "productproperty", id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除属性
        /// </summary>
        public void Property_Del()
        {
            if (!EX_Admin.Power("property_del", "删除属性规格"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请先选择") + "\"}");
                return;
            }
            List<Lebi_ProPerty> models = B_Lebi_ProPerty.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_ProPerty model in models)
            {
                if (model.parentid == 0)
                {
                    //删除子规格或规格值
                    List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid =" + model.id + "", "");
                    foreach (Lebi_ProPerty p in ps)
                    {
                        if (p.ImageUrl != "")
                        {
                            ImageHelper.LebiImagesDelete("productproperty", p.id);
                        }
                        B_Lebi_ProPerty.Delete(p.id);
                    }
                }
                B_Lebi_ProPerty.Delete(model.id);
            }
            Log.Add("删除属性规格", "ProPerty", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑属性标签
        /// </summary>
        public void Property_Tag_Edit()
        {
            if (!EX_Admin.Power("property_edit", "编辑属性规格"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ProPerty_Tag model = B_Lebi_ProPerty_Tag.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ProPerty_Tag();
            }
            model = B_Lebi_ProPerty_Tag.SafeBindForm(model);
            if (addflag)
            {
                B_Lebi_ProPerty_Tag.Add(model);
                id = B_Lebi_ProPerty_Tag.GetMaxId();
                Log.Add("添加属性标签", "ProPerty_Tag", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                B_Lebi_ProPerty_Tag.Update(model);
                Log.Add("编辑属性标签", "ProPerty_Tag", id.ToString(), CurrentAdmin, model.Name);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 删除属性标签
        /// </summary>
        public void Property_Tag_Del()
        {
            if (!EX_Admin.Power("property_edit", "编辑属性规格"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            List<Lebi_ProPerty_Tag> models = B_Lebi_ProPerty_Tag.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_ProPerty_Tag model in models)
            {
                string sql = "update [Lebi_ProPerty] set Tag = '' where Supplier_id = " + model.Supplier_id + " and Type_id_ProPertyType = " + model.Type_id_ProPertyType + " and Tag='" + model.Name + "'";
                Common.ExecuteSql(sql);
                B_Lebi_ProPerty_Tag.Delete(model.id);
            }
            Log.Add("删除属性标签", "ProPerty_Tag", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改属性关联分类
        /// </summary>
        public void Property_Category_Edit()
        {
            if (!EX_Admin.Power("property_edit", "编辑属性规格"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            string Pro_Type_id = RequestTool.RequestSafeString("Pro_Type_id");
            int tid = RequestTool.RequestInt("tid", 133);
            int UpdateType = RequestTool.RequestInt("UpdateType", 0);
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请先选择") + "\"}");
                return;
            }
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("id in (lbsql{" + Pro_Type_id + "})", "");
            foreach (Lebi_Pro_Type model in models)
            {
                string ProPerty = "";
                switch (tid)
                {
                    case 131:
                        ProPerty = "," + model.ProPerty131 + ",";
                        break;
                    case 132:
                        ProPerty = "," + model.ProPerty132 + ",";
                        break;
                    case 133:
                        ProPerty = "," + model.ProPerty133 + ",";
                        break;
                    case 134:
                        ProPerty = "," + model.ProPerty134 + ",";
                        break;
                }
                string[] arr_ids = ids.Split(',');
                for (int i = 0; i < arr_ids.Count(); i++)
                {
                    ProPerty = ProPerty.Replace("," + arr_ids[i] + ",", ",");
                }
                if (UpdateType == 1)
                {
                    if (ProPerty.LastIndexOf(",") > -1)
                    {
                        ProPerty += ids;
                    }
                    else
                    {
                        ProPerty += "," + ids;
                    }
                }
                if (UpdateType == 2)
                {
                    ProPerty = ids;
                }
                if (ProPerty.IndexOf(",") > -1)
                {
                    ProPerty = ProPerty.Trim(',');
                }
                switch (tid)
                {
                    case 131:
                        model.ProPerty131 = ProPerty;
                        break;
                    case 132:
                        model.ProPerty132 = ProPerty;
                        break;
                    case 133:
                        model.ProPerty133 = ProPerty;
                        break;
                    case 134:
                        model.ProPerty134 = ProPerty;
                        break;
                }
                B_Lebi_Pro_Type.Update(model);
            }
            Log.Add("更新属性关联分类", "Property", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品信息
        /// </summary>
        public void Product_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            //int randnum = RequestTool.RequestInt("randnum", 0);
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Product();
            }
            //检查商品编号，不能重复
            string Number = RequestTool.RequestSafeString("Number");
            if (Number == "")
            {
                Number = EX_Product.RandomProductNumber(model.id);
            }
            int count = B_Lebi_Product.Counts("Number=lbsql{'" + Number + "'} and id!=" + model.id + "");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("商品编码已经存在") + "\"}");
                return;
            }
            B_Lebi_Product.BindForm(model);
            model.Name = Language.RequestSafeString("Name");
            model.Introduction = Language.RequestStringForUserEditor("Introduction");
            model.Description = Language.RequestStringForUserEditor("Description");
            model.Specification = Language.RequestStringForUserEditor("Specification");
            model.Packing = Language.RequestStringForUserEditor("Packing");
            model.SEO_Title = Language.RequestSafeString("SEO_Title");
            model.SEO_Keywords = Language.RequestSafeString("SEO_Keywords");
            model.SEO_Description = Language.RequestSafeString("SEO_Description");
            model.Service = Language.RequestStringForUserEditor("Service");
            model.MobileDescription = Language.RequestString("MobileDescription");
            model.IsSupplierTransport = RequestTool.RequestInt("IsSupplierTransport", 0);
            model.Supplier_id = RequestTool.RequestInt("Supplier_id", 0);
            model.Type_id_ProductType = RequestTool.RequestInt("Type_id_ProductType", 0);
            model.Time_Edit = DateTime.Now;
            model.Pro_Tag_id = RequestTool.RequestSafeString("Pro_Tag_id");
            model.Pro_Type_id_other = RequestTool.RequestSafeString("Pro_Type_id_other");
            model.Number = Number;
            model.IsCombo = RequestTool.RequestInt("IsCombo");
            model.Location = RequestTool.RequestSafeString("Location");
            //====================================================
            //更新自定义文字属性
            List<Lebi_ProPerty> pros133;
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id, model.Supplier_id);
            pros133 = B_Lebi_ProPerty.GetList("Type_id_ProPertyType =133 and id in (" + property + ")", "Sort desc");
            if (pros133 == null)
            {
                pros133 = new List<Lebi_ProPerty>();
            }
            List<KeyValue> kvs = new List<KeyValue>();
            foreach (Lebi_ProPerty pro in pros133)
            {
                KeyValue kv = new KeyValue();
                kv.V = Language.RequestSafeString("Property133_" + pro.id);
                kv.K = pro.id.ToString();
                kvs.Add(kv);
            }
            model.ProPerty133 = Common.KeyValueToJson(kvs);
            model.ProPerty134 = RequestTool.RequestSafeString("ProPerty134");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (Shop.LebiAPI.Service.Instanse.Check("plugin_product_price"))
            {
                //阶梯价格ProductStepPrice
                string step_count = RequestTool.RequestString("step_count");
                string step_price = RequestTool.RequestString("step_price");
                if (step_count == "")
                {
                    model.StepPrice = "";
                }
                else
                {
                    List<ProductStepPrice> sprices = new List<ProductStepPrice>();
                    string[] counts = step_count.Split(',');
                    string[] prices = step_price.Split(',');
                    for (int i = 0; i < counts.Length; i++)
                    {
                        int s_count = 0;
                        decimal s_price = 0;
                        int.TryParse(counts[i], out s_count);
                        decimal.TryParse(prices[i], out s_price);
                        ProductStepPrice sprice = new ProductStepPrice();
                        sprice.Count = s_count;
                        sprice.Price = s_price;
                        sprices.Add(sprice);

                    }

                    if (sprices.Count > 0)
                    {
                        sprices = sprices.OrderByDescending(a => a.Count).ToList();
                        model.StepPrice = jss.Serialize(sprices);
                    }
                }
                //更新会员分组价格
                List<ProductUserLevelPrice> ulprices = new List<ProductUserLevelPrice>();
                List<Lebi_UserLevel> userlevels = B_Lebi_UserLevel.GetList("", "Grade asc");
                foreach (Lebi_UserLevel ul in userlevels)
                {
                    decimal p = RequestTool.RequestDecimal("userlevelprice" + ul.id);
                    if (p > 0)
                    {
                        ProductUserLevelPrice ulprice = new ProductUserLevelPrice();
                        ulprice.Price = p;
                        ulprice.UserLevel_id = ul.id;
                        ulprices.Add(ulprice);
                    }
                }
                if (ulprices.Count > 0)
                {
                    model.UserLevelPrice = jss.Serialize(ulprices);
                }
                //更新会员分组起订量
                List<ProductUserLevelCount> ulcounts = new List<ProductUserLevelCount>();
                foreach (Lebi_UserLevel ul in userlevels)
                {
                    int c = RequestTool.RequestInt("userlevelcount" + ul.id);
                    if (c > 1)
                    {
                        ProductUserLevelCount ulcount = new ProductUserLevelCount();
                        ulcount.Count = c;
                        ulcount.UserLevel_id = ul.id;
                        ulcounts.Add(ulcount);
                    }
                }
                if (ulcounts.Count > 0)
                {
                    model.UserLevelCount = jss.Serialize(ulcounts);
                }
            }
            //UpdateUserLevelPrice
            //更新自定义文字属性结束
            //====================================================
            if (model.id == 0)
            {
                //判断系统授权情况，限制添加
                //int top = 100;
                //if (Shop.LebiAPI.Service.Instanse.Check("zengqiang"))
                //    top = 0;
                //else if (Shop.LebiAPI.Service.Instanse.Check("biaozhun"))
                //    top = 500;
                //if (top > 0)
                //{
                //    int count = B_Lebi_Product.Counts("Product_id=0") + 1;
                //    if (count > top)
                //    {
                //        Response.Write("{\"msg\":\"" + Tag("数据量已达到上限,请升级您的授权") + "\"}");
                //        return;
                //    }
                //}
                if (!EX_Admin.Power("product_add", "添加商品"))
                {
                    AjaxNoPower();
                    return;
                }
                model.Type_id_ProductStatus = 101;//默认状态：下架
                B_Lebi_Product.Add(model);
                id = B_Lebi_Product.GetMaxId();

                Log.Add("添加商品", "Product", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("product_edit", "编辑商品"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Product.Update(model);
                //更新子商品信息
                List<Lebi_Product> sons = B_Lebi_Product.GetList("Product_id=" + model.id, "");
                int UpdateBrand_id = RequestTool.RequestInt("UpdateBrand_id", 0);
                int UpdatePrice_Market = RequestTool.RequestInt("UpdatePrice_Market", 0);
                int UpdatePrice_Cost = RequestTool.RequestInt("UpdatePrice_Cost", 0);
                int UpdatePrice = RequestTool.RequestInt("UpdatePrice", 0);
                int UpdateCount_StockCaution = RequestTool.RequestInt("UpdateCount_StockCaution", 0);
                int UpdateCount_Sales = RequestTool.RequestInt("UpdateCount_Sales", 0);
                int UpdateCount_Views = RequestTool.RequestInt("UpdateCount_Views", 0);
                int UpdatePackageRate = RequestTool.RequestInt("UpdatePackageRate", 0);
                int UpdateWeight = RequestTool.RequestInt("UpdateWeight", 0);
                int UpdateNetWeight = RequestTool.RequestInt("UpdateNetWeight", 0);
                int UpdateVolume = RequestTool.RequestInt("UpdateVolume", 0);
                int UpdateType_id_ProductType = RequestTool.RequestInt("UpdateType_id_ProductType", 0);
                int UpdateProPerty133 = RequestTool.RequestInt("UpdateProPerty133", 0);
                int UpdateDescription = RequestTool.RequestInt("UpdateDescription", 0);
                int UpdateSpecification = RequestTool.RequestInt("UpdateSpecification", 0);
                int UpdateMobileDescription = RequestTool.RequestInt("UpdateMobileDescription", 0);
                int UpdateSEO = RequestTool.RequestInt("UpdateSEO", 0);
                int UpdateUnits_id = RequestTool.RequestInt("UpdateUnits_id", 0);
                int UpdateTags = RequestTool.RequestInt("UpdateTags", 0);
                int UpdateStepPrice = RequestTool.RequestInt("UpdateStepPrice", 0);
                int UpdateUserLevelPrice = RequestTool.RequestInt("UpdateUserLevelPrice", 0);
                int UpdateUserLevelCount = RequestTool.RequestInt("UpdateUserLevelCount", 0);
                int UpdateName = RequestTool.RequestInt("UpdateName", 0);
                int UpdateIntroduction = RequestTool.RequestInt("UpdateIntroduction", 0);
                int UpdateLocation = RequestTool.RequestInt("UpdateLocation", 0);
                foreach (Lebi_Product son in sons)
                {
                    //判断是否同步到子商品
                    if (UpdateName == 1)
                        son.Name = model.Name;
                    if (UpdateIntroduction == 1)
                        son.Introduction = model.Introduction;
                    if (UpdateDescription == 1)
                        son.Description = model.Description;
                    if (UpdateSpecification == 1)
                        son.Specification = model.Specification;
                    if (UpdateMobileDescription == 1)
                        son.MobileDescription = model.MobileDescription;
                    if (UpdateSEO == 1)
                    {
                        son.SEO_Title = model.SEO_Title;
                        son.SEO_Keywords = model.SEO_Keywords;
                        son.SEO_Description = model.SEO_Description;
                    }
                    if (UpdateUnits_id == 1)
                        son.Units_id = model.Units_id;
                    if (UpdateTags == 1)
                        son.Tags = model.Tags;
                    if (UpdateBrand_id == 1)
                        son.Brand_id = model.Brand_id;

                    if (UpdatePrice_Market == 1)
                        son.Price_Market = model.Price_Market;
                    if (UpdatePrice_Cost == 1)
                        son.Price_Cost = model.Price_Cost;
                    if (UpdatePrice == 1)
                        son.Price = model.Price;
                    if (UpdateCount_StockCaution == 1)
                        son.Count_StockCaution = model.Count_StockCaution;
                    if (UpdateCount_Sales == 1)
                    {
                        son.Count_Sales = model.Count_Sales;
                        son.Count_Sales_Show = model.Count_Sales_Show;
                    }
                    if (UpdateCount_Views == 1)
                    {
                        son.Count_Views = model.Count_Views;
                        son.Count_Views_Show = model.Count_Views_Show;
                    }
                    if (UpdatePackageRate == 1)
                        son.PackageRate = model.PackageRate;
                    if (UpdateWeight == 1)
                        son.Weight = model.Weight;
                    if (UpdateNetWeight == 1)
                        son.NetWeight = model.NetWeight;
                    if (UpdateVolume == 1)
                    {
                        son.VolumeL = model.VolumeL;
                        son.VolumeW = model.VolumeW;
                        son.VolumeH = model.VolumeH;
                    }
                    if (UpdateType_id_ProductType == 1)
                    {
                        son.Type_id_ProductType = model.Type_id_ProductType;
                        son.Count_Limit = model.Count_Limit;
                        son.Price_Sale = model.Price_Sale;
                        son.Time_Start = model.Time_Start;
                        son.Time_Expired = model.Time_Expired;
                    }
                    if (UpdateProPerty133 == 1)
                    {
                        son.ProPerty133 = model.ProPerty133;
                        son.ProPerty132 = model.ProPerty132;
                        son.ProPerty134 = model.ProPerty134;
                    }
                    if (UpdateStepPrice == 1)
                    {
                        son.StepPrice = model.StepPrice;
                    }
                    if (UpdateUserLevelPrice == 1)
                    {
                        son.UserLevelPrice = model.UserLevelPrice;
                    }
                    if (UpdateUserLevelCount == 1)
                    {
                        son.UserLevelCount = model.UserLevelCount;
                    }
                    if (UpdateLocation == 1)
                    {
                        son.Location = model.Location;
                    }
                    son.Supplier_id = RequestTool.RequestInt("Supplier_id", 0);
                    son.IsSupplierTransport = RequestTool.RequestInt("IsSupplierTransport", 0);
                    son.Pro_Type_id = model.Pro_Type_id;
                    son.Pro_Type_id_other = model.Pro_Type_id_other;
                    B_Lebi_Product.Update(son);
                }

                Log.Add("编辑商品", "Product", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }

            if (model.IsCombo == 1)
            {
                string combo_Number = RequestTool.RequestString("combo_Number");
                string combo_Count = RequestTool.RequestString("combo_Count");
                string[] nums = combo_Number.Split(',');
                string[] counts = combo_Count.Split(',');
                B_Lebi_Product_Combo.Delete("Product_id=" + model.id + "");
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] == model.Number)
                    {
                        Response.Write("{\"msg\":\"" + Tag("组合商品出错") + "\",\"id\":\"" + id + "\"}");
                        return;
                    }
                    Lebi_Product pro = B_Lebi_Product.GetModel("Number='" + nums[i] + "'");
                    if (pro != null)
                    {
                        Lebi_Product_Combo co = B_Lebi_Product_Combo.GetModel("Product_id=" + model.id + " and Product_id_son=" + pro.id + "");
                        if (co == null)
                        {
                            co = new Lebi_Product_Combo();
                            co.Product_id = model.id;
                            co.Product_id_son = pro.id;
                            co.Count = 1;

                            int c = 0;
                            int.TryParse(counts[i], out c);
                            co.Count = c == 0 ? 1 : c;

                            B_Lebi_Product_Combo.Add(co);
                        }

                    }
                }


            }

            //处理图片
            ImageHelper.LebiImagesUsed(model.ImageOriginal + "@" + model.Images, "Product", id);
            //处理静态页面
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_Product(model, themepage);


            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 编辑子商品
        /// </summary>
        public void SubProduct_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            //检查商品编号，不能重复
            string Number = RequestTool.RequestSafeString("Number");
            int count = B_Lebi_Product.Counts("Number=lbsql{'" + Number + "'} and id!=" + model.id + " and Product_id=0");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("商品编码已经存在") + "\"}");
                return;
            }
            B_Lebi_Product.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            model.Description = Language.RequestStringForUserEditor("Description");
            model.Specification = Language.RequestStringForUserEditor("Specification");
            model.MobileDescription = Language.RequestStringForUserEditor("MobileDescription");
            model.Time_Edit = DateTime.Now;
            //====================================================
            //更新自定义文字属性
            List<Lebi_ProPerty> pros;
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id);
            pros = B_Lebi_ProPerty.GetList("Type_id_ProPertyType =133 and id in (" + property + ")", "Sort desc");
            if (pros == null)
            {
                pros = new List<Lebi_ProPerty>();
            }
            List<KeyValue> kvs = new List<KeyValue>();
            foreach (Lebi_ProPerty pro in pros)
            {
                KeyValue kv = new KeyValue();
                kv.V = Language.RequestSafeString("Property133_" + pro.id);
                kv.K = pro.id.ToString();
                kvs.Add(kv);

            }
            model.ProPerty133 = Common.KeyValueToJson(kvs);
            //更新自定义文字属性结束
            //====================================================
            if (Shop.LebiAPI.Service.Instanse.Check("plugin_product_price"))
            {
                //阶梯价格ProductStepPrice
                string step_count = RequestTool.RequestString("step_count");
                string step_price = RequestTool.RequestString("step_price");
                if (step_count == "")
                {
                    model.StepPrice = "";
                }
                else
                {
                    List<ProductStepPrice> sprices = new List<ProductStepPrice>();
                    string[] counts = step_count.Split(',');
                    string[] prices = step_price.Split(',');
                    for (int i = 0; i < counts.Length; i++)
                    {
                        int s_count = 0;
                        decimal s_price = 0;
                        int.TryParse(counts[i], out s_count);
                        decimal.TryParse(prices[i], out s_price);
                        ProductStepPrice sprice = new ProductStepPrice();
                        sprice.Count = s_count;
                        sprice.Price = s_price;
                        sprices.Add(sprice);

                    }
                    if (sprices.Count > 0)
                    {
                        sprices = sprices.OrderByDescending(a => a.Count).ToList();
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        model.StepPrice = jss.Serialize(sprices);
                    }
                }
                //更新会员分组价格
                List<ProductUserLevelPrice> ulprices = new List<ProductUserLevelPrice>();
                List<Lebi_UserLevel> userlevels = B_Lebi_UserLevel.GetList("", "Grade asc");
                foreach (Lebi_UserLevel ul in userlevels)
                {
                    decimal p = RequestTool.RequestDecimal("userlevelprice" + ul.id);
                    if (p > 0)
                    {
                        ProductUserLevelPrice ulprice = new ProductUserLevelPrice();
                        ulprice.Price = p;
                        ulprice.UserLevel_id = ul.id;
                        ulprices.Add(ulprice);
                    }
                }
                if (ulprices.Count > 0)
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    model.UserLevelPrice = jss.Serialize(ulprices);
                }
                //更新会员分组起订量
                List<ProductUserLevelCount> ulcounts = new List<ProductUserLevelCount>();
                foreach (Lebi_UserLevel ul in userlevels)
                {
                    int c = RequestTool.RequestInt("userlevelcount" + ul.id);
                    if (c > 1)
                    {
                        ProductUserLevelCount ulcount = new ProductUserLevelCount();
                        ulcount.Count = c;
                        ulcount.UserLevel_id = ul.id;
                        ulcounts.Add(ulcount);
                    }
                }
                if (ulcounts.Count > 0)
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    model.UserLevelCount = jss.Serialize(ulcounts);
                }
            }

            B_Lebi_Product.Update(model);
            if (model.IsCombo == 1)
            {
                string combo_Number = RequestTool.RequestString("combo_Number");
                string combo_Count = RequestTool.RequestString("combo_Count");
                string[] nums = combo_Number.Split(',');
                string[] counts = combo_Count.Split(',');
                B_Lebi_Product_Combo.Delete("Product_id=" + model.id + "");
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] == model.Number)
                    {
                        Response.Write("{\"msg\":\"" + Tag("组合商品出错") + "\",\"id\":\"" + id + "\"}");
                        return;
                    }
                    Lebi_Product pro = B_Lebi_Product.GetModel("Number='" + nums[i] + "'");
                    if (pro != null)
                    {
                        Lebi_Product_Combo co = B_Lebi_Product_Combo.GetModel("Product_id=" + model.id + " and Product_id_son=" + pro.id + "");
                        if (co == null)
                        {
                            co = new Lebi_Product_Combo();
                            co.Product_id = model.id;
                            co.Product_id_son = pro.id;
                            co.Count = 1;

                            int c = 0;
                            int.TryParse(counts[i], out c);
                            co.Count = c == 0 ? 1 : c;

                            B_Lebi_Product_Combo.Add(co);
                        }

                    }
                }


            }
            Log.Add("编辑商品", "Product", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            //处理图片
            ImageHelper.LebiImagesUsed(model.ImageOriginal + "@" + model.Images, "Product", id);
            //处理静态页面
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_Product(model, themepage);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 编辑商品名称
        /// </summary>
        public void Product_Name_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Product.SafeBindForm(model);
            model.Name = Language.RequestString("Name");
            B_Lebi_Product.Update(model);
            //处理静态页面
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_Product(model, themepage);
            Log.Add("编辑商品名称", "Product", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品内部备注
        /// </summary>
        public void Product_Remark_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.Units_id = RequestTool.RequestInt("Units_id", 0);
            model.Remarks = RequestTool.RequestSafeString("Sub_Remarks");
            B_Lebi_Product.Update(model);
            Log.Add("编辑商品内部备注", "Product", id.ToString(), CurrentAdmin, RequestTool.RequestSafeString("Sub_Remarks"));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        public void Product_Del()
        {
            if (!EX_Admin.Power("product_del", "删除商品"))
            {
                AjaxNoPower();
                return;
            }
            int father = RequestTool.RequestInt("father", 0);
            string id = "";
            if (father == 1)
            {
                id = RequestTool.RequestString("productid");
            }
            else
            {
                id = RequestTool.RequestString("sonproductid");
            }
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }

            List<Lebi_Product> pros = B_Lebi_Product.GetList("Product_id in (lbsql{" + id + "})", "");
            foreach (Lebi_Product pro in pros)
            {
                id += "," + pro.id;
            }
            if (RequestTool.GetConfigKey("IsDelFalse").ToLower() == "true")
            {
                List<Lebi_Product> ps = B_Lebi_Product.GetList("id in (lbsql{" + id + "})", "");
                foreach (var p in ps)
                {
                    if (p.IsDel == 1)
                    {
                        B_Lebi_Product.Delete(p.id);
                        ImageHelper.LebiImagesDelete("product", p.id);
                    }
                    else
                    {
                        p.IsDel = 1;
                        B_Lebi_Product.Update(p);
                    }
                }
            }
            else
            {
                B_Lebi_Product.Delete("id in (lbsql{" + id + "})");
                //删除图片
                ImageHelper.LebiImagesDelete("product", id);
            }
            Log.Add("删除商品", "Product", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 恢复商品
        /// </summary>
        public void Product_Recovery()
        {
            if (!EX_Admin.Power("product_del", "删除商品"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("productid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                return;
            }
            List<Lebi_Product> pros = B_Lebi_Product.GetList("Product_id in (lbsql{" + id + "})", "");
            foreach (Lebi_Product pro in pros)
            {
                id += "," + pro.id;
            }
            List<Lebi_Product> ps = B_Lebi_Product.GetList("id in (lbsql{" + id + "})", "");
            foreach (var p in ps)
            {
                p.IsDel = 0;
                B_Lebi_Product.Update(p);
            }
            Log.Add("恢复商品", "Product", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 更新商品上架状态
        /// </summary>
        public void Product_Status()
        {

            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Status = RequestTool.RequestInt("Status", 101);
            if (Status == 101)
            {
                Status = 100;
            }
            else if (Status == 100)
            {
                Status = 101;
            }
            else
            {
                Status = 101;
            }
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.Type_id_ProductStatus = Status;
            B_Lebi_Product.Update(model);
            if (model.Product_id == 0)
            {
                //<-{同步更新子商品状态 by lebi.kingdge 2015-02-13
                string sql = "update [Lebi_Product] set Type_id_ProductStatus=" + Status + " where Product_id=" + id + " and Product_id<>0";
                Common.ExecuteSql(sql);
                //}->
            }
            Log.Add("编辑商品上架状态", "Product", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 图片管理
        /// </summary>
        public void ProductImages()
        {
            string images = RequestTool.RequestString("images");
            images = images.Replace("@@", "@");
            string[] arr = images.Split('@');
            string str = "<table width=\"100%\"><tr>";
            int i = 0;
            foreach (string image in arr)
            {
                if (image != "")
                {
                    str += "<td><image src=\"" + image.Replace("$", "small") + "\" width=\"50\" /><div>删除</div></td>";
                    i++;
                }
                if (i % 8 == 0)
                    str += "</tr><tr>";
            }

            str += "</tr></table>";
            Response.Write(str);

        }
        /// <summary>
        /// 按照规格生成同款式商品
        /// </summary>
        public void CreateProductGuiGe()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int pid = RequestTool.RequestInt("pid", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            //int randnum = RequestTool.RequestInt("randnum", 0);
            //if (pid == 0 || (pid > 0 && randnum > 0))
            //    pid = randnum;
            //int Pro_Type_id = RequestTool.RequestInt("Pro_Type_id", 0);
            string ggs = RequestTool.RequestString("ggs");
            Lebi_Product model = B_Lebi_Product.GetModel(pid);
            if (model == null)
            {
                model = new Lebi_Product();
                model.Product_id = pid;
                //Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                //return;
            }
            if (tid == 0)
            {
                tid = model.Pro_Type_id;
            }
            model.ProPerty131 = ggs;
            model.Pro_Type_id = tid;
            model.ProPertyMain = RequestTool.RequestInt("ProPertyMain", 0);
            B_Lebi_Product.Update(model);
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id, model.Supplier_id);
            List<Lebi_ProPerty> pros = B_Lebi_ProPerty.GetList("id in (" + property + ") and Type_id_ProPertyType=131", "Sort desc");//父亲规格
            int top = pros.Count;
            //生成
            //0,1
            //0,2,3
            //0
            //0,4,5
            //格式的数组
            string arrstr = "";
            foreach (Lebi_ProPerty pro in pros)
            {
                string v = "";
                List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid =" + pro.id + " ", "Sort desc");
                foreach (Lebi_ProPerty p in ps)
                {
                    if (("," + ggs + ",").Contains("," + p.id + ","))
                    {
                        if (v == "")
                            v = p.id.ToString();
                        else
                            v += "," + p.id;
                    }
                }
                if (v == "")
                    continue;
                if (arrstr == "")
                    arrstr = v;
                else
                    arrstr += "$" + v;

            }
            if (arrstr == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择规格") + "\"}");
                return;
            }
            string[] arr = arrstr.Split('$');
            //按数字组合数组
            string[] tt = arr[0].Split(',');
            for (int j = 0; j < tt.Length; j++)
            {
                string instr = tt[j];
                if (arr.Length > 1)
                    CreatePropertyKey(arr, 1, instr, model, j);
                else
                    Create131Product(model, instr, j);
            }
            Log.Add("生成商品规格", "Product", tid.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }

        public void CreatePropertyKey(string[] arr, int i, string instr, Lebi_Product pro, int loop)
        {
            string str = instr;
            string[] tt = arr[i].Split(',');
            for (int j = 0; j < tt.Length; j++)
            {
                str += "," + tt[j];
                if (i + 1 < arr.Length)
                {
                    CreatePropertyKey(arr, i + 1, str, pro, j);
                    str = instr;
                }
                else
                {
                    //Response.Write(str + "<br>");
                    Create131Product(pro, str, j);
                    str = instr;
                }

            }
        }
        /// <summary>
        /// 生成商品数据
        /// </summary>
        /// <param name="model">商品实体</param>
        /// <param name="id131">规格的ID</param>
        public void Create131Product(Lebi_Product model, string id131, int loop)
        {
            //if (("," + id131 + ",").Contains(",0,"))
            //{
            //排除所有空值
            //    return;
            //}
            //检查是否已生成

            int count = B_Lebi_Product.Counts("Product_id=" + model.id + " and Property131=lbsql{'" + id131 + "'}");
            if (count > 0)
                return;
            List<Lebi_ProPerty> pps = B_Lebi_ProPerty.GetList("id in (lbsql{" + id131 + "})", "parentSort desc");
            string ggstr = "";
            foreach (Lebi_ProPerty pp in pps)
            {
                ggstr += pp.Code;
            }
            Lebi_Product pro = new Lebi_Product();
            pro.Product_id = model.id;
            //pro.Code = model.Code;
            pro.Number = model.Number;
            if (ggstr != "")
                pro.Number += ggstr;
            else
                pro.Number += (loop + 1);
            pro.Name = model.Name;
            pro.Price = model.Price;
            pro.Price_Cost = model.Price_Cost;
            pro.Price_Market = model.Price_Market;
            pro.Price_Sale = model.Price_Sale;
            pro.Count_Limit = model.Count_Limit;
            pro.ProPerty131 = id131;
            pro.Weight = model.Weight;
            pro.NetWeight = model.NetWeight;
            pro.ImageBig = model.ImageBig;
            pro.ImageMedium = model.ImageMedium;
            pro.ImageOriginal = model.ImageOriginal;
            pro.Images = model.Images;
            pro.ImageSmall = model.ImageSmall;
            pro.Brand_id = model.Brand_id;//商品品牌
            pro.Pro_Type_id = model.Pro_Type_id;//商品分类
            pro.Pro_Type_id_other = model.Pro_Type_id_other;
            pro.Units_id = model.Units_id;//单位
            pro.ProPerty132 = model.ProPerty132;//商品属性
            if (model.Type_id_ProductStatus == 0)
            {
                pro.Type_id_ProductStatus = 101;
            }
            else
            {
                pro.Type_id_ProductStatus = model.Type_id_ProductStatus;
            }
            if (model.Type_id_ProductType == 0)
            {
                pro.Type_id_ProductType = 320;
            }
            else
            {
                pro.Type_id_ProductType = model.Type_id_ProductType;
            }
            pro.Time_Expired = model.Time_Expired;
            pro.Time_Start = model.Time_Start;
            pro.Count_StockCaution = model.Count_StockCaution;//预警库存同父产品
            pro.VolumeH = model.VolumeH;
            pro.VolumeL = model.VolumeL;
            pro.VolumeW = model.VolumeW;
            pro.PackageRate = model.PackageRate;
            pro.Supplier_id = model.Supplier_id;
            pro.IsSupplierTransport = model.IsSupplierTransport;
            pro.Site_ids = model.Site_ids;
            B_Lebi_Product.Add(pro);
            //处理静态页面
            pro.id = B_Lebi_Product.GetMaxId();
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_Product(pro, themepage);
        }
        /// <summary>
        /// 批量修改商品价格和库存
        /// </summary>
        public void Product_Edit_muti_price_store()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            //string ids = RequestTool.RequestString("sonproductid");
            int pid = RequestTool.RequestInt("pid", 0);
            //int randnum = RequestTool.RequestInt("randnum", 0);
            //if (pid == 0)
            //    pid = randnum;
            Lebi_Product modelp = B_Lebi_Product.GetModel(pid);
            if (modelp == null)
            {
                modelp = new Lebi_Product();
            }
            List<Lebi_Product> models = B_Lebi_Product.GetList("Product_id=" + pid + "", "");
            int count = 0;
            int Count_Freeze = 0;
            foreach (Lebi_Product model in models)
            {
                model.Price_Market = RequestTool.RequestDecimal("Price_Market" + model.id + "", 0);
                model.Price = RequestTool.RequestDecimal("Price" + model.id + "", 0);
                model.Price_Cost = RequestTool.RequestDecimal("Price_Cost" + model.id + "", 0);
                model.Price_Sale = RequestTool.RequestDecimal("Price_Sale" + model.id + "", 0);
                if (IsEditStock)
                {
                    model.Count_Stock = RequestTool.RequestInt("Count_Stock" + model.id + "", 0);
                }
                model.Count_Sales_Show = RequestTool.RequestInt("Count_Sales_Show" + model.id + "", 0);
                model.Type_id_ProductStatus = RequestTool.RequestInt("Type_id_ProductStatus" + model.id + "", 0);
                B_Lebi_Product.Update(model);
                count = count + model.Count_Stock;
                Count_Freeze = Count_Freeze + model.Count_Freeze;
            }
            //更新主商品的库存
            modelp.Count_Stock = count;
            modelp.Count_Freeze = Count_Freeze;
            modelp.ProPertyMain = RequestTool.RequestInt("ProPertyMain", 0);
            B_Lebi_Product.Update(modelp);
            Log.Add("更新商品规格价格及库存", "Product", pid.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(modelp.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改商品上架状态
        /// </summary>
        public void Product_Status_Edit_muti()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int father = RequestTool.RequestInt("father", 0);
            int status = RequestTool.RequestInt("status", 0);
            string ids = "";
            if (father == 1)
            {
                ids = RequestTool.RequestString("productid");
            }
            else
            {
                ids = RequestTool.RequestString("sonproductid");
            }
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                return;
            }
            status = status == 1 ? 101 : 100;
            string[] arr_ids = ids.Split(',');
            foreach (string arr_id in arr_ids)
            {
                int id = Convert.ToInt16(arr_id);
                Lebi_Product model = B_Lebi_Product.GetModel(id);
                if (model == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                model.Type_id_ProductStatus = status;
                B_Lebi_Product.Update(model);
                if (model.Product_id == 0)
                {
                    //<-{同步更新子商品状态 by lebi.kingdge 2015-02-13
                    string sql = "update [Lebi_Product] set Type_id_ProductStatus=" + status + " where Product_id=" + id + " and Product_id<>0";
                    Common.ExecuteSql(sql);
                    //}->
                }

            }
            //List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
            //foreach (Lebi_Product model in models)
            //{
            //    model.Type_id_ProductStatus = status;
            //    B_Lebi_Product.Update(model);
            //    if (model.Product_id == 0)
            //    {
            //        string sql = "update [Lebi_Product] set Type_id_ProductStatus=" + status + " where Product_id=" + model.id + " and Product_id<>0";
            //        Common.ExecuteSql(sql);
            //    }
            //}
            Log.Add("更新商品上架状态", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 批量修改商品图片
        /// </summary>
        public void Product_Image_Edit_muti()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            string ImageOriginal = RequestTool.RequestString("smalliamge");
            string images = RequestTool.RequestString("images");
            List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
            int i = 0;
            foreach (Lebi_Product model in models)
            {
                model.ImageOriginal = ImageOriginal;
                model.Images = images;
                B_Lebi_Product.Update(model);
                //处理静态页面
                Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
                if (themepage.Type_id_PublishType == 122)
                    PageStatic.Greate_Product(model, themepage);
                if (i == 0)
                {
                    ImageHelper.LebiImagesUsed(images + "@" + ImageOriginal, "Product", model.id);
                    //修改主商品图片
                    Lebi_Product parent = B_Lebi_Product.GetModel(model.Product_id);
                    if (parent != null)
                    {
                        models = B_Lebi_Product.GetList("Product_id=" + parent.id + "", "");
                        parent.ImageBig = model.ImageBig;
                        parent.ImageMedium = model.ImageMedium;
                        parent.ImageOriginal = model.ImageOriginal;
                        parent.Images = model.Images;
                        parent.ImageSmall = model.ImageSmall;
                        B_Lebi_Product.Update(parent);
                        //修改主商品图片结束
                    }
                }
                i++;
            }
            //Lebi_Product pro = models.FirstOrDefault();
            //if (pro != null)
            //{
            //    ImageHelper.LebiImagesUsed(images + "@" + ImageOriginal, "Product", pro.id);
            //    //修改主商品图片
            //    Lebi_Product parent = B_Lebi_Product.GetModel(pro.Product_id);
            //    if (parent != null)
            //    {
            //        models = B_Lebi_Product.GetList("Product_id=" + parent.id + "", "");
            //        pro = models.FirstOrDefault();
            //        parent.ImageBig = pro.ImageBig;
            //        parent.ImageMedium = pro.ImageMedium;
            //        parent.ImageOriginal = pro.ImageOriginal;
            //        parent.Images = pro.Images;
            //        parent.ImageSmall = pro.ImageSmall;
            //        B_Lebi_Product.Update(parent);
            //        //修改主商品图片结束
            //    }
            //}
            Log.Add("更新商品图片", "Product", ids.ToString(), CurrentAdmin, ImageOriginal.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 回复商品评价
        /// </summary>
        public void Comment_Edit()
        {
            if (!EX_Admin.Power("comment_edit", "编辑商品评价"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Comment model = B_Lebi_Comment.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            // <-{审核商品评价，增加会员积分 56770.kingdge 2013-8-20
            if (model.Status != 281)
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user != null)
                {
                    BaseConfig SYS = ShopCache.GetBaseConfig();
                    Lebi_User_Point modelpoint = new Lebi_User_Point();
                    Log.Add("审核商品评价-添加会员积分", "User_Point", id.ToString(), CurrentAdmin, SYS.CommentPoint);
                    modelpoint.Point = int.Parse(SYS.CommentPoint);
                    modelpoint.Type_id_PointStatus = 171;
                    modelpoint.Admin_UserName = CurrentAdmin.UserName;
                    modelpoint.Admin_id = CurrentAdmin.id;
                    modelpoint.Remark = RequestTool.RequestSafeString("Remark");
                    modelpoint.Time_Update = DateTime.Now;
                    modelpoint.User_id = user.id;
                    modelpoint.User_RealName = user.RealName;
                    modelpoint.User_UserName = user.UserName;
                    B_Lebi_User_Point.Add(modelpoint);
                    Point.UpdateUserPoint(user);
                }
            }
            // }->
            model.Status = 281;
            model.IsRead = 0;
            B_Lebi_Comment.Update(model);
            if (RequestTool.RequestString("Content") != "")
            {
                Lebi_Comment newmodel = new Lebi_Comment();
                newmodel.TableName = "Product";
                newmodel.Keyid = model.Keyid;
                newmodel.Admin_UserName = CurrentAdmin.UserName;
                newmodel.Admin_id = CurrentAdmin.id;
                newmodel.Content = RequestTool.RequestString("Content");
                newmodel.Parentid = id;
                newmodel.Status = 281;
                newmodel.IsRead = 0;
                newmodel.User_id = model.User_id;
                B_Lebi_Comment.Add(newmodel);
                Log.Add("回复商品评价", "Comment", RequestTool.RequestInt("id", 0).ToString(), CurrentAdmin, RequestTool.RequestString("Content"));
                //发送短信
                SMS.SendSMS_commentreply(newmodel);
                //APP推送
                APP.Push_commentreply(newmodel);
            }
            else
            {
                Log.Add("编辑商品评价", "Comment", RequestTool.RequestInt("id", 0).ToString(), CurrentAdmin, model.Content);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新商品评价状态
        /// </summary>
        public void Comment_Update()
        {
            if (!EX_Admin.Power("comment_edit", "编辑商品评价"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            List<Lebi_Comment> models = B_Lebi_Comment.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Comment model in models)
            {
                // <-{审核商品评价，增加会员积分 56770.kingdge 2013-8-20
                if (model.Status != 281 && RequestTool.RequestInt("Status" + model.id, 0) == 281)
                {
                    Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                    if (user != null)
                    {
                        BaseConfig SYS = ShopCache.GetBaseConfig();
                        Lebi_User_Point modelpoint = new Lebi_User_Point();
                        Log.Add("审核商品评价-添加会员积分", "User_Point", model.id.ToString(), CurrentAdmin, SYS.CommentPoint);
                        modelpoint.Point = int.Parse(SYS.CommentPoint);
                        modelpoint.Type_id_PointStatus = 171;
                        modelpoint.Admin_UserName = CurrentAdmin.UserName;
                        modelpoint.Admin_id = CurrentAdmin.id;
                        modelpoint.Remark = RequestTool.RequestSafeString("Remark");
                        modelpoint.Time_Update = DateTime.Now;
                        modelpoint.User_id = user.id;
                        modelpoint.User_RealName = user.RealName;
                        modelpoint.User_UserName = user.UserName;
                        B_Lebi_User_Point.Add(modelpoint);
                        Point.UpdateUserPoint(user);
                    }
                }
                // }->
                model.Status = RequestTool.RequestInt("Status" + model.id, 0);
                model.Admin_UserName = CurrentAdmin.UserName;
                model.Admin_id = CurrentAdmin.id;
                B_Lebi_Comment.Update(model);
            }
            Log.Add("编辑商品评价", "Comment", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品评价
        /// </summary>
        public void Comment_Del()
        {
            if (!EX_Admin.Power("comment_del", "删除商品评价"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Comment.Delete("Parentid in (lbsql{" + id + "})");
            B_Lebi_Comment.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除商品评价", "Comment", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 回复商品咨询
        /// </summary>
        public void Ask_Edit()
        {
            if (!EX_Admin.Power("ask_edit", "编辑商品咨询"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            if (RequestTool.RequestString("Content") == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请填写内容") + "\"}");
                return;
            }
            Lebi_Comment model = B_Lebi_Comment.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.Status = 283;
            model.IsRead = 0;
            B_Lebi_Comment.Update(model);
            Lebi_Comment newmodel = new Lebi_Comment();
            newmodel.TableName = "Product_Ask";
            newmodel.Keyid = model.Keyid;
            newmodel.Admin_UserName = CurrentAdmin.UserName;
            newmodel.Admin_id = CurrentAdmin.id;
            newmodel.Content = RequestTool.RequestString("Content");
            newmodel.Parentid = id;
            newmodel.Status = 283;
            newmodel.IsRead = 0;
            newmodel.User_id = model.User_id;
            B_Lebi_Comment.Add(newmodel);
            Log.Add("回复商品咨询", "Comment", RequestTool.RequestInt("id", 0).ToString(), CurrentAdmin, RequestTool.RequestString("Content"));
            //发送短信
            SMS.SendSMS_askreply(newmodel);
            //APP推送
            APP.Push_askreply(newmodel);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品咨询
        /// </summary>
        public void Ask_Del()
        {
            if (!EX_Admin.Power("ask_del", "删除商品咨询"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Comment.Delete("Parentid in (lbsql{" + id + "})");
            B_Lebi_Comment.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除商品咨询", "Comment", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑商品信息
        /// </summary>
        public void Product_Batch_Update()
        {
            if (!EX_Admin.Power("product_batch_edit", "批量编辑"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
            List<Lebi_UserLevel> userlevels = B_Lebi_UserLevel.GetList("", "Grade asc");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            foreach (Lebi_Product model in models)
            {
                model.Name = Language.RequestSafeString("Name" + model.id + "");
                model.Number = RequestTool.RequestSafeString("Number" + model.id + "");
                model.Price = RequestTool.RequestDecimal("Price" + model.id + "", 0);
                model.Price_Market = RequestTool.RequestDecimal("Price_Market" + model.id + "", 0);
                model.Price_Cost = RequestTool.RequestDecimal("Price_Cost" + model.id + "", 0);
                if (!IsEditStock)
                {
                    model.Count_Stock = RequestTool.RequestInt("Count_Stock" + model.id + "", 0);
                }
                model.Count_Sales_Show = RequestTool.RequestInt("Count_Sales_Show" + model.id + "", 0);
                model.Count_Views_Show = RequestTool.RequestInt("Count_Views_Show" + model.id + "", 0);
                model.Type_id_ProductStatus = RequestTool.RequestInt("ProductStatus" + model.id + "", 0);
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                if (Shop.LebiAPI.Service.Instanse.Check("plugin_product_price"))
                {
                    //更新会员分组价格
                    List<ProductUserLevelPrice> ulprices = new List<ProductUserLevelPrice>();
                    foreach (Lebi_UserLevel ul in userlevels)
                    {
                        decimal p = RequestTool.RequestDecimal("userlevelprice" + model.id + "_" + ul.id);
                        if (p > 0)
                        {
                            ProductUserLevelPrice ulprice = new ProductUserLevelPrice();
                            ulprice.Price = p;
                            ulprice.UserLevel_id = ul.id;
                            ulprices.Add(ulprice);
                        }
                    }
                    if (ulprices.Count > 0)
                    {
                        model.UserLevelPrice = jss.Serialize(ulprices);
                    }
                }
                B_Lebi_Product.Update(model);
            }
            Log.Add("批量编辑商品", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量调价
        /// </summary>
        public void Product_Batch_Price_Update()
        {
            if (!EX_Admin.Power("product_batch_price", "批量调价"))
            {
                AjaxNoPower();
                return;
            }
            string step = RequestTool.RequestString("step");
            string dateFrom = RequestTool.RequestString("dateFrom");
            string dateTo = RequestTool.RequestString("dateTo");
            string Pro_Type_id = RequestTool.RequestString("Pro_Type_id");
            int brand = RequestTool.RequestInt("brand", 0);
            int tag = RequestTool.RequestInt("tag", 0);
            int price_markettype = RequestTool.RequestInt("price_markettype", 0);
            int price_marketvalue = RequestTool.RequestInt("price_marketvalue", 0);
            int price_marketadd = RequestTool.RequestInt("price_marketadd", 0);
            int price_costtype = RequestTool.RequestInt("price_costtype", 0);
            int price_costvalue = RequestTool.RequestInt("price_costvalue", 0);
            int price_costadd = RequestTool.RequestInt("price_costadd", 0);
            int pricetype = RequestTool.RequestInt("pricetype", 0);
            int pricevalue = RequestTool.RequestInt("pricevalue", 0);
            int priceadd = RequestTool.RequestInt("priceadd", 0);
            int addtype = RequestTool.RequestInt("addtype", 0);
            int addvalue = RequestTool.RequestInt("addvalue", 0);
            int reducetype = RequestTool.RequestInt("reducetype", 0);
            int reducevalue = RequestTool.RequestInt("reducevalue", 0);
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string mes = "";
            string where = "1=1";
            if (dateFrom != "" && dateTo != "")
            {
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
                mes += "上架日期" + dateFrom + "-" + dateTo + ";";
            }
            if (Pro_Type_id != "")
            {
                where += " and Pro_Type_id in (" + Shop.Bussiness.EX_Product.Categoryid(Pro_Type_id) + ")";
                mes += "商品分类" + Pro_Type_id + ";";
            }
            if (brand > 0)
            {
                where += " and Brand_id=" + brand + "";
                mes += "商品品牌" + brand + ";";
            }
            if (tag > 0)
            {
                if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "sqlserver")
                {
                    where += " and Charindex('" + tag + "',Pro_Tag_id)>0";
                }
                else if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "access")
                {
                    where += " and Instr(Pro_Tag_id,'" + tag + "')>0";
                }
                else if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "mysql")
                {
                    where += " and Instr(Pro_Tag_id,'" + tag + "')>0";
                }
                mes += "商品标签" + tag + ";";
            }
            Lebi_Currency DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (step == "1")
            {
                if (price_marketvalue == 0 && price_costvalue == 0 && pricevalue == 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("请先填写调价数值") + "\"}");
                    return;
                }
                if (price_marketvalue > 0)
                {
                    if (price_markettype == 0)
                    {
                        if (price_marketadd == 0)
                        {
                            mes += "市场价上浮" + price_marketvalue + "%;";
                        }
                        else
                        {
                            mes += "市场价下调" + price_marketvalue + "%;";
                        }
                    }
                    else
                    {
                        if (price_marketadd == 0)
                        {
                            mes += "市场价上浮" + price_marketvalue + ";";
                        }
                        else
                        {
                            mes += "市场价下调" + price_marketvalue + ";";
                        }
                    }
                }
                if (Shop.Bussiness.EX_Admin.CheckPower("product_price_cost"))
                {
                    if (price_costvalue > 0)
                    {
                        if (price_costtype == 0)
                        {
                            if (price_costadd == 0)
                            {
                                mes += "成本价上浮" + price_costvalue + "%;";
                            }
                            else
                            {
                                mes += "成本价下调" + price_costvalue + "%;";
                            }
                        }
                        else
                        {
                            if (price_costadd == 0)
                            {
                                mes += "成本价上浮" + price_costvalue + ";";
                            }
                            else
                            {
                                mes += "成本价下调" + price_costvalue + ";";
                            }
                        }
                    }
                }
                if (pricevalue > 0)
                {
                    if (pricetype == 0)
                    {
                        if (priceadd == 0)
                        {
                            mes += "销售价上浮" + pricevalue + "%;";
                        }
                        else
                        {
                            mes += "销售价下调" + pricevalue + "%;";
                        }
                    }
                    else
                    {
                        if (priceadd == 0)
                        {
                            mes += "销售价上浮" + pricevalue + ";";
                        }
                        else
                        {
                            mes += "销售价下调" + pricevalue + ";";
                        }
                    }
                }
                List<Lebi_Product> models = B_Lebi_Product.GetList(where, "");
                foreach (Lebi_Product model in models)
                {
                    if (price_marketvalue > 0)
                    {
                        if (price_markettype == 0)
                        {
                            if (price_marketadd == 0)
                            {
                                model.Price_Market = model.Price_Market + (model.Price_Market * price_marketvalue / 100);
                            }
                            else
                            {
                                model.Price_Market = model.Price_Market + price_marketvalue;
                            }
                        }
                        else
                        {
                            if (price_marketadd == 0)
                            {
                                model.Price_Market = model.Price_Market - (model.Price_Market * price_marketvalue / 100);
                            }
                            else
                            {
                                model.Price_Market = model.Price_Market - price_marketvalue;
                            }
                        }
                        if (DefaultCurrency != null)
                        {
                            string FormatPrice = model.Price_Market.ToString("f" + DefaultCurrency.DecimalLength);
                            model.Price_Market = Convert.ToDecimal(FormatPrice);
                        }
                    }
                    if (Shop.Bussiness.EX_Admin.CheckPower("product_price_cost"))
                    {
                        if (price_costvalue > 0)
                        {
                            if (price_costtype == 0)
                            {
                                if (price_costadd == 0)
                                {
                                    model.Price_Cost = model.Price_Cost + (model.Price_Cost * price_costvalue / 100);
                                }
                                else
                                {
                                    model.Price_Cost = model.Price_Cost + price_costvalue;
                                }
                            }
                            else
                            {
                                if (price_costadd == 0)
                                {
                                    model.Price_Cost = model.Price_Cost - (model.Price_Cost * price_costvalue / 100);
                                }
                                else
                                {
                                    model.Price_Cost = model.Price_Cost - price_costvalue;
                                }
                            }
                        }
                        if (DefaultCurrency != null)
                        {
                            string FormatPrice = model.Price_Cost.ToString("f" + DefaultCurrency.DecimalLength);
                            model.Price_Cost = Convert.ToDecimal(FormatPrice);
                        }
                    }
                    if (pricevalue > 0)
                    {
                        if (pricetype == 0)
                        {
                            if (priceadd == 0)
                            {
                                model.Price = model.Price + (model.Price * pricevalue / 100);
                            }
                            else
                            {
                                model.Price = model.Price + pricevalue;
                            }
                        }
                        else
                        {
                            if (priceadd == 0)
                            {
                                model.Price = model.Price - (model.Price * pricevalue / 100);
                            }
                            else
                            {
                                model.Price = model.Price - pricevalue;
                            }
                        }
                        if (DefaultCurrency != null)
                        {
                            string FormatPrice = model.Price.ToString("f" + DefaultCurrency.DecimalLength);
                            model.Price = Convert.ToDecimal(FormatPrice);
                        }
                    }
                    B_Lebi_Product.Update(model);
                    string sql = "update [Lebi_Product] set Price=" + model.Price + " where Product_id=" + model.id + " and Product_id<>0";
                    Common.ExecuteSql(sql);
                }
            }
            if (step == "2")
            {
                if (addvalue == 0 && reducevalue == 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("请先填写调价数值") + "\"}");
                    return;
                }
                if (addvalue > 0)
                {
                    if (addtype == 0)
                    {
                        mes += "销售价=成本价上浮" + addvalue + "%;";
                    }
                    else
                    {
                        mes += "销售价=成本价上浮" + addvalue + ";";
                    }
                }
                if (reducevalue > 0)
                {
                    if (reducetype == 0)
                    {
                        mes += "销售价=市场价下调" + reducevalue + "%;";
                    }
                    else
                    {
                        mes += "销售价=市场价下调" + reducevalue + ";";
                    }
                }
                List<Lebi_Product> models = B_Lebi_Product.GetList(where, "");
                foreach (Lebi_Product model in models)
                {
                    if (addvalue > 0)
                    {
                        if (addtype == 0)
                        {
                            model.Price = model.Price_Cost + (model.Price_Cost * addvalue / 100);
                        }
                        else
                        {
                            model.Price = model.Price_Cost + addvalue;
                        }
                    }
                    if (reducevalue > 0)
                    {
                        if (reducetype == 0)
                        {
                            model.Price = model.Price_Market - (model.Price_Market * reducevalue / 100);
                        }
                        else
                        {
                            model.Price = model.Price_Market - reducevalue;
                        }
                    }
                    if (DefaultCurrency != null)
                    {
                        string FormatPrice = model.Price.ToString("f" + DefaultCurrency.DecimalLength);
                        model.Price = Convert.ToDecimal(FormatPrice);
                    }
                    B_Lebi_Product.Update(model);
                    string sql = "update [Lebi_Product] set Price=" + model.Price + " where Product_id=" + model.id + " and Product_id<>0";
                    Common.ExecuteSql(sql);
                }
            }
            Log.Add("批量调价", "Product", step.ToString(), CurrentAdmin, mes);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改商品分类
        /// </summary>
        public void Product_Category_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                return;
            }
            List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Product model in models)
            {
                model.Pro_Type_id = RequestTool.RequestInt("Pro_Type_id", 0);
                model.Pro_Type_id_other = RequestTool.RequestSafeString("Pro_Type_id_other");
                B_Lebi_Product.Update(model);
                string sql = "update [Lebi_Product] set Pro_Type_id='" + RequestTool.RequestInt("Pro_Type_id", 0) + "' where Product_id=" + model.id + " and Product_id<>0";
                Common.ExecuteSql(sql);
            }
            Log.Add("更新商品类别", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改商品品牌
        /// </summary>
        public void Product_Brand_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                return;
            }
            List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Product model in models)
            {
                model.Brand_id = RequestTool.RequestInt("Brand_id", 0);
                B_Lebi_Product.Update(model);
                string sql = "update [Lebi_Product] set Brand_id=" + RequestTool.RequestInt("Brand_id", 0) + " where Product_id=" + model.id + " and Product_id<>0";
                Common.ExecuteSql(sql);
            }
            Log.Add("更新商品品牌", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改商品标签
        /// </summary>
        public void Product_Tag_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                return;
            }
            List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Product model in models)
            {
                model.Pro_Tag_id = RequestTool.RequestString("Pro_Tag_id");
                model.Tags = RequestTool.RequestSafeString("Tags");
                B_Lebi_Product.Update(model);
                string sql = "update [Lebi_Product] set Pro_Tag_id='" + RequestTool.RequestString("Pro_Tag_id") + "' where Product_id=" + model.id + " and Product_id<>0";
                Common.ExecuteSql(sql);
            }
            Log.Add("更新商品标签", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改商品站点
        /// </summary>
        public void Product_Site_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int DataType = RequestTool.RequestInt("DataType", 0);
            int UpdateType = RequestTool.RequestInt("UpdateType", 0);
            string ids = RequestTool.RequestSafeString("ids");
            if (DataType == 0)
            {
                if (ids == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请选择要修改的类别") + "\"}");
                    return;
                }
            }
            string where = "id in (lbsql{" + ids + "})";
            if (DataType == 1)
            {
                where = "Product_id = 0";
            }
            List<Lebi_Product> models = B_Lebi_Product.GetList(where, "");
            string Site_ids = RequestTool.RequestSafeString("Site_ids");
            foreach (Lebi_Product model in models)
            {
                if (UpdateType == 0)
                {
                    model.Site_ids = Site_ids;
                }
                else
                {
                    model.Site_ids += "," + Site_ids;
                }
                B_Lebi_Product.Update(model);
                List<Lebi_Product> pmodels = B_Lebi_Product.GetList("Product_id=" + model.id + "", "");
                foreach (Lebi_Product pmodel in pmodels)
                {
                    if (UpdateType == 0)
                    {
                        pmodel.Site_ids = Site_ids;
                    }
                    else
                    {
                        pmodel.Site_ids += "," + Site_ids;
                    }
                    B_Lebi_Product.Update(pmodel);
                }
            }
            Log.Add("更新商品站点", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量生成商品规格
        /// </summary>
        public void Product_Property_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string where = "";
            string ids = RequestTool.RequestSafeString("ids");
            int ProPertyMain = RequestTool.RequestInt("ProPertyMain", 0);
            int Way = RequestTool.RequestInt("Way", 0);
            if (Way == 1)
            {
                if (ids == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                    return;
                }
                where = "id in (lbsql{" + ids + "})";
            }
            else
            {
                where = "Product_id = 0";
            }
            List<Lebi_Product> products = B_Lebi_Product.GetList(where, "");

            Lebi_Product son;
            string Operators_Price_Market = "";
            string Operators_Price = "";
            string Operators_Price_Cost = "";
            string Operators_Count_Stock = "";

            decimal Price_Market = 0;
            decimal Price = 0;
            decimal Price_Cost = 0;
            int Count_Stock = 0;
            int Type_id_ProductStatus = 0;
            foreach (Lebi_Product product in products)
            {
                //循环将生成的规格列表 写入到各个商品的子商品，如果子商品存在直接更新，如果不存在新建
                List<Lebi_Product> temps = B_Lebi_Product.GetList("Product_id=99999999", "");
                foreach (Lebi_Product temp in temps)
                {
                    Operators_Price_Market = RequestTool.RequestString("Operators_Price_Market" + temp.id);
                    Operators_Price = RequestTool.RequestString("Operators_Price" + temp.id);
                    Operators_Price_Cost = RequestTool.RequestString("Operators_Price_Cost" + temp.id);
                    Operators_Count_Stock = RequestTool.RequestString("Operators_Count_Stock" + temp.id);
                    Price_Market = RequestTool.RequestDecimal("Price_Market" + temp.id);
                    Price = RequestTool.RequestDecimal("Price" + temp.id);
                    Price_Cost = RequestTool.RequestDecimal("Price_Cost" + temp.id);
                    Count_Stock = RequestTool.RequestInt("Count_Stock" + temp.id);
                    Type_id_ProductStatus = RequestTool.RequestInt("Type_id_ProductStatus" + temp.id);

                    son = B_Lebi_Product.GetModel("Product_id=" + product.id + " and ProPerty131='" + temp.ProPerty131 + "'");
                    if (son == null)
                    {
                        son = temp;
                        son.id = 0;
                        son.Product_id = product.id;
                        son.Name = product.Name;
                        son.Pro_Type_id = product.Pro_Type_id;
                        son.Pro_Type_id_other = son.Pro_Type_id_other;
                        son.Number = product.Number + "-" + temp.Number;
                        son.Units_id = product.Units_id;
                        son.Weight = product.Weight;
                        son.Introduction = product.Introduction;
                        son.Description = product.Description;
                        son.MobileDescription = product.MobileDescription;
                        son.SEO_Description = product.SEO_Description;
                        son.SEO_Keywords = product.SEO_Keywords;
                        son.SEO_Title = product.SEO_Title;
                        son.NetWeight = product.NetWeight;
                        son.ImageBig = product.ImageBig;
                        son.ImageMedium = product.ImageMedium;
                        son.ImageOriginal = product.ImageOriginal;
                        son.Images = product.Images;
                        son.ImageSmall = product.ImageSmall;
                        son.Brand_id = product.Brand_id;
                        son.ProPerty132 = product.ProPerty132;
                        son.Type_id_ProductType = product.Type_id_ProductType;
                        son.Time_Expired = product.Time_Expired;
                        son.Count_StockCaution = product.Count_StockCaution;//预警库存同父产品
                        son.VolumeH = product.VolumeH;
                        son.VolumeL = product.VolumeL;
                        son.VolumeW = product.VolumeW;
                        son.PackageRate = product.PackageRate;
                        son.Supplier_id = product.Supplier_id;
                        son.IsSupplierTransport = product.IsSupplierTransport;
                        son.Site_ids = product.Site_ids;
                    }
                    if (Operators_Price_Market == "")
                    {
                        son.Price_Market = Price_Market;
                    }
                    else if (Operators_Price_Market == "+")
                    {
                        son.Price_Market = product.Price_Market + Price_Market;
                    }
                    else
                    {
                        son.Price_Market = product.Price_Market * Price_Market;
                    }
                    if (Operators_Price == "")
                    {
                        son.Price = Price;
                    }
                    else if (Operators_Price == "+")
                    {
                        son.Price = product.Price + Price;
                    }
                    else
                    {
                        son.Price = product.Price * Price;
                    }
                    if (Operators_Price_Cost == "")
                    {
                        son.Price_Cost = Price_Cost;
                    }
                    else if (Operators_Price_Cost == "+")
                    {
                        son.Price_Cost = product.Price_Cost + Price_Cost;
                    }
                    else
                    {
                        son.Price_Cost = product.Price_Cost * Price_Cost;
                    }
                    if (Operators_Count_Stock == "")
                    {
                        son.Count_Stock = Count_Stock;
                    }
                    else if (Operators_Count_Stock == "+")
                    {
                        son.Count_Stock = product.Count_Stock + Count_Stock;
                    }
                    else
                    {
                        son.Count_Stock = product.Count_Stock * Count_Stock;
                    }
                    son.Type_id_ProductStatus = Type_id_ProductStatus;
                    if (son.id == 0)
                        B_Lebi_Product.Add(son);
                    else
                        B_Lebi_Product.Update(son);
                }
                product.ProPertyMain = ProPertyMain;
                B_Lebi_Product.Update(product);
            }
            Log.Add("批量生成商品规格", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新商品属性
        /// </summary>
        public void Product_Property132_Edit()
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string where = "";
            string ids = RequestTool.RequestSafeString("ids");
            int Way = RequestTool.RequestInt("Way", 0);
            if (Way == 1)
            {
                if (ids == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                    return;
                }
                where = "id in (lbsql{" + ids + "})";
            }
            else
            {
                where = "1=1";
            }
            List<Lebi_Product> products = B_Lebi_Product.GetList(where, "");
            foreach (Lebi_Product product in products)
            {
                //更新自定义文字属性
                List<Lebi_ProPerty> pros;
                string property = EX_Product.ProductType_ProPertystr(RequestTool.RequestInt("Pro_Type_id", 0));
                pros = B_Lebi_ProPerty.GetList("Type_id_ProPertyType =133 and id in (" + property + ")", "Sort desc");
                if (pros == null)
                {
                    pros = new List<Lebi_ProPerty>();
                }
                List<KeyValue> kvs = new List<KeyValue>();
                foreach (Lebi_ProPerty pro in pros)
                {
                    KeyValue kv = new KeyValue();
                    kv.V = Language.RequestString("Property133_" + pro.id);
                    kv.K = pro.id.ToString();
                    kvs.Add(kv);

                }
                product.ProPerty133 = Common.KeyValueToJson(kvs);
                product.ProPerty132 = RequestTool.RequestString("ProPerty132");
                B_Lebi_Product.Update(product);
                string sql = "update [Lebi_Product] set ProPerty133='" + Common.KeyValueToJson(kvs) + "',ProPerty132='" + RequestTool.RequestSafeString("ProPerty132") + "' where Product_id=" + product.id + " and Product_id<>0";
                Common.ExecuteSql(sql);
            }
            Log.Add("批量生成商品属性", "Product", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        #region 后台分类列表
        /// <summary>
        /// 生成分类列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public void CreateTree()
        {
            int pid = RequestTool.RequestInt("pid");
            int deep = RequestTool.RequestInt("deep");
            string str = "";
            //的道所有的更节点
            // List<Lebi_Pro_Type> types = B_Lebi_Pro_Type.GetList("Parentid=" + pid + "", "Sort desc");
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            //将根节点进行遍历
            string style = "";
            foreach (Lebi_Pro_Type t in types)
            {
                string caozuo = "<a href=\"javascript:Edit(" + t.id + ",0)\">" + Tag("添加子类") + "</a> |  <a href=\"javascript:Edit(0," + t.id + ")\">" + Tag("编辑") + "</a>";

                int count = B_Lebi_Pro_Type.Counts("Parentid=" + t.id + "");
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    caozuo += " | <a href=\"javascript:DeleteConfirm(" + t.id + ")\">" + Tag("删除") + "</a>";
                    str += "<tr ondblclick=\"Edit(0," + t.id + ")\" name=\"tr" + t.Parentid + "\" " + style + " id=\"tr" + t.id + "\">";
                    str += "<td><label class=\"custom-control custom-checkbox\"><input type=\"checkbox\" id=\"check" + t.id + "\" name=\"id\" value=\"" + t.id + "\" class=\"custom-control-input\" del=\"del\"><span class=\"custom-control-label\"></span></label></td>";
                    str += "<td>" + t.id + "</td>";
                    str += "<td>" + deepstr(deep);
                    if (count > 0)
                        str += "<i class=\"ti-plus\" name=\"img" + t.Parentid + "\" id=\"img" + t.id + "\" style=\"cursor: pointer;\" onclick=\"ShowProductTypeChild('" + findpath(t.id) + "'," + t.id + "," + (deep + 1) + ")\" title=\"" + Tag("展开") + "\"></i>&nbsp;&nbsp;";
                    else
                        str += "<i class=\"ti-minus\" style=\"cursor: pointer;\"></i>&nbsp;&nbsp;";
                    if (t.ImageSmall != "")
                    {
                        str += "<img src=\"" + t.ImageSmall + "\" height=\"16\" />&nbsp;";
                    }
                    str += Language.Content(t.Name, CurrentLanguage.Code) + "&nbsp;<a href=\"" + Shop.Bussiness.ThemeUrl.GetURL("P_ProductCategory", t.id.ToString(), "", CurrentLanguage.Code) + "\" target=\"_blank\"><i class=\"ti-new-window\"></i></a></td>";
                    str += "<td><a href=\"default.aspx?Pro_Type_id=" + t.id + "&Type_id_ProductType=320,321,322,323\">" + EX_Product.TypeProductCount(t.id) + "</a></td>";
                    str += "<td>" + (t.IsShow == 1 ? "<span class=\"label label-success\">" + Tag("是") + "</span>" : "<span class=\"label label-danger\">" + Tag("否") + "</span>") + "</td>";
                    str += "<td>" + (t.IsIndexShow == 1 ? "<span class=\"label label-success\">" + Tag("是") + "</span>" : "<span class=\"label label-danger\">" + Tag("否") + "</span>") + "</td>";
                    str += "<td>" + t.Sort + "</td>";
                    str += "<td>" + caozuo + "</td></tr>";
                }else
                {
                    caozuo += " | <a href=\"javascript:Del(" + t.id + ")\">" + Tag("删除") + "</a>";
                    str += "<tr class=\"list\" ondblclick=\"Edit(0," + t.id + ")\" name=\"tr" + t.Parentid + "\" " + style + " id=\"tr" + t.id + "\">";
                    str += "<td style=\"text-align:center\"><input type='checkbox' id=\"check" + t.id + "\" value='" + t.id + "' name='id' del=\"del\" /></td>";
                    str += "<td>" + t.id + "</td>";
                    str += "<td>" + deepstr(deep);
                    if (count > 0)
                        str += "<img src=\"" + AdminImage("plus.gif") + "\" name=\"img" + t.Parentid + "\" id=\"img" + t.id + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\" onclick=\"ShowChild('" + findpath(t.id) + "'," + t.id + "," + (deep + 1) + ")\" title=\"" + Tag("展开") + "\" />&nbsp;&nbsp;";
                    else
                        str += "<img src=\"" + AdminImage("minus.gif") + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\" />&nbsp;&nbsp;";
                    if (t.ImageSmall != "")
                    {
                        str += "<img src=\"" + t.ImageSmall + "\" height=\"16\" />&nbsp;";
                    }
                    str += Language.Content(t.Name, CurrentLanguage.Code) + "&nbsp;<a href=\"" + Shop.Bussiness.ThemeUrl.GetURL("P_ProductCategory", t.id.ToString(), "", CurrentLanguage.Code) + "\" target=\"_blank\"><img src=\"" + PageImage("icon/newWindow.png") + "\" style=\"vertical-align:absmiddle\" /></a></td>";
                    str += "<td>" + LB.Tools.Utils.GetUnicodeSubString(shuxinglianjie(ProPertystring(t.ProPerty132), ProPertystring(t.ProPerty133)), 20, "...") + "</td>";
                    str += "<td>" + LB.Tools.Utils.GetUnicodeSubString(ProPertystring(t.ProPerty131), 20, "...") + "</td>";
                    str += "<td><a href=\"default.aspx?Pro_Type_id=" + t.id + "&Type_id_ProductType=320,321,322,323\">" + EX_Product.TypeProductCount(t.id) + "</a></td>";
                    str += "<td>" + t.Sort + "</td>";
                    str += "<td>" + (t.IsShow == 1 ? "" + Tag("是") + "" : "" + Tag("否") + "") + "</td>";
                    str += "<td>" + (t.IsIndexShow == 1 ? "" + Tag("是") + "" : "" + Tag("否") + "") + "</td>";
                    str += "<td>" + caozuo + "</td></tr>";
                }
            }
            Response.Write(str);
        }
        private string deepstr(int deep)
        {
            string str = "";
            for (int i = 0; i < deep; i++)
            {
                str += "&nbsp;&nbsp;&nbsp;";
            }
            return str;
        }
        /// <summary>
        /// 某个结点下的所有结点
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private string findpath(int pid)
        {
            string str = "";
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            if (types.Count == 0)
                return "";
            foreach (Lebi_Pro_Type t in types)
            {
                if (str == "")
                    str = t.id.ToString();
                else
                    str += "," + t.id;
                string f = findpath(t.id);
                //if (f != "")
                str += "," + f;
            }

            return str;

        }
        /// <summary>
        /// 属性规格字符串
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string ProPertystring(string ids)
        {
            if (ids == "")
                return "";
            string str = "";
            List<Lebi_ProPerty> pros = B_Lebi_ProPerty.GetList("id in (lbsql{" + ids + "})", "Sort desc");
            foreach (Lebi_ProPerty pro in pros)
            {
                if (str == "")
                    str = Language.Content(pro.Name, CurrentLanguage.Code);
                else
                    str += "," + Language.Content(pro.Name, CurrentLanguage.Code);
            }
            if (str == ",")
                str = "-";
            return str;
        }
        private string shuxinglianjie(string str1, string str2)
        {
            if (str1 == "")
                return str2;
            else
            {
                if (str2 == "")
                    return str1;
                else
                    return str1 + "," + str2;
            }
        }
        #endregion


        /// <summary>
        /// 通过会员或会员分组修改商品的权限
        /// </summary>
        public void ProductLimit_Edit()
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                AjaxNoPower();
                return;
            }
            int userlevelid = RequestTool.RequestInt("userlevelid");
            int userid = RequestTool.RequestInt("userid");
            Lebi_User user = B_Lebi_User.GetModel(userid);
            Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(userlevelid);
            if (user == null)
                user = new Lebi_User();
            else
            {
                //判断是否有设置会员的权限，没有的先继承分组的权限
                int c = B_Lebi_Product_Limit.Counts("User_id=" + user.id + "");
                if (c == 0)
                {
                    List<Lebi_Product_Limit> ls = B_Lebi_Product_Limit.GetList("UserLevel_id=" + user.UserLevel_id + " and User_id=0", "");
                    foreach (Lebi_Product_Limit l in ls)
                    {
                        l.UserLevel_id = 0;
                        l.User_id = user.id;
                        B_Lebi_Product_Limit.Add(l);
                    }
                }
                Shop.Bussiness.ProductLimit.UserLimitRemove(user.id);
            }
            if (userlevel == null)
                userlevel = new Lebi_UserLevel();
            string limitproductid = RequestTool.RequestString("limitproductid");
            if (limitproductid == "")
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            List<Lebi_Product> ps = B_Lebi_Product.GetList("id in (" + limitproductid + ")", "");
            foreach (Lebi_Product p in ps)
            {

                Lebi_Product_Limit model = B_Lebi_Product_Limit.GetModel("Product_id=" + p.id + " and User_id=" + user.id + " and UserLevel_id=" + userlevel.id + "");
                if (model == null)
                    model = new Lebi_Product_Limit();
                model.IsBuy = RequestTool.RequestInt("IsBuy" + p.id);
                model.IsPriceShow = RequestTool.RequestInt("IsPriceShow" + p.id);
                model.IsShow = RequestTool.RequestInt("IsShow" + p.id);
                model.User_id = user.id;
                model.UserLevel_id = userlevel.id;
                model.Product_id = p.id;
                if (model.IsBuy == 0 && model.IsPriceShow == 0 && model.IsShow == 0)
                {
                    B_Lebi_Product_Limit.Delete(model.id);
                }
                else
                {
                    if (model.id == 0)
                        B_Lebi_Product_Limit.Add(model);
                    else
                    {
                        B_Lebi_Product_Limit.Update(model);
                    }
                }
                Shop.Bussiness.ProductLimit.UserLevelLimitRemove(model.id);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 通过商品修改会员和会员分组的商品权限
        /// </summary>
        public void ProductLimit_Edit_product()
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                AjaxNoPower();
                return;
            }
            int userlevelid = RequestTool.RequestInt("userlevelid");
            int productid = RequestTool.RequestInt("productid");
            Lebi_Product product = B_Lebi_Product.GetModel(productid);
            Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(userlevelid);
            if (product == null)
                product = new Lebi_Product();
            if (userlevel != null)
            {
                Lebi_Product_Limit model = B_Lebi_Product_Limit.GetModel("Product_id=" + product.id + " and UserLevel_id=" + userlevel.id + "");
                if (model == null)
                    model = new Lebi_Product_Limit();
                model.IsBuy = RequestTool.RequestInt("IsBuy" + userlevel.id);
                model.IsPriceShow = RequestTool.RequestInt("IsPriceShow" + userlevel.id);
                model.IsShow = RequestTool.RequestInt("IsShow" + userlevel.id);
                model.User_id = 0;
                model.UserLevel_id = userlevel.id;
                model.Product_id = product.id;
                if (model.IsBuy == 0 && model.IsPriceShow == 0 && model.IsShow == 0)
                {
                    B_Lebi_Product_Limit.Delete(model.id);
                }
                else
                {
                    if (model.id == 0)
                        B_Lebi_Product_Limit.Add(model);
                    else
                    {
                        B_Lebi_Product_Limit.Update(model);
                    }
                }
            }
            string limituserid = RequestTool.RequestString("limituserid");
            if (limituserid == "")
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            List<Lebi_User> ps = B_Lebi_User.GetList("id in (" + limituserid + ")", "");
            foreach (Lebi_User user in ps)
            {


                //判断是否有设置会员的权限，没有的先继承分组的权限
                int c = B_Lebi_Product_Limit.Counts("User_id=" + user.id + "");
                if (c == 0)
                {
                    List<Lebi_Product_Limit> ls = B_Lebi_Product_Limit.GetList("UserLevel_id=" + user.UserLevel_id + " and User_id=0", "");
                    foreach (Lebi_Product_Limit l in ls)
                    {
                        l.UserLevel_id = 0;
                        l.User_id = user.id;
                        B_Lebi_Product_Limit.Add(l);
                    }
                }

                Lebi_Product_Limit model = B_Lebi_Product_Limit.GetModel("Product_id=" + product.id + " and User_id=" + user.id + "");
                if (model == null)
                    model = new Lebi_Product_Limit();
                model.IsBuy = RequestTool.RequestInt("IsBuy" + user.id);
                model.IsPriceShow = RequestTool.RequestInt("IsPriceShow" + user.id);
                model.IsShow = RequestTool.RequestInt("IsShow" + user.id);
                model.User_id = user.id;
                model.UserLevel_id = 0;
                model.Product_id = product.id;
                if (model.IsBuy == 0 && model.IsPriceShow == 0 && model.IsShow == 0)
                {
                    B_Lebi_Product_Limit.Delete(model.id);
                }
                else
                {
                    if (model.id == 0)
                        B_Lebi_Product_Limit.Add(model);
                    else
                    {
                        B_Lebi_Product_Limit.Update(model);
                    }
                }
            }

            Response.Write("{\"msg\":\"OK\"}");
        }
        public void ProductLimit_mutiEdit()
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("limituserid");
            if (ids != "")
            {
                List<Lebi_Product_Limit> ls = B_Lebi_Product_Limit.GetList("id in (" + ids + ")", "");
                foreach (var model in ls)
                {
                    model.IsBuy = RequestTool.RequestInt("IsBuy" + model.id);
                    model.IsPriceShow = RequestTool.RequestInt("IsPriceShow" + model.id);
                    model.IsShow = RequestTool.RequestInt("IsShow" + model.id);
                    B_Lebi_Product_Limit.Update(model);
                    if (model.UserLevel_id > 0)
                        Shop.Bussiness.ProductLimit.UserLevelLimitRemove(model.UserLevel_id);
                    if (model.User_id > 0)
                        Shop.Bussiness.ProductLimit.UserLimitRemove(model.User_id);
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void ProductLimit_Del()
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("limitid");
            if (ids != "")
            {
                B_Lebi_Product_Limit.Delete("id in (" + ids + ")");
                List<Lebi_Product_Limit> ls = B_Lebi_Product_Limit.GetList("id in (" + ids + ")", "");
                foreach (var model in ls)
                {
                    if (model.UserLevel_id > 0)
                        Shop.Bussiness.ProductLimit.UserLevelLimitRemove(model.UserLevel_id);
                    if (model.User_id > 0)
                        Shop.Bussiness.ProductLimit.UserLimitRemove(model.User_id);
                }

            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 通过商品修改会员价格
        /// </summary>
        public void Product_Price_Edit()
        {
            if (!EX_Admin.Power("product_user_price", "商品会员价格"))
            {
                AjaxNoPower();
                return;
            }
            int productid = RequestTool.RequestInt("productid");
            Lebi_Product product = B_Lebi_Product.GetModel(productid);
            if (product == null)
                product = new Lebi_Product();
            string userid = RequestTool.RequestString("userid");
            if (userid == "")
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            List<Lebi_User> ps = B_Lebi_User.GetList("id in (" + userid + ")", "");
            foreach (Lebi_User user in ps)
            {
                Lebi_Product_Price model = B_Lebi_Product_Price.GetModel("Product_id=" + product.id + " and User_id=" + user.id + "");
                if (model == null)
                    model = new Lebi_Product_Price();
                model.Price = RequestTool.RequestDecimal("Price" + user.id);
                model.User_id = user.id;
                model.Product_id = product.id;
                if (model.Price == 0)
                {
                    B_Lebi_Product_Price.Delete(model.id);
                }
                else
                {
                    if (model.id == 0)
                        B_Lebi_Product_Price.Add(model);
                    else
                    {
                        B_Lebi_Product_Price.Update(model);
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
            return;
        }
        public void Product_Price_Update()
        {
            if (!EX_Admin.Power("product_user_price", "商品会员价格"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            if (ids != "")
            {
                List<Lebi_Product_Price> ls = B_Lebi_Product_Price.GetList("id in (" + ids + ")", "");
                foreach (var model in ls)
                {
                    model.Price = RequestTool.RequestDecimal("Price" + model.id);
                    B_Lebi_Product_Price.Update(model);
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void Product_Price_Del()
        {
            if (!EX_Admin.Power("product_user_price", "商品会员价格"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            if (ids != "")
            {
                B_Lebi_Product_Price.Delete("id in (" + ids + ")");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}