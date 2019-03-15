<%@ Page Language="C#" AutoEventWireup="true" Inherits="Paypal._Default" Codebehind="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<form action="https://www.paypal.com/cgi-bin/webscr" method="post">
<%--<form action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">--%>
<input type="hidden" name="cmd" value="_xclick"><br>
<input type="hidden" name="charset" value="utf-8" /><br>
<input type="hidden" name="no_note" value="" /><br>
<input type="hidden" name="business" value="<%=business %>"><br>
<input type="hidden" name="item_name" value="<%=Pid %>"><br>
<input type="hidden" name="item_number" value="<%=OrderCode %>"><br>
<input type="hidden" name="amount" value="<%=Money %>"><br>
<input type="hidden" name="no_shipping" value="0"><br>
<input type="hidden" name="tax" value="0" /><br />
<input type="hidden" name="quantity" value="1" /><br />
<input type="hidden" name="currency_code" value="<%=pay.Currency_Code %>"><br>
<input type="hidden" name="bn" value="IC_Sample"><br>
<input type="hidden" name="return" value="<%=ReturnUrl %>"><br>
<input type="hidden" name="notify_url" value="<%=ReturnUrl %>"><br>
</form>
 <script type="text/javascript">
     document.forms[0].submit();
</script>
</html>
