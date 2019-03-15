using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using System.Text;
using System.Collections.Generic;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace INIpayWeb
{
    public partial class INIStdPayReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
            if (!Page.IsPostBack)
                StartINIStdReturn();
        }

        private void StartINIStdReturn()
        {
            NameValueCollection parameters = Request.Params;

            IEnumerator enumerator = parameters.GetEnumerator();

            StringBuilder sb = new StringBuilder("paramMap : ");
            while (enumerator.MoveNext())
            {
                // get the current query parameter
                string key = enumerator.Current.ToString();

                // insert the parameter into the url
                sb.Append(string.Format("{0}={1}&", key, HttpUtility.UrlEncode(parameters[key])));

            }
            Lebi_OnlinePay pay = B_Lebi_OnlinePay.GetModel("Code='INIpayWeb'");
            if (pay == null)
            {
                SystemLog.Add("INIpayWeb在线支付接口异常");
                return;
            }
            Console.WriteLine(sb.ToString());


            //#####################
            // 인증이 성공일 경우만
            //#####################
            if ("0000".Equals(parameters["resultCode"]))
            {

                Response.Write("####인증성공/승인요청####");
                Response.Write("<br/>");

                Console.WriteLine("####인증성공/승인요청####");

                //############################################
                // 1.전문 필드 값 설정(***가맹점 개발수정***)
                //############################################

                String mid = parameters.Get("mid");                           // 가맹점 ID 수신 받은 데이터로 설정

                String signKey = pay.UserKey;    // 가맹점에 제공된 키(이니라이트키) (가맹점 수정후 고정) !!!절대!! 전문 데이터로 설정금지


                string timeTemp = "" + DateTime.UtcNow.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
                string[] artime = timeTemp.Split('.');
                String timestamp = artime[0];

                String charset = "UTF-8";                                             // 리턴형식[UTF-8,EUC-KR](가맹점 수정후 고정)

                String format = "JSON";                                             // 리턴형식[XML,JSON,NVP](가맹점 수정후 고정)

                String authToken = parameters.Get("authToken");           // 취소 요청 tid에 따라서 유동적(가맹점 수정후 고정)

                String authUrl = parameters.Get("authUrl");                 // 승인요청 API url(수신 받은 값으로 설정, 임의 세팅 금지)

                String netCancel = parameters.Get("netCancelUrl");      // 망취소 API url(수신 받은 값으로 설정, 임의 세팅 금지)

                String mKey = ComputeHash(signKey);                   // 가맹점 확인을 위한 signKey를 해시값으로 변경 (SHA-256방식 사용)




                //#####################
                // 2.signature 생성
                //#####################

                String signParam = "authToken=" + @authToken + "&timestamp=" + timestamp;

                String signature = ComputeHash(signParam);


                //#####################
                // 3.API 요청 전문 생성
                //#####################
                System.Collections.Generic.Dictionary<String, String> authMap = new System.Collections.Generic.Dictionary<String, String>();

                authMap.Add("mid", mid);                        // 필수
                authMap.Add("authToken", HttpUtility.UrlEncode(authToken)); // 필수 - 반드시 urlencode 해서 전달.
                authMap.Add("timestamp", timestamp);                // 필수
                authMap.Add("signature", signature);                // 필수            
                authMap.Add("charset", charset);                    // default=UTF-8
                authMap.Add("format", format);                  // default=XML
                authMap.Add("mkey", mKey);                  // default=XML


                Console.WriteLine("##승인요청 API 요청##");

                try
                {
                    //#####################
                    // 4.API 통신 시작
                    //#####################
                    String authResultString = "";
                    authResultString = processHTTP(authMap, authUrl);

                    //############################################################
                    //5.API 통신결과 처리(***가맹점 개발수정***)
                    //############################################################
                    Response.Write("## 승인 API 결과 ##");
                    String strReplace = authResultString.Replace(",", "&").Replace(":", "=").Replace("\"", "").Replace(" ", "").Replace("\n", "").Replace("}", "").Replace("{", "");
                    System.Collections.Generic.Dictionary<string, string> resultMap = parseStringToMap(strReplace);         //문자열을 MAP형식으로 파싱

                    Response.Write("<pre>");
                    Response.Write("<table width='565' border='0' cellspacing='0' cellpadding='0'>");

                    /*************************  결제보안 추가 START ****************************/
                    Dictionary<String, String> secureMap = new Dictionary<String, String>();
                    secureMap.Add("mid", mid);                                //mid
                    secureMap.Add("tstamp", timestamp);                     //timestemp
                    secureMap.Add("MOID", resultMap["MOID"]);           //MOID
                    secureMap.Add("TotPrice", resultMap["TotPrice"]);   //TotPrice

                    // signature 데이터 생성 
                    String secureSignature = makeSignatureAuth(secureMap);
                    /*************************  결제보안 추가 END ****************************/

                    if ("0000".Equals((resultMap.ContainsKey("resultCode") ? resultMap["resultCode"] : "null")) && secureSignature.Equals(resultMap["authSignature"]))  //결제보안 추가
                    {


                        string s = "";
                        foreach (var k in resultMap.Keys)
                        {
                            s += k + " : " + resultMap[k] + "\r\n";
                        }

                        //SystemLog.Add("INIpay支付结果，参数：" + s);
                        //SystemLog.Add("INIpay支付结果，订单号：" + resultMap["MOID"]);
                        // VACT_Date: 20171228
                        // VACT_InputName: jinjiani
                        // VACT_Name : 주식회사타오한
                        // EventCode : 
                        // currency: WON
                        // resultMsg : 정상처리되었습니다.
                        // p_SubCnt : 
                        // authSignature: 303f0d74bc4cb75a0af26b3decede82676b9db93a80c6cd59e03c68fb0843591
                        // VACT_Num : 27484423118228
                        // VACT_BankCode: 20
                        // custEmail: swh2046 @naver.com
                        // VACT_Time : 235959
                        // resultCode: 0000
                        // TotPrice: 6601
                        // tid: StdpayVBNKtaohan582520171128105240669600
                        // MOID : 171128095205603
                        // buyerTel: 13003311010
                        // goodName: 171128095205603
                        // payMethod: VBank
                        // p_Sub : 
                        // payDevice: PC
                        // buyerEmail : 
                        // buyerName: jinjiani
                        // vactBankName : 우리은행
                        // applDate : 20171128
                        // parentEmail:
                        // mid: taohan5825
                        //goodsName : 171128095205603
                        if (resultMap["payMethod"] == "VBank")
                        {
                            Lebi_Order order = B_Lebi_Order.GetModel("Code='" + resultMap["MOID"] + "'");
                            if (order != null)
                            {
                                string str = "송금기한: " + resultMap["VACT_Date"] + "<br/>";
                                str += "예금주명: " + resultMap["VACT_Name"] + "<br/>";
                                str += "입금계좌번호: " + resultMap["VACT_Num"] + "<br/>";
                                str += "결제 수단: " + resultMap["payDevice"] + "<br/>";
                                str += "구매자명: " + resultMap["buyerName"] + "<br/>";
                                str += "입금 은행명: " + resultMap["vactBankName"] + "<br/>";
                                order.Remark_User = str;
                                B_Lebi_Order.Update(order);
                                Lebi_Site site = B_Lebi_Site.GetModel(order.Site_id_pay);
                                if (site == null)
                                    HttpContext.Current.Response.Redirect("/user/OrderDetails.aspx?id=" + order.id);
                                else
                                {
                                    Lebi_Language lang = B_Lebi_Language.GetModel(order.Language_id);
                                    string path = "";
                                    if (lang != null)
                                        path = lang.Path.TrimEnd('/');

                                    if (site.Domain == "")
                                        HttpContext.Current.Response.Redirect(site.Path.TrimEnd('/') + path + "/user/OrderDetails.aspx?id=" + order.id);
                                    else
                                    {
                                        HttpContext.Current.Response.Redirect(path + "/user/OrderDetails.aspx?id=" + order.id);
                                    }

                                }


                            }
                        }
                        else
                        {
                            Order.OnlinePaySuccess("INIpayWeb", resultMap["MOID"], "", true);
                        }
                        return;

                        //交易成功
                        /*****************************************************************************
                        * 여기에 가맹점 내부 DB에 결제 결과를 반영하는 관련 프로그램 코드를 구현한다.  

                          [중요!] 승인내용에 이상이 없음을 확인한 뒤 가맹점 DB에 해당건이 정상처리 되었음을 반영함
                                  처리중 에러 발생시 망취소를 한다.
                        ******************************************************************************/

                        System.Collections.Generic.Dictionary<string, string> checkMap = new System.Collections.Generic.Dictionary<string, string>();

                        checkMap.Add("mid", mid);           // 필수
                        checkMap.Add("authToken", HttpUtility.UrlEncode(authToken));    // 필수 - 반드시 urlencode 해서 전달.
                        checkMap.Add("applDate", (resultMap.ContainsKey("applDate") ? resultMap["applDate"] : "null"));     // 필수					
                        checkMap.Add("applTime", (resultMap.ContainsKey("applTime") ? resultMap["applTime"] : "null"));     // 필수	
                        checkMap.Add("timestamp", timestamp);   // 필수
                        checkMap.Add("signature", signature);   // 필수            
                        checkMap.Add("charset", charset);       // default=UTF-8
                        checkMap.Add("format", format);     // default=XML

                        Response.Write("<tr><th class='td01'><p>거래 성공 여부</p></th>");
                        Response.Write("<td class='td02'><p>성공</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결과 코드</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("resultCode") ? resultMap["resultCode"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결과 내용</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("resultMsg") ? resultMap["resultMsg"] : "null") + "</p></td></tr>");

                    }
                    else
                    {
                        //交易失败
                        Response.Write("<tr><th class='td01'><p>거래 성공 여부</p></th>");
                        Response.Write("<td class='td02'><p>실패</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결과 코드</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("resultCode") ? resultMap["resultCode"] : "null") + "</p></td></tr>");

                        //결제보안키가 다른 경우.
                        if (!secureSignature.Equals(resultMap["authSignature"]))
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>결과 내용</p></th>");
                            Response.Write("<td class='td02'><p>* 데이터 위변조 체크 실패</p></td></tr>");

                            //망취소
                            if ("0000".Equals((resultMap.ContainsKey("resultCode") ? resultMap["resultCode"] : "null")))
                            {
                                throw new Exception("데이터 위변조 체크 실패");
                            }
                        }
                        else
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>결과 내용</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("resultMsg") ? resultMap["resultMsg"] : "null") + "</p></td></tr>");
                        }

                    }

                    //공통 부분만
                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                    Response.Write("<tr><th class='td01'><p>거래 번호</p></th>");
                    Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("tid") ? resultMap["tid"] : "null") + "</p></td></tr>");

                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                    Response.Write("<tr><th class='td01'><p>결제방법(지불수단)</p></th>");
                    Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null") + "</p></td></tr>");

                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                    Response.Write("<tr><th class='td01'><p>결제완료금액</p></th>");
                    Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("TotPrice") ? resultMap["TotPrice"] : "null") + "원</p></td></tr>");
                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    Response.Write("<tr><th class='td01'><p>주문 번호</p></th>");
                    Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("MOID") ? resultMap["MOID"] : "null") + "</p></td></tr>");
                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");


                    Response.Write("<tr><th class='td01'><p>승인날짜</p></th>");
                    Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applDate") ? resultMap["applDate"] : "null") + "</p></td></tr>");
                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                    Response.Write("<tr><th class='td01'><p>승인시간</p></th>");
                    Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applTime") ? resultMap["applTime"] : "null") + "</p></td></tr>");
                    Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    if ("VBank".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //가상계좌

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>입금 계좌번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("VACT_Num") ? resultMap["VACT_Num"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>입금 은행코드</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("VACT_BankCode") ? resultMap["VACT_BankCode"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>입금 은행명</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("vactBankName") ? resultMap["vactBankName"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>예금주 명</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("VACT_Name") ? resultMap["VACT_Name"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>송금자 명</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("VACT_InputName") ? resultMap["VACT_InputName"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>송금 일자</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("VACT_Date") ? resultMap["VACT_Date"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>송금 시간</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("VACT_Time") ? resultMap["VACT_Time"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("DirectBank".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //실시간계좌이체
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>은행코드</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("ACCT_BankCode") ? resultMap["ACCT_BankCode"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>현금영수증 발급결과코드</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CSHR_ResultCode") ? resultMap["CSHR_ResultCode"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>현금영수증 발급구분코드</p> <font color=red><b>(0 - 소득공제용, 1 - 지출증빙용)</b></font></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CSHR_Type") ? resultMap["CSHR_Type"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("HPP".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //휴대폰
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>통신사</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("HPP_Corp") ? resultMap["HPP_Corp"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결제장치</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("payDevice") ? resultMap["payDevice"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>휴대폰번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("HPP_Num") ? resultMap["HPP_Num"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                    }
                    else if ("KWPY".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//뱅크월렛 카카오
                        Response.Write("<tr><th class='td01'><p>휴대폰번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("KWPY_CellPhone") ? resultMap["KWPY_CellPhone"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>거래금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("KWPY_SalesAmount") ? resultMap["KWPY_SalesAmount"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>공급가액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("KWPY_Amount") ? resultMap["KWPY_Amount"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>부가세</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("KWPY_Tax") ? resultMap["KWPY_Tax"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>봉사료</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("KWPY_ServiceFee") ? resultMap["KWPY_ServiceFee"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("Culture".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {   //문화상품권
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>문화상품권승인일자</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applDate") ? resultMap["applDate"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>문화상품권 승인시간</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applTime") ? resultMap["applTime"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>문화상품권 승인번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applNum") ? resultMap["applNum"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>컬처랜드 아이디</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CULT_UserID") ? resultMap["CULT_UserID"] : "null") + "</p></td></tr>");
                    }
                    else if ("DGCL".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//게임문화상품권
                     // String sum = "0", sum2 = "0", sum3 = "0", sum4 = "0", sum5 = "0", sum6 = "0";
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>게임문화상품권승인금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_ApplPrice") ? resultMap["GAMG_ApplPrice"] : "null") + "원</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>사용한 카드수</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Cnt") ? resultMap["GAMG_Cnt"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>사용한 카드번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Num1") ? resultMap["GAMG_Num1"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>카드잔액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Price1") ? resultMap["GAMG_Price1"] : "null") + "원</p></td></tr>");
                        if (!"".Equals((resultMap.ContainsKey("GAMG_Num2") ? resultMap["GAMG_Num2"] : "null")))
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>사용한 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Num2") ? resultMap["GAMG_Num2"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>카드잔액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Price2") ? resultMap["GAMG_Price2"] : "null") + "원</p></td></tr>");
                        }
                        if (!"".Equals((resultMap.ContainsKey("GAMG_Num3") ? resultMap["GAMG_Num3"] : "null")))
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>사용한 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Num3") ? resultMap["GAMG_Num3"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>카드잔액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Price3") ? resultMap["GAMG_Price3"] : "null") + "원</p></td></tr>");
                        }
                        if (!"".Equals((resultMap.ContainsKey("GAMG_Num4") ? resultMap["GAMG_Num4"] : "null")))
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>사용한 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Num4") ? resultMap["GAMG_Num4"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>카드잔액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Price4") ? resultMap["GAMG_Price4"] : "null") + "원</p></td></tr>");
                        }
                        if (!"".Equals((resultMap.ContainsKey("GAMG_Num5") ? resultMap["GAMG_Num5"] : "null")))
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>사용한 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Num5") ? resultMap["GAMG_Num5"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>카드잔액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Price5") ? resultMap["GAMG_Price5"] : "null") + "원</p></td></tr>");
                        }
                        if (!"".Equals((resultMap.ContainsKey("GAMG_Num6") ? resultMap["GAMG_Num6"] : "null")))
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>사용한 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Num6") ? resultMap["GAMG_Num6"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>카드잔액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GAMG_Price6") ? resultMap["GAMG_Price6"] : "null") + "원</p></td></tr>");
                        }
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("OCBPoint".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //오케이 캐쉬백

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>지불구분</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("PayOption") ? resultMap["PayOption"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결제완료금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applPrice") ? resultMap["applPrice"] : "null") + "원</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>OCB 카드번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_Num") ? resultMap["OCB_Num"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>적립 승인번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_SaveApplNum") ? resultMap["OCB_SaveApplNum"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>사용 승인번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_PayApplNum") ? resultMap["OCB_PayApplNum"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>OCB 지불 금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_PayPrice") ? resultMap["OCB_PayPrice"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("GSPT".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //GSPoint

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>지불구분</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("PayOption") ? resultMap["PayOption"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>GS 포인트 승인금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GSPT_ApplPrice") ? resultMap["GSPT_ApplPrice"] : "null") + "원</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>GS 포인트 적립금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GSPT_SavePrice") ? resultMap["GSPT_SavePrice"] : "null") + "원</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>GS 포인트 지불금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GSPT_PayPrice") ? resultMap["GSPT_PayPrice"] : "null") + "원</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("UPNT".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //U-포인트

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>U포인트 카드번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("UPoint_Num") ? resultMap["UPoint_Num"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>가용포인트</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("UPoint_usablePoint") ? resultMap["UPoint_usablePoint"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>포인트지불금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("UPoint_ApplPrice") ? resultMap["UPoint_ApplPrice"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("KWPY".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //뱅크월렛 카카오
                        Response.Write("<tr><th class='td01'><p>결제방법</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결과 코드</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("resultCode") ? resultMap["resultCode"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결과 내용</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("resultMsg") ? resultMap["resultMsg"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>거래 번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("tid") ? resultMap["tid"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>주문 번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("orderNumber") ? resultMap["orderNumber"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>결제완료금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("price") ? resultMap["price"] : "null") + "원</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>사용일자</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applDate") ? resultMap["applDate"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>사용시간</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("applTime") ? resultMap["applTime"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("YPAY".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    { //엘로우 페이
                      //별도 응답 필드 없음

                    }
                    else if ("TEEN".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//틴캐시
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>틴캐시 승인번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("TEEN_ApplNum") ? resultMap["TEEN_ApplNum"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>틴캐시아이디</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("TEEN_UserID") ? resultMap["TEEN_UserID"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>틴캐시승인금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("TEEN_ApplPrice") ? resultMap["TEEN_ApplPrice"] : "null") + "원</p></td></tr>");

                    }
                    else if ("Bookcash".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//도서문화상품권

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>도서상품권 승인번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("BCSH_ApplNum") ? resultMap["BCSH_ApplNum"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>도서상품권 사용자ID</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("BCSH_UserID") ? resultMap["BCSH_UserID"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>도서상품권 승인금액</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("BCSH_ApplPrice") ? resultMap["BCSH_ApplPrice"] : "null") + "원</p></td></tr>");

                    }
                    else if ("PhoneBill".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//폰빌전화결제
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>승인전화번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("PHNB_Num") ? resultMap["PHNB_Num"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                    }
                    else if ("Bill".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//빌링결제
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>빌링키</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_BillKey") ? resultMap["CARD_BillKey"] : "null") + "</p></td></tr>");

                    }
                    else if ("Auth".Equals((resultMap.ContainsKey("payMethod") ? resultMap["payMethod"] : "null")))
                    {//빌링결제
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>빌링키</p></th>");
                        if ("BILL_CARD".Equals((resultMap.ContainsKey("payMethodDetail") ? resultMap["payMethodDetail"] : "null")))
                        {
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_BillKey") ? resultMap["CARD_BillKey"] : "null") + "</p></td></tr>");
                        }
                        else if ("BILL_HPP".Equals((resultMap.ContainsKey("payMethodDetail") ? resultMap["payMethodDetail"] : "null")))
                        {
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("HPP_BillKey") ? resultMap["HPP_BillKey"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>통신사</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("HPP_Corp") ? resultMap["HPP_Corp"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>결제장치</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("payDevice") ? resultMap["payDevice"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>휴대폰번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("HPP_Num") ? resultMap["HPP_Num"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>상품명</p></th>");         //상품명
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("goodName") ? resultMap["goodName"] : "null") + "</p></td></tr>");

                        }
                        else
                        {
                            //
                        }
                    }
                    else
                    {//카드
                        int quota = Convert.ToInt16((resultMap.ContainsKey("CARD_Quota") ? resultMap["CARD_Quota"] : "null"));
                        if ((resultMap.ContainsKey("EventCode") ? resultMap["EventCode"] : "null") != null)
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>이벤트 코드</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("EventCode") ? resultMap["EventCode"] : "null") + "</p></td></tr>");
                        }

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>카드번호</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_Num") ? resultMap["CARD_Num"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>할부기간</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_Quota") ? resultMap["CARD_Quota"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");

                        if ("1".Equals((resultMap.ContainsKey("CARD_Interest") ? resultMap["CARD_Interest"] : "null")) || "1".Equals((resultMap.ContainsKey("EventCode") ? resultMap["EventCode"] : "null")))
                        {
                            Response.Write("<tr><th class='td01'><p>할부 유형</p></th>");
                            Response.Write("<td class='td02'><p>무이자</p></td></tr>");
                        }
                        else if (quota > 0 && !"1".Equals((resultMap.ContainsKey("CARD_Interest") ? resultMap["CARD_Interest"] : "null")))
                        {
                            Response.Write("<tr><th class='td01'><p>할부 유형</p></th>");
                            Response.Write("<td class='td02'><p>유이자 <font color='red'> *유이자로 표시되더라도 EventCode 및 EDI에 따라 무이자 처리가 될 수 있습니다.</font></p></td></tr>");
                        }

                        if ("1".Equals((resultMap.ContainsKey("point") ? resultMap["point"] : "null")))
                        {
                            Response.Write("<td class='td02'><p></p></td></tr>");
                            Response.Write("<tr><th class='td01'><p>포인트 사용 여부</p></th>");
                            Response.Write("<td class='td02'><p>사용</p></td></tr>");
                        }
                        else
                        {
                            Response.Write("<td class='td02'><p></p></td></tr>");
                            Response.Write("<tr><th class='td01'><p>포인트 사용 여부</p></th>");
                            Response.Write("<td class='td02'><p>미사용</p></td></tr>");
                        }

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>카드 종류</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_Code") ? resultMap["CARD_Code"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>카드 발급사</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_BankCode") ? resultMap["CARD_BankCode"] : "null") + "</p></td></tr>");

                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>부분취소 가능여부</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_PRTC_CODE") ? resultMap["CARD_PRTC_CODE"] : "null") + "</p></td></tr>");
                        Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                        Response.Write("<tr><th class='td01'><p>체크카드 여부</p></th>");
                        Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("CARD_CheckFlag") ? resultMap["CARD_CheckFlag"] : "null") + "</p></td></tr>");

                        if ((resultMap.ContainsKey("OCB_Num") ? resultMap["OCB_Num"] : "null") != null && (resultMap.ContainsKey("OCB_Num") ? resultMap["OCB_Num"] : "null") != "")
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>OK CASHBAG 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_Num") ? resultMap["OCB_Num"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>OK CASHBAG 적립 승인번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_SaveApplNum") ? resultMap["OCB_SaveApplNum"] : "null") + "</p></td></tr>");
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>OK CASHBAG 포인트지불금액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("OCB_PayPrice") ? resultMap["OCB_PayPrice"] : "null") + "</p></td></tr>");
                        }
                        if ((resultMap.ContainsKey("GSPT_Num") ? resultMap["GSPT_Num"] : "null") != null && (resultMap.ContainsKey("GSPT_Num") ? resultMap["GSPT_Num"] : "null") != "")
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>GS&Point 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GSPT_Num") ? resultMap["GSPT_Num"] : "null") + "</p></td></tr>");

                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>GS&Point 잔여한도</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GSPT_Remains") ? resultMap["GSPT_Remains"] : "null") + "</p></td></tr>");

                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>GS&Point 승인금액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("GSPT_ApplPrice") ? resultMap["GSPT_ApplPrice"] : "null") + "</p></td></tr>");
                        }

                        if ((resultMap.ContainsKey("UNPT_CardNum") ? resultMap["UNPT_CardNum"] : "null") != null && (resultMap.ContainsKey("UNPT_CardNum") ? resultMap["UNPT_CardNum"] : "null") != "")
                        {
                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>U-Point 카드번호</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("UNPT_CardNum") ? resultMap["UNPT_CardNum"] : "null") + "</p></td></tr>");

                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>U-Point 가용포인트</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("UPNT_UsablePoint") ? resultMap["UPNT_UsablePoint"] : "null") + "</p></td></tr>");

                            Response.Write("<tr><th class='line' colspan='2'><p></p></th></tr>");
                            Response.Write("<tr><th class='td01'><p>U-Point 포인트지불금액</p></th>");
                            Response.Write("<td class='td02'><p>" + (resultMap.ContainsKey("UPNT_PayPrice") ? resultMap["UPNT_PayPrice"] : "null") + "</p></td></tr>");
                        }
                    }


                    Response.Write("</pre>");

                    // 수신결과를 파싱후 resultCode가 "0000"이면 승인성공 이외 실패
                    // 가맹점에서 스스로 파싱후 내부 DB 처리 후 화면에 결과 표시

                    // payViewType을 popup으로 해서 결제를 하셨을 경우
                    // 내부처리후 스크립트를 이용해 opener의 화면 전환처리를 하세요

                    //throw new Exception("강제 Exception");
                }
                catch (Exception ex)
                {

                    //####################################
                    // 실패시 처리(***가맹점 개발수정***)
                    //####################################

                    //---- db 저장 실패시 등 예외처리----//
                    Console.WriteLine(ex);


                    //#####################
                    // 망취소 API
                    //#####################

                    String netcancelResultString = processHTTP(authMap, netCancel); // 망취소 요청 API url(고정, 임의 세팅 금지)

                    Response.Write("<br/>## 망취소 API 결과 ##<br/>");

                    // 취소 결과 확인
                    Response.Write("<p>" + netcancelResultString.Replace("<", "&lt;").Replace(">", "&gt;") + "</p>");
                }

            }
            else
            {
                //#############
                // 인증 실패시
                //#############
                //Response.Write("<br/>");
                //Response.Write("####인증실패####");

                Response.Write("<p>" + HttpUtility.UrlDecode(sb.ToString()) + "</p>");
                Response.Redirect("/user/Orders.aspx");
            }


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




        private string processHTTP(System.Collections.Generic.Dictionary<string, string> mapParam, string url)
        {


            string postData = "";
            foreach (System.Collections.Generic.KeyValuePair<string, string> kvp in mapParam)
            {
                string param = kvp.Key + "=" + kvp.Value + "&";
                postData += param;
            }

            postData = postData.Substring(0, postData.Length - 1);

            System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            System.IO.Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            System.Net.WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((System.Net.HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }


        System.Collections.Generic.Dictionary<string, string> parseStringToMap(string text)
        {
            System.Collections.Generic.Dictionary<string, string> retMap = new System.Collections.Generic.Dictionary<string, string>();
            string[] arText = text.Split('&');
            for (int i = 0; i < arText.Length; i++)
            {
                string[] arKeyVal = arText[i].Split('=');
                retMap.Add(arKeyVal[0], arKeyVal[1]);
            }
            return retMap;
        }


        // fake server 막기 위해 추가 2016.05.16 김종현
        private string makeSignatureAuth(Dictionary<string, string> parameters)
        {

            if (parameters == null || parameters.Count == 0)
            {
                throw new Exception("Parameters can not be empty.");
            }

            string stringToSign = "";                                                           //반환용 text
            string mid = parameters["mid"];                                                //mid
            string tstamp = parameters["tstamp"];                                               //auth timestamp
            string MOID = parameters["MOID"];                                               //OID
            string TotPrice = parameters["TotPrice"];                                           //total price
            string tstampKey = parameters["tstamp"].Substring(parameters["tstamp"].Length - 1); // timestamp 마지막 자리 1자리 숫자


            switch (uint.Parse(tstampKey))
            {
                case 1:
                    stringToSign = "MOID=" + MOID + "&mid=" + mid + "&tstamp=" + tstamp;
                    break;
                case 2:
                    stringToSign = "MOID=" + MOID + "&tstamp=" + tstamp + "&mid=" + mid;
                    break;
                case 3:
                    stringToSign = "mid=" + mid + "&MOID=" + MOID + "&tstamp=" + tstamp;
                    break;
                case 4:
                    stringToSign = "mid=" + mid + "&tstamp=" + tstamp + "&MOID=" + MOID;
                    break;
                case 5:
                    stringToSign = "tstamp=" + tstamp + "&mid=" + mid + "&MOID=" + MOID;
                    break;
                case 6:
                    stringToSign = "tstamp=" + tstamp + "&MOID=" + MOID + "&mid=" + mid;
                    break;
                case 7:
                    stringToSign = "TotPrice=" + TotPrice + "&mid=" + mid + "&tstamp=" + tstamp;
                    break;
                case 8:
                    stringToSign = "TotPrice=" + TotPrice + "&tstamp=" + tstamp + "&mid=" + mid;
                    break;
                case 9:
                    stringToSign = "TotPrice=" + TotPrice + "&MOID=" + MOID + "&tstamp=" + tstamp;
                    break;
                case 0:
                    stringToSign = "TotPrice=" + TotPrice + "&tstamp=" + tstamp + "&MOID=" + MOID;
                    break;
            }


            //Console.WriteLine("stringToSign="+stringToSign) ;
            //Console.WriteLine("tstampKey,tstamp=" + tstampKey + "," + tstamp);

            string signature = ComputeHash(stringToSign);            // sha256 처리하여 hash 암호화

            return signature;
        }

    }
}
