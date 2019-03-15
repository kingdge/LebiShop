/*
 *********************************************
收集表单数据
约定：
页面的INPUT表签添加CZFormObj=true属性
 *********************************************
 */
function GetFormJsonData(id) {
	if (id == undefined)
		id="CZFormObj";
	return $("[" + id + "]").serialize();
}

// 获取chk选择的值 以 ,隔开
function GetChkCheckedValues(id) {
    if(id==undefined)
        id='ChkSingleID';
	var ids = "";
	var chks = $("input[type='checkbox'][name='"+id+"']:checked");
	for ( var index = 0; index < chks.length; index++) {
		if (index > 0)
			ids += ",";
		ids += $(chks[index]).val();
	}
	return ids;
}
// 获取chk选择的值 以 , 隔开
function GetChkValues(name) {
	var result = "";
	var chks = $("input[name='" + name + "']:checked");
	$.each(chks, function(i, o) {
		if (i > 0)
			result += ",";
		result += $(o).val();
	});
	return result;
}
//通用表单验证方法
function CheckForm(id,box) {
	
    if (id == undefined)
		id = 'CZFormObj';
	var formElement
	formElement = $("[" + id + "]");
	var str = "";
	var key = "";
	var value = "";
	var len = 0;
	var min = 0;
	var max = 0;
	var status = true;
	$.each(formElement, function(i, obj) {
//debugger;

		min = $(obj).attr("min");
		max = $(obj).attr("max");
		min = min == undefined ? 0 : min;
		max = max == undefined ? 0 : max;
		key = $(obj).attr("id");
		value = $(obj).val();
        if(value!=null)
		len = value.length;

		switch ($(obj).attr("type")) {
		case "text":
			if(!CheckText(key, len, min, max,box))
                status=false;
			break
		case "radio":
			//if(!CheckText(key, len, min, max,box))
            //   status=false;
			break
		case "checkbox":
			//if(!CheckText(key, len, min, max,box))
            //    status=false;
			break
		default:
			if(!CheckText(key, len, min, max,box))
                status=false;
			break
		}
	});
	return status;

}
//输入长度验证
function CheckText(key, len, min, max,box) {
	 //debugger;
    var status=true;
    if (min == 'notnull') {
   		if (len ==0) {
   			CheckNO(key, " ",box);
   			status = false;
   		} else {
             CheckOK(key, '', box);
   		}
   	}
     else if (min == 'email') {
         status = CheckEmail(key, box);
   	}else{
        if (min > 0 && max > 0) {
    		if (len < min || len > max) {
    		    CheckNO(key, Tag("长度限制") + "{ " + min + " - " + max + " }", box);
    			status = false;
    		} else {
    			CheckOK(key,'',box);
    		}
    	} else if (min > 0) {
    		if (len < min) {
    		    CheckNO(key, Tag("长度不能小于") + min, box);
    			status = false;
    		} else {
                CheckOK(key, '', box);
    		}
    	} else if (max > 0) {
    		if (len > max) {
    		    CheckNO(key, Tag("长度不能大于") + max, box);
    			status = false;
    		} else {
                CheckOK(key, '', box);
    		}
    	}

    }
    return status;
}
//输入长度验证
function CheckLength(key,box) {
    var min = $("#"+key+"").attr("min");
	var max = $("#"+key+"").attr("max");
	var	min = min == undefined ? 0 : min;
	var	max = max == undefined ? 0 : max;
	var	value = $("#"+key+"").val();
	var	len = value.length;
    return CheckText(key, len, min, max,box);
}
//输入提示
function InputStatus(id,box){
	//debugger;
	var obj = $("#"+id);
	var max=$(obj).attr("max");
	if(max==0)
		return;
	var len=$(obj).val().length;
    if (box == undefined)
		box = "span";
	if (document.getElementById("Form" + id) == undefined)
		$("#" + id + "").after("<"+box+" id=\"Form" + id + "\"></"+box+">");
    $("#Form" + id + "").removeClass("FormNO");
    $("#Form" + id + "").removeClass("FormYES");
    $("#Form" + id + "").addClass("FormALT");
	if(len>max || len==max)
		$(obj).val($(obj).val().substring(0,max));
	//$("#Form" + id + "").html("当前输入 "+len+" 个字符，最多 "+max+" 个，剩余 "+((max-len)<0?0:(max-len))+" 个");
    $("#Form" + id + "").html(Tag("字数")+":"+len+"/"+max+"");
	
}
//验证成功
function CheckOK(id, msg,box) {
    //return;
	if (msg == undefined)
		msg = "OK";
    if (box == undefined)
		box = "span";
	if (document.getElementById("Form" + id) == undefined)
		$("#" + id + "").after("<"+box+" id=\"Form" + id + "\"></"+box+">");
    $("#Form" + id + "").removeClass("FormNO");
    $("#Form" + id + "").removeClass("FormALT");
    $("#Form" + id + "").addClass("FormYES");
	$("#Form" + id + "").html(msg);
}
//验证失败
function CheckNO(id, msg,box) {
	if (msg == undefined)
		msg = "NO";
    if (box == undefined)
		box = "span";
	if (document.getElementById("Form" + id) == undefined)
		$("#" + id + "").after("<"+box+" id=\"Form" + id + "\"></"+box+">");
    $("#Form" + id + "").removeClass("FormYES");
    $("#Form" + id + "").removeClass("FormALT");
    $("#Form" + id + "").addClass("FormNO");
	$("#Form" + id + "").html(msg);
}

