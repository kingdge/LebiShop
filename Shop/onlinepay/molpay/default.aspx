<%@ Page Language="C#" AutoEventWireup="true" Inherits="molpay._Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<form action="https://www.onlinepayment.com.my/MOLPay/pay/<%=mid %>/" method="post">
<input type="hidden" name="amount" value="<%=Money %>"><br/>
<input type="hidden" name="orderid" value="<%=OrderCode %>"><br/>
<input type="hidden" name="bill_name" value="<%=order.User_UserName %>"><br/>
<input type="hidden" name="bill_email" value="<%=order.T_Email %>"><br/>
<input type="hidden" name="bill_mobile" value="<%=order.T_MobilePhone %>"><br/>
<input type="hidden" name="bill_desc" value="<%=OrderCode %>"><br/>
<input type="hidden" name="country" value="CN"><br/>
<input type="hidden" name="vcode" value="<%=vcode %>"><br/>
</form>
 <script type="text/javascript">
     document.forms[0].submit();
</script>
</html>
