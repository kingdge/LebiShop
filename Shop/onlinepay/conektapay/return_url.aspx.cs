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
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace conektapay
{


    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string body = "";
            byte[] byts = new byte[Request.InputStream.Length];
            Request.InputStream.Read(byts, 0, byts.Length);
            string req = System.Text.Encoding.Default.GetString(byts);
            SystemLog.Add(req);
            try
            {
                JObject obj = JObject.Parse(req);
                if (obj["type"].ToString() == "charge.paid")
                {
                    if (obj["data"]["object"]["status"].ToString() == "paid")
                    {
                        string outordercode = obj["data"]["object"]["order_id"].ToString();
                        Lebi_Order order = B_Lebi_Order.GetModel("OnlinePay_Code='" + outordercode + "'");
                        if(order!=null)
                        {
                            Order.OnlinePaySuccess("conekta", order.Code, outordercode);
                            Response.Write("success");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SystemLog.Add(ex.ToString());
            }

        }


    }
}


//{
//  "data": {
//    "object": {
//      "id": "588258fbedbb6e85e7000f95",
//      "livemode": false,
//      "created_at": 1484937467,
//      "currency": "MXN",
//      "payment_method": {
//        "service_name": "OxxoPay",
//        "object": "cash_payment",
//        "type": "oxxo",
//        "expires_at": 1487548800,
//        "store_name": "OXXO",
//        "reference": "93345678901234"
//      },
//      "details": {
//        "name": "Fulanito Pérez",
//        "phone": "+5218181818181",
//        "email": "fulanito@conekta.com",
//        "line_items": [{
//            "name": "Tacos",
//            "unit_price": 1000,
//            "quantity": 12
//        }],
//        "shipping_contact": {
//           "phone": "5555555555",
//           "receiver": "Bruce Wayne",
//           "address": {
//             "street1": "Calle 123 int 2 Col. Chida",
//             "city": "Cuahutemoc",
//             "state": "Ciudad de Mexico",
//             "country": "MX",
//             "postal_code": "06100",
//             "residential": true
//           }
//         },
//        "object": "details"
//      },
//      "object": "charge",
//      "status": "paid",
//      "amount": 13500,
//      "paid_at": 1484937498,
//      "fee": 1421,
//      "customer_id": "",
//      "order_id": "ord_2fshhd1RAEnB5zUfG",
//    },
//    "previous_attributes": {
//      "status": "pending_payment"
//    }
//  },
//  "livemode": false,
//  "webhook_status": "successful",
//  "webhook_logs": [
//    {
//      "id": "webhl_2fshi2CmCGqx4p6go",
//      "url": "<a href="http://www.example.com"">www.example.com"</a>,
//      "failed_attempts": 0,
//      "last_http_response_status": 200,
//      "object": "webhook_log",
//      "last_attempted_at": 1484937503
//    }
//  ],
//  "id": "5882591b5906e7819c0007f1",
//  "object": "event",
//  "type": "charge.paid",
//  "created_at": 1484937499
//}