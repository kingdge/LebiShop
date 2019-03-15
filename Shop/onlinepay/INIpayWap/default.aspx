<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="INIpayWap.INIStdPayRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=euc-kr" />
    <meta name="viewport" content="width=device-width" />
 <title><%=Lang(SYS.Name) %></title>
    <style>
        body, tr, td {
            font-size: 10pt;
            font-family: 돋움,verdana;
            color: #433F37;
            line-height: 19px;
        }

        table, img {
            border: none;
        }
    </style>
    <script type="application/x-javascript">
    
    addEventListener("load", function()
    {
        setTimeout(updateLayout, 0);
    }, false);
 
    var currentWidth = 0;
    
    function updateLayout()
    {
        if (window.innerWidth != currentWidth)
        {
            currentWidth = window.innerWidth;
 
            var orient = currentWidth == 320 ? "profile" : "landscape";
            document.body.setAttribute("orient", orient);
            setTimeout(function()
            {
                window.scrollTo(0, 1);
            }, 100);            
        }
    }
 
    setInterval(updateLayout, 400);
    
    </script>

    <script language="javascript">
        window.name = "BTPG_CLIENT";

        var width = 330;
        var height = 480;
        var xpos = (screen.width - width) / 2;
        var ypos = (screen.width - height) / 2;
        var position = "top=" + ypos + ",left=" + xpos;
        var features = position + ", width=320, height=440";
        var date = new Date();
        var date_str = "testoid_" + date.getFullYear() + "" + date.getMinutes() + "" + date.getSeconds();
        if (date_str.length != 16) {
            for (i = date_str.length ; i < 16 ; i++) {
                date_str = date_str + "0";
            }
        }
        function setOid() {
            document.ini.P_OID.value = "" + date_str;
        }

        function on_web() {
            var order_form = document.ini;
            var paymethod = order_form.paymethod.value;
            //var wallet = window.open("", "BTPG_WALLET", features);


            //order_form.target = "BTPG_WALLET";
            order_form.action = "https://mobile.inicis.com/smart/" + paymethod + "/";
            order_form.submit();
        }

        function onSubmit() {
            var order_form = document.ini;
            var inipaymobile_type = order_form.inipaymobile_type.value;
            if (inipaymobile_type == "web")
                return on_web();
        }

    </script>
</head>

<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
    <table width="320" border="0" cellpadding="0" cellspacing="0" style="display:none">
        <form id="form1" name="ini" method="post" action="">

            <tr>
                <td height="347" align="center" valign="top" background="images/bg_01.png">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="298" height="296" align="center" background="images/table_bg.png">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="95" height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">방식</td>
                                        <td align="left">
                                            <select name="inipaymobile_type" id="select">
                                                <option value="web">
                                                INIpayMobile Web
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">주문번호</td>
                                        <td align="left">
                                            <input type="text" name="P_OID" id="" value="<%=oid %>" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">상품명</td>
                                        <td align="left">
                                            <input type="text" name="P_GOODS" value="<%=strGoodName %>" id="textfield3" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">가격 </td>
                                        <td align="left">
                                            <input type="text" name="P_AMT" value="<%=strPrice %>" id="textfield4" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">구매자이름</td>
                                        <td align="left">
                                            <input type="text" name="P_UNAME" value="<%=strBuyerName %>" id="textfield5" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">상점이름 </td>
                                        <td align="left">
                                            <input type="text" name="P_MNAME" value="<%=Lang(SYS.Name) %>" id="textfield6" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">휴대폰번호</td>
                                        <td align="left">
                                            <input type="text" name="P_MOBILE" id="textfield7" value="<%=strBuyerTel %>" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">이메일</td>
                                        <td align="left">
                                            <input type="text" name="P_EMAIL" value="smart@inicis.com" id="textfield8" value="<%=strBuyerEmail %>" style="border-color: #cdcdcd; border-width: 1px; border-style: solid; color: #555555; height: 15px;" /></td>
                                    </tr>
                                    <!--
          <tr>
            <td width="95" height="25" align="left" style="background-image:url(images/bullet.png); background-repeat:no-repeat; background-position:0px 40%; padding-left:8px; font-size:12px; color:#607c90;">결과페이지 링크</td>
            <td align="left"><input type="text" name="P_NEXT_URL" value="https://mobile.inicis.com/smart/testmall/next_url_test.php" id="textfield9" style="border-color:#cdcdcd; border-width:1px; border-style:solid; color:#555555; height:15px;"/></td>
          </tr>
-->
                                    <tr>
                                        <td height="25" align="left" style="background-image: url(images/bullet.png); background-repeat: no-repeat; background-position: 0px 40%; padding-left: 8px; font-size: 12px; color: #607c90;">결제방법 </td>
                                        <td align="left">
                                            <label>
                                                <select name="paymethod" id="select">
                                                    <option value="wcard">신용카드
				<!--<option value="DBANK">계좌이체-->
                                                        <option value="vbank">가상계좌
				<option value="mobile">휴대폰
				<option value="culture">문화 상품권
				<option value="hpmn">
                                                    해피머니 상품권
                                                </select>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="39" align="center" valign="bottom">


                                <input type="button" value="submit" onclick="onSubmit();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <input type="hidden" name="P_MID" value="<%=strMid %>">
            <input type="hidden" name="P_NEXT_URL" value="<%=returnUrl %>">
            <input type="hidden" name="P_NOTI_URL" value="<%=returnUrl %>">
            <input type="hidden" name="P_HPP_METHOD" value="1">
        </form>
        <script type="text/javascript">

            //alert = function (a) {
            //    console.log(a);
            //}
            onSubmit();
        </script>
    </table>
</body>
</html>
