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
    public partial class product_sales_window : SupplierAjaxBase
    {
        protected string Number;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_product_list", "商品列表"))
            {
                WindowNoPower();
            }
            Number = RequestTool.RequestString("Number");
            string sql = "";
            Lebi_Product pro = B_Lebi_Product.GetModel("Number = lbsql{'" + Number + "'}");
            int SonCount = B_Lebi_Product.Counts("Product_id = " + pro.id + "");
            if (SonCount == 0)
            {
                sql = "Product_Number = lbsql{'" + Number + "'}";
            }
            else
            {
                sql = "Product_Number in (select Lebi_Product.Number from Lebi_Product where Lebi_Product.Product_id = " + pro.id + ")";
            }
            pros = B_Lebi_Order_Product.GetList("" + sql + " and Supplier_id = " + CurrentSupplier.id + " and Order_id in(select Lebi_Order.id from Lebi_Order where Lebi_Order_Product.Order_id = Lebi_Order.id and Lebi_Order.Type_id_OrderType=211 and Lebi_Order.IsCompleted = 1)", "");
        }
    }
}