//验证邮箱
function CheckEmail(key,box)
{
    var str=$("#"+key+"").val();
    if(str==''){
        CheckNO(key, Tag("请填写您的邮箱"),box);
        return false;
    }
    if (IsEmail(str)) {
        CheckOK(key, "",box);
        return true;
    }else{
        CheckNO(key, Tag("邮箱格式错误"),box);
        return false;
    }
}
//验证邮箱
function IsEmail(email) {
    var reg = /^([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,5}$/;
    if (reg.test(email)) {
        return true;
    }
    return false;
}
function Len(s) {
 var l = 0;
 var a = s.split("");
 for (var i=0;i<a.length;i++) {
  if (a[i].charCodeAt(0)<299) {
   l++;
  } else {
   l+=2;
  }
 }
 return l;
}
/*
 * ******************************************** 
 编辑窗口
 * ********************************************
 */
function EditWindow(title_, url_, width_, height_, modal_, div_) {
    modal_ = false;
	if ($("#" + div_)[0] == undefined) {
		$("body").after("<div id=\"" + div_ + "\"></div>");
	}
	$('#' + div_ + '').dialog({
		autoOpen : false,
		height : height_,
		width : width_,
		modal : modal_,
		title : title_
	});
    $("#" + div_ + "").dialog("open");
    $(".ui-dialog-titlebar").attr("class", "ui-dialog-title-" + div_ + " ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix");
    $(".ui-dialog-title-" + div_ + "").show();
	$.ajax({
		type : 'POST',
		url : url_,
		data : '',
		dataType : 'html',
		beforeSend : function() {
		    $('#' + div_ + '').html('<img src="' + path + '/theme/system/images/flag_loading.gif">');
		},
		success : function(data) {
			$('#' + div_ + '').html(data);

		}
	});
}

/*
***********************************************
更改下来框内容
***********************************************
*/
function UpdateOption(objid, data, selectedvalue,firstitem) {
        //参数字符串是JSON 数据格式[{name:'1',value:'0'},{name:'1',value:'0'}]
        data = eval("(" + data + ")"); 
        //data = [{name:'1',value:'0'},{name:'10',value:'001'}]; 
        var stlect = "";
        $("#" + objid + "").empty();
        if(firstitem!=undefined && firstitem!='')
        $("#" + objid + "").append("<option value='0'>"+firstitem+"</option>");
        for(var o in data){   
        //$.each(data, function (i) {
            //var o=data[i];
            //if(o==undefined)
            //   alert(i);
            stlect = "";
            if (selectedvalue == data[o].value) {
                stlect = "selected";
            }
            $("#" + objid + "").append("<option value='" + data[o].value + "' " + stlect + ">" + data[o].name + "</option>");
        //});
        }
    }
    /*
    *********************************************
    全部选中/取消
    *********************************************
    */

    function CheckALL(obj, tag) {
        if (obj.checked) {
            $("input[" + tag + "='true']").each(function () {
                $(this).attr("checked", true);
            });
        }
        else {
            $("input[" + tag + "='true']").each(function () {
                $(this).attr("checked", false);
            });
        }
    }
/**
***********************************************
获取远程HTML加载到指定ID的控件中
***********************************************
*/
function GetHtml(url,obj){
    $.ajax({
            type: 'POST',
            url: url,
            dataType: 'html',
            success: function (data) {
                $("#"+obj+"").html(data);
           }
    });
}
/**
***********************************************
登录窗口
***********************************************
*/
function LoginWindow(){
    var url =path+ "/login.aspx";
    Dialog(Tag("登录"), "url:"+url, 450, 300);
}


function SetCookie(name,value,hours)//两个参数，一个是cookie的名子，一个是值
{
   
    var exp  = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + hours*60*60*1000);
    document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString()+";path=/";
}
function GetCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
     if(arr != null) return unescape(arr[2]); return null;

}
function DelCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval=GetCookie(name);
    if(cval!=null) document.cookie= name + "="+cval+";expires="+exp.toGMTString();
}

