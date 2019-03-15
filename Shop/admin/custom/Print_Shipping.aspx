<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.Bussiness.PageBase.AdminCustomPageBase.cs" Inherits="Shop.Bussiness.AdminCustomPageBase" %>
<%@ Import Namespace="Shop.Bussiness" %>
<%@ Import Namespace="Shop.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%
int id = Rint("id");
Lebi_Order model = B_Lebi_Order.GetModel(id);
if (model == null)
{
    model = new Lebi_Order();
}
List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
List<Lebi_Comment> comms = B_Lebi_Comment.GetList("TableName='Order' and Keyid=" + model.id + " and User_id = "+ model.User_id +" and Admin_id = 0", "id desc");
%>
<html>
<head>
    <title>
        <%=Tag("发货单")%>-<%=Tag("单据打印")%>-<%=site.title%></title>
    <meta name="author" content="LebiShop">
    <link rel="stylesheet" type="text/css" href="<%=site.AdminCssPath %>/css.css">
    <script type="text/javascript" language="javascript" src="<%=site.AdminJsPath %>/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="<%=site.AdminJsPath %>/messagebox.js"></script>
    <script type="text/javascript" language="javascript" src="<%=site.AdminJsPath %>/jquery-ui.min.js"></script>
    <link rel="stylesheet" type="text/css" href="<%=site.AdminJsPath %>/jqueryuicss/redmond/jquery-ui.css" />
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
</head>
<body>
    <div class="print-btn">
        <div class="tools">
            <ul>
                <li class="print"><a href="javascript:void(0);" onclick="window.print();"><b></b><span>
                    <%=Tag("打印")%></span></a></li>
                <li class="close"><a href="javascript:void(0);" onclick="window.close();"><b></b><span>
                    <%=Tag("关闭")%></span></a></li>
            </ul>
        </div>
    </div>
    <div class="order-print">
        <h2>
            <%=Shop.Bussiness.Language.Content(config.Name,CurrentLanguage.Code)%>
            <%=Tag("发货单")%></h2>
        <table cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td style="width: 33%">
                    <%=Tag("订单编号")%>：<%=model.Code%>
                </td>
                <td style="width: 33%">
                    <%=Tag("姓名")%>：<%=model.T_Name %>
                </td>
                <td style="width: 33%">
                    <%=Tag("会员账号")%>：<%=model.User_UserName%>
                </td>
            </tr>
            <tr>
                <td>
                    <%=Tag("打印时间")%>：<%=DateTime.Now%>
                </td>
                <td>
                    <%=Tag("电话")%>：<%=model.T_Phone%>
                </td>
                <td>
                    <%=Tag("手机")%>：<%=model.T_MobilePhone%>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <%=Tag("地区")%>：<%=Shop.Bussiness.EX_Area.GetAreaName(model.T_Area_id) %>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <%=Tag("地址")%>：<%=model.T_Address%>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <%=Tag("订单留言")%>：<%int i =0;foreach (Shop.Model.Lebi_Comment comm in comms){ %><%=comm.Content%><%if (i>0){Response.Write("<br/>");}i++;} %>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" align="center">
            <tr>
                <th style="width: 5%">
                    <%=Tag("序号")%>
                </th>
                <th style="width: 10%">
                    <%=Tag("商品编号")%>
                </th>
                <th style="width: 10%">
                    <%=Tag("商品货号")%>
                </th>
                <th>
                    <%=Tag("商品名称")%>
                </th>
                <th style="width: 10%">
                    <%=Tag("规格")%>
                </th>
                <th style="width: 10%">
                    <%=Tag("数量")%>
                </th>
                <th style="width: 10%">
                    <%=Tag("单价")%>
                </th>
                <th style="width: 10%">
                    <%=Tag("小计")%>
                </th>
            </tr>
            <%i = 0; int totalqty = 0; foreach (Shop.Model.Lebi_Order_Product pro in pros)
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
                    <%=Shop.Bussiness.Language.Content(pro.Product_Name, "EN")%>
                </td>
                <td class="list">
                    <%=Shop.Bussiness.EX_Product.ProPertyNameStr(Shop.Bussiness.EX_Product.GetProduct(pro.Product_id).ProPerty131, "EN")%>&nbsp;
                </td>
                <td class="list">
                    <%=pro.Count%>
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
                    <%=Tag("商品总数")%>：<%=totalqty%>
                </td>
                <td style="width: 20%">
                    <%=Tag("商品总额")%>：<%=FormatMoney(model.Money_Product)%>
                </td>
                <td style="width: 20%">
                    <%=Tag("配送费用")%>：<%=FormatMoney(model.Money_Transport)%>
                </td>
                <td style="width: 20%">
                    <%=Tag("税金")%>：<%=FormatMoney(model.Money_Bill)%>
                </td>
                <td style="width: 20%">
                    <%=Tag("订单总额")%>：<%=FormatMoney(model.Money_Order)%>
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
</body>
</html>
