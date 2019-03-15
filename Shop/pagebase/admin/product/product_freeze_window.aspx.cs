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
    public partial class product_freeze_window : AdminAjaxBase
    {
        protected int id;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestInt("id");
            string sql = "";
            Lebi_Product pro = B_Lebi_Product.GetModel(id);
            int SonCount = B_Lebi_Product.Counts("Product_id = "+ pro.id +"");
            if (SonCount == 0)
            {
                sql = "a.Product_id = " + pro.id;
            }else{
                sql = "a.Product_id in (select Lebi_Product.id from Lebi_Product where Lebi_Product.Product_id = " + pro.id + ")";
            }
            //string where = "select a.id from Lebi_Order_Product as a inner join  Lebi_Order as b on a.Order_id = b.id where Product_Number=lbsql{'" + Number + "'} and a.Count > a.Count_Shipped and b.IsInvalid = 0";
            string where = "select a.id from Lebi_Order_Product as a inner join  Lebi_Order as b on a.Order_id = b.id where " + sql + " and (a.Count - a.Count_Shipped)!=0 and b.IsInvalid = 0 and b.IsCompleted = 0 and b.Type_id_OrderType = 211";
            if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderconfirm")
            {
                where += " and IsVerified=1";
            }
            //where += " and IsShipped=0";
            pros = B_Lebi_Order_Product.GetList("id in ("+where+")", "");
        }
    }
}