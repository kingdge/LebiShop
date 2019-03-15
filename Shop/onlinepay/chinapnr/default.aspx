<%@ Page Language="C#" AutoEventWireup="true" Inherits="chinapnr.default_" Codebehind="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form action="http://mas.chinapnr.com/gar/RecvMerchant.do" method="post">
    <%--<form action="http://test.chinapnr.com/gar/RecvMerchant.do" method="post">--%>
    <input type="hidden" id="Version" name="Version" value="<%=Version %>" />
    <input type="hidden" id="CmdId" name="CmdId" value="<%=CmdId %>" />
    <input type="hidden" id="MerId" name="MerId" value="<%=MerId %>" />
    <input type="hidden" id="OrdId" name="OrdId" value="<%=OrdId %>" />
    <input type="hidden" id="OrdAmt" name="OrdAmt" value="<%=OrdAmt %>" />
    <input type="hidden" id="CurCode" name="CurCode" value="<%=CurCode %>" />
    <input type="hidden" id="Pid" name="Pid" value="<%=Pid %>" />
    <input type="hidden" id="RetUrl" name="RetUrl" value="<%=RetUrl %>" />
    <input type="hidden" id="BgRetUrl" name="BgRetUrl" value="<%=BgRetUrl %>" />
    <input type="hidden" id="MerPriv" name="MerPriv" value="<%=MerPriv %>" />
    <input type="hidden" id="GateId" name="GateId" value="<%=GateId %>" />
    <input type="hidden" id="UsrMp" name="UsrMp" value="<%=UsrMp %>" />
    <input type="hidden" id="DivDetails" name="DivDetails" value="<%=DivDetails %>" />
    <input type="hidden" id="PayUsrId" name="PayUsrId" value="<%=PayUsrId %>" />
    <input type="hidden" id="ChkValue" name="ChkValue" value="<%=ChkValue %>" />
    <input type="hidden" id="OrderTime" name="MerDate" value="<%=System.DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    
    </form>
    <script type="text/javascript">
        document.forms[0].submit();
    </script>
</body>
</html>
