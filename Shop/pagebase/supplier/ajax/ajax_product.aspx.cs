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
using Shop.Bussiness;
using Shop.Tools;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Threading;
namespace Shop.supplier.Ajax
{
    public partial class Ajax_product : SupplierAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Shop.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 生成商品分类选择框
        /// </summary>
        public void GetProductTypeList()
        {
            int id = RequestTool.RequestInt("id", 0);
            int Parentid = 0;
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadGetProductTypeList(id, Parentid); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            Response.Write(Types);
        }
        public string ThreadGetProductTypeList(int id, int Parentid)
        {
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid=" + id + "", "Sort desc");
            Lebi_Pro_Type area = B_Lebi_Pro_Type.GetModel(id);
            if (models.Count == 0)
            {
                if (area == null)
                    Parentid = 0;
                else
                    Parentid = area.Parentid;
                models = B_Lebi_Pro_Type.GetList("Parentid=" + Parentid + "", "Sort desc");

            }
            else
            {
                Parentid = id;
            }
            string str = "<select id=\"Pro_Type_id\" name=\"Pro_Type_id\" shop=\"true\" onchange=\"SelectProductType('Pro_Type_id');\">";
            str += "<option value=\"0\" selected>" + Tag(" 请选择 ") + "</option>";
            foreach (Lebi_Pro_Type model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
            }
            str += "</select>";
            str = CreateProductTypeSelect(Parentid) + str;
            return str;
        }
        private string CreateProductTypeSelect(int id)
        {
            string str = "<select id=\"ProductType_" + id + "\" name=\"ProductType_" + id + "\" shop=\"true\" onchange=\"SelectProductType('ProductType_" + id + "');\">";
            Lebi_Pro_Type area = B_Lebi_Pro_Type.GetModel(id);
            if (area == null)
                return "";
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid=" + area.Parentid + "", "Sort desc");
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Pro_Type model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
            }
            str += "</select> ";
            str = CreateProductTypeSelect(area.Parentid) + str;
            return str;
        }
        /// <summary>
        /// 编辑商品信息
        /// </summary>
        public void Product_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);

            int count = 0;

            Lebi_Product model = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id =" + id);
            if (model == null)
            {
                if (CurrentSupplier.ProductTop > 0)
                {
                    count = B_Lebi_Product.Counts("Supplier_id = " + CurrentSupplier.id + " and Product_id=0");
                    if (count >= CurrentSupplier.ProductTop)
                    {
                        Response.Write("{\"msg\":\"" + Tag("商品数量达到上限") + "\"}");
                        return;
                    }
                }
                model = new Lebi_Product();
            }
            //检查商品货，不能重复
            string Code = RequestTool.RequestSafeString("Code");
            if (Code != "")
            {
                count = B_Lebi_Product.Counts("Code=lbsql{'" + Code + "'} and id!=" + model.id + " and Supplier_id = " + CurrentSupplier.id + " and Product_id=0");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("商品货号已经存在") + "\"}");
                    return;
                }
            }
            string Number = RequestTool.RequestSafeString("Number");
            if (Number == "")
            {
                Number = EX_Product.RandomProductNumber(model.id);
            }
            B_Lebi_Product.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            model.Introduction = Language.RequestStringForUserEditor("Introduction");
            model.Description = Language.RequestStringForUserEditor("Description");
            model.Specification = Language.RequestStringForUserEditor("Specification");
            model.Packing = Language.RequestStringForUserEditor("Packing");
            model.SEO_Title = Language.RequestSafeString("SEO_Title");
            model.SEO_Keywords = Language.RequestSafeString("SEO_Keywords");
            model.SEO_Description = Language.RequestSafeString("SEO_Description");
            model.Service = Language.RequestStringForUserEditor("Service");
            model.MobileDescription = Language.RequestStringForUserEditor("MobileDescription");
            model.Supplier_ProductType_ids = RequestTool.RequestSafeString("Supplier_ProductType_ids");
            model.Supplier_id = CurrentSupplier.id;
            model.Type_id_ProductType = 320;
            model.IsSupplierTransport = CurrentSupplierGroup.IsSubmit;
            model.Number = Number;

           
            //int tid = EX_Product.SuplierTypeid(model.Supplier_ProductType_ids);
            //Lebi_Supplier_ProductType st = B_Lebi_Supplier_ProductType.GetModel(tid);
            //model.Supplier_ProductType_ids = EX_Product.SuplierTypePath(st, st.id.ToString());

            //====================================================
            //更新自定义文字属性
            List<Lebi_ProPerty> pros;
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id,model.Supplier_id);
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
                if (!Power("supplier_product_add", "添加商品"))
                {
                    AjaxNoPower();
                    return;
                }
                model.Type_id_ProductStatus = 102;//默认状态：未审核
                B_Lebi_Product.Add(model);
                id = B_Lebi_Product.GetMaxId();

                Log.Add("添加商品", "Product", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!Power("supplier_product_edit", "编辑商品"))
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
                int UpdateMobileDescription = RequestTool.RequestInt("UpdateMobileDescription", 0);
                int UpdateSEO = RequestTool.RequestInt("UpdateSEO", 0);
                int UpdateUnits_id = RequestTool.RequestInt("UpdateUnits_id", 0);
                int UpdateTags = RequestTool.RequestInt("UpdateTags", 0);
                int UpdateStepPrice = RequestTool.RequestInt("UpdateStepPrice", 0);
                int UpdateUserLevelPrice = RequestTool.RequestInt("UpdateUserLevelPrice", 0);
                int UpdateName = RequestTool.RequestInt("UpdateName", 0);
                int UpdateIntroduction = RequestTool.RequestInt("UpdateIntroduction", 0);
                foreach (Lebi_Product son in sons)
                {
                    //判断是否同步到子商品
                    if (UpdateName == 1)
                        son.Name = model.Name;
                    if (UpdateIntroduction == 1)
                        son.Introduction = model.Introduction;
                    if (UpdateDescription == 1)
                        son.Description = model.Description;
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
                    son.Supplier_id = RequestTool.RequestInt("Supplier_id", 0);
                    son.IsSupplierTransport = RequestTool.RequestInt("IsSupplierTransport", 0);
                    B_Lebi_Product.Update(son);
                }
                Log.Add("编辑商品", "Product", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
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
        /// 编辑商品名称
        /// </summary>
        public void Product_Name_Edit()
        {
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Product model = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Product.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            B_Lebi_Product.Update(model);
            //处理静态页面
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_Product(model, themepage);
            Log.Add("编辑商品名称", "Product", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品内部备注
        /// </summary>
        public void Product_Remark_Edit()
        {
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Product model = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Product.SafeBindForm(model);
            model.Remarks = RequestTool.RequestSafeString("Remarks");
            B_Lebi_Product.Update(model);
            Log.Add("编辑商品内部备注", "Product", id.ToString(), CurrentSupplier, RequestTool.RequestSafeString("Remarks"));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        public void Product_Del()
        {
            if (!Power("supplier_product_del", "删除商品"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("sonproductid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }

            List<Lebi_Product> pros = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and Product_id in (lbsql{" + id + "})", "");
            foreach (Lebi_Product pro in pros)
            {
                id += "," + pro.id;
            }
            if (RequestTool.GetConfigKey("IsDelFalse").ToLower() == "true")
            {
                List<Lebi_Product> ps = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
                foreach (var p in ps)
                {
                    p.IsDel = 1;
                    B_Lebi_Product.Update(p);
                }
            }
            else
            {
                //删除图片
                ImageHelper.LebiImagesDelete("product", id);
                B_Lebi_Product.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            }
            Log.Add("删除商品", "Product", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 更新商品上架状态
        /// </summary>
        public void Product_Status()
        {

            if (!Power("supplier_product_edit", "编辑商品"))
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
            Lebi_Product model = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (model.Type_id_ProductStatus == 102)
            {
                Response.Write("{\"msg\":\"" + Tag("商品未审核") + "\"}");
                return;
            }
            B_Lebi_Product.SafeBindForm(model);
            model.Type_id_ProductStatus = Status;
            B_Lebi_Product.Update(model);
            Log.Add("编辑商品上架状态", "Product", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 图片管理
        /// </summary>
        public void ProductImages()
        {
            string images = RequestTool.RequestSafeString("images");
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
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int pid = RequestTool.RequestInt("pid", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            int randnum = 0;
            try
            {
                randnum = (int)Session["randnum"];
            }
            catch
            {
                randnum = 0;
            }
            if (pid == 0 || (pid > 0 && randnum > 0))
                pid = randnum;
            //int Pro_Type_id = RequestTool.RequestInt("Pro_Type_id", 0);
            string ggs = RequestTool.RequestSafeString("ggs");
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
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id, CurrentSupplier.id);
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
                    CreatePropertyKey(arr, 1, instr, model, randnum, j);
                else
                    Create131Product(model, instr, randnum, j);
            }
            Log.Add("生成商品规格", "Product", tid.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }

        public void CreatePropertyKey(string[] arr, int i, string instr, Lebi_Product pro, int randnum, int loop)
        {
            string str = instr;
            string[] tt = arr[i].Split(',');
            for (int j = 0; j < tt.Length; j++)
            {
                str += "," + tt[j];
                if (i + 1 < arr.Length)
                {
                    CreatePropertyKey(arr, i + 1, str, pro, randnum, j);
                    str = instr;
                }
                else
                {
                    //Response.Write(str + "<br>");
                    Create131Product(pro, str, randnum, j);
                    str = instr;
                }

            }
        }
        /// <summary>
        /// 生成商品数据
        /// </summary>
        /// <param name="model">商品实体</param>
        /// <param name="id131">规格的ID</param>
        public void Create131Product(Lebi_Product model, string id131, int randnum, int loop)
        {
            //if (("," + id131 + ",").Contains(",0,"))
            //{
            //排除所有空值
            //    return;
            //}
            //检查是否已生成
            if (model.id == 0)
                model.id = randnum;
            int count = B_Lebi_Product.Counts("Product_id=" + model.id + " and Property131='" + id131 + "'");
            if (count > 0)
                return;
            List<Lebi_ProPerty> pps = B_Lebi_ProPerty.GetList("id in (" + id131 + ")", "parentSort desc");
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
                pro.Number += "-" + loop;
            pro.Name = model.Name;
            pro.Price = model.Price;
            pro.Price_Cost = model.Price_Cost;
            pro.Price_Market = model.Price_Market;
            pro.Price_Sale = model.Price_Sale;
            pro.ProPerty131 = id131;
            pro.Weight = model.Weight;
            pro.Type_id_ProductStatus = 102;//未审核
            pro.ImageBig = model.ImageBig;
            pro.ImageMedium = model.ImageMedium;
            pro.ImageOriginal = model.ImageOriginal;
            pro.Images = model.Images;
            pro.ImageSmall = model.ImageSmall;
            pro.Brand_id = model.Brand_id;//商品品牌
            pro.Pro_Type_id = model.Pro_Type_id;//商品分类
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
            pro.Supplier_id = model.Supplier_id;
            pro.Count_StockCaution = model.Count_StockCaution;//预警库存同父产品
            pro.Time_Expired = model.Time_Expired;
            pro.Time_Start = model.Time_Start;
            pro.VolumeH = model.VolumeH;
            pro.VolumeL = model.VolumeL;
            pro.VolumeW = model.VolumeW;
            pro.PackageRate = model.PackageRate;
            pro.Supplier_id = model.Supplier_id;
            pro.IsSupplierTransport = model.IsSupplierTransport;
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
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            //string ids = RequestTool.RequestString("sonproductid");
            int pid = RequestTool.RequestInt("pid", 0);
            int randnum = RequestTool.RequestInt("randnum", 0);
            if (pid == 0)
                pid = randnum;
            Lebi_Product modelp = B_Lebi_Product.GetModel(pid);
            if (modelp == null)
            {
                modelp = new Lebi_Product();
            }
            List<Lebi_Product> models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and Product_id=" + pid + "", "");
            int count = 0;
            foreach (Lebi_Product model in models)
            {
                model.Price_Market = RequestTool.RequestDecimal("Price_Market" + model.id + "", 0);
                if (CurrentSupplierGroup.IsSubmit != 0)
                {
                    model.Price = RequestTool.RequestDecimal("Price" + model.id + "", 0);
                }
                model.Price_Cost = RequestTool.RequestDecimal("Price_Cost" + model.id + "", 0);
                model.Count_Stock = RequestTool.RequestInt("Count_Stock" + model.id + "", 0);
                model.Count_Sales_Show = RequestTool.RequestInt("Count_Sales_Show" + model.id + "", 0);
                //model.Type_id_ProductStatus = RequestTool.RequestInt("Type_id_ProductStatus" + model.id + "", 0);
                model.Number = RequestTool.RequestSafeString("Number" + model.id + "");
                B_Lebi_Product.Update(model);
                count = count + model.Count_Stock;
            }
            //更新主商品的库存
            modelp.Count_Stock = count;
            modelp.ProPertyMain = RequestTool.RequestInt("ProPertyMain", 0);
            B_Lebi_Product.Update(modelp);
            Log.Add("更新商品规格价格及库存", "Product", pid.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(modelp.Name, CurrentLanguage.Code));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改商品上架状态--下架
        /// </summary>
        public void Product_Status_Edit_muti()
        {
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            int status = RequestTool.RequestInt("status", 0);
            string ids = RequestTool.RequestSafeString("sonproductid");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要修改的商品") + "\"}");
                return;
            }
            status = status == 1 ? 101 : 100;
            if (CurrentSupplierGroup.IsSubmit == 0)
                status = 100;
            List<Lebi_Product> models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Product model in models)
            {
                if (model.Type_id_ProductStatus == 102)
                    continue;
                model.Type_id_ProductStatus = status;
                B_Lebi_Product.Update(model);
                string sql = "update [Lebi_Product] set Type_id_ProductStatus=" + status + " where Product_id=" + model.id + " and Product_id<>0";
                Common.ExecuteSql(sql);
            }
            Log.Add("更新商品上架状态", "Product", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 批量修改商品图片
        /// </summary>
        public void Product_Image_Edit_muti()
        {
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            string ImageOriginal = RequestTool.RequestString("smalliamge");
            string images = RequestTool.RequestString("images");
            List<Lebi_Product> models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})", "");
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
                        models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and Product_id=" + parent.id + "", "");
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
            //        models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and Product_id=" + parent.id + "", "");
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
            Log.Add("更新商品图片", "Product", ids.ToString(), CurrentSupplier, ImageOriginal.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 回复商品评价
        /// </summary>
        public void Comment_Edit()
        {
            if (!Power("supplier_comment", "商品评价"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Comment model = B_Lebi_Comment.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            // <-{审核商品评价，增加会员积分 56770.kingdge 2013-8-20
            if (model.Status != 281)
            {
                Lebi_User user = B_Lebi_User.GetModel("UserName = '" + model.User_id + "'");
                if (user != null)
                {
                    BaseConfig SYS = ShopCache.GetBaseConfig();
                    Lebi_User_Point modelpoint = new Lebi_User_Point();
                    Log.Add("审核商品评价-添加会员积分", "User_Point", id.ToString(), CurrentSupplier, SYS.CommentPoint);
                    modelpoint.Point = int.Parse(SYS.CommentPoint);
                    modelpoint.Type_id_PointStatus = RequestTool.RequestInt("Type_id_PointStatus", 0);
                    modelpoint.Admin_UserName = CurrentSupplier.UserName;
                    modelpoint.Admin_id = CurrentSupplier.id;
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
            B_Lebi_Comment.Update(model);
            if (RequestTool.RequestSafeString("Content") != "")
            {
                Lebi_Comment newmodel = new Lebi_Comment();
                newmodel.TableName = "Product";
                newmodel.Keyid = model.Keyid;
                newmodel.Admin_UserName = CurrentSupplier.UserName;
                newmodel.Admin_id = CurrentSupplier.id;
                newmodel.Content = RequestTool.RequestSafeString("Content");
                newmodel.Parentid = id;
                newmodel.Status = 281;
                B_Lebi_Comment.Add(newmodel);
                Log.Add("回复商品评价", "Comment", RequestTool.RequestInt("id", 0).ToString(), CurrentSupplier, RequestTool.RequestSafeString("Content"));
            }
            else
            {
                Log.Add("编辑商品评价", "Comment", RequestTool.RequestInt("id", 0).ToString(), CurrentSupplier, model.Content);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新商品评价状态
        /// </summary>
        public void Comment_Update()
        {
            if (!Power("supplier_comment", "商品评价"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("IDS");
            List<Lebi_Comment> models = B_Lebi_Comment.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Comment model in models)
            {
                // <-{审核商品评价，增加会员积分 56770.kingdge 2013-8-20
                if (model.Status != 281 && RequestTool.RequestInt("Status" + model.id, 0) == 281)
                {
                    Lebi_User user = B_Lebi_User.GetModel("UserName = '" + model.User_id + "'");
                    if (user != null)
                    {
                        BaseConfig SYS = ShopCache.GetBaseConfig();
                        Lebi_User_Point modelpoint = new Lebi_User_Point();
                        Log.Add("审核商品评价-添加会员积分", "User_Point", model.id.ToString(), CurrentSupplier, SYS.CommentPoint);
                        modelpoint.Point = int.Parse(SYS.CommentPoint);
                        modelpoint.Type_id_PointStatus = RequestTool.RequestInt("Type_id_PointStatus", 0);
                        modelpoint.Admin_UserName = CurrentSupplier.UserName;
                        modelpoint.Admin_id = CurrentSupplier.id;
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
                model.Admin_UserName = CurrentSupplier.UserName;
                model.Admin_id = CurrentSupplier.id;
                B_Lebi_Comment.Update(model);
            }
            Log.Add("编辑商品评价", "Comment", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品评价
        /// </summary>
        public void Comment_Del()
        {
            if (!Power("supplier_comment", "商品评价"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Comment.Delete("Supplier_id = " + CurrentSupplier.id + " and Parentid in (lbsql{" + id + "})");
            B_Lebi_Comment.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除商品评价", "Comment", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 回复商品咨询
        /// </summary>
        public void Ask_Edit()
        {
            if (!Power("supplier_ask", "商品咨询"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            if (RequestTool.RequestSafeString("Content") == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请填写内容") + "\"}");
                return;
            }
            Lebi_Comment model = B_Lebi_Comment.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.Status = 283;
            B_Lebi_Comment.Update(model);
            Lebi_Comment newmodel = new Lebi_Comment();
            newmodel.TableName = "Product_Ask";
            newmodel.Keyid = model.Keyid;
            newmodel.Admin_UserName = CurrentSupplier.UserName;
            newmodel.Admin_id = CurrentSupplier.id;
            newmodel.Content = RequestTool.RequestSafeString("Content");
            newmodel.Parentid = id;
            newmodel.Status = 283;
            newmodel.Supplier_id = CurrentSupplier.id;
            B_Lebi_Comment.Add(newmodel);
            Log.Add("回复商品咨询", "Comment", RequestTool.RequestInt("id", 0).ToString(), CurrentSupplier, RequestTool.RequestSafeString("Content"));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品咨询
        /// </summary>
        public void Ask_Del()
        {
            if (!Power("supplier_ask", "商品咨询"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Comment.Delete("Supplier_id = " + CurrentSupplier.id + " and Parentid in (lbsql{" + id + "})");
            B_Lebi_Comment.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除商品咨询", "Comment", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑商品信息
        /// </summary>
        public void Product_Batch_Update()
        {
            if (!Power("supplier_product_batch_edit", "批量编辑"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("IDS");
            List<Lebi_Product> models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Product model in models)
            {
                model.Name = Language.RequestSafeString("Name" + model.id + "");
                model.Number = RequestTool.RequestSafeString("Number" + model.id + "");
                model.Price = RequestTool.RequestDecimal("Price" + model.id + "", 0);
                model.Price_Market = RequestTool.RequestDecimal("Price_Market" + model.id + "", 0);
                model.Price_Cost = RequestTool.RequestDecimal("Price_Cost" + model.id + "", 0);
                model.Count_Stock = RequestTool.RequestInt("Count_Stock" + model.id + "", 0);
                model.Count_Sales_Show = RequestTool.RequestInt("Count_Sales_Show" + model.id + "", 0);
                model.Count_Views_Show = RequestTool.RequestInt("Count_Views_Show" + model.id + "", 0);
                model.Type_id_ProductStatus = RequestTool.RequestInt("ProductStatus" + model.id + "", 0);
                B_Lebi_Product.Update(model);
            }
            Log.Add("批量编辑商品", "Product", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量调价
        /// </summary>
        public void Product_Batch_Price_Update()
        {
            if (!Power("supplier_product_batch_price", "批量调价"))
            {
                AjaxNoPower();
                return;
            }
            string step = RequestTool.RequestSafeString("step");
            string dateFrom = RequestTool.RequestSafeString("dateFrom");
            string dateTo = RequestTool.RequestSafeString("dateTo");
            string Pro_Type_id = RequestTool.RequestSafeString("Pro_Type_id");
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
            string where = "Supplier_id = " + CurrentSupplier.id;
            if (dateFrom != "" && dateTo != "")
            {
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";
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
                if (DataBase.DBType == "sqlserver")
                {
                    where += " and Charindex('" + tag + "',Pro_Tag_id)>0";
                }
                if (DataBase.DBType == "access")
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
                }
            }
            Log.Add("批量调价", "Product", step.ToString(), CurrentSupplier, mes);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品分类
        /// </summary>
        public void Class_Edit()
        {
            if (!Power("supplier_pro_type", "商品分类"))
            {
                AjaxNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Supplier_ProductType model = B_Lebi_Supplier_ProductType.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Supplier_ProductType();
            }
            model = B_Lebi_Supplier_ProductType.SafeBindForm(model);
            model.Supplier_id = CurrentSupplier.id;
            model.Name = Language.RequestSafeString("Name");
            model.Url = Language.RequestSafeString("Url");
            model.ImageUrl = RequestTool.RequestSafeString("ImageUrl");
            if (addflag)
            {

                B_Lebi_Supplier_ProductType.Add(model);
                id = B_Lebi_Pro_Type.GetMaxId();
                Log.Add("添加商品分类", "Pro_Type", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                B_Lebi_Supplier_ProductType.Update(model);
                Log.Add("编辑商品分类", "Pro_Type", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            ImageHelper.LebiImagesUsed(model.ImageUrl, "supplierproducttype", id);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        public void Type_Del()
        {
            if (!Power("supplier_pro_type", "商品分类"))
            {
                AjaxNoPower();
            }
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Supplier_ProductType.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            ImageHelper.LebiImagesDelete("supplierproducttype", id);
            Log.Add("删除商品分类", "Pro_Type", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑子商品
        /// </summary>
        public void SubProduct_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Product model = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (!Power("supplier_product_edit", "编辑商品"))
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
            model.Description = Language.RequestSafeString("Description");
            model.MobileDescription = Language.RequestSafeString("MobileDescription");
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
            Log.Add("编辑商品", "Product", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            //处理图片
            ImageHelper.LebiImagesUsed(model.ImageOriginal + "@" + model.Images, "Product", id);
            //处理静态页面
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_Product(model, themepage);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 批量修改自定义商品分类
        /// </summary>
        public void Supplier_ProductType_Edit()
        {
            if (!Power("supplier_product_edit", "编辑商品"))
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
            List<Lebi_Product> models = B_Lebi_Product.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Product model in models)
            {
                model.Supplier_ProductType_ids = RequestTool.RequestSafeString("Supplier_ProductType_ids");
                B_Lebi_Product.Update(model);
                string sql = "update [Lebi_Product] set Supplier_ProductType_ids='" + RequestTool.RequestSafeString("Supplier_ProductType_ids") + "' where Product_id=" + model.id + " and Product_id<>0";
                Common.ExecuteSql(sql);
            }
            Log.Add("更新商品类别", "Product", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑品牌
        /// </summary>
        public void Brand_Edit()
        {
            if (!Power("supplier_brand", "商品品牌"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Brand checkbrand = B_Lebi_Brand.GetModel("Name = '" + Language.RequestSafeString("Name") + "' and Supplier_id = " + CurrentSupplier.id + " and id <> " + id);
            if (checkbrand != null)
            {
                Response.Write("{\"msg\":\"" + Tag("品牌名称已存在") + "\"}");
                return;
            }
            Lebi_Brand model = B_Lebi_Brand.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Brand();
            }
            model = B_Lebi_Brand.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            model.Description = Language.RequestSafeString("Description");
            model.Type_id_BrandStatus = 451;
            if (addflag)
            {
                model.IsRecommend = 0;
                model.Sort = 0;
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_Brand.Add(model);
                id = B_Lebi_Brand.GetMaxId();
                Log.Add("添加品牌", "Brand", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                B_Lebi_Brand.Update(model);
                Log.Add("编辑品牌", "Brand", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            ImageHelper.LebiImagesUsed(model.ImageUrl, "productbrand", id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品品牌
        /// </summary>
        public void Brand_Del()
        {
            if (!Power("supplier_brand", "商品品牌"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Brand.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            //处理图片
            ImageHelper.LebiImagesDelete("productbrand", id);
            Log.Add("删除品牌", "Brand", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑商品品牌
        /// </summary>
        public void Brands_Edit()
        {
            if (!Power("supplier_brand", "商品品牌"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("IDS");
            List<Lebi_Brand> models = B_Lebi_Brand.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Brand model in models)
            {
                model.FirstLetter = RequestTool.RequestSafeString("FirstLetter" + model.id);
                model.Sort = RequestTool.RequestInt("Sort" + model.id, 0);
                model.IsRecommend = RequestTool.RequestInt("IsRecommend" + model.id, 0);
                B_Lebi_Brand.Update(model);
            }
            Log.Add("更新品牌", "Brand", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑属性
        /// </summary>
        public void Property_Edit()
        {
            if (!Power("supplier_property", "属性规格"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string TagName = RequestTool.RequestSafeString("Tag");
            Lebi_ProPerty model = B_Lebi_ProPerty.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                model = new Lebi_ProPerty();
            }
            B_Lebi_ProPerty.SafeBindForm(model);
            model.Name = Language.RequestSafeString("Name");
            //model.Value = Language.RequestString("Value");
            if (model.id == 0)
            {
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_ProPerty.Add(model);
                id = B_Lebi_ProPerty.GetMaxId();
                Log.Add("添加属性规格", "ProPerty", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else{
                B_Lebi_ProPerty.Update(model);
                List<Lebi_ProPerty> sons = B_Lebi_ProPerty.GetList("parentid=" + model.id + "", "");
                foreach (Lebi_ProPerty son in sons)
                {
                    son.parentSort = model.Sort;
                    B_Lebi_ProPerty.Update(son);
                }
                Log.Add("编辑属性规格", "ProPerty", id.ToString(), CurrentSupplier, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            if (TagName != "")  //写入标签 by lebi.kingdge 20150722
            {
                Lebi_ProPerty_Tag tagmodel = B_Lebi_ProPerty_Tag.GetModel("Name = lbsql{'" + TagName + "'}");
                if (tagmodel == null)
                {
                    tagmodel = new Lebi_ProPerty_Tag();
                    tagmodel.Type_id_ProPertyType = model.Type_id_ProPertyType;
                    tagmodel.Supplier_id = CurrentSupplier.id;
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
            if (!Power("supplier_property", "属性规格"))
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
            List<Lebi_ProPerty> models = B_Lebi_ProPerty.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
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
            Log.Add("删除属性规格", "ProPerty", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑属性标签
        /// </summary>
        public void Property_Tag_Edit()
        {
            if (!Power("supplier_property", "属性规格"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ProPerty_Tag model = B_Lebi_ProPerty_Tag.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ProPerty_Tag();
            }
            model = B_Lebi_ProPerty_Tag.SafeBindForm(model);
            if (addflag)
            {
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_ProPerty_Tag.Add(model);
                id = B_Lebi_ProPerty_Tag.GetMaxId();
                Log.Add("添加属性标签", "ProPerty_Tag", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                B_Lebi_ProPerty_Tag.Update(model);
                Log.Add("编辑属性标签", "ProPerty_Tag", id.ToString(), CurrentSupplier, model.Name);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 删除属性标签
        /// </summary>
        public void Property_Tag_Del()
        {
            if (!Power("supplier_property", "属性规格"))
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
            List<Lebi_ProPerty_Tag> models = B_Lebi_ProPerty_Tag.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
            foreach (Lebi_ProPerty_Tag model in models)
            {
                string sql = "update [Lebi_ProPerty] set Tag = '' where Supplier_id = " + CurrentSupplier.id + " and Type_id_ProPertyType = "+ model.Type_id_ProPertyType +" and Tag='" + model.Name + "'";
                Common.ExecuteSql(sql);
                B_Lebi_ProPerty_Tag.Delete(model.id);
            }
            Log.Add("删除属性标签", "ProPerty_Tag", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改属性关联分类
        /// </summary>
        public void Property_Category_Edit()
        {
            if (!Power("supplier_property", "属性规格"))
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
            string ProPerty = "";
            bool addflag = false;
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("id in (lbsql{" + Pro_Type_id + "})", "");
            foreach (Lebi_Pro_Type model in models)
            {
                Lebi_Supplier_ProPerty supplier_property = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + CurrentSupplier.id + " and Pro_Type_id = " + model.id + " and Type_id_ProPertyType = " + tid + "");
                if (supplier_property != null)
                {
                    ProPerty = supplier_property.ProPerty;
                }
                else
                {
                    supplier_property = new Lebi_Supplier_ProPerty();
                    addflag = true;
                }
                ProPerty = "," + supplier_property.ProPerty + ",";
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
                supplier_property.ProPerty = ProPerty;
                if (addflag)
                {
                    supplier_property.Pro_Type_id = model.id;
                    supplier_property.Supplier_id = CurrentSupplier.id;
                    supplier_property.Type_id_ProPertyType = tid;
                    B_Lebi_Supplier_ProPerty.Add(supplier_property);
                }
                else
                {
                    B_Lebi_Supplier_ProPerty.Update(supplier_property);
                }
            }
            Log.Add("更新属性关联分类", "Property", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除属性关联分类
        /// </summary>
        public void Property_Category_Del()
        {
            if (!Power("supplier_property", "属性规格"))
            {
                AjaxNoPower();
                return;
            }
            int pid = RequestTool.RequestInt("pid", 0);
            string ids = RequestTool.RequestSafeString("ids");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请先选择") + "\"}");
                return;
            }
            string ProPerty = "";
            string[] arr_ids = ids.Split(',');
            for (int i = 0; i < arr_ids.Count(); i++)
            {
                Lebi_Supplier_ProPerty supplier_property = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + CurrentSupplier.id + " and Pro_Type_id = " + arr_ids[i] + " and ','+ProPerty+',' like '%," + pid + ",%'");
                if (supplier_property != null)
                {
                    ProPerty = "," + supplier_property.ProPerty + ",";
                    ProPerty = ProPerty.Replace("," + pid + ",", ",");
                    if (ProPerty.IndexOf(",") > -1)
                    {
                        ProPerty = ProPerty.Trim(',');
                    }
                    if (ProPerty != "")
                    {
                        supplier_property.ProPerty = ProPerty;
                        B_Lebi_Supplier_ProPerty.Update(supplier_property);
                    }
                    else
                    {
                        B_Lebi_Supplier_ProPerty.Delete(supplier_property.id);
                    }
                }
            }
            Log.Add("删除属性关联分类", "Property", ids.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}