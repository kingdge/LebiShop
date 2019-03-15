using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Linq;
using System.Collections.Specialized;
using Shop.Bussiness;
namespace Shop
{

    public class P_Product : ShopPage
    {
        protected List<Lebi_Node> nodes;
        protected Lebi_Product product;
        protected Lebi_Pro_Type Protype;
        protected List<LBimage> images;
        //protected string ProductProperty;
        protected List<KeyValue> ProPertys;//商品的自定义文字属性
        protected int ProductStar;//整数的商品评分，最大为5
        protected int Pro_Type_id;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            pageindex = RequestTool.RequestInt("page", 1);
            LoadTheme(themecode, siteid, languagecode, pcode);
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            int id = Rint_Para("0");
            product = EX_Product.GetProduct(id);
            if (product.id == 0)
            {
                Response.Redirect(URL("P_404", ""));
                Response.End();
            }
            int num = 0;
            int Count_Views_Show = 0;
            if (SYS.ClickFlag == "0")
            {
                int.TryParse(SYS.ClickNum1, out num);
                Count_Views_Show = num;
            }
            else
            {
                int.TryParse(SYS.ClickNum2, out num);
                Random r = new Random();
                int c = r.Next(1, num);
                Count_Views_Show = c;
            }
            string sql = "update [Lebi_Product] set Count_Views=Count_Views+1,Count_Views_Show=Count_Views_Show+" + Count_Views_Show + " where id=" + id + "";
            Common.ExecuteSql(sql);
            if (product.Product_id > 0)
            {
                sql = "update [Lebi_Product] set Count_Views=Count_Views+1,Count_Views_Show=Count_Views_Show+" + Count_Views_Show + " where Product_id=" + product.Product_id + "";
                Common.ExecuteSql(sql);
            }
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                sql = "update [Lebi_DT_Product] set Count_Views=Count_Views+1,Count_Views_Show=Count_Views_Show+" + Count_Views_Show + " where DT_id = " + DT_id + " and Product_id=" + id + "";
                Common.ExecuteSql(sql);
                if (product.Product_id > 0)
                {
                    Common.ExecuteSql("update [Lebi_DT_Product] set Count_Views=Count_Views+1,Count_Views_Show=Count_Views_Show+" + Count_Views_Show + " where DT_id = " + DT_id + " and Product_id=" + product.Product_id + "");
                }
            }
            Protype = B_Lebi_Pro_Type.GetModel(product.Pro_Type_id);
            images = EX_Product.ProductImages(product, CurrentTheme);
            //===============================================================
            //处理规格选项
            //ProductProperty = Get_guige(product);
            //处理规格选项结束
            //==================================================================


            //添加访问记录
            int Product_id = product.Product_id == 0 ? product.id : product.Product_id;
            EX_User.UserProduct_Edit(CurrentUser, Product_id, 1, 143, "", 0, "");
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em>";
            if (product.Type_id_ProductType == 321)
            {
                path += "<a href=\"" + URL("P_LimitBuy", "") + "\"><span>" + Tag("限时抢购") + "</span></a><em>&raquo;</em>";
            }
            else if (product.Type_id_ProductType == 322)
            {
                path += "<a href=\"" + URL("P_GroupPurchase", "") + "\"><span>" + Tag("团购") + "</span></a><em>&raquo;</em>";
            }
            else if (product.Type_id_ProductType == 323)
            {
                path += "<a href=\"" + URL("P_Exchange", "") + "\"><span>" + Tag("积分换购") + "</span></a><em>&raquo;</em>";
            }
            else
            {
                if (Protype != null)
                {
                    string[,] parr = Categorypath(Protype.id);
                    for (int i = 0; i <= parr.GetUpperBound(0); i++)
                    {
                        path += "<a href=\"" + URL("P_ProductCategory", "" + parr[i, 0] + "") + "\"><span>" + parr[i, 1] + "</span></a><em>&raquo;</em>";
                    }
                }
            }
            path += "<a href=\"" + URL("P_Product", id) + "\"><span>" + Lang(product.Name) + "</span></a>";


