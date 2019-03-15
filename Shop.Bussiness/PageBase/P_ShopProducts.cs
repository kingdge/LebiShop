using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using Shop.Tools;
using System.Linq;
using System.Collections.Specialized;

namespace Shop.Bussiness
{

    public class P_ShopProducts : ShopPage
    {
        protected List<Lebi_Product> products;
        protected Lebi_Supplier_ProductType producttype;
        protected Lebi_Supplier supplier;
        protected int id;
        protected int cid;
        protected int parentcid;
        protected string list;
        protected string sort;
        protected string where;
        protected string order;
        protected string ordertmp;
        protected int recordCount;
        protected string HeadPage;
        protected string FootPage;
        protected string[,] CategoryPath = { };
        protected string headcontent = "";
        protected string shortbar = "";
        protected string longbar = "";
        protected string key = "";
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            pcode = "P_ShopProducts";
            LoadTheme(themecode, siteid, languagecode, pcode);
            id = Rint_Para("0");//商家ID
            cid = Rint_Para("1");//商品分类
            list = Rstring_Para("2");//列表或网格
            sort = Rstring_Para("3");//排序
            key = Rstring_Para("5");//关键词
            pageindex = RequestTool.RequestInt("page", 1);
            parentcid = 0;
            supplier = B_Lebi_Supplier.GetModel("id = " + id);
            if (supplier == null)
            {
                Response.Redirect(URL("P_404", ""));
                Response.End();
            }
            int Supplier_id = supplier.User_id;
            if (cid != 0)
            {
                producttype = B_Lebi_Supplier_ProductType.GetModel(cid);
                if (producttype.parentid > 0)
                {
                    parentcid = producttype.parentid;
                }
                else
                {
                    parentcid = cid;
                }
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_ShopIndex", id) + "\"><span>" + Lang(supplier.Name) + "</span></a>";
            if (key != "")
                path += " > " + key; 
            
            where = "Type_id_ProductStatus = 101 and Product_id=0 and Supplier_id = " + id + "";
            if (cid > 0)
            {
                if (DataBase.DBType == "sqlserver")
                {
                    where += " and Charindex('," + cid + ",',','+Supplier_ProductType_ids+',')>0";
                }
                if (DataBase.DBType == "access")
                {
                    where += " and Instr(','+Supplier_ProductType_ids+',','," + cid + ",')>0";
                }
            }
            if (key != "")
            {
                where += " and Name like '%" + key + "%'";
            }
            if (sort == "")
                sort = "1";
            if (sort == "1") { order = " Count_Sales_Show desc"; ordertmp = "a"; }
            else if (sort == "1a") { order = " Count_Sales_Show asc"; ordertmp = ""; }
            else if (sort == "2") { order = " Price desc"; ordertmp = "a"; }
            else if (sort == "2a") { order = " Price asc"; ordertmp = ""; }
            else if (sort == "3") { order = " Count_Comment desc"; ordertmp = "a"; }
            else if (sort == "3a") { order = " Count_Comment asc"; ordertmp = ""; }
            else if (sort == "4") { order = " Time_Add desc"; ordertmp = "a"; }
            else if (sort == "4a") { order = " Time_Add asc"; ordertmp = ""; }
            else if (sort == "5") { order = " Count_Views_Show desc"; ordertmp = "a"; }
            else if (sort == "5a") { order = " Count_Views_Show asc"; ordertmp = ""; }
            else { order = " Count_Sales_Show desc"; ordertmp = "a"; }
            products = B_Lebi_Product.GetList(where, order, PageSize, pageindex);
            recordCount = B_Lebi_Product.Counts(where);
            //id={0}&pid={1}&cid={2}&list={3}&sort={4}&tid={5}&page={6}
            string url = URL("P_ShopProducts", id + "," + cid + "," + list + "," + sort + ",{0}," + key + "");
            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple(url, pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, pageindex, PageSize, recordCount, CurrentLanguage);
            string supplierservicepannelcon = supplier.ServicePanel;
            supplierservicepannel = B_ServicePanel.GetModel(supplierservicepannelcon);

            headcontent = supplier.head;
            longbar = supplier.longbar;
            shortbar = supplier.shortbar;

            
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            string Page_Keywords = "";
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(supplier.SEO_Description) == "")
                        str = Page_Keywords;
                    else
                        str = Lang(supplier.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(supplier.SEO_Keywords) == "")
                        str = Page_Keywords;
                    else
                        str = Lang(supplier.SEO_Keywords);
                    break;
                default:
                    if (Lang(supplier.SEO_Title) == "")
                        str = Lang(supplier.Name);
                    else
                        str = Lang(supplier.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }



    }
}