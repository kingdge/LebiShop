<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WXPay.aspx.cs" Inherits="weixinpay.WXPay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>公众号JSAPI支付测试网页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <script language="javascript" src="http://res.mail.qq.com/mmr/static/lib/js/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="http://res.mail.qq.com/mmr/static/lib/js/lazyloadv3.js" type="text/javascript"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
    <script language="javascript" type="text/javascript">
        // 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
        //        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        $(function() {
            //公众号支付
            jQuery('a#getBrandWCPayRequest').click(function(e) {
                WeixinJSBridge.invoke('getBrandWCPayRequest', {
                    "appId": "<%= appId %>", //公众号名称，由商户传入
                    "timeStamp": "<%= timeStamp %>", //时间戳
                    "nonceStr": "<%= nonceStr %>", //随机串
                    "package": "<%= package %>", //扩展包
                    "signType": "MD5", //微信签名方式:1.sha1
                    "paySign": "<%= paySign %>" //微信签名
                }, function(res) {
                    if (res.err_msg == "get_brand_wcpay_request:ok") {
                        alert("微信支付成功!");
                    } else if (res.err_msg == "get_brand_wcpay_request:cancel") {
                        alert("用户取消支付!");
                    } else {
                        alert(res.err_msg);
                        alert("支付失败!");
                    }
                    // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                    //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
                });
            });
        });
    </script>
</head>
<body>
    <div class="WCPay">
        <a id="getBrandWCPayRequest" href="javascript:void(0);">
            <h1 class="title">点击提交可体验微信支付</h1>
        </a>

    </div>
</body>
</html>
