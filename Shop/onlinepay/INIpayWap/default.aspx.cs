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
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace INIpayWap
{
    public partial class INIStdPayRequest : ShopPage
    {
        public string strMid = "";
        public string strVersion = "1.0";
        public string strGoodName = "";
        public string strPrice = "";
        public string strCurrency = "";
        public string strBuyerName = "";
        public string strBuyerTel = "";
        public string strBuyerEmail = "";
        public string oid = "";
        public string signature = "";
        public string returnUrl = "";
        public string signKey = "";
        public string timestamp = "";
        public string mKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
            if (!Page.IsPostBack)
                StartINIStd();

        }

        private void StartINIStd()
        {
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
            Lebi_OnlinePay pay = Shop.Bussiness.Money.GetOnlinePay(order, "INIpayWap");
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
            Lebi_Currency currendy = B_Lebi_Currency.GetModel(pay.Currency_id);
            BaseConfig SYS = ShopCache.GetBaseConfig();
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();

            //각 필드 설정
            //string strMid = "INIwebplat";
            strMid = pay.UserName;
            strVersion = "1.0";
            strGoodName = order.Code;
            strPrice = (order.Money_Pay * currendy.ExchangeRate * (1 + (pay.FeeRate / 100))).ToString("0");
            strCurrency = "WON";

            //Lebi_Transport_Order torder = B_Lebi_Transport_Order.GetModel("Order_id=" + order.id + "");
            //if (torder == null)
            //    torder = new Lebi_Transport_Order();
            strBuyerName = order.User_UserName;
            strBuyerTel = order.T_MobilePhone;
            strBuyerEmail = order.T_Email;




            // TimeStamp 생성
            string timeTemp = "" + DateTime.UtcNow.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
            string[] artime = timeTemp.Split('.');
            timestamp = artime[0];

            //oid = strMid + "_" + timestamp;
            oid = order.Code;
            //Signature 생성 - 알파벳 순으로 정렬후 hash
            string param = "oid=" + oid + "&price=" + strPrice + "&timestamp=" + timestamp;
            signature = ComputeHash(param);

            //closeURL
            //string close = "http://127.0.0.1/close.aspx";
            //closeUrl.Text = close;

            //popupURL
            //string popup = "http://127.0.0.1/popup.aspx";
            //popupUrl.Text = popup;

            //가맹점 전환 페이지 설정
            //string strReturnUrl = "http://127.0.0.1/INIStdPayReturn.aspx";
            string strReturnUrl = "http://" + RequestTool.GetRequestDomain() + "/onlinepay/INIpayWap/INIStdPayReturn.aspx";
            returnUrl = strReturnUrl;

            // 가맹점확인을 위한 signKey 를 해쉬값으로 변경(SHA-256)
            string signKey = pay.UserKey;   // 가맹점에 제공된 키(이니라이트키) (가맹점 수정후 고정) !!!절대!! 전문 데이터로 설정금지
            mKey = ComputeHash(signKey);



        }

        // SHA256  256bit 암호화
        private string ComputeHash(string input)
        {
            System.Security.Cryptography.SHA256 algorithm = System.Security.Cryptography.SHA256Managed.Create();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
            {
                sb.Append(String.Format("{0:x2}", hashedBytes[i]));
            }


            return sb.ToString();
        }





    }
}
