<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="yinlianqmf._Default"
    ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<form action="http://116.228.21.162:9127/umsFrontWebQmjf/umspay" method="post">
<%--<form action="https://mpos.quanminfu.com:8018/umsFrontWebQmjf/umspay" method="post">--%>
<input id="merSign" name="merSign" type="text" value="<%=msg["merSign"] %>">
<input id="chrCode" name="chrCode" type="text" value="<%=msg["chrCode"] %>">
<input id="tranId" name="tranId" type="text" value="<%=msg["tranId"] %>">
<input id="url" name="url" type="text" value="<%=returnurl %>">
<input id="mchantUserCode" name="mchantUserCode" type="text" value="<%=config.MerId %>">
<input id="bankName" name="bankName" type="text" value="">
<input id="cardType" name="cardType" type="text" value="d">
</form>
<script type="text/javascript">
    document.forms[0].submit();
</script>
</html>
