using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class product_ask : Bussiness.ShopPage
    {
        protected List<Lebi_Comment> models;
        protected string PageString;
        protected int id;
        protected int product_id;
        public void LoadPage()
        {
            id = RequestTool.RequestInt("id",0);
            product_id = RequestTool.RequestInt("product_id", 0);
            pageindex = RequestTool.RequestInt("page", 1);
            int PageSize = RequestTool.RequestInt("pagesize", 10);
            string where = "Parentid = 0 and TableName = 'Product_Ask' and (Keyid = " + id + "";
            if (product_id != 0)
            {
                where += " or Product_id = " + product_id + "";
            }
            where += ") and (Status = 283 or User_id = " + CurrentUser.id + ")";
            models = B_Lebi_Comment.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_Comment.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&id=" + id + "#tab", pageindex, PageSize, recordCount, CurrentLanguage);
        }
    }
}