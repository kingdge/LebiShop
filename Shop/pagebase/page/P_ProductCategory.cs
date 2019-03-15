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

    public class P_ProductCategory : ShopPage
    {
        protected List<Lebi_Product> products;
        protected Lebi_Pro_Type pro_type;
        protected Lebi_Pro_Type property;
        protected Lebi_Pro_Tag tag;
        protected Lebi_Brand brand;
        protected Lebi_ProPerty ProPerty;
        protected int id;
        protected int pid;
        protected int tid;
        protected int transport;
        protected int stock;
        protected string cid;
        protected string list;
        protected string sort;
        protected string where;
        protected string order;
        protected string ordertmp;
        protected int recordCount;
        protected string HeadPage;
        protected string FootPage;
        protected string[,] CategoryPath = {};
        protected string keyword;
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            id = Rint_Para("0");//商品分类
            pid = Rint_Para("1");//品牌
            cid = Rstring_Para("2");//属性
            list = Rstring_Para("3");//列表或网格
            sort = Rstring_Para("4");//排序
            tid = Rint_Para("5");//商品标签
            transport = Rint_Para("7");//配送 0全部 1商城 2商家
            stock = Rint_Para("8");//是否有库存 1只显示有货
            pageindex = RequestTool.RequestInt("page", 1);
            keyword = Rstring("keyword");
            pro_type = B_Lebi_Pro_Type.GetModel(id);
            if (pro_type == null) { pro_type = new Lebi_Pro_Type(); }
            if (Lang(pro_type.Url) != "")  //如果存在自定义URL 跳转至自定义URL by kingdge 2014-10-30
            {
                Response.Redirect(Lang(pro_type.Url));
                Response.End();
                return;
            }
            property = EX_Product.ProductType_ProPerty(pro_type);
            if (property == null)
            {
                property = new Lebi_Pro_Type(); property.ProPerty132 = "0";
            }
            if (property.ProPerty132 == "")
                property.ProPerty132 = "0";
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em>";
            if (pro_type.id > 0)
            {
                CategoryPath = Categorypath(id);
                for (int i = 0; i <= CategoryPath.GetUpperBound(0); i++)
                {
                    path += "<a href=\"" + URL("P_ProductCategory", "" + CategoryPath[i, 0] + "", CategoryPath[i, 2]) + "\"><span>" + CategoryPath[i, 1] + "</span></a>";
                    if (i < CategoryPath.GetUpperBound(0)) path += "<em>&raquo;</em>";
                }
            }
            else if (tid == 0)
            {
                path += "<a href=\"" + URL("P_ProductCategory", "0") + "\"><span>" + Tag("商品列表") + "</span></a>";
            }
            if (tid > 0)
            {
                tag = B_Lebi_Pro_Tag.GetModel(tid);
                if (tag != null)
                    path += "<em>&raquo;</em><a href=\"" + URL("P_ProductCategory", "" + id + ",$,$,$,$," + tid + "") + "\"><span>" + Lang(tag.Name) + "</span></a>";
            }
            where = ProductWhere +" and Type_id_ProductType <> 323";
            if (pid > 0)
            {
                where += " and Brand_id=" + pid + "";
                brand = B_Lebi_Brand.GetModel(pid);
                path += "<em>&raquo;</em><a href=\"" + URL("P_ProductCategory", "" + id + "," + pid + ",$,$,$,$") + "\"><span>" + Lang(brand.Name) + "</span></a>";
            }
            if (cid != "")
            {
                where += " and " + Categorywhere(cid);
                string _cidlast = "";
                if (cid.IndexOf("$") > -1)
                {
                    string[] cidarr = cid.Split('$');
                    for (int i = 0; i < cidarr.Count(); i++)
                    {
                        if (cidarr[i].IndexOf("|") > -1)
                        {
                            string[] _cids = cidarr[i].Split('|');
                            _cidlast = _cids[1];
                            Lebi_ProPerty ProPerty = B_Lebi_ProPerty.GetModel(int.Parse(_cidlast));
                            if (ProPerty!=null)
                                path += "<em>&raquo;</em><a href=\"" + URL("P_ProductCategory", "" + id + "," + pid + "," + ProPerty.parentid + "|" + ProPerty.id + ",$,$,$") + "\"><span>" + Lang(ProPerty.Name) + "</span></a>";
                        }
                    }
                }
                else
                {
                    if (cid.IndexOf("|") > -1)
                    {
                        string[] _cids = cid.Split('|');
                        _cidlast = _cids[1];
                        Lebi_ProPerty ProPerty = B_Lebi_ProPerty.GetModel(int.Parse(_cidlast));
                        if (ProPerty != null)
                            path += "<em>&raquo;</em><a href=\"" + URL("P_ProductCategory", "" + id + "," + pid + "," + ProPerty.parentid + "|" + ProPerty.id + ",$,$,$") + "\"><span>" + Lang(ProPerty.Name) + "</span></a>";
                    }
                }
            }
            if (id > 0){
                where += " and " + CategoryWhere(id) ;
                //where += " and (Pro_Type_id in (" + Categorywhereforid(id) + ")";
                //if (DataBase.DBType == "sqlserver")
                //{
                //    where += " or Charindex('," + id + ",',','+Pro_Type_id_other+',')>0)";
                //}
                //if (DataBase.DBType == "access")
                //{
                //    where += " or Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
                //}
            }
            if (tid > 0)
            {
                if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "sqlserver")
                {
                    where += " and Charindex('," + tid + ",',','+Pro_Tag_id+',')>0";
                }
                else if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "access")
                {
                    where += " and Instr(','+Pro_Tag_id+',','," + tid + ",')>0";
                }
                else if (LB.DataAccess.DB.BaseUtilsInstance.DBType == "mysql")
                {
                    where += " and Instr(','+Pro_Tag_id+',','," + tid + ",')>0";
                }
            }
            if (transport == 1)
            {
                where += " and IsSupplierTransport  = 0";
            }
            else if (transport == 2)
            {
                where += " and IsSupplierTransport  = 1";
            }
            if (stock == 1) {
                where += " and Count_Stock > 0";
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
            }

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
            else if (sort == "6") { order = " Count_Stock desc"; ordertmp = "a"; }
            else if (sort == "6a") { order = " Count_Stock asc"; ordertmp = ""; }
            else { order = " Sort desc,id desc"; ordertmp = ""; }
            products = B_Lebi_Product.GetList(where, order, PageSize, pageindex);
            recordCount = B_Lebi_Product.Counts(where);
            //id={0}&pid={1}&cid={2}&list={3}&sort={4}&tid={5}&page={6}
            string url = URL("P_ProductCategory", id + "," + pid + "," + cid + "," + list + "," + sort + "," + tid + ",{0}," + transport + "," + stock + ","+keyword+"", Lang(pro_type.Url));
            HeadPage = Shop.Bussiness.Pager.GetPaginationStringForWebSimple(url, pageindex, PageSize, recordCount, CurrentLanguage);
            FootPage = Shop.Bussiness.Pager.GetPaginationStringForWeb(url, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = URL("P_ProductCategory", id + "," + pid + "," + cid + "," + list + "," + sort + "," + tid + "," + (pageindex + 1) + "," + transport + "," + stock + "," + keyword + "", Lang(pro_type.Url));
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            string Page_Title = "";
            string Page_Keywords = "";
            if (cid != "")
            {
                string _cidlast = "";
                if (cid.IndexOf("$") > -1)
                {
                    string[] cidarr = cid.Split('$');
                    for (int i = 0; i < cidarr.Count(); i++)
                    {
                        if (cidarr[i].IndexOf("|") > -1)
                        {
                            string[] _cids = cidarr[i].Split('|');
                            _cidlast = _cids[1];
                            Lebi_ProPerty ProPerty = B_Lebi_ProPerty.GetModel(int.Parse(_cidlast));
                            if (ProPerty != null)
                            {
                                Page_Title += Lang(ProPerty.Name) + " - ";
                                Page_Keywords += Lang(ProPerty.Name) + ",";
                            }
                        }
                    }
                }else {
                    if (cid.IndexOf("|") > -1)
                    {
                        string[] _cids = cid.Split('|');
                        _cidlast = _cids[1];
                        Lebi_ProPerty ProPerty = B_Lebi_ProPerty.GetModel(int.Parse(_cidlast));
                        if (ProPerty != null)
                        {
                            Page_Title += Lang(ProPerty.Name) + " - ";
                            Page_Keywords += Lang(ProPerty.Name) + ",";
                        }
                    }
                }
            }
            if (pid > 0)
            {
                Lebi_Brand brand = B_Lebi_Brand.GetModel(pid);
                Page_Title += Lang(brand.Name) + " - ";
                Page_Keywords += Lang(brand.Name) + ",";
            }
            if (pro_type.id > 0)
            {
                string[,] parr = Categorypath(id);
                for (int i = parr.GetUpperBound(0); i >= 0; i--)
                {
                    Page_Title += parr[i, 1];
                    Page_Keywords += parr[i, 1];
                    if (i < parr.GetUpperBound(0))
                    {
                        Page_Title += " - ";
                        Page_Keywords += ",";
                    }
                }
            }
            if (tid > 0)
            {
                Lebi_Pro_Tag Tag = B_Lebi_Pro_Tag.GetModel(tid);
                if (Tag != null)
                    Page_Title += Lang(Tag.Name) + " - ";
            }
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(pro_type.SEO_Description) == "")
                        str = Page_Keywords;
                    else
                        str = Lang(pro_type.SEO_Description) + ",";
                    break;
                case "keywords":
                    if (Lang(pro_type.SEO_Keywords) == "")
                        str = Page_Keywords;
                    else
                        str = Lang(pro_type.SEO_Keywords) + ",";
                    break;
                default:
                    if (Lang(pro_type.SEO_Title) == "")
                        str = Page_Title;
                    else
                        str = Lang(pro_type.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
        }
    }
}