//复制
function windowcopy(str){
    window.clipboardData.setData('text', str);
}
//退出登录
function LoginOut() {
    $.ajax({
        type: 'POST',
        url: path + "/ajax/ajax_user.aspx?__Action=User_LoginOut",
        dataType: 'json',
        success: function (data) {
            if (data.msg == "OK")
                MsgBox(1, data.mes, data.url);
            else
                MsgBox(2, Tag("系统异常"), "?");
        }
    });
}
//将一个商品放入常购清单
function AddOftenBuy(id, num, property){
    var title_ = Tag("加入常购清单");
    var url_ = path + "/inc/OftenBuyDialog.aspx?pid=" + id + "&num=" + num + "&property=" + escape(property) + "&random=" + Math.random();
    var width_ = 300;
    var height_ = 150;
    var modal_ = true;
    var div_ = "div_window";
    EditWindow(title_, url_, width_, height_, modal_, div_);
}
//将一个商品放入收藏夹141或购物车142
function UserProduct_Edit(id, type, num, action, property) {
    if (num == undefined)
        num = 1;
    if (action == undefined)
        action = 'add';
    if (property == undefined)
        property = 'Property134';
    var propertys = '';
    $("input[name='" + property + "']").each(function () {
        var properid = $(this).attr('propertyid');
        var propervalue = $('#' + property + '_' + properid).val();
        if (propervalue == '') {
            return true;
        }
        var tempstr = $(this).val() + ":" + propervalue;
        if (propertys == '')
            propertys = tempstr;
        else
            propertys += ', ' + tempstr;
    });
    var propertypriceids = '';
    $("select[selectname='" + property + "']").each(function () {
        var ppid = $(this).find("option:selected").attr('propertypriceid');
        if (propertypriceids == '')
            propertypriceids = ppid;
        else
            propertypriceids += ',' + ppid;
    });
    $.ajax({
        type: 'POST',
        url: path + "/ajax/ajax_user.aspx?__Action=UserProduct_Edit",
        dataType: 'json',
        data: { "pid": id, "type": type, "num": num, "property": propertys, "propertypriceids": propertypriceids },
        success: function (data) {
            if (data.msg == "OK") {
                if (type == 142 && action == "quickbuy") {
                    window.location = data.url;
                } else {
                    if (type == 142) {
                        $("#shoppingcartline_count").html(data.count);
                        $("#shoppingcartline_amount").html(data.amount);
                        $("#shoppingcart_count").html(data.count);
                        $("#shoppingcart_amount").html(data.amount);
                        MsgBox(1, data.mes, "", "", 10000, "addtocart");
                    } else {
                        MsgBox(1, data.mes, "");
                    }
                }
            } else {
                MsgBox(2, data.msg, data.url);
            }
        }
    });
}
//将一个商品从购物车或收藏夹中删除，收藏夹141或购物车142
function UserProduct_Del(id, type) {
    $.ajax({
        type: 'POST',
        url: path + "/ajax/ajax_user.aspx?__Action=UserProduct_Del&random=" + Math.random(),
        dataType: 'json',
        data: { "pid": id, "type": type},
        success: function (data) {
            if (data.msg == "OK")
                MsgBox(1, data.mes, '');
            else
                MsgBox(2, data.msg, '');
        }
    });
}
//将收藏夹商品加入购物车
function LikeToBasket() {
    var postData = GetFormJsonData("sel");
    $.ajax({
        type: 'POST',
        url: path + "/ajax/ajax_userin.aspx?__Action=LikeToBasket&random=" + Math.random(),
        dataType: 'json',
        data: postData,
        success: function (data) {
            if (data.msg == "OK") {
                $("#shoppingcartline_count").html(data.count);
                $("#shoppingcartline_amount").html(data.amount);
                $("#shoppingcart_count").html(data.count);
                $("#shoppingcart_amount").html(data.amount);
                MsgBox(1, data.mes, "", "", 10000, "addtocart");
            } else {
                MsgBox(2, data.msg, data.url);
            }
        }
    });
}
//选择地区下拉框
function GetAreaList(topid,area_id) {
    $.ajax({
        type: 'POST',
        url: path + "/ajax/ajax.aspx?__Action=GetAreaList&random=" + Math.random(),
        dataType: 'html',
        data: { "topid": topid, "area_id": area_id},
        success: function (data) {
            $("#Area_id_div").html(data);
        }
    });
}
//选择地区下拉框-选择动作
function SelectAreaList(topid,objid) {
    $.ajax({
        type: 'POST',
        url: path + "/ajax/ajax.aspx?__Action=GetAreaList&random=" + Math.random(),
        dataType: 'html',
        data: { "topid": topid, "area_id": $("#" + objid).val() },
        success: function (data) {
            $("#Area_id_div").html(data);
        }
    });
}
//加载一个页面到一个容器
function LoadPage(url, div) {
    if (url.indexOf("?") > -1) {
        url = url + "&random=" + Math.random();
    } else {
        url = url + "?random=" + Math.random();
    }
    $.ajax({
        type: "POST",
        url: url,
        data: '',
        dataType: 'html',
        beforeSend: function () {
            $("#" + div + "").html('<img width="25" height="25" src="'+ path + '/theme/system/images/load.gif"/>');
        },
        success: function (res) {
            $("#" + div + "").html(res);
        }
    });
}
function LoadPageNoSend(url, div) {
    if (url.indexOf("?") > -1) {
        url = url + "&random=" + Math.random();
    } else {
        url = url + "?random=" + Math.random();
    }
    $.ajax({
        type: "POST",
        url: url,
        data: '',
        dataType: 'html',
        success: function (res) {
            $("#" + div + "").html(res);
        }
    });
}
function PrintPage(url) {
    if (url.indexOf("?") > -1) {
        url = url + "&random=" + Math.random();
    } else {
        url = url + "?random=" + Math.random();
    }
    $.ajax({
        type: "POST",
        url: url,
        data: '',
        dataType: 'html',
        beforeSend: function () {
            document.write('<img width="25" height="25" src="' + path + '/theme/system/images/load.gif"/>');
        },
        success: function (res) {
            document.write(res);
        }
    });
}
//通用数据提交方法
function RequestAjax(url, jsondata, callback) {
    if (url.indexOf("?") > -1) {
        url = url + "&random=" + Math.random();
    } else {
        url = url + "?random=" + Math.random();
    }
    $.ajax({
        type: "POST",
        url: url,
        data: jsondata,
        dataType: 'json',
        beforeSend: function () {
            MsgBox(4, Tag("正在处理") + " ……", "-");
        },
        success: function (res) {
            if (res.msg == "OK") {
                callback(res);
                return false;
            }
            else {
                if (res.url != "" && res.url != undefined) {
                    MsgBox(2, res.msg, res.url);
                } else {
                    MsgBox(2, res.msg, "","",3000);
                }
                return false;
            }
        }
    });
}
//切换语言
function SetLanguage(id, code, lpath) {
    DelCookie("Currency");//删除币种信息
    if (lpath == undefined)
        lpath = "";
    if(lpath=="")
        window.location.reload();
    else
        window.location = (sitepath + lpath).replace('//','/');
}
//切换币种
function SetCurrency(id, Code, ExchangeRate, Msige, Length) {
    $("meta[name='CurrenctCurrency']").attr('content', Code);
    $("meta[name='CurrentExchangeRate']").attr('content', ExchangeRate);
    $("meta[name='CurrenctCurrencyMsige']").attr('content', Msige);
    $("meta[name='CurrenctCurrencyLength']").attr('content', Length);
    $(".currency .noclick s").html(Code);
    $(".currency_li_content").hide();
    SetCookie("Currency", Code, 30);
    //SetCookie("Currencyid", id, 30);
    var money0 = 0;
    var money1 = 0;
    $(".lebimoney").each(function () {
        money0 = $(this).children("font.money_0").html();
        money1 = (money0 * ExchangeRate).toFixed(Length);
        $(this).children("font.msige").html(Msige);
        $(this).children("font.money_1").html(money1);
    });
    $(".lebimsige").each(function () {
        $(this).html(Msige);
    });
}
//格式化货币
function FormatMoney(money) {
    var msige = $("meta[name='CurrenctCurrencyMsige']").attr('content');
    var ExchangeRate = $("meta[name='CurrentExchangeRate']").attr('content');
    var Length = $("meta[name='CurrenctCurrencyLength']").attr('content');
    var res = "";
    res+="<span class='lebimoney'>";
    res += "<font class='msige'>" + msige + "</font>";
    res += "<font class='money_1 price'>" + (money * ExchangeRate).toFixed(Length) + "</font>";
    res += "<font class='money_0' style='display:none;'>" + money + "</font>";
    res += "</span>";
    return res;
}
//=====================================
//cookie操作
function SetCookie(name, value, days)//两个参数，一个是cookie的名子，一个是值
{
    var exp = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
}
function GetCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}
function DelCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = GetCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
//cookie操作
//=====================================
//收缩展开菜单
$(function () {
    var $dropcurrency = $(".dropcurrency"), $dropcurrency_li = $dropcurrency.find('li.currency_li'), $dropcurrency_li_content = $dropcurrency_li.find('.currency_li_content');
    $dropcurrency_li.hover(function () {
        $dropcurrency_li_content.stop(true, true).fadeIn(0);
    }, function () {
        $dropcurrency_li_content.fadeOut(500, function () {
            $dropcurrency_li_content.css("display", "none");
        });
    });
    var $droplanguage = $(".droplanguage"), $droplanguage_li = $droplanguage.find('li.language_li'), $droplanguage_li_content = $droplanguage_li.find('.language_li_content');
    $droplanguage_li.hover(function () {
        $droplanguage_li_content.stop(true, true).fadeIn(0);
    }, function () {
        $droplanguage_li_content.fadeOut(500, function () {
            $droplanguage_li_content.css("display", "none");
        });
    });
    $("a.noclick").click(function (event) {
        event.preventDefault();
    });
});
//点击隐藏文本框输入
function blurInput(objName, strTip) {
    $(objName).attr('tip', strTip).val(strTip);
    $(objName).focus(
    function () {
        $(this).val("");
    }
)
.blur(
    function () {
        if ($(this).val() == "") {
            $(this).val($(this).attr('tip'));
        }
    }
);
}
/*
*********************************************
js的语言切换
*********************************************
*/
var languagetags; //JS用到的语言标签
function Tag(strin) {
    if (languagetags[strin] == undefined)
        return strin;
    return languagetags[strin];

}
//检查购买商品限购
function checkinputtop(id, low,top) {
    var c = $("#" + id).val();
    if (c > top && top>0) {
        c = $("#" + id).val(top);
        return false;
    }
    if (c < low) {
        c = $("#" + id).val(low);
        return false;
    }
    return true;
}
$(document).ready(function () {
    $(".time_expired").each(function () {
        var endDate = $(this).attr("endDate");
        var productId = $(this).attr("Product");
        var _countdown = new CountDown();
        _countdown.init({
            id: 'time_expired_' + productId,
            time_Dom: $("#time_expired_" + productId),
            //天数
            day_Dom: $("#dayEnd_" + productId),
            //小时
            hour_Dom: $("#hourEnd_" + productId),
            //分钟
            min_Dom: $("#minEnd_" + productId),
            //秒
            sec_Dom: $("#secEnd_" + productId),
            endTime: endDate
        });
    });
});
/*
****************************************************
功能特点：
1.自动向页面添加DIV块
2.无需外部CSS支持

参数说明：

errFlag 错误标志  1:成功2:错误3:警告4:等待
errcu   错误提示
href    跳转页面  空值无跳转，"？"刷新,"-"一直显示 "#"转到来源页面
mode    显示模式  为空时没有按钮，非空时显示按钮，按钮文字为mode值
****************************************************
*/

