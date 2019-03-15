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
    public class P_ShopProductCategory : ShopPage
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
        protected string keyword;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            pcode = "P_ShopProductCategory";
            LoadTheme(themecode, siteid, languagecode, pcode);
            id = RequestTool.RequestInt("id", 0);//商家ID
            cid = RequestTool.RequestInt("cid", 0);//商品分类
            list = RequestTool.RequestString("list");//列表或网格
            sort = RequestTool.RequestString("sort");//排序
            keyword = RequestTool.RequestSafeString("keyword");//关键词
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
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_ShopIndex", id) + "\"><span>" + Lang(supplier.Name) + "</span></a><em>&raquo;</em><a href=\"" + URL("P_ShopProductCategory", "" + id + "," + cid + "," + list + "," + sort + ",1," + keyword + "") + "\"><span>" + Tag("商品列表") + "</span></a>";

            where = ProductWhere + " and Type_id_ProductType = 320 and Supplier_id = " + id + "";
            if (cid > 0)
            {
                where += " and "+ ShopCategoryWhere(cid);
            }
            if (keyword != "")
            {
                //增加空格划词搜索 by kingdge 2013-09-18
                string wherekeyword = "";
                if (keyword.IndexOf(" ") > -1)
                {
                    string[] keywordsArr;
                    keywordsArr = keyword.Split(new char[1] { ' ' });
                    foreach (string keywords in keywordsArr)
                    {
                        if (keywords != "")
                            if (wherekeyword == "")
                                wherekeyword = "Name like lbsql{'%" + keywords + "%'}";
                            else
                                wherekeyword += " and Name like lbsql{'%" + keywords + "%'}";
                    }
                }
                else
                {
                    wherekeyword = "Name like lbsql{'%" + keyword + "%'}";
                }
                where += " and ((" + wherekeyword + ") or Number like lbsql{'%" + keyword + "%'} or Code like lbsql{'%" + keyword + "%'})";
                path += "<em>&raquo;</em><a class=\"text\"><span>“" + keyword + "”</span></a>";
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
            string url = URL("P_ShopProductCategory", id + "," + cid + "," + list + "," + sort + ",{0}," + keyword + "");
            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple(url, pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = URL("P_ShopProductCategory", id + "," + cid + "," + list + "," + sort + "," + (pageindex + 1) + "," + keyword + "");
            string supplierservicepannelcon = supplier.ServicePanel;
            supplierservicepannel = B_ServicePanel.GetModel(supplierservicepannelcon);

            headcontent = supplier.head;
            longbar = supplier.longbar;
            shortbar = supplier.shortbar;
            //跳转至设置的皮肤页面
            Lebi_Supplier_Skin skin = B_Lebi_Supplier_Skin.GetModel(supplier.Supplier_Skin_id);
            if (skin != null)
            {
                string filename = HttpContext.Current.Request.Url.AbsolutePath.ToString().ToLower();
                if (!filename.Contains("default" + skin.id + ".aspx") && filename.Contains("default.aspx"))
                {
                    string tourl = Shop.Bussiness.Site.Instance.WebPath + "/" + CurrentLanguage.Path + "/shop/default" + skin.id + ".aspx?id=" + supplier.id + "&cid=" + cid + "&list=" + list + "&sort=" + sort + "&page=" + pageindex + "&keyword=" + keyword;
                    tourl = ThemeUrl.CheckPath(tourl);
                    Response.Redirect(tourl);
                    //Response.Write(tourl);
                }
            }

        }

        public List<shopindeximage> Getindeximages(int top)
        {
            List<shopindeximage> indeximgaes = new List<shopindeximage>();
            Lebi_Node node = NodePage.GetNodeByCode("shopindeximages");
            if (node != null)
            {
                List<Lebi_Page> ps = B_Lebi_Page.GetList("Node_id=" + node.id + " and Supplier_id=" + supplier.id + "", "Sort desc", top, 1);
                foreach (Lebi_Page p in ps)
                {
                    shopindeximage img = new shopindeximage();
                    img.image = p.ImageOriginal;
                    img.title = p.Name;
                    img.url = p.url;
                    indeximgaes.Add(img);
                }
            }
            return indeximgaes;
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


        public class shopindeximage
        {
            public string image
            {
                set;
                get;
            }
            public string url
            {
                set;
                get;
            }
            public string title
            {
                set;
                get;
            }
        }
    }
    
}