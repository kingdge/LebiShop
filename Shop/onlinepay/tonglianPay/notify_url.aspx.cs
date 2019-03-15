using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Xml;

namespace tonglianPay
{

    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request["data"];

            string msg = "";
            bool result = false;
            try
            {
                SortedDictionary<string, object> dict = new SortedDictionary<string, object>();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(data);
                XmlNode xmlNode = xmlDoc.DocumentElement;//获取到根节点<xml>
                XmlNodeList nodes = xmlNode.ChildNodes;
                foreach (XmlNode xn in nodes)
                {
                    XmlElement xe = (XmlElement)xn;
                    dict[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
                }

                if (dict["ResultCode"].ToString() == "SUCCESS")
                {
                    string return_sign = dict["Sign"].ToString();

                    string ordercode = dict["OrderId"].ToString();
                    Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + ordercode + "'}");
                    if (order == null)
                    {
                        Response.Write("系统错误");
                        Response.End();
                        return;
                    }
                    Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "tonglianPay");
                    if (pay == null)
                    {
                        Response.Write("系统错误");
                        Response.End();
                        return;
                    }
                    string cal_sign = PayHelp.MakeSign(dict, pay.UserKey);
                    //签名正确
                    if (cal_sign == return_sign)
                    {
                        //Loger.WriteEvent(new Exception(data), "签名完成");
                        Order.OnlinePaySuccess("tonglianPay", order.Code);
                        result = true;
                    }
                    else
                    {
                        //Loger.WriteEvent(new Exception(data), "签名失败");
                    }
                }
                else
                {
                    ////Loger.WriteEvent(new Exception(data), "Fail");
                }
            }
            catch (Exception ex)
            {
                result = false;
                msg = "未知异常";
                //Loger.WriteEvent(ex, "Notify");
            }

            Response.Write(string.Format(@"<xml><return_code><![CDATA[{0}]]></return_code><return_msg><![CDATA[{1}]]></return_msg></xml>", (result ? "SUCCESS" : "FAIL"), msg));
        }
    }
}