function MsgBox(errFlag, err, href, mode, time, title) {
    if (mode == undefined)
        mode = "";
    if (time == undefined)
        time = 2000;
    if (title == undefined)
        title = "error";
    err = msgstr(errFlag, err, href, mode);
    title_ = "";
    modal_ = false;
    switch (errFlag) {
        case 1:
            title_ = '系统信息：';
            $("#div_window").dialog('close');
            break;
        case 2:
            title_ = '出错啦！';
            if (err == "") err = "数据保存失败";
            break;
        case 3:
            title_ = '系统信息：';
            break;
        case 4:
            title_ = '请等待...';
            break;
    }
    if ($("#div_" + title + "")[0] == undefined) {
        $("body").after("<div id=\"div_" + title + "\"></div>");
    }
    $("#div_" + title + "").dialog({
        width: 'auto',
        height: 'auto',
        autoOpen: false,
        modal: modal_,
        hide: {
            effect: "Fade",
            duration: 1500
        }
    });
    $("#div_" + title + "").dialog('open');
    $(".ui-dialog-titlebar").attr("class", "ui-dialog-title-div_" + title + " ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix");
    $(".ui-dialog-title-div_" + title + "").hide();
    $("#div_" + title + "").attr("style", "display:block;min-height:25px;line-height:160%;padding:5px 20px;text-align:left;height:auto !important;width:auto !important");
    $("#div_" + title + " img").attr("style", "margin:17px 0;vertical-align:middle");
    if (title == "error") {
        $("#div_" + title + "").html("<span onclick=\"cloesedialog('error');\">" + err + "</span>");
    } else {
        $("#div_" + title + "").html(err);
    }
    if (mode == "")
        setTimeout(function () { closemsgbox(href, title); }, time);
}
function cloesedialog(title) {
    if (title == undefined)
        title = "addtocart";
    $("#div_" + title + "").dialog("close");
}
function closemsgbox(href, title) {
    //    if (href != "" && href != "?" && href != "-" && href != "#") {
    //        setTimeout(function () { window.location = href; }, t);
    //    }
    //    else if (href == "-") {
    //        //一直显示
    //    }
    //    else if (href == "#") {
    //        window.history.go(-1);
    //    }
    //    else if (href == "?") {
    //        setTimeout(function () { window.location.reload(); }, t);
    //    }
    //    else {
    //        setTimeout(function () { MsgBoxClose(href); }, t);
    //    }
    if (href == "-") {
        //一直显示
    }
    else {
        MsgBoxClose(href, title);
    }
}
function msgstr(flag, err, href, mode) {
    var table = '<img width="25" height="25" src="' + path + '/theme/system/images/';
    switch (flag) {
        case 1:
            table += 'flag_ok.gif';
            if (err == "") err = "数据保存成功";
            break;
        case 2:
            table += 'flag_no.gif';
            if (err == "") err = "数据保存失败";
            break;
        case 3:
            table += 'flag_alert.gif';
            break;
        case 4:
            table += 'flag_loading.gif';
            break;
    }
    table += '"/> ' + err + '';
    return table;
}
function MsgBoxClose(href, title) {
    if (href == undefined)
        href = "";
    if (title == undefined)
        title = "error";
    href = href.replace("//", "/");
    $("#div_"+ title +"").dialog("close");
    if (href != "" && href != "?" && href != "-" && href != "#") {
        setTimeout(function () { window.location = href; }, 0);
    } else if (href == "?") {
        setTimeout(function () { window.location.reload(); }, 0);
    } else if (href == "#") {
        setTimeout(function () { window.history.go(-1); }, 0);
    } else if (href == "") {
        $("#div_" + title + "").dialog("close");
    }

}
function MsgHtml(flag, err, href) {
    $(".tools .mes").show();
    var mes = '<img width="16" height="16" align="absmiddle" src="' + path + '/theme/system/images/';
    switch (flag) {
        case 1:
            mes += 'succes.gif';
            break;
        case 2:
            mes += 'error.gif';
            break;
    }
    mes += '"/>';
    switch (flag) {
        case 1:
            if (err == "") err = "数据保存成功";
            mes += '<font class="suc">' + err + '</font>';
            break;
        case 2:
            if (err == "") err = "数据保存失败";
            mes += '<font class="err">' + err + '</font>';
            break;
    }
    if (href != "") {
        mes += '(<font color=red><span id="backtime">3</span></font>)';
        if (!href) { href = document.referrer }
        countDown(3);
        setTimeout(function () { window.location.href = href; }, 2000);
    }
    $("#mes").html(mes);
    if (href == "") {
        setTimeout(function () { $(".tools .mes").hide(); }, 3000);
    }
}
function MsgError(err) {
    $("#msg-error").show()
    $("#msg-error").html("<b></b>" + err + "");
}
function refresh(id) {
    $("#"+ id +"").attr("src","" + path + "/code.aspx?t="+Math.random());
}
function countDown(secs) { $("#backtime").html(secs); if (--secs > 0) { setTimeout("countDown(" + secs + ")", 1000); } }
function CountDown(parameters) {
    var obj = {

        //countdown元素id
        countId: '',

        //倒计时结束时请求的url
        endurl: '',

        //倒计时结束时的回调函数,用于处理页面倒计时元素的移除等操作
        endcallback: '',

        //倒计时是否结束
        isEnd: false,

        //结束时间点时间对象
        endStemp: null,
        //天数
        day_Dom: null,
        //小时
        hour_Dom: null,
        //分钟
        min_Dom: null,
        //秒
        sec_Dom: null,
        /**
        * 初始化倒计时
        * @param currnt
        * @param end
        */
        init: function (o) {
            this.countId = o.id || 'CountDown';
            this.endurl = o.endurl || '';
            this.time_Dom = o.time_Dom;
            this.day_Dom = o.day_Dom;
            this.hour_Dom = o.hour_Dom;
            this.min_Dom = o.min_Dom;
            this.sec_Dom = o.sec_Dom;
            this.endcallback = o.callback || function () { return false; };
            this.endStemp = new Date(o.endTime.replace(/-/g,"/"));
            this._CountDownLoop();
        },

        /**
        * 倒计时循环
        * @private
        */
        _CountDownLoop: function () {
            var currStemp = new Date();
            //        console.log(currStemp.getTime());
            //如果结束时间戳减去当前时间时间戳小于等于0则设置倒计时结束标识为true
            if ((this.endStemp.getTime() - currStemp.getTime()) <= 0) {
                this.time_Dom.text(Tag("已过期"));
                this.isEnd = true;
            }
            //如果结束则调用结束回调
            if (this.isEnd === true) {
                // console.log('countdown end');
                this.endcallback.apply(this, [this.endurl]);
            } else {
                this._render(currStemp);
                var that = this;
                requestAnimation(function () {
                    that._CountDownLoop();
                });
            }
        },

        /**
        * 使用倒计时时间渲染倒计时元素
        * @private
        */
        _render: function (currStemp) {
            var t = this.endStemp.getTime() - currStemp.getTime();
            // 总秒数  
            var xt = parseInt(t / 1000);
            // 秒数  
            var remain_sec = xt % 60;
            xt = parseInt(xt / 60);
            // 分数  
            var remain_minute = xt % 60;
            xt = parseInt(xt / 60);
            // 小时数  
            var remain_hour = xt % 24;
            xt = parseInt(xt / 24);
            // 天数  
            var remain_day = xt;
            //        console.log(remain_day);
            //        console.log($('#day'));
            this.day_Dom.text(remain_day);
            this.hour_Dom.text(remain_hour);
            this.min_Dom.text(remain_minute);
            this.sec_Dom.text(remain_sec);
        }

    }, requestAnimation = (function (callback) {
        return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame ||
        function (callback) {
            window.setTimeout(callback, 1000 / 60);
        };
    })();
    return obj;
}