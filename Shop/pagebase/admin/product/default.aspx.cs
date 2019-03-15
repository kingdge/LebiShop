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
    public partial class default_ : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected string Pro_Type_id;
        protected int status;
        protected int brand;
        protected int tag;
        protected string dateFrom;
        protected string dateTo;
        protected string OrderBy;
        protected string orderstr = "";
        protected List<Lebi_Product> models;
        protected string PageString;
        protected SearchProduct sp;
        protected int Type_id_ProductType;
        protected string mode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_list", "商品列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            sp = new SearchProduct(CurrentAdmin, CurrentLanguage.Code);
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestSafeString("key");
            OrderBy = RequestTool.RequestString("OrderBy");
            mode = RequestTool.RequestString("mode");
            string where = "Product_id=0 and (IsDel!=1 or IsDel is null)";
            if (mode == "recycle")
            {
                where = "IsDel=1";
            }
            if (key != "")
            {
                where += " and (";
                where += " Name like lbsql{'%" + key + "%'} or Number like lbsql{'%" + key + "%'} or Code like lbsql{'%" + key + "%'} or id like lbsql{'%" + key + "%'}";
                List<Lebi_Product> parents = B_Lebi_Product.GetList("Number like lbsql{'%" + key + "%'} or Code like lbsql{'%" + key + "%'} or id like lbsql{'%" + key + "%'}", "");
                if (parents.Count > 0)
                {
                    foreach (Lebi_Product parent in parents)
                    {
                        where += " or id ="+ parent.Product_id + "";
                    }
                }
                where += " )";
            }
            where += sp.SQL;
            if (mode != "recycle")
            {
                if (!string.IsNullOrEmpty(EX_Admin.Project().Site_ids))
                {
                    string[] arr = EX_Admin.Project().Site_ids.Split(',');
                    string sonwhere = "";
                    foreach (string sid in arr)
                    {
                        if (sonwhere == "")
                            sonwhere = "','+Site_ids+',' like '%," + sid + ",%'";
                        else
                            sonwhere += " or ','+Site_ids+',' like '%," + sid + ",%'";
                    }
                    where += " and (" + sonwhere + " or Site_ids='')";
                }
                if (!string.IsNullOrEmpty(EX_Admin.Project().Pro_Type_ids))
                {
                    string[] ids = EX_Admin.Project().Pro_Type_ids.Split(',');
                    string sonwhere = "";
                    foreach (string id in ids)
                    {
                        sonwhere += " or Path like '%," + id + ",%'";
                    }
                    sonwhere = "select id from Lebi_Pro_Type where id in (" + EX_Admin.Project().Pro_Type_ids + ")" + sonwhere;
                    where += " and (Pro_Type_id in (" + sonwhere + "))";
                }
                if (!string.IsNullOrEmpty(EX_Admin.Project().Supplier_ids))
                {
                    where += " and (Supplier_id in (" + EX_Admin.Project().Supplier_ids + "))";
                }
            }
            if (OrderBy == "StatusDesc")
            {
                orderstr = " Type_id_ProductStatus desc";
            }
            else if (OrderBy == "StatusAsc")
            {
                orderstr = " Type_id_ProductStatus asc";
            }
            else if (OrderBy == "ViewsDesc")
            {
                orderstr = " Count_Views desc";
            }
            else if (OrderBy == "ViewsAsc")
            {
                orderstr = " Count_Views asc";
            }
            else if (OrderBy == "SalesDesc")
            {
                orderstr = " Count_Sales desc";
            }
            else if (OrderBy == "SalesAsc")
            {
                orderstr = " Count_Sales asc";
            }
            else if (OrderBy == "CountDesc")
            {
                orderstr = " (Count_Stock+Count_Freeze) desc";
            }
            else if (OrderBy == "CountAsc")
            {
                orderstr = " (Count_Stock+Count_Freeze) asc";
            }
            else if (OrderBy == "Price_CostDesc")
            {
                orderstr = " Price_Cost desc";
            }
            else if (OrderBy == "Price_CostAsc")
            {
                orderstr = " Price_Cost asc";
            }
            else if (OrderBy == "PriceDesc")
            {
                orderstr = " Price desc";
            }
            else if (OrderBy == "PriceAsc")
            {
                orderstr = " Price asc";
            }
            else if (OrderBy == "FreezeDesc")
            {
                orderstr = " Count_Freeze desc";
            }
            else if (OrderBy == "FreezeAsc")
            {
                orderstr = " Count_Freeze asc";
            }
            else if (OrderBy == "SortDesc")
            {
                orderstr = " Sort desc";
            }
            else if (OrderBy == "SortAsc")
            {
                orderstr = " Sort asc";
            }
            else
            {
                orderstr = " id desc";
            }
            models = B_Lebi_Product.GetList(where, orderstr, PageSize, page);
            int recordCount = B_Lebi_Product.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&OrderBy="+ OrderBy + "&key=" + key + "&mode="+ mode + "&" + sp.URL, page, PageSize, recordCount);
            //Response.Write(where);
        }

        public int CountSon(int pid)
        {
            return B_Lebi_Product.Counts("Product_id=" + pid + " and (IsDel!=1 or IsDel is null) and Product_id<>0");
        }
        /// <summary>
        /// 返回某个商品分类下，所有子id的组合
        /// 逗号分隔
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void LicenseWord()
        {
            return;
        }
    }
}