<%@ Page Language="C#" AutoEventWireup="true" Inherits="Com.Alipay.batch._Default" Codebehind="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>支付宝批量付款到支付宝账户有密接口</title>
    <style>
*{
	margin:0;
	padding:0;
}
ul,ol{
	list-style:none;
}
.title{
    color: #ADADAD;
    font-size: 14px;
    font-weight: bold;
    padding: 8px 16px 5px 10px;
}
.hidden{
	display:none;
}

.new-btn-login-sp{
	border:1px solid #D74C00;
	padding:1px;
	display:inline-block;
}

.new-btn-login{
    background-color: #ff8c00;
	color: #FFFFFF;
    font-weight: bold;
	border: medium none;
	width:82px;
	height:28px;
}
.new-btn-login:hover{
    background-color: #ffa300;
	width: 82px;
	color: #FFFFFF;
    font-weight: bold;
    height: 28px;
}
.bank-list{
	overflow:hidden;
	margin-top:5px;
}
.bank-list li{
	float:left;
	width:153px;
	margin-bottom:5px;
}

#main{
	width:750px;
	margin:0 auto;
	font-size:14px;
	font-family:'宋体';
}
#logo{
	background-color: transparent;
    background-image: url("images/new-btn-fixed.png");
    border: medium none;
	background-position:0 0;
	width:166px;
	height:35px;
    float:left;
}
.red-star{
	color:#f00;
	width:10px;
	display:inline-block;
}
.null-star{
	color:#fff;
}
.content{
	margin-top:5px;
}

.content dt{
	width:160px;
	display:inline-block;
	text-align:right;
	float:left;
	
}
.content dd{
	margin-left:100px;
	margin-bottom:5px;
}
#foot{
	margin-top:10px;
}
.foot-ul li {
	text-align:center;
}
.note-help {
    color: #999999;
    font-size: 12px;
    line-height: 130%;
    padding-left: 3px;
}

.cashier-nav {
    font-size: 14px;
    margin: 15px 0 10px;
    text-align: left;
    height:30px;
    border-bottom:solid 2px #CFD2D7;
}
.cashier-nav ol li {
    float: left;
}
.cashier-nav li.current {
    color: #AB4400;
    font-weight: bold;
}
.cashier-nav li.last {
    clear:right;
}
.alipay_link {
    text-align:right;
}
.alipay_link a:link{
    text-decoration:none;
    color:#8D8D8D;
}
.alipay_link a:visited{
    text-decoration:none;
    color:#8D8D8D;
}
</style>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="main">
            <div id="head">
                <dl class="alipay_link">
                    <a target="_blank" href="http://www.alipay.com/"><span>支付宝首页</span></a>| <a target="_blank"
                        href="https://b.alipay.com/home.htm"><span>商家服务</span></a>| <a target="_blank" href="http://help.alipay.com/support/index_sh.htm">
                            <span>帮助中心</span></a>
                </dl>
                <span class="title">支付宝批量付款到支付宝账户有密接口快速通道</span>
            </div>
            <div class="cashier-nav">
                <ol>
                    <li class="current">1、确认信息 →</li>
                    <li>2、点击确认 →</li>
                    <li class="last">3、确认完成</li>
                </ol>
            </div>
            <div id="body" style="clear: left">
                <dl class="content">
                    <dt>付款账号：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDemail" name="WIDemail" runat="server"></asp:TextBox>
                        <span>必填
</span>
                    </dd>
                    <dt>付款账户名：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDaccount_name" name="WIDaccount_name" runat="server"></asp:TextBox>
                        <span>必填，个人支付宝账号是真实姓名公司支付宝账号是公司名称
</span>
                    </dd>
                    <dt>付款当天日期：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDpay_date" name="WIDpay_date" runat="server"></asp:TextBox>
                        <span>必填，格式：年[4位]月[2位]日[2位]，如：20100801
</span>
                    </dd>
                    <dt>批次号：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDbatch_no" name="WIDbatch_no" runat="server"></asp:TextBox>
                        <span>必填，格式：当天日期[8位]+序列号[3至16位]，如：201008010000001
</span>
                    </dd>
                    <dt>付款总金额：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDbatch_fee" name="WIDbatch_fee" runat="server"></asp:TextBox>
                        <span>必填，即参数detail_data的值中所有金额的总和
</span>
                    </dd>
                    <dt>付款笔数：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDbatch_num" name="WIDbatch_num" runat="server"></asp:TextBox>
                        <span>必填，即参数detail_data的值中，“|”字符出现的数量加1，最大支持1000笔（即“|”字符出现的数量999个）
</span>
                    </dd>
                    <dt>付款详细数据：</dt>
                    <dd>
                        <span class="null-star">*</span>
                        <asp:TextBox ID="WIDdetail_data" name="WIDdetail_data" runat="server"></asp:TextBox>
                        <span>必填，格式：流水号1^收款方帐号1^真实姓名^付款金额1^备注说明1|流水号2^收款方帐号2^真实姓名^付款金额2^备注说明2....</span>
                    </dd></dl>
            </div>
            <div id="foot">
                <ul class="foot-ul">
                    <li><font class="note-help">如果您点击“确认”按钮，即表示您同意该次的执行操作。 </font></li>
                    <li>支付宝版权所有 2011-2015 ALIPAY.COM </li>
                </ul>
                <ul>
            </div>
        </div>
    </form>
</body>
</html>