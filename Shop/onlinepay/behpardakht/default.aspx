<%@ Page Language="C#" AutoEventWireup="true" Inherits="behpardakht._Default" Codebehind="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<form action="https://bpm.shaparak.ir/pgwchannel/enstartpay.mellat" method="post">
<%-- //             https://bpm.shaparak.ir/pgwchannel/enstartpay.mellat--%>
<%--<input type="hidden" name="terminalId" value="<%=pay.terminal %>"><br>
<input type="hidden" name="userName" value="<%=pay.UserName %>" /><br>
<input type="hidden" name="userPassword" value="<%=pay.UserKey %>" /><br>
<input type="hidden" name="orderId" value="<%=OrderCode %>"><br>
<input type="hidden" name="amount" value="<%=Money %>"><br>
<input type="hidden" name="localDate" value="<%=System.DateTime.Now.ToString("yyyyMMdd") %>"><br>
<input type="hidden" name="localTime" value="<%=System.DateTime.Now.ToString("HHmmss") %>"><br>
<input type="hidden" name="callBackUrl" value="<%=ReturnUrl %>"><br>

<input type="hidden" name="additionalData" value="<%=OrderCode %>"><br>
<input type="hidden" name="payerId" value="0"><br>--%>
    <input type="hidden" name="RefId" value="<%=refid %>"><br>
</form>
 <script type="text/javascript">
     document.forms[0].submit();
</script>
</html>
