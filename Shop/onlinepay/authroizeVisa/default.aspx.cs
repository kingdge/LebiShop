using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
namespace authroizeVisa
{
    public partial class _Default : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("ERROR");
                Response.End();
                return;
            }
            Lebi_Language language = B_Lebi_Language.GetModel(order.Language_id);
            if (order.IsPaid == 1)
            {
                Response.Write(Language.Tag("已付款", language.Code));
                Response.End();
                return;
            }
            order.Site_id_pay = CurrentSite.id;
            order.Language_id = CurrentLanguage.id;
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "authroize");
            if (pay == null)
            {
                Response.Write("系统错误");
                Response.End();
                return;
            }
            if (pay.FreeFeeRate == 1)
            {
                pay.FeeRate = 0;
            }
            if (pay.FeeRate > 0)
            {
                order.Money_OnlinepayFee = order.Money_Pay * pay.FeeRate / 100;
            }
            if (order.OnlinePay_id != pay.id)
            {
                order.OnlinePay_id = pay.id;
                order.OnlinePay_Code = pay.Code;
                order.OnlinePay = pay.Name;
            }
            B_Lebi_Order.Update(order);
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = pay.UserName,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = pay.Email,
            };

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;

            //create a transaction
            var opaqueDataType = new opaqueDataType
            {
                dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
                dataKey = RequestTool.RequestString("datakey"),
                dataValue = RequestTool.RequestString("datavalue"),

            };

            var decryptPaymentDataRequest = new decryptPaymentDataRequest()
            {
                opaqueData = opaqueDataType,
                callId = RequestTool.RequestString("callid")
            };


            SystemLog.Add("orderid:" + order.id + "datakey:" + RequestTool.RequestString("datakey"));
            SystemLog.Add("orderid:" + order.id + "datavalue:" + RequestTool.RequestString("datavalue"));
            SystemLog.Add("orderid:" + order.id + "callid:" + RequestTool.RequestString("callid"));
            //create controller, execute and get response
            var decryptPaymentDataController = new decryptPaymentDataController(decryptPaymentDataRequest);
            decryptPaymentDataController.Execute();
            var decryptPaymentDataResponse = decryptPaymentDataController.GetApiResponse();

            if (decryptPaymentDataResponse != null)
            {
                //validate response
                //Console.WriteLine("Result : " + decryptPaymentDataResponse.messages.message);
                //Console.WriteLine("       : " + decryptPaymentDataResponse.messages.resultCode);
                //Console.WriteLine("First Name : " + decryptPaymentDataResponse.billingInfo.firstName);
                //Console.WriteLine("Last name  : " + decryptPaymentDataResponse.billingInfo.lastName);
                //Console.WriteLine("Card Number : " + decryptPaymentDataResponse.cardInfo.cardNumber);
                //Console.WriteLine("Amount : " + decryptPaymentDataResponse.paymentDetails.amount);
                if (decryptPaymentDataResponse.messages.resultCode == messageTypeEnum.Ok)
                {
                    Shop.Bussiness.Order.OnlinePaySuccess("authroize", order.Code, "", false);
                    Response.Write("{\"status\":\"ok\",\"msg\":\"" + decryptPaymentDataResponse.messages.message + "\"}");
                }
                else
                {
                    Response.Write("{\"status\":\"error\",\"msg\":\"error1-" + decryptPaymentDataResponse.messages.message + "\"}");
                }

            }
            else
            {
                Response.Write("{\"status\":\"error\",\"msg\":\"error\"}");
            }




        }
    }
}