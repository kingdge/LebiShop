<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="INIpayWeb.INIStdPayRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
        <title><%=Lang(SYS.Name) %></title>

        <style type="text/css">
            body { background-color: #efefef;}
            body, tr, td {font-size:9pt; font-family:굴림,verdana; color:#433F37; line-height:19px;}
            table, img {border:none}
        </style>

        <!-- 이니시스 표준결제 js -->
        <!--
        
        연동시 유의 사항!!
        
        1) 테스트 URL(stgstdpay.inicis.com) - 샘플에 제공된 테스트 MID 전용으로 실제 가맹점 MID 사용 시 에러가 발생 할 수 있습니다.
        3) 상용 URL(stdpay.inicis.com) - 실제 가맹점 MID 로 테스트 및 오픈 시 해당 URL 변경하여 사용합니다.
      	3) 가맹점의 URL이 http:// 인경우 js URL도 https://stgstdpay.inicis.com/stdjs/INIStdPay.js 로 변경합니다.	
      	4) 가맹점에서 사용하는 케릭터셋이 EUC-KR 일 경우 charset="UTF-8"로 UTF-8 일 경우 charset="UTF-8"로 설정합니다.
      	
        -->	
        
        <!-- 상용 JS(가맹점 MID 변경 시 주석 해제, 테스트용 JS 주석 처리 필수!) -->
      	<!--script language="javascript" type="text/javascript" src="https://stdpay.inicis.com/stdjs/INIStdPay.js" charset="UTF-8"></script-->
        
        <!-- 테스트 JS(샘플에 제공된 테스트 MID 전용) -->
      	<!--script-- language="javascript" type="text/javascript" src="https://stgstdpay.inicis.com/stdjs/INIStdPay.js" charset="UTF-8"></script-->
       <script language="javascript" type="text/javascript" src="https://stdpay.inicis.com/stdjs/INIStdPay.js" charset="UTF-8"></script>

    </head>
    <body >
       
        <table width="650" cellspacing="0" cellpadding="0"   border="0" align="center" style="display:none;">
            <tr>
                <td style="padding:10px; background-color: #6095BC; text-align: center;" align="center">
                    <table style="background-color: White">

                        <tr>
                            <td>
                                이 페이지는 INIpay Standard 결제요청을 위한 예시입니다.<br/>
                                <br/>
                                결제처리를 위한 action등의 모든 동작은 Import 된 스크립트에 의해 자동처리됩니다.<br/>

                                <br/>
                                Form에 설정된 모든 필드의 name은 대소문자 구분하며,<br/>
                                이 Sample은 결제를 위해서 설정된 Form은 테스트 / 이해돕기를 위해서 모두 type="text"로 설정되어 있습니다.<br/>
                                운영에 적용시에는 일부 가맹점에서 필요에 의해 사용자가 변경하는 경우를 제외하고<br/>
                                모두 type="hidden"으로 변경하여 사용하시기 바랍니다.<br/>

                                <br/>
                                <strong style="color: #336699;">함께 제공되는 메뉴얼을 참조하여 작성 개발하시기 바랍니다.</strong>
                                <br/><br/>
                            </td>
                        </tr>
                        <tr>
                            
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td >
                                            <form id="SendPayForm_id" method="post"    >   
                                                           
                                                <%--<asp:Button id="Button1"   Text="결제요청" OnClientClick="send();return false;" Height="100px" Width="200px" ></asp:Button>--%>

                                                <!-- 필수 -->
                                                <br/><b>***** 필 수 *****</b>
                                                <div style="border:2px #dddddd double;padding:10px;background-color:#f3f3f3;text-align:left">

                                                    <br/><b>version</b> :
                                                    <br/><input type="text"    id="version" name="version" value="<%=strVersion %>"  />


                                                    <br/><b>mid</b> :
                                                    <br/><input type="text"    name="mid" value="<%=strMid %>"  />

                                                    <br/><b>goodname</b> :
                                                    <br/><input type="text"   name="goodname" value="<%=strGoodName %>"  />

                                                    <br/><b>oid</b> :
                                                    <br/><input type="text"   name="oid" value="<%=oid %>" />

                                                    <br/><b>price</b> :
                                                    <br/><input type="text"   name="price" value="<%=strPrice %>" />

                                                    <br/><b>currency</b> :
                                                    <br/>[WON|USD]
                                                    <br/><input type="text"   name="currency" value="<%=strCurrency %>"  />

                                                    <br/><b>buyername</b> :
                                                    <br/><input type="text"   name="buyername" value="<%=strBuyerName %>" />

                                                    <br/><b>buyertel</b> :
                                                    <br/><input type="text"   name="buyertel" value="<%=strBuyerTel %>"  />

                                                    <br/><b>buyeremail</b> :
                                                    <br/><input type="text"   name="buyeremail" value="<%=strBuyerEmail %>" />

                                                    <br/><b>timestamp</b> :
                                                    <br/><input type="text"    name="timestamp" value="<%=timestamp %>"   Width="400px" />   


                                                    <br/><b>signature</b> :
                                                    <br/><input type="text"   name="signature"  Width="400px" value="<%=signature %>"/>

                                                    <br/><b>returnUrl</b> :
                                                    <br/><input type="text"   name="returnUrl" value="<%=returnUrl %>"  Width="400px"/>
                                                    <br/><b>MKEY(가맹점키)</b> :
                                                    <br/><input type="text"  name="mKey" value="<%=mKey %>" />
                                                </div>

                                                <br/><br/>
                                                <b>***** 기본 옵션 *****</b>
                                                <div style="border:2px #dddddd double;padding:10px;background-color:#f3f3f3;text-align:left">
                                                    <b>gopaymethod</b> : 결제 수단 선택
                                                    <br/>ex) Card (계약 결제 수단이 존재하지 않을 경우 에러로 리턴)
                                                    <br/>사용 가능한 입력 값
                                                    <br/>Card,DirectBank,HPP,Vbank,kpay,Swallet,Paypin,EasyPay,PhoneBill,GiftCard,EWallet
                                                    <br/>onlypoint,onlyocb,onyocbplus,onlygspt,onlygsptplus,onlyupnt,onlyupntplus
                                                    <br/><input type="text"   name="gopaymethod" value=""  />
                                                    <br/><br/>

                                                    <br/>
                                                    <b>offerPeriod</b> : 제공기간
                                                    <br/>ex)20150101-20150331, [Y2:년단위결제, M2:월단위결제, yyyyMMdd-yyyyMMdd : 시작일-종료일]
                                                    <br/><input type="text"   name="offerPeriod" value="2015010120150331"   />
                                                    <br/><br/>

                                                    <br/><b>acceptmethod</b> : acceptmethod
                                                    <br/>acceptmethod  ex) CARDPOINT:SLIMQUOTA(코드-개월:개월):no_receipt:va_receipt:vbanknoreg(0):vbank(20150425):va_ckprice:vbanknoreg: 
                                                    <br/>KWPY_TYPE(0):KWPY_VAT(10|0) 기타 옵션 정보 및 설명은 연동정의보 참조 구분자 ":"
                                                    <br/><input type="text"  name="acceptmethod" value="HPP(1):no_receipt:va_receipt:vbanknoreg(0):below1000"  Width="500px" />
                                                </div>

                                                <br/><br/>
                                                <b>***** 표시 옵션 *****</b>
                                                <div style="border:2px #dddddd double;padding:10px;background-color:#f3f3f3;text-align:left">
                                                    <br/><b>languageView</b> : 초기 표시 언어
                                                    <br/>[ko|en] (default:ko)
                                                    <br/><input type="text"  name="languageView" value=""  />

                                                    <br/><b>charset</b> : 리턴 인코딩
                                                    <br/>[UTF-8|EUC-KR] (default:UTF-8)
                                                    <br/><input type="text"  name="charset" value=""  />

                                                    <br/><b>payViewType</b> : 결제창 표시방법
                                                    <br/>[overlay] (default:overlay)
                                                    <br/><input type="text"  name="payViewType" value=""  Width="210px"/>

                                                    <br/><b>closeUrl</b> : payViewType='overlay','popup'시 취소버튼 클릭시 창닫기 처리 URL(가맹점에 맞게 설정)
                                                    <br/>close.jsp 샘플사용(생략가능, 미설정시 사용자에 의해 취소 버튼 클릭시 인증결과 페이지로 취소 결과를 보냅니다.)
                                                    <br/><input type="text"  name="closeUrl" value=""  Width="400px" />

                                                    <br/><b>popupUrl</b> : payViewType='popup'시 팝업을 띄울수 있도록 처리해주는 URL(가맹점에 맞게 설정)
                                                    <br/>popup.jsp 샘플사용(생략가능,payViewType='popup'으로 사용시에는 반드시 설정)
                                                    <br/><input type="text"  name="popupUrl" value=""  Width="400px"/>

                                                </div>

                                                <b>***** 결제 수단별 옵션 *****</b>
                                                <br/>
                                                <b>-- 카드(간편결제도 사용) --</b>
                                                <div style="border:2px #cccccc solid;padding:10px;background-color:#f3f3f3;text-align:left">
                                                    <br/><b>nointerest</b> : 무이자 할부 개월
                                                    <br/>ex) 11-2:3:4,04-2:3:4
                                                    <br/><input type="text"   name="nointerest" value="11-2:3:,34-5:12,14-6:12:24,12-12:36,06-9:12,01-3:4"  Width="400px" />

                                                    <br/><b>quotabase</b> : 할부 개월
                                                    <br/>ex) 2:3:4
                                                    <br/><input type="text"   name="quotabase" value="2:3:4:5:6:11:12:24:36"  Width="400px"  />	

                                                </div>

                                                <b>-- 가상계좌 --</b>
                                                <div style="border:2px #cccccc solid;padding:10px;background-color:#f3f3f3;text-align:left">
                                                    <br/><b>INIregno</b> : 주민번호 설정 기능
                                                    <br/>13자리(주민번호),10자리(사업자번호),미입력시(화면에서입력가능)
                                                    <br/><input type="text"   name="vbankRegNo" value=""  Width="400px" />
                                                </div>

                                                <br/><br/>
                                                <b>***** 추가 옵션 *****</b>
                                                <div style="border:2px #dddddd double;padding:10px;background-color:#f3f3f3;text-align:left">
                                                    <br/><b>merchantData</b> : 가맹점 관리데이터(1000byte)
                                                    <br/>인증결과 리턴시 함께 전달됨
                                                    <br/><input type="text"   name="merchantData" value=""   Width="400px" />
                                                </div>
                                                <input type="button" value="submit" onclick="INIStdPay.pay('SendPayForm_id');" />
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </body>
 <script type="text/javascript">

     //alert = function (a) {
     //    console.log(a);
     //}
     INIStdPay.pay('SendPayForm_id');
</script>
</html>