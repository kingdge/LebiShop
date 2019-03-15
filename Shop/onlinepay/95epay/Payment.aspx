<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="onlinepay.epay95.Payment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
</head>
<body>
    <form action="https://www.95epay.cn/sslpayment" method="post">
    <input type="hidden" name="MerNo" value="<%=MerNo%>" />
    <input type="hidden" name="BillNo" value="<%=BillNo%>" />
    <input type="hidden" name="Amount" value="<%=Amount%>" />
    <input type="hidden" name="PayType" value="<%=PayType%>" />
    <input type="hidden" name="ReturnURL" value="<%=ReturnURL%>" />
    <input type="hidden" name="NotifyURL" value="<%=NotifyURL%>" />
    <input type="hidden" name="MD5info" value="<%=MD5info%>" />
    <input type="hidden" name="MerRemark" value="<%=MerRemark%>" />
    </form>
</body>
 <script type="text/javascript">
     document.forms[0].submit();
</script>
</html>
