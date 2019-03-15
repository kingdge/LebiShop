<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INIStdPayRelay.aspx.cs" Inherits="INIpayWeb.iframe_relay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <title>INIStd 웹표준결제</title>

    <script type="text/javascript">
        function Submit_me() {
            frm.target = "INIpayStd_Return";
            frm.submit();
            self.close();
        } 
    </script>
</head>
<body bgcolor="#FFFFFF" text="#242424" leftmargin=0 topmargin=15 marginwidth=0 marginheight=0 bottommargin=0 rightmargin=0 onload="Submit_me();return false;">
    <form id="frm" runat="server" method="post" action="INIStdPayReturn.aspx" >
 
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
 
    </form>
</body>
</html>
