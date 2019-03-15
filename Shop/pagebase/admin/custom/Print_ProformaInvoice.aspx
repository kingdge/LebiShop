<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.Bussiness.PageBase.AdminCustomPageBase.cs" Inherits="Shop.Bussiness.AdminCustomPageBase" %>
<%@ Import Namespace="DB.LebiShop" %>
<%@ Import Namespace="Shop.Bussiness" %>
<%@ Import Namespace="Shop.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%
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
List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
List<Lebi_Comment> comms = B_Lebi_Comment.GetList("TableName='Order' and Keyid=" + model.id + " and User_id = "+ model.User_id +" and Admin_id = 0", "id desc");
%>
<html>
<head>
<title>形式发票-<%=Tag("单据打印")%>-<%=site.title%></title>
<META name="author" content="LebiShop">
<link href="<%=site.AdminAssetsPath %>/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
<link href="<%=site.AdminCssPath %>/style.css" rel="stylesheet">
<style>
body{background:#fff;margin:0;padding:0;font-size:12px;text-align:left}h1,h2,h3{text-align:center;font-weight:bold;}h1{font-size:22px},h2{font-size:18px}h3{font-size:14px}input{font-size:12px}.order-print{width:754px;margin:0 auto}.order-print table{width:100%; margin-bottom:10px;border-collapse:collapse;}.order-print table td{border-bottom:1px solid #000}.order-print table th{text-align:left}
.order-print h2{margin:0 0 10px 0; padding:10px 0; border-bottom:1px dotted #000; font-weight:bold; font-size:15px; text-align:Center}
.order-print .headmenu{padding-left:5px; height:27px;color:#666;font-size:13px;font-weight:bold}
.order-print .list{border-top:1px solid #000}
.order-print .list th{padding-left:5px; line-height:25px; font-weight:normal; text-align:left; border-bottom:1px solid #000}
.order-print .list td{padding-left:5px; line-height:25px; text-align:left; background:#fff; border-bottom:1px solid #fff}
.order-print .list TR.list {background-color:expression((this.rowIndex%2==0)?"#FFFFFF":"#FFFFFF")}
.order-print .list TH.list .pro-pic {width: 45px; height: 45px}
.order-print .list TH.list .pro-pic IMG {width: expression(this.width > 45 ? 45 : true); height: expression(this.height > 45 ? 45 : true); max-width: 45px; max-height: 45px; vertical-align: middle; text-align: center}</style>
<style media=print>.print-btn{display:none;}</style>
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
<h1>中领国际实业有限公司</h1>
<h1>SINOLINK International Industrial Co.,Limited</h1>
<h3>ADD: Room H,15/F,Siu King Building.6 On Wah Street,Ngau Tau Kok.Kowloon,HongKong.</h3>
<h1>形式发票</h1>
<h1>PROFORMA INVOICE</h1>
<table cellspacing="0" cellpadding="0" align="center">
<tr>
<th style="width:55%">
<table cellspacing="0" cellpadding="0" align="center">
<tr>
<th style="width:10%">TO：</th><td style="width:60%"><%=model.T_Address %> <%=Shop.Bussiness.EX_Area.GetAreaName(model.T_Area_id)%></td>
<th style="width:10%">Shioment<br />Period：</th><td>WITHIN 60 DAYS</td>
</tr>
<tr>
<th>No.：</th><td><%=model.Code %></td>
<th>Date：</th><td><%=model.Time_Add %></td>
</tr>
</table>
</th>
</tr>
</table>
<table cellspacing="0" cellpadding="0" border="1" align="center" class="list">
<tr>
<th style="width:10%;line-height:130%">Picture<br />图片</th>
<th>Description<br />商品名称</th>
<th style="width:10%;line-height:130%">Model No.<br />商品型号及规格</th>
<th style="width:8%;line-height:130%">Unit<br />单位</th>
<th style="width:8%;line-height:130%">QTY<br />数量</th>
<th style="width:8%;line-height:130%">Nos.ofPackage<br />件数</th>
<th style="width:10%;line-height:130%">Unit price(USDEXW)</th>
<th style="width:15%;line-height:130%">Total USD EXW<br />合计（不含税）</th>
</tr>
<%int totalqty = 0;decimal totalmoney = 0; foreach (DB.LebiShop.Lebi_Order_Product pro in pros)
  { %>
<tr class="list">
<th class="list"><img src="<%=Image(pro.ImageOriginal,50,50) %>" style="max-width:40px;max-height:40px;padding:3px 0" /></th>
<th class="list"><%=Shop.Bussiness.Language.Content(pro.Product_Name, CurrentLanguage)%></th>
<th class="list"><%=pro.Product_Number%></th>
<th class="list"><%=Lang(Shop.Bussiness.EX_Product.ProductUnit(pro.Units_id))%></th>
<th class="list"><%=pro.Count%></th>
<th class="list"><%=pro.Count%></th>
<th class="list"><%=FormatMoney(pro.Price) %></th>
<th class="list"><%=FormatMoney(pro.Money)%></th>
</tr>
<%totalqty += pro.Count;totalmoney += pro.Money;;
  } %>
<tr>
<td class="list" colspan="5" style="text-align:right">TOTAL:</td>
<th class="list" colspan="2"><%=totalqty%> CTNS</th>
<th class="list" colspan="2"><%=FormatMoney(totalmoney)%></th>
</tr>
</table>
</div>
</div>
</div>
</body>
</html>