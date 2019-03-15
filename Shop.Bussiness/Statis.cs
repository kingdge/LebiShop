using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;

namespace Shop.Bussiness
{
    public class Statis
    {
         /// <summary>
        /// 返回销售商品数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int ProductCount(string where)
        {
            string sum = B_Lebi_Order_Product.GetValue("sum(Count)", "Order_id in (select id from Lebi_Order where " + where + ")");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToInt32(sum);
        }
        /// <summary>
        /// 返回订单总金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static decimal MoneyCount(string where)
        {
            string sum = B_Lebi_Order.GetValue("sum(Money_Order)", "" + where + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToDecimal(sum);
        }
        /// <summary>
        /// 返回订单商品金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static decimal Money_ProductCount(string where)
        {
            string sum = B_Lebi_Order.GetValue("sum(Money_Product)", "" + where + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToDecimal(sum);
        }
        /// <summary>
        /// 返回订单运费金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static decimal Money_TransportCount(string where)
        {
            string sum = B_Lebi_Order.GetValue("sum(Money_Transport)", "" + where + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToDecimal(sum);
        }
        /// <summary>
        /// 返回订单税金金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static decimal Money_BillCount(string where)
        {
            string sum = B_Lebi_Order.GetValue("sum(Money_Bill)", "" + where + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToDecimal(sum);
        }
        /// <summary>
        /// 返回成本
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static decimal Money_CostCount(string where)
        {
            List<Lebi_Order_Product> models = B_Lebi_Order_Product.GetList("Order_id in (select id from Lebi_Order where " + where + ") and Price_Cost = 0", "");
            if (models.Count > 0)
            {
                foreach (Lebi_Order_Product m in models)
                {
                    m.Price_Cost = Shop.Bussiness.EX_Product.GetProduct(m.Product_id).Price_Cost;
                    B_Lebi_Order_Product.Update(m);
                }
            }
            string sum = B_Lebi_Order_Product.GetValue("sum(Price_Cost * Count)", "Order_id in (select id from Lebi_Order where " + where + ")");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToDecimal(sum);
        }
        /// <summary>
        /// 返回利润
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static decimal Money_ProfitCount(string where)
        {
            string sum = B_Lebi_Order.GetValue("sum(Money_product - Money_Cost - Money_Cut - Money_Transport_Cut)", "" + where + "");
            if (String.IsNullOrEmpty(sum))
                sum = 0.ToString();
            return Convert.ToDecimal(sum);
        }
    }
}
