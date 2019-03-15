<%@ Page Language="C#" AutoEventWireup="true" Inherits="qianhai.submit" Codebehind="submit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<!--form action="https://secure.oceanpayment.com/gateway/service/test" -->
<form action="https://secure.oceanpayment.com/gateway/service/pay" >
       <input type="hidden" name="account" value="<%=business %>">
       <input type="hidden" name="terminal" value="<%=terminal %>">
       <input type="hidden" name="order_number" value="<%=OrderCode %>">
       <input type="hidden" name="order_currency" value="USD">
       <input type="hidden" name="order_amount" value="<%=Money %>">
       <input type="hidden" name="billing_firstName" value="<%=billing_firstName %>">
       <input type="hidden" name="billing_lastName" value="<%=billing_lastName %>">
       <input type="hidden" name="billing_email" value="<%=billing_email %>">
       <input type="hidden" name="backUrl" value="<%=ReturnUrl %>">
       <input type="hidden" name="methods" value="Credit Card">
       <input type="hidden" name="billing_phone" value="<%=billing_phone %>">
       <input type="hidden" name="billing_country" value="<%=billing_country %>">
       <input type="hidden" name="billing_city" value="<%=billing_city %>">
       <input type="hidden" name="billing_address" value="<%=billing_address %>">
       <input type="hidden" name="billing_zip" value="<%=billing_zip %>">
       <input type="hidden" name="order_notes" value="remark">
       <input type="hidden" name="signValue" value="<%=scode %>">
       <input type="hidden" name="pages" value="<%=pages %>">
       
</form>
    
<script type="text/javascript">
    document.forms[0].submit();
</script>



</html>
