using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Web.Script.Serialization;

namespace Shop.Admin.order
{
    public partial class selectproduct_window : AdminAjaxBase
    {
        protected Lebi_Order order;
        protected Lebi_Product product;
        protected List<Lebi_Product> models;
        protected Lebi_Order_Product orderproduct;
        protected void Page_Load(object sender, EventArgs e)
        {
            int orderid = RequestTool.RequestInt("orderid", 0);
            int id = RequestTool.RequestInt("id", 0);
            string pnumber = RequestTool.RequestString("pnumber").Trim();
            order = B_Lebi_Order.GetModel(orderid);
            orderproduct = B_Lebi_Order_Product.GetModel(id);
            if (orderproduct == null)
            {
                product = B_Lebi_Product.GetModel("Number=lbsql{'" + pnumber + "'} order by id desc");
                orderproduct = new Lebi_Order_Product();
            }
            else
            {
                product = B_Lebi_Product.GetModel(orderproduct.Product_id);
            }
            if (order == null || product == null)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            //int pid = product.Product_id == 0 ? product.id : product.Product_id;
            if (product.Product_id > 0)
                models = B_Lebi_Product.GetList("Product_id=" + product.Product_id + "", "");
            else
            {
                models = B_Lebi_Product.GetList("Product_id=" + product.id + "", "");
                if (models.Count == 0)
                {
                    models = new List<Lebi_Product>();
                    models.Add(product);
                }
            }

        }
        /// <summary>
        /// 返回规格
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string getproperty(string ids)
        {
            string str = "-";
            List<Lebi_ProPerty> list;
            try
            {
                list = B_Lebi_ProPerty.GetList("id in (lbsql{" + ids + "})", "");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                list = new List<Lebi_ProPerty>();
            }
            foreach (Lebi_ProPerty model in list)
            {
                str += Language.Content(model.Name, CurrentLanguage.Code) + ", ";
            }
            return str;
        }
    }
}