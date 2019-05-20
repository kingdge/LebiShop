using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.product
{
    public partial class product_freeze_window : SupplierAjaxBase
    {
        protected int id;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_product_list", "商品列表"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestInt("id");
            string sql = "";
            Lebi_Product pro = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = "+ id);
            int SonCount = B_Lebi_Product.Counts("Supplier_id = " + CurrentSupplier.id + " and Product_id = " + pro.id + "");
            if (SonCount == 0)
            {
                sql = "Product_id = " + pro.id;
            }else{
                sql = "Product_id in (select Lebi_Product.id from Lebi_Product where Lebi_Product.Product_id = " + pro.id + ")";
            }
            //string where = "select a.id from Lebi_Order_Product as a inner join  Lebi_Order as b on a.Order_id = b.id where Product_Number=lbsql{'" + Number + "'} and a.Count > a.Count_Shipped and b.IsInvalid = 0";
            string where = "select a.id from Lebi_Order_Product as a inner join  Lebi_Order as b on a.Order_id = b.id where b.Supplier_id = " + CurrentSupplier.id + " and " + sql + " and a.Count > a.Count_Shipped and b.IsInvalid = 0";
            if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderconfirm")
                where += " and IsVerified=1";

            pros = B_Lebi_Order_Product.GetList("id in ("+where+")", "");
        }
    }
}