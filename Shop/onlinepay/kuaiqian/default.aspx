<%@ Page Language="C#" AutoEventWireup="true" Inherits="onlinepay.kuaiqian._Default"
    CodeBehind="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<form name="kqPay" method="post" action="https://www.99bill.com/gateway/recvMerchantInfoAction.htm" />
<input type="hidden" id="inputCharset" runat="server" />
<input type="hidden" id="bgUrl" runat="server" />
<input type="hidden" id="pageUrl" runat="server" />
<input type="hidden" id="version" runat="server" />
<input type="hidden" id="language" runat="server" />
<input type="hidden" id="signType" runat="server" />
<input type="hidden" id="signMsg" runat="server" />
<input type="hidden" id="merchantAcctId" runat="server" />
<input type="hidden" id="payerName" runat="server" />
<input type="hidden" id="payerContactType" runat="server" />
<input type="hidden" id="payerContact" runat="server" />
<input type="hidden" id="orderId" runat="server" />
<input type="hidden" id="orderAmount" runat="server" />
<input type="hidden" id="orderTime" runat="server" />
<input type="hidden" id="productName" runat="server" />
<input type="hidden" id="productNum" runat="server" />
<input type="hidden" id="productId" runat="server" />
<input type="hidden" id="productDesc" runat="server" />
<input type="hidden" id="ext1" runat="server" />
<input type="hidden" id="ext2" runat="server" />
<input type="hidden" id="payType" runat="server" />
<input type="hidden" id="bankId" runat="server" />
<input type="hidden" id="redoFlag" runat="server" />
<input type="hidden" id="pid" runat="server" />
<%--<input type="submit" id="submit" value="提交">--%>
</form>
<script type="text/javascript">
    document.forms[0].submit();
</script>
</html>
