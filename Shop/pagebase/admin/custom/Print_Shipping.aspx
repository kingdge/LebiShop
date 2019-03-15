<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.Bussiness.PageBase.AdminCustomPageBase.cs" Inherits="Shop.Bussiness.AdminCustomPageBase" %>
<%@ Import Namespace="DB.LebiShop" %>
<%@ Import Namespace="Shop.Bussiness" %>
<%@ Import Namespace="Shop.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%
string UserLanguage = CurrentLanguage.Code;
string id = LB.Tools.RequestTool.RequestSafeString("id");
int loop = LB.Tools.RequestTool.RequestInt("loop",0);
string where = "";
int Count = 1;
if (!id.Contains(",")){
    where = "id = lbsql{"+id+"}";
}else{
    string[] ids = id.Split(',');
    Count = ids.Count();
    if (ids[loop] != "")
    {
        where = "id = lbsql{"+ ids[loop] +"}";
    }
}
Lebi_Order model = B_Lebi_Order.GetModel(where);
if (model == null)
{
    model = new Lebi_Order();
}
Lebi_User user = B_Lebi_User.GetModel(model.User_id);
if (user != null)
{
    UserLanguage = user.Language;
}
List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
List<Lebi_Comment> comms = B_Lebi_Comment.GetList("TableName='Order' and Keyid=" + model.id + " and User_id = "+ model.User_id +" and Admin_id = 0", "id desc");
%>
<html>
<head>
<title>
    <%=Shop.Bussiness.Language.Tag("发货单", UserLanguage)%>-<%=Shop.Bussiness.Language.Tag("单据打印", UserLanguage)%>-<%=site.title%></title>
