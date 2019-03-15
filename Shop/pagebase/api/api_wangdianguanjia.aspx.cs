using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Shop.api
{
    public partial class api_wangdianguanjia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userCode = ShopCache.GetBaseConfig().APIPassWord;
            string uCode = RequestTool.RequestSafeString("uCode");
            if (userCode != uCode)
            {
                Response.Write("<rsp><result>0</result><cause>验证失败</cause></rsp>");
                return;
            }

            string action = LB.Tools.RequestTool.RequestString("mType");

            ////==============
            //string t1 = RequestTool.RequestString("TimeStamp1");
            //string t2 = RequestTool.RequestString("TimeStamp2");
            //DateTime TimeStamp1 = RequestTool.RequestTime("TimeStamp1");
            //string OrderNO = RequestTool.RequestString("OrderNO");
            //Log.Add(userCode + "--" + action + "--" + t1 + "--" + t2 + "-" + TimeStamp1 + "--单号" + OrderNO);
            //=============


            if (action != "mTest" && action != "mOrderSearch" && action != "mGetOrder" && action != "mUpdateStock" && action != "mSndGoods")
            {
                Response.Write("<rsp><result>0</result><cause>验证失败</cause></rsp>");
                return;
            }

            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);
        }
        #region 工具
        private string FormatXMLStr(string instr)
        {
            return "<![CDATA[" + instr + "]]>";
        }
        private DateTime ConvertDate(string datestr)
        {
            DateTime conTime = new DateTime();
            DateTime firstTime = new DateTime(1970, 1, 1);
            int stime = -1;
            if (int.TryParse(datestr, out stime) == false)
            {
                conTime = Convert.ToDateTime(datestr);
            }
            else
            {
                conTime = Convert.ToDateTime(firstTime.AddSeconds(stime));
            }
            return conTime;
        }
        #endregion
        /// <summary>
        /// 测试
        /// </summary>
        public void mTest()
        {
            //string res = "<rsp><result>0</result><cause>拒绝原因</cause></rsp>";
            string res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<rsp><result>1</result></rsp>";
            Response.Write(res);
        }
        /// <summary>
        /// 订单查询,订单数量
        /// </summary>
        public void mOrderSearch()
        {
            string t1 = RequestTool.RequestString("TimeStamp1");
            string t2 = RequestTool.RequestString("TimeStamp2");
            StringBuilder sb = new System.Text.StringBuilder();
            string where = "IsPaid=1";
            if (t1 != "0" && t1 != "")
            {
                DateTime TimeStamp1 = ConvertDate(t1);
                where += " and Time_Add>='" + TimeStamp1 + "'";
            }
            if (t2 != "0" && t2 != "")
            {
                DateTime TimeStamp2 = ConvertDate(t2);
                where += " and Time_Add<='" + TimeStamp2 + "'";
            }
            List<Lebi_Order> orders = B_Lebi_Order.GetList("", "id desc");
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
            sb.Append("<OrderList>\r\n");
            foreach (Lebi_Order order in orders)
            {
                sb.Append(" <OrderNO>" + order.Code + "</OrderNO>\r\n");
            }
            sb.Append(" <OrderCount>" + orders.Count + "</OrderCount>\r\n");
            sb.Append("</OrderList>\r\n");
            Log.Add(sb.ToString(), "网店管家查询订单", "");
            Response.Write(sb.ToString());
        }
        /// <summary>
        /// 订单下载，获得一个订单的详细信息
        /// </summary>
        public void mGetOrder()
        {
            string OrderNO = RequestTool.RequestString("OrderNO");
            Lebi_Order model = B_Lebi_Order.GetModel("Code=lbsql{'" + OrderNO + "'}");
            Lebi_Language_Code lang = Language.DefaultLanguage();
            if (model == null)
            {
                Response.Write("无此单号");
                return;
            }
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
            StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
            sb.Append("<Order>\r\n");
            sb.Append("<Ver>2.0</Ver>\r\n");
            //OrderNO	订单号
            sb.Append("<OrderNO>" + model.Code + "</OrderNO>\r\n");
            //DateTime	成交日期
            sb.Append("<DateTime>" + model.Time_Add.ToString("yyyy-MM-dd HH:mm:ss") + "</DateTime>\r\n");
            //BuyerID	买家用户名
            sb.Append("<BuyerID>" + FormatXMLStr(model.User_UserName) + "</BuyerID>\r\n");
            //BuyerName	买家姓名
            sb.Append("<BuyerName>" + FormatXMLStr(model.T_Name) + "</BuyerName>\r\n");
            //Country	国家
            sb.Append("<Country>" + FormatXMLStr(EX_Area.GetAreaNameNoPath(model.T_Area_id, 4)) + "</Country>\r\n");
            //Province	省/州
            sb.Append("<Province>" + FormatXMLStr(EX_Area.GetAreaNameNoPath(model.T_Area_id, 3)) + "</Province>\r\n");
            //City	市/县
            sb.Append("<City>" + FormatXMLStr(EX_Area.GetAreaNameNoPath(model.T_Area_id, 2)) + "</City>\r\n");
            //Town	区/镇
            sb.Append("<Town>" + FormatXMLStr(EX_Area.GetAreaNameNoPath(model.T_Area_id, 1)) + "</Town>\r\n");
            //Adr	地址
            sb.Append("<Adr>" + FormatXMLStr(model.T_Address) + "</Adr>\r\n");
            //Zip	邮编
            sb.Append("<Zip>" + FormatXMLStr(model.T_Postalcode) + "</Zip>\r\n");
            //Email	Email
            sb.Append("<Email>" + FormatXMLStr(model.T_Email) + "</Email>\r\n");
            //Phone	联系电话
            sb.Append("<Phone>" + FormatXMLStr(model.T_MobilePhone) + "</Phone>\r\n");
            //Total	货款总额
            sb.Append("<Total>" + model.Money_Product + "</Total>\r\n");
            //Postage	货运费用
            sb.Append("<Postage>" + model.Money_Transport + "</Postage>\r\n");
            //PayAccount	支付方式
            sb.Append("<PayAccount>" + FormatXMLStr(Language.Content(model.Pay, lang.Code)) + "</PayAccount>\r\n");
            //PayID	支付编号
            sb.Append("<PayID></PayID>\r\n");
            //LogisticsName	发货方式
            sb.Append("<LogisticsName>" + FormatXMLStr(model.Transport_Name) + "</LogisticsName>\r\n");
            //Chargetype	结算方式
            sb.Append("<Chargetype>" + FormatXMLStr(Language.Content(model.OnlinePay, lang.Code)) + "</Chargetype>\r\n");
            //CustomerRemark	客户备注
            //发票信息

            sb.Append("<CustomerRemark>" + FormatXMLStr(model.Remark_User) + "</CustomerRemark>\r\n");
            //Remark	客服备注
            sb.Append("<Remark>" + FormatXMLStr(model.Remark_Admin) + "</Remark>\r\n");

            foreach (Lebi_Order_Product pro in pros)
            {
                Lebi_Product p = B_Lebi_Product.GetModel(pro.Product_id);
                if (p == null)
                    continue;
                sb.Append(" <Item>\r\n");
                //GoodsID	库存编码
                sb.Append(" <GoodsID>" + FormatXMLStr(pro.Product_Number) + "</GoodsID>\r\n");
                //GoodsName	货品名称
                sb.Append(" <GoodsName>" + FormatXMLStr(Language.Content(pro.Product_Name, lang.Code)) + "</GoodsName>\r\n");
                //GoodsSpec	货品规格
                //sb.Append(" <GoodsSpec>" + FormatXMLStr(EX_Product.ProPertyNameStr(p.ProPerty131, lang)) + "</GoodsSpec>\r\n");
                sb.Append(" <GoodsSpec></GoodsSpec>\r\n");//书生定制
                //Count	数量
                sb.Append(" <Count>" + pro.Count + "</Count>\r\n");
                //Price	单价
                sb.Append(" <Price>" + pro.Price + "</Price>\r\n");
                sb.Append(" </Item>\r\n");

            }


            sb.Append("</Order>\r\n");
            Log.Add(sb.ToString(), "网店管家下载订单", "");
            Response.Write(sb.ToString());


        }
        /// <summary>
        /// 同步库存
        /// </summary>
        public void mUpdateStock()
        {
            //mType	请求类别，同步库存时，该值为“mUpdateStock”。
            //GoodsNO	货品编号
            //BarCode	货品条码(主条码)
            //Stock	库存量
            string GoodsNO = RequestTool.RequestString("GoodsNO");
            int Stock = RequestTool.RequestInt("Stock");
            string res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
            Lebi_Product product = B_Lebi_Product.GetModel("Number=lbsql{'" + GoodsNO + "'}");
            if (product == null)
            {
                res += "<rsp><result>0</result><cause>无此商品</cause></rsp>";
                Response.Write(res);
            }
            product.Count_Stock = Stock;
            B_Lebi_Product.Update(product);
            if (product.Product_id > 0)
            {
                //子商品库存变更，修改父商品库存
                Lebi_Product model = B_Lebi_Product.GetModel(product.Product_id);
                if (model != null)
                {
                    List<Lebi_Product> pros = B_Lebi_Product.GetList("Product_id=" + model.id + "", "");
                    int count = 0;
                    foreach (Lebi_Product pro in pros)
                    {
                        count = count + pro.Count_Stock;
                    }
                    model.Count_Stock = count;
                    B_Lebi_Product.Update(model);
                }
            }
            res += "<rsp><result>1</result></rsp>";
            Log.Add("商品编码：" + GoodsNO + "数量：" + Stock + "--" + res, "网店管家同步库存", "");
            Response.Write(res);
        }

        /// <summary>
        /// 订单发货通知-不更新库存
        /// </summary>
        public void mSndGoods()
        {
            //OrderID	订单编号
            //OrderNO	原始订单编号
            //CustomerID	客户网名
            //SndStyle	发货方式
            //BillID	货运单号
            //SndDate	发货时间
            string OrderID = RequestTool.RequestString("OrderID");
            string OrderNO = RequestTool.RequestString("OrderNO");
            string CustomerID = RequestTool.RequestString("CustomerID");
            string SndStyle = RequestTool.RequestString("SndStyle");
            string BillID = RequestTool.RequestString("BillID");
            string SndDate = RequestTool.RequestString("SndDate");
            Lebi_Order model = B_Lebi_Order.GetModel("Code=lbsql{'" + OrderNO + "'}");
            Lebi_Language_Code lang = Language.DefaultLanguage();
            string res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
            if (model == null)
            {
                res += "<rsp><result>0</result><cause>无此单号</cause></rsp>";
                Response.Write(res);
                return;
            }

            Lebi_Transport tran = B_Lebi_Transport.GetModel("[Name]=lbsql{'" + SndStyle + "'}");
            if (tran == null)
                tran = new Lebi_Transport();
            Lebi_Transport_Order torder = new Lebi_Transport_Order();
            torder.Code = BillID;
            torder.Order_id = model.id;

            torder.T_Address = model.T_Address;
            torder.T_Email = model.T_Email;
            torder.T_MobilePhone = model.T_MobilePhone;
            torder.T_Name = model.T_Name;
            torder.T_Phone = model.T_Phone;
            torder.Transport_Code = tran == null ? model.Transport_Code : tran.Code;
            torder.Transport_id = tran == null ? model.Transport_id : tran.id;
            torder.Transport_Name = tran == null ? model.Transport_Name : tran.Name;
            torder.User_id = model.User_id;
            List<TransportProduct> tps = new List<TransportProduct>();
            TransportProduct tp;
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
            bool isfahuo_all = true;
            foreach (Lebi_Order_Product pro in pros)
            {

                tp = new TransportProduct();
                tp.Count = pro.Count;
                tp.ImageBig = pro.ImageBig;
                tp.ImageMedium = pro.ImageMedium;
                tp.ImageOriginal = pro.ImageOriginal;
                tp.ImageSmall = pro.ImageSmall;
                tp.Product_Number = pro.Product_Number;
                tp.Product_id = pro.Product_id;
                tp.Product_Name = pro.Product_Name;
                tps.Add(tp);

                pro.Count_Shipped = pro.Count;

                B_Lebi_Order_Product.Update(pro);
                //更新库存
                Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                EX_Product.ProductStock_Change(product, (0 - pro.Count), 302, model);
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            torder.Product = jss.Serialize(tps);
            torder.Type_id_TransportOrderStatus = 220;//默认状态：在途
            B_Lebi_Transport_Order.Add(torder);
            model.IsShipped = 1;
            model.IsShipped_All = isfahuo_all ? 1 : 0;
            model.Time_Shipped = System.DateTime.Now; ;
            B_Lebi_Order.Update(model);
            Log.Add("订单发货-网站管家", "Order", model.id.ToString());
            //发送邮件
            if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("dingdanfahuo"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                Email.SendEmail_ordershipping(user, model, torder);
            }
            res += "<rsp><result>1</result></rsp>";
            Response.Write(res);
            Log.Add("订单编号：" + OrderNO + "--" + res, "网店管家同步发货", "");
        }
    }
}