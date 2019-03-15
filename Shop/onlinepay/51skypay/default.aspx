<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="onlinepay.epay._default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" name="order" method="post" action="http://121.199.22.222:8080/ebuyweb/payment/payment.do">
    <%--<form id="form1"  name="order"   method="post"   action="VirtualSky.aspx">   --%>
    <input type="hidden" name="ver" value="<%=onlinepay.epay.epayCommon.ToBase64(sorder.ver) %>">
    <input type="hidden" name="mrch_no" value="<%=onlinepay.epay.epayCommon.ToBase64(sorder.mrch_no) %>">
    <input type="hidden" name="ord_no" value="<%=onlinepay.epay.epayCommon.ToBase64(sorder.ord_no) %>">
    <input type="hidden" name="ord_date" value="<%=onlinepay.epay.epayCommon.ToBase64(sorder.ord_date) %>">
    <input type="hidden" name="ord_amt" value="<%=onlinepay.epay.epayCommon.ToBase64(sorder.ord_amt.ToString()) %>">
    <input type="hidden" name="mac" value="<%=sorder.mac %>">
    </form>
    <script type="text/javascript">
        document.forms["form1"].submit();
    </script>
</body>
</html>