<meta name="author" content="LebiShop">
<link href="<%=site.AdminAssetsPath %>/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
<link href="<%=site.AdminCssPath %>/style.css" rel="stylesheet">
<style>body{background:#fff;margin:0;padding:0;font-size:12px;text-align:left}input{font-size:12px}.order-print table{width:100%; margin-bottom:10px}
.order-print h2{margin:0 0 10px 0; padding:10px 0; border-bottom:1px dotted #000; font-weight:bold; font-size:15px; text-align:Center}
.order-print .headmenu{padding-left:5px; height:27px;color:#666;font-size:13px;font-weight:bold}
.order-print th{padding-left:5px; line-height:25px; font-weight:normal; text-align:left; border-bottom:1px dotted #000}
.order-print td{padding-left:5px; line-height:25px; text-align:left; background:#fff}
.order-print TR.list {background-color:expression((this.rowIndex%2==0)?"#FFFFFF":"#FFFFFF")}
.order-print TD.list {padding-left:3px; ; border-bottom:1px dotted #000; line-height:25px}
.order-print TD.list .pro-pic {width: 45px; height: 45px}
.order-print TD.list .pro-pic IMG {width: expression(this.width > 45 ? 45 : true); height: expression(this.height > 45 ? 45 : true); max-width: 45px; max-height: 45px; vertical-align: middle; text-align: center}</style>
    <style media="print">
        .print-btn
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        javascript: window.print();
    </script>
    <%if ((loop+1) < Count){ %>
    <script type="text/javascript">
        function NextPrint() {
            window.location.href = "?id=<%=id%>&loop=<%=loop+1 %>";
        }
        setTimeout("NextPrint()", 3000);
    </script>
    <%} %>
</head>
<body class="fix-sidebar fix-header card-no-border">
<div id="main-wrapper">
<div class="container-fluid">
<div class="row page-titles print-btn">
    <div class="col-md-12 col-12 align-self-center">
        <%if ((loop+1) < Count){ %>
            <a class="btn btn-default" href="?id=<%=id%>&loop=<%=loop+1 %>"><i class="ti-info-alt"></i> <%=Tag("下一个")%>(<%=Tag("剩余")%>：<%=Count-loop-1 %>)</a>
        <%} %>
        <button class="btn btn-info" onclick="window.print();"><i class="ti-printer"></i> <%=Tag("打印")%></button>
        <button class="btn btn-default" onclick="window.close();"><i class="ti-close"></i> <%=Tag("关闭")%></button>
    </div>
</div>
    <div class="order-print">
        <h2>
            <%=Shop.Bussiness.Language.Content(config.Name,UserLanguage)%>
            <%=Shop.Bussiness.Language.Tag("发货单", UserLanguage)%></h2>
        <table cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td style="width: 33%">
                    <%=Shop.Bussiness.Language.Tag("订单编号", UserLanguage)%>：<%=model.Code%>
                </td>
                <td style="width: 33%">
                    <%=Shop.Bussiness.Language.Tag("姓名", UserLanguage)%>：<%=model.T_Name %>
                </td>
                <td style="width: 33%">
                    <%=Shop.Bussiness.Language.Tag("会员账号", UserLanguage)%>：<%=model.User_UserName%>
                </td>
            </tr>
            <tr>
                <td>
                    <%=Shop.Bussiness.Language.Tag("打印时间", UserLanguage)%>：<%=DateTime.Now%>
                </td>
                <td>
                    <%=Shop.Bussiness.Language.Tag("电话", UserLanguage)%>：<%=model.T_Phone%>
                </td>
                <td>
                    <%=Shop.Bussiness.Language.Tag("手机")%>：<%=model.T_MobilePhone%>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <%=Shop.Bussiness.Language.Tag("地区", UserLanguage)%>：<%=Shop.Bussiness.EX_Area.GetAreaName(model.T_Area_id) %>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <%=Shop.Bussiness.Language.Tag("地址", UserLanguage)%>：<%=model.T_Address%>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <%=Shop.Bussiness.Language.Tag("订单留言", UserLanguage)%>：<%int i =0;foreach (DB.LebiShop.Lebi_Comment comm in comms){ %><%=comm.Content%><%if (i>0){Response.Write("<br/>");}i++;} %>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" align="center">
            <tr>
                <th style="width: 5%">
                    <%=Shop.Bussiness.Language.Tag("序号", UserLanguage)%>
                </th>
                <th style="width: 10%">
                    <%=Shop.Bussiness.Language.Tag("商品编号", UserLanguage)%>
                </th>
                <th style="width: 10%">
                    <%=Shop.Bussiness.Language.Tag("商品货号", UserLanguage)%>
                </th>
                <th>
                    <%=Shop.Bussiness.Language.Tag("商品名称", UserLanguage)%>
                </th>
                <th style="width: 10%">
                    <%=Shop.Bussiness.Language.Tag("规格", UserLanguage)%>
                </th>
                <th style="width: 10%">
                    <%=Shop.Bussiness.Language.Tag("数量", UserLanguage)%>
                </th>
                <th style="width: 10%">
                    <%=Shop.Bussiness.Language.Tag("单价", UserLanguage)%>
                </th>
                <th style="width: 10%">
                    <%=Shop.Bussiness.Language.Tag("小计", UserLanguage)%>
                </th>
            </tr>
            <%i = 0; int totalqty = 0; foreach (DB.LebiShop.Lebi_Order_Product pro in pros)
  { %>
            <tr class="list">
                <td class="list">
                    <%=i + 1%>
                </td>
                <td class="list">
                    <%=pro.Product_Number%>&nbsp;
                </td>
                <td class="list">
                    <%=Shop.Bussiness.EX_Product.GetProduct(pro.Product_id).Code%>&nbsp;
                </td>
                <td class="list">
                    <%=Shop.Bussiness.Language.Content(pro.Product_Name, UserLanguage)%>
                </td>
                <td class="list">
                    <%=Shop.Bussiness.EX_Product.ProPertyNameStr(Shop.Bussiness.EX_Product.GetProduct(pro.Product_id).ProPerty131, UserLanguage)%>&nbsp;
                </td>
                <td class="list">
                    <%=pro.Count%> <%=Shop.Bussiness.Language.Content(Shop.Bussiness.EX_Product.ProductUnit(pro.Units_id),UserLanguage)%>
                </td>
                <td class="list">
                    <%=FormatMoney(pro.Price)%>
                </td>
                <td class="list">
                    <%=FormatMoney(pro.Money)%>
                </td>
            </tr>
            <%i += 1; totalqty += pro.Count;
  } %>
        </table>
        <table cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td style="width: 20%">
                    <%=Shop.Bussiness.Language.Tag("商品总数", UserLanguage)%>：<%=totalqty%>
                </td>
                <td style="width: 20%">
                    <%=Shop.Bussiness.Language.Tag("商品总额", UserLanguage)%>：<%=FormatMoney(model.Money_Product)%>
                </td>
                <td style="width: 20%">
                    <%=Shop.Bussiness.Language.Tag("配送费用", UserLanguage)%>：<%=FormatMoney(model.Money_Transport)%>
                </td>
                <td style="width: 20%">
                    <%=Shop.Bussiness.Language.Tag("税金", UserLanguage)%>：<%=FormatMoney(model.Money_Bill)%>
                </td>
                <td style="width: 20%">
                    <%=Shop.Bussiness.Language.Tag("订单总额", UserLanguage)%>：<%=FormatMoney(model.Money_Order)%>
                </td>
            </tr>
        </table>
		<table cellspacing="0" cellpadding="0" align="center">
		<tr>
		<th style="width:14%;text-align:right">Shipper Name：</th><th><%=model.SealNo %></th>
		</tr>
		<tr>
		<th style="width:14%;text-align:right">B/L No.：</th><th><%=model.BLNo %></th>
		</tr>
		<tr>
		<th style="width:14%;text-align:right">Container No.：</th><th><%=model.ContainerNo %></th>
		</tr>
		</table>
    </div>
</div>
</div>
</body>
</html>