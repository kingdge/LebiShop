<%@ Page Language="C#" AutoEventWireup="true" Inherits="huifubao.PostAction"  EnableViewStateMac="false" Codebehind="Default.aspx.cs" %>
<div style="color:Red">以下仅为参考</div>
<form id="frmSubmit" method="post" name="frmSubmit" action="https://pay.heepay.com/Payment/Index.aspx">
<input type="hidden" name="version" value="<%=version %>" />
<input type="hidden" name="agent_id" value="<%= agent_id%>" />
<input type="hidden" name="agent_bill_id" value="<%=agent_bill_id %>" />
<input type="hidden" name="agent_bill_time" value="<%= agent_bill_time%>" />
<input type="hidden" name="pay_type" value="<%=pay_type %>" />
<input type="hidden" name="pay_code" value="<%=pay_code %>" />
<input type="hidden" name="pay_amt" value="<%=pay_amt %>" />
<input type="hidden" name="notify_url" value="<%= notify_url%>" />
<input type="hidden" name="return_url" value="<%= return_url%>" />
<input type="hidden" name="user_ip" value="<%=user_ip %>" />
<input type="hidden" name="goods_name" value="<%= goods_name%>" />
<input type="hidden" name="goods_num" value="<%= goods_num%>" />
<input type="hidden" name="goods_note" value="<%=goods_note %>" />
<input type="hidden" name="is_test" value="<%=is_test %>" />
<input type="hidden" name="remark" value="<%= remark%>" />

<input type="hidden" name="sign" value="<%=sign %>" />

</form>

<script language='javascript'>
window.onload=function(){document.frmSubmit.submit();}
</script>