            GetProWords();
            ProductStar = Convert.ToInt32(product.Star_Comment);
            if (ProductStar > 5)
                ProductStar = 5;
            if (ProductStar < 0)
                ProductStar = 0;
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            string Page_Title = Lang(product.Name);
            string Page_Keywords = Lang(product.Name);
            if (Protype != null)
            {
                string[,] parr = Categorypath(Protype.id);
                for (int i = parr.GetUpperBound(0); i >= 0; i--)
                {
                    Page_Title += " - " + parr[i, 1] + "";
                    Page_Keywords += "," + parr[i, 1] + "";
                }
            }
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(product.SEO_Description) == "")
                        str = Page_Keywords;
                    else
                        str = Lang(product.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(product.SEO_Keywords) == "")
                        str = Page_Keywords;
                    else
                        str = Lang(product.SEO_Keywords);
                    break;
                default:
                    if (Lang(product.SEO_Title) == "")
                        str = Page_Title;
                    else
                        str = Lang(product.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
        /// <summary>
        /// 返回商品的自定义文字属性
        /// </summary>
        /// <returns></returns>
        public void GetProWords()
        {
            ProPertys = new List<KeyValue>();
            try
            {
                List<KeyValue> kvs = Common.KeyValueToList(product.ProPerty133);
                foreach (KeyValue kv in kvs)
                {
                    if (Lang(kv.V) == "")
                        continue;
                    Lebi_ProPerty pro = B_Lebi_ProPerty.GetModel("id=" + kv.K + "");
                    KeyValue rkv = new KeyValue();
                    rkv.K = Lang(pro.Name);
                    rkv.V = Lang(kv.V);
                    ProPertys.Add(rkv);
                }
            }
            catch
            {
                ProPertys = new List<KeyValue>();
            }
        }
        /// <summary>
        /// 生成规格选项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Get_guige(Lebi_Product model)
        {
            string res = "";
            if (model.ProPerty134 != "")
            {
                List<Lebi_ProPerty> ps134 = B_Lebi_ProPerty.GetList("id in (" + model.ProPerty134 + ")", "Sort desc");
                string Msige = Language.DefaultCurrency().Msige;
                foreach (Lebi_ProPerty p in ps134)
                {
                    res += "<dl class=\"clearfix\"><dt>" + Lang(p.Name) + "：</dt><dd>";
                    res += "<input type=\"hidden\" name=\"Property134\" propertyid=\"" + p.id + "\" value=\"" + Lang(p.Name) + "\" />";
                    List<Lebi_ProPerty> ps134list = B_Lebi_ProPerty.GetList("parentid = " + p.id + "", "Sort desc");
                    if (ps134list.Count > 0)
                    {
                        Lebi_ProPerty ps134listfirst = ps134list.FirstOrDefault();
                        res += "<select id=\"Property134_" + p.id + "\" selectname=\"Property134\">";
                        foreach (Lebi_ProPerty pl in ps134list)
                        {
                            if (pl.Price > 0)
                                res += "<option propertypriceid=\"" + pl.id + "\" value=\"" + Lang(pl.Name) + Msige + pl.Price + "\">" + Lang(pl.Name) + " " + Msige + pl.Price + "</option>";
                            else
                                res += "<option propertypriceid=\"" + pl.id + "\" value=\"" + Lang(pl.Name) + "\">" + Lang(pl.Name) + "</option>";
                        }
                        res += "</select>";
                    }
                    else
                    {
                        res += "<input type=\"text\" id=\"Property134_" + p.id + "\" class=\"input\" value=\"\" />";
                    }
                    res += "</dd></dl>";
                }
            }
            //return "model.ProPerty134" + model.ProPerty134;
            List<ProductProperty> rmodels = new List<ProductProperty>();
            if (model.Product_id == 0)
            {
                //无同款子商品
                return res;
            }
            List<Lebi_Product> pros = B_Lebi_Product.GetList("(IsDel!=1 or IsDel is null) and Product_id=" + model.Product_id + "", "");
            if (pros.Count == 0)
            {
                return res;
            }
            Lebi_Product pmodel = B_Lebi_Product.GetModel(model.Product_id);
            if (pmodel == null)
            {
                return res;
            }
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id, 131, model.Supplier_id);
            if (property == "")
            {
                return res;
            }
            List<Lebi_ProPerty> pps = B_Lebi_ProPerty.GetList("id in (" + property + ")", "Sort desc");
            List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid in (" + property + ")", "Sort desc");
            //分析当前商品的规格
            //将当前商品的规格与主父规格进行对应
            List<Lebi_ProPerty> Currentpps = new List<Lebi_ProPerty>();//保存当时商品的父规则值
            string[] temps = model.ProPerty131.Split(',');
            foreach (string k in temps)
            {
                Lebi_ProPerty p = (from m in ps
                                   where m.id == Convert.ToInt32(k)
                                   select m).ToList().FirstOrDefault();
                if (p != null)
                {
                    Lebi_ProPerty kv = (from m in pps
                                        where m.id == p.parentid
                                        select m).ToList().FirstOrDefault();
                    kv.Sort = p.id;//临时征用排序字段，存放规格
                    Currentpps.Add(kv);
                }
            }
            foreach (Lebi_ProPerty pp in Currentpps)
            {
                List<Lebi_ProPerty> cps = (from m in ps
                                           where m.parentid == pp.id
                                           select m).ToList();
                if (cps.Count == 0)
                    continue;
                bool showimage = true;
                if (cps.FirstOrDefault().ImageUrl == "")
                {
                    showimage = false;
                }
                if (pp.id == model.ProPertyMain)
                {
                    //res += "<dl class=\"choose-image clearfix\"><dt>" + Lang(pp.Name) + "：</dt><dd>";
                    if (!showimage)
                    {
                        res += "<dl class=\"choose-text clearfix\"><dt>" + Lang(pp.Name) + "：</dt><dd>";
                    }
                    else
                    {
                        res += "<dl class=\"choose-image clearfix\"><dt>" + Lang(pp.Name) + "：</dt><dd>";
                    }
                    foreach (Lebi_ProPerty p in cps)
                    {
                        //计算对应的商品
                        string propertystr = ("," + model.ProPerty131 + ",").Replace("," + pp.Sort + ",", "," + p.id + ",");
                        Lebi_Product pro = (from m in pros
                                            where ("," + m.ProPerty131 + ",") == propertystr
                                            select m).ToList().FirstOrDefault();
                        if (pro == null)
                            continue;
                        if (showimage)
                        {
                            if (pro.Type_id_ProductStatus == 100 || (EX_Product.ProductStock(pro) < 1 && SYS.IsNullStockSale != "1"))//下架 无库存
                            {
                                if (pp.Sort == p.id)
                                {
                                    res += "<div><a class=\"spva-imgonout\"><img src=\"" + Image(model.ImageOriginal) + "\" width=\"50\" title=\"" + Lang(p.Name) + "\" alt=\"" + Lang(p.Name) + "\"/><span></span></a></div>";
                                }
                                else
                                {
                                    res += "<div><a class=\"spva-imgout\" href=\"" + URL("P_Product", pro.id) + "\"><img src=\"" + Image(pro.ImageOriginal) + "\" width=\"50\" title=\"" + Lang(p.Name) + "\" alt=\"" + Lang(p.Name) + "\"/><span></span></a></div>";
                                }
                                continue;
                            }
                            if (pp.Sort == p.id)
                                res += "<div><a class=\"spva-imgon\" href=\"" + URL("P_Product", model.id) + "\"><img src=\"" + Image(model.ImageOriginal) + "\" width=\"50\" title=\"" + Lang(p.Name) + "\" alt=\"" + Lang(p.Name) + "\"/><span></span></a></div>";
                            else
                                res += "<div><a class=\"spva-img\" href=\"" + URL("P_Product", pro.id) + "\"><img src=\"" + Image(pro.ImageOriginal) + "\" width=\"50\" title=\"" + Lang(p.Name) + "\" alt=\"" + Lang(p.Name) + "\"/><span></span></a></div>";
                        }
                        else
                        {
                            if (pro.Type_id_ProductStatus == 100 || (EX_Product.ProductStock(pro) < 1 && SYS.IsNullStockSale != "1"))//下架 无库存
                            {
                                if (pp.Sort == p.id)
                                {
                                    res += "<div><a class=\"spvaonout\">" + Lang(p.Name) + "<span></span></a></div>";
                                }
                                else
                                {
                                    res += "<div><a class=\"spvaout\" href=\"" + URL("P_Product", pro.id) + "\">" + Lang(p.Name) + "<span></span></a></div>";
                                }
                                continue;
                            }
                            if (pp.Sort == p.id)
                                res += "<div><a class=\"spvaon\" href=\"" + URL("P_Product", model.id) + "\">" + Lang(p.Name) + "<span></span></a></div>";
                            else
                                res += "<div><a class=\"spva\" href=\"" + URL("P_Product", pro.id) + "\">" + Lang(p.Name) + "<span></span></a></div>";
                        }
                    }
                }
                else
                {
                    if (!showimage)
                    {
                        res += "<dl class=\"choose-text clearfix\"><dt>" + Lang(pp.Name) + "：</dt><dd>";
                    }
                    else
                    {
                        res += "<dl class=\"choose-image clearfix\"><dt>" + Lang(pp.Name) + "：</dt><dd>";
                    }
                    foreach (Lebi_ProPerty p in cps)
                    {

                        //计算对应的商品
                        string propertystr = ("," + model.ProPerty131 + ",").Replace("," + pp.Sort + ",", "," + p.id + ",");
                        Lebi_Product pro = (from m in pros
                                            where ("," + m.ProPerty131 + ",") == propertystr
                                            select m).ToList().FirstOrDefault();
                        if (pro == null)
                            continue;
                        string tt = "";
                        string aclass = "";
                        if (p.ImageUrl == "")
                        {
                            tt = Lang(p.Name);
                        }
                        else
                        {
                            tt = "<img src=\"" + p.ImageUrl + "\" width=\"50\"\" />";
                        }

                        if (pro.Type_id_ProductStatus == 100 || (EX_Product.ProductStock(pro) < 1 && SYS.IsNullStockSale != "1"))//下架 无库存
                        {
                            if (pp.Sort == p.id)
                            {
                                if (cps.FirstOrDefault().ImageUrl == "")
                                    aclass = "spvaonout";
                                else
                                    aclass = "spva-imgonout";
                                res += "<div><a class=\"" + aclass + "\" title=\"" + Lang(p.Name) + "\">" + tt + "<span></span></a></div>";
                            }
                            else
                            {
                                if (cps.FirstOrDefault().ImageUrl == "")
                                    aclass = "spvaout";
                                else
                                    aclass = "spva-imgout";
                                res += "<div><a class=\"" + aclass + "\" href=\"" + URL("P_Product", pro.id) + "\" title=\"" + Lang(p.Name) + "\">" + tt + "<span></span></a></div>";
                            }
                            continue;
                        }

                        if (pp.Sort == p.id)
                        {
                            if (!showimage)
                                aclass = "spvaon";
                            else
                                aclass = "spva-imgon";
                            res += "<div><a class=\"" + aclass + "\" href=\"" + URL("P_Product", model.id) + "\" title=\"" + Lang(p.Name) + "\">" + tt + "<span></span></a></div>";
                        }
                        else
                        {

                            if (!showimage)
                                aclass = "spva";
                            else
                                aclass = "spva-img";
                            res += "<div><a class=\"" + aclass + "\" href=\"" + URL("P_Product", pro.id) + "\" title=\"" + Lang(p.Name) + "\">" + tt + "<span></span></a></div>";
                        }
                    }
                }
                res += "</dd></dl>";
            }
            return res;
        }
        public string StepPriceShow(Lebi_Product pro)
        {
            string str = "";
            List<ProductStepPrice> StepPrices = EX_Product.StepPrice(pro.StepPrice);
            if (StepPrices == null)
                StepPrices = new List<ProductStepPrice>();
            if (StepPrices.Count > 0)
            {
                foreach (var v in StepPrices)
                {
                    if (v.Count > 1)
                        str += ">" + v.Count + ":" + FormatMoney(v.Price) + ";";
                }
            }
            str = str.Trim(';');
            return str;
        }
        public string Get_guigeforwap(Lebi_Product model)
        {
            string res = "";
            if (model.ProPerty134 != "")
            {
                List<Lebi_ProPerty> ps134 = B_Lebi_ProPerty.GetList("id in (" + model.ProPerty134 + ")", "Sort desc");
                string Msige = Language.DefaultCurrency().Msige;
                if (ps134.Count > 0)
                {
                    foreach (Lebi_ProPerty p in ps134)
                    {
                        res += "<dd class=\"clearfix\"><em>" + Lang(p.Name) + "：</em><em><input type=\"hidden\" name=\"Property134\" propertyid=\"" + p.id + "\" value=\"" + Lang(p.Name) + Msige + p.Price + "：\" />";
                        List<Lebi_ProPerty> ps134list = B_Lebi_ProPerty.GetList("parentid = " + p.id + "", "Sort desc");
                        if (ps134list.Count > 0)
                        {
                            res += "<select id=\"Property134_" + p.id + "\" >";
                            foreach (Lebi_ProPerty pl in ps134list)
                            {
                                if (pl.Price > 0)
                                    res += "<option propertypriceid=\"" + pl.id + "\" value=\"" + Lang(pl.Name) + Msige + pl.Price + "\">" + Lang(pl.Name) + " " + Msige + pl.Price + "</option>";
                                else
                                    res += "<option propertypriceid=\"" + pl.id + "\" value=\"" + Lang(pl.Name) + "\">" + Lang(pl.Name) + "</option>";
                            }
                            res += "</select>";
                        }
                        else
                        {
                            res += "<input type=\"text\" id=\"Property134_" + p.id + "\" class=\"input\" value=\"\" />";
                        }
                        res += "</em></dd>";

                    }
                }
            }
            List<ProductProperty> rmodels = new List<ProductProperty>();
            if (model.Product_id == 0)
            {
                //无同款子商品
                return res;
            }
            List<Lebi_Product> pros = B_Lebi_Product.GetList("(IsDel!=1 or IsDel is null) and Product_id=" + model.Product_id + "", "");
            if (pros.Count == 0)
            {
                return res;
            }
            Lebi_Product pmodel = B_Lebi_Product.GetModel(model.Product_id);
            if (pmodel == null)
            {
                return res;
            }
            string property = EX_Product.ProductType_ProPertystr(model.Pro_Type_id, 131);
            if (property == "")
            {
                return res;
            }
            List<Lebi_ProPerty> pps = B_Lebi_ProPerty.GetList("id in (" + property + ")", "Sort desc");
            List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid in (" + property + ")", "Sort desc");
            //分析当前商品的规格
            //将当前商品的规格与主父规格进行对应
            List<Lebi_ProPerty> Currentpps = new List<Lebi_ProPerty>();//保存当前商品的父规则值
            string[] temps = model.ProPerty131.Split(',');
            foreach (string k in temps)
            {
                Lebi_ProPerty p = (from m in ps
                                   where m.id == Convert.ToInt32(k)
                                   select m).ToList().FirstOrDefault();
                if (p != null)
                {
                    Lebi_ProPerty kv = (from m in pps
                                        where m.id == p.parentid
                                        select m).ToList().FirstOrDefault();
                    kv.Sort = p.id;//临时征用排序字段，存放规格
                    Currentpps.Add(kv);
                }
            }
            foreach (Lebi_ProPerty pp in Currentpps)
            {
                List<Lebi_ProPerty> cps = (from m in ps
                                           where m.parentid == pp.id
                                           select m).ToList();
                if (cps.Count == 0)
                    continue;


                res += "<dd class=\"cartOption\"><em>" + Lang(pp.Name) + ":</em><em><select name=\"select_same_goods_\" onChange=\"window.location=$(this).val();\">";

                foreach (Lebi_ProPerty p in cps)
                {

                    //计算对应的商品
                    string propertystr = ("," + model.ProPerty131 + ",").Replace("," + pp.Sort + ",", "," + p.id + ",");
                    Lebi_Product pro = (from m in pros
                                        where ("," + m.ProPerty131 + ",") == propertystr
                                        select m).ToList().FirstOrDefault();
                    if (pro == null)
                        continue;
                    if (pp.Sort == p.id)
                    {

                        res += " <option value=\"" + URL("P_Product", pro.id) + "\" selected>" + Lang(p.Name) + "</option>";
                    }
                    else
                    {
                        res += " <option value=\"" + URL("P_Product", pro.id) + "\">" + Lang(p.Name) + "</option>";
                    }


                }

                res += "</select></em><em></em></dd>";

            }
            return res;
        }
    }
}