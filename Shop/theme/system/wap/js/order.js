function EditAddress() {
    if ($("#address_edit").is(":hidden"))
        $("#address_edit").show();
    else
        $("#address_edit").hide();
}
function AddAddress(id) {
    if ($("#address_add").is(":hidden"))
        $("#address_add").show();
        if (id > 0)
            LoadPage(path + "/inc/CheckOut_Address.aspx?id=" + id + "&edit_id=" + id, "checkout_address");
    else
        $("#address_add").hide();
}
function Setpay() {
    var obj = $("input[name='pay_id']:checked");
    if (obj.attr("Code") == 'OnlinePay')
        $("#onlinepay").show();
    else
        $("#onlinepay").hide();
}
function Edittransport() {
    if ($("#transport_edit").is(':hidden')) {
        $("#transport_edit").show();
        $("#transport_show").hide();
    }
    else {
        $("#transport_edit").hide();
        $("#transport_show").show();
    }

}
function Changebilltitletype(t) {
    if (t == 1) {
        $("#billtitlecont").hide();
    }
    else {
        $("#billtitlecont").show();
    }
}
function Changebilltype() {
    var obj = $("input[name='billtype_id']:checked");
    var t = obj.attr('billtype');
    var con = obj.attr('billcontent');
    var money = obj.attr('billmoney');
    var TaxRate = obj.attr('TaxRate');
    if (TaxRate == 0) {
        $("#bill151").hide();
        $("#bill152").hide();
        $("#billcondiv").hide();
    }
    else {
        $("#billcondiv").show();
        if (t == 151) {
            $("#bill151").show();
            $("#bill152").hide();
        }
        else {
            $("#bill152").show();
            $("#bill151").hide();
        }
        var strs = new Array();
        strs = con.split(",");
        con = '';
        for (i = 0; i < strs.length; i++) {
            if (i == 0)
                con += '<label><input name="bill_content" value="' + strs[i] + '" type="radio" order="true" checked> ' + strs[i] + '</label> ';
            else
                con += '<label><input name="bill_content" value="' + strs[i] + '" type="radio" order="true"> ' + strs[i] + '</label> ';
        }
        $("#billcon").html(con);
    }
    $("#money_bill").val(money);
    Setmoney();//计算总金额
}
/*
*********************************************
表单相关
*********************************************
*/
function Setmoney() {
    var money_transport = 0;
    var ts = $("input[name='money_transport']");
    if (ts.length == 0)
        return false;
    ts.each(function (i) {
        money_transport = money_transport + parseFloat($(this).val());
    });
    var money_bill = $("#money_bill").val();
    var money_card311 = $("#money_card311").val();
    var money_card312 = $("#money_card312").val();
    var Money_UserCut = $("#Money_UserCut").val();
    $.ajax({
        type: "POST",
        url: path + "/inc/CheckOut_Form.aspx?",
        data: { "money_transport": money_transport, "money_bill": money_bill, "Money_UserCut": Money_UserCut, "money_card311": money_card311, "money_card312": money_card312 },
        dataType: 'html',
        success: function (res) {
            $("#orderform").html(res);
            //SetMoneyCardAndUserMoney();
        }
    });
}
function checkmoneyused() {
    var v = $("#Money_UserCut").val();
    var money_canused = $("#Money_canused").val();
    var CurrentExchangeRate = $("meta[name='CurrentExchangeRate']").attr('content');
    money_canused = (money_canused * CurrentExchangeRate).toFixed(2);
    if (v > money_canused)
        $("#Money_UserCut").val(money_canused);
}
//突出显示一个DIV
function HighlightDiv(objid) {
    $("#overlay").show();
    $("#"+objid).addClass("highlight");
}
function HighlightDivHide() {
    $("#overlay").hide();
}
