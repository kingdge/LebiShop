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
    public partial class product_comment : Bussiness.ShopPage
    {
        protected List<Lebi_Comment> models;
        protected string PageString;
        protected int id;
        protected int product_id;
        protected Lebi_Product model;
        protected int Star5Count;
        protected int Star4Count;
        protected int Star3Count;
        protected int Star2Count;
        protected int Star1Count;
        protected int Star5Percent;
        protected int Star4Percent;
        protected int Star3Percent;
        protected int Star2Percent;
        protected int Star1Percent;
        public void LoadPage()
        {
            int PageSize = RequestTool.RequestInt("pagesize", 10);
            pageindex = RequestTool.RequestInt("page", 1);
            id = RequestTool.RequestInt("id",0);
            product_id = RequestTool.RequestInt("product_id", 0);
            string where = "Parentid = 0 and TableName = 'Product' and (Keyid = " + id + "";
            if (product_id != 0)
            {
                where += " or Product_id = " + product_id + "";
            }
            where += ") and (Status = 281 or User_id = " + CurrentUser.id + ")";
            models = B_Lebi_Comment.GetList(where, "id desc", PageSize, pageindex);
            int recordCount = B_Lebi_Comment.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&id=" + id +"#tab", pageindex, PageSize, recordCount, CurrentLanguage);
            model = GetProduct(id);
            //获取星级评分人数
            string Starwhere = "Parentid = 0 and TableName = 'Product' and (Keyid = " + id + "";
            if (product_id != 0)
            {
                Starwhere += " or Product_id = " + product_id + "";
            }
            Starwhere += ")";
            Star5Count = B_Lebi_Comment.Counts(Starwhere + " and Star = 5");
            Star4Count = B_Lebi_Comment.Counts(Starwhere + " and Star = 4");
            Star3Count = B_Lebi_Comment.Counts(Starwhere + " and Star = 3");
            Star2Count = B_Lebi_Comment.Counts(Starwhere + " and Star = 2");
            Star1Count = B_Lebi_Comment.Counts(Starwhere + " and Star = 1");
            if (model.Count_Comment == 0)
            {
                Star5Percent = 0;
                Star4Percent = 0;
                Star3Percent = 0;
                Star2Percent = 0;
                Star1Percent = 0;
            }
            else
            {
                Star5Percent = Star5Count / model.Count_Comment * 100;
                Star4Percent = Star4Count / model.Count_Comment * 100;
                Star3Percent = Star3Count / model.Count_Comment * 100;
                Star2Percent = Star2Count / model.Count_Comment * 100;
                Star1Percent = Star1Count / model.Count_Comment * 100;
            }
        }
    }
}