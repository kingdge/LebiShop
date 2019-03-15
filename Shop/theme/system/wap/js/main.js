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
   		CheckEmail(key,box);
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
function CheckEmail(key, box) {
    var str = $("#" + key + "").val();
    if (str == '') {
        CheckNO(key, Tag("请填写您的邮箱"), box);
        return false;
    }
    if (IsEmail(str)) {
        CheckOK(key, "", box);
        return true;
    } else {
        CheckNO(key, Tag("邮箱格式错误"), box);
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
    for (var i = 0; i < a.length; i++) {
        if (a[i].charCodeAt(0) < 299) {
            l++;
        } else {
            l += 2;
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
	$('#' + div_ + '').dialog('open');
	$.ajax({
		type : 'POST',
		url : url_,
		data : '',
		dataType : 'html',
		beforeSend : function() {
		    $('#' + div_ + '').html('<img src="' + path + '/theme/system/images/flagwait.gif">');
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
function AddOftenBuy(id, num, property) {
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
    $("select[name='" + property + "']").each(function () {
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
function refresh(id) {
    $("#" + id + "").attr("src", "" + path + "/code.aspx?random=" + Math.random());
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
                    MsgBox(2, res.msg, "", "", 3000);
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
        window.location = sitepath + lpath;
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

}
//格式化货币
function FormatMoney(money) {
    var msige = $("meta[name='CurrenctCurrencyMsige']").attr('content');
    var ExchangeRate = $("meta[name='CurrentExchangeRate']").attr('content');
    var Length = $("meta[name='CurrenctCurrencyLength']").attr('content');
    var res = "";
    res += "<span class='lebimoney'>";
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
function checkinputtop(id, low, top) {
    var c = $("#" + id).val();
    if (c > top && top > 0) {
        c = $("#" + id).val(top);
        return false;
    }
    if (c < low) {
        c = $("#" + id).val(low);
        return false;
    }
    return true;
}
$(function () {
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
            this.endStemp = new Date(o.endTime.replace(/-/g, "/"));
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

function InstantSearchForm()
{
//	alert("showSearchForm");
/*
	if((navigator.userAgent.indexOf('iPad') != -1 || 
	(navigator.userAgent.indexOf('iPhone')!= -1 ||
	(navigator.userAgent.indexOf('iPod')!= -1)
*/
	this.isMobile = (navigator.userAgent.indexOf('iPhone') != -1);
	
	this.searchFormSwitcher = document.querySelectorAll('.btnSearch');

	this.init();
}

//
InstantSearchForm.prototype.init = function()
{
	var event = (this.isMobile) ? 'touchend' : 'click';
	for(var i=0; i<this.searchFormSwitcher.length; i++)
	{
        var switcher = this.searchFormSwitcher[i];
		switcher.addEventListener(event, this.switchForm.bind(this, switcher), false);
	}
}

InstantSearchForm.prototype.switchForm = function(switcher)
{
	this.initForm();
	var isVisable = this.searchPanel.hasClassName('visable');
	var curCls = isVisable ? 'visable' : 'hidden';
	var newCls = isVisable ? 'hidden' : 'visable';
	
	header.style.height = isVisable ? '47px' : '83px';
	this.searchPanel.removeClassName(curCls);
	this.searchPanel.addClassName(newCls);
}


InstantSearchForm.prototype.initForm = function()
{
	
	if(this.searchPanel)
		return;
	this.searchPanel = document.getElementById("instantSearchPanel");
}

function testSubmit(){
	var reg = /[^\w\.]/g;
	alert("ee")
	//var ss = kw.replace(reg, "");
	kw  = encodeURIComponent($.trim($("#k").val()).replace(reg, "-"));
	//category = $.trim($("#category").val());
	if ((kw == '') || (kw == 'Products keyword')){
		alert('Please enter keywords!');
		$("#k").val('');						   
		$("#k").focus();
		return false;
	}else{
		kw = kw.replace('%20','-');
		//if (category == '0'){
			window.location.href='/s/'+kw+'/';
		//}else{
		//	window.location.href='/s/'+kw+'/'+category+'/';
		//}
	}
};
///////////////
var popWinConfig = {
	returningVisitor: false,
	animationIn: 'bubble',
	animationOut: 'bubble',
	lifespan:30000,
	expire:720,//720
	touchIcon:false,	
	message:'<dl><dd><a class="popWinIco" href="http://appstore.com/sammydress"> </a></dd><dd>Download Sammydress App, Make Order By 6% OFF.</dd></dl>'

};
///////////////

var gInstantSearchForm;
function setupInstantSearchForm()
{
	gInstantSearchForm = new InstantSearchForm();
}

/* ==================== Element Extensions ==================== */
Function.prototype.bind = function(thisObject)
{
    var func = this;
    var args = Array.prototype.slice.call(arguments, 1);
    return function() {
    	return func.apply(thisObject, args.concat(Array.prototype.slice.call(arguments, 0)))
    };
}

Element.prototype.removeAllChildren = function ()
{
  while (this.firstChild) {
    this.removeChild(this.firstChild);
  }
};

/**
 *  Indicates whether the element has a given class name within its class attribute.
 */
Element.prototype.hasClassName = function (className) {
  return new RegExp('(?:^|\\s+)' + className + '(?:\\s+|$)').test(this.className);
};

/**
 *  Adds the given class name to the element's class attribute if it's not already there.
 */
Element.prototype.addClassName = function (className) {
  if (!this.hasClassName(className)) {
    this.className = [this.className, className].join(' ').replace(/^\s*|\s*$/g, "");
  }
};

/**
 *  Removes the given class name from the element's class attribute if it's there.
 */
Element.prototype.removeClassName = function (className) {
  if (this.hasClassName(className)) {
    var curClasses = this.className;
    this.className = curClasses.replace(new RegExp('(?:^|\\s+)' + className + '(?:\\s+|$)', 'g'), ' ').replace(/^\s*|\s*$/g, "");
  }
};

/* ==================== event swipe ==================== */
window.Swipe=function(element,options)
{
	if(!element) return null;
	
	var _this=this;
	this.options=options||{};
	this.index=this.options.startSlide||0;
	this.speed=this.options.speed||300;
	this.callback=this.options.callback||function(){};
	this.delay=this.options.autoPlay||0;
	//this.delay = 1000;
	//alert(this.delay);
	this.container=element;
 	this.indicatorStyle=this.options.indicatorStyle;

	this.element=this.container.children[0];
	
	this.container.style.overflow='hidden';
	this.element.style.listStyle='none';
	
	this.gestureAllowed=this.options.gestureAllowed||false;
	this.tapAllowed=this.options.tapAllowed||false;
	this.zoomingAllowed=this.options.zoomingAllowed||false;
	
	this.items=this.options.items;

	this.init();
	this.setup();
	
	this.beginAnimation();
	
	if(this.element.addEventListener){
		this.element.addEventListener('touchstart',this,false);
		this.element.addEventListener('touchmove',this,false);
		this.element.addEventListener('touchend',this,false);
	
		this.element.addEventListener('webkitTransitionEnd',this,false);
		this.element.addEventListener('msTransitionEnd',this,false);
		this.element.addEventListener('oTransitionEnd',this,false);
		this.element.addEventListener('transitionend',this,false);

		if(this.tapAllowed)
			this.element.addEventListener('click',this,false);

		window.addEventListener('resize',this,false)
	}
};
Swipe.prototype={
	setup:function(){
		this.slides=this.element.children;
		this.length=this.slides.length;
		
		if(this.length < 2)
			return null;		
			
		this.width=this.container.getBoundingClientRect().width;
		
		if(!this.width)
			return null;
		
		this.container.style.visibility='hidden';
		this.element.style.width=(this.slides.length*this.width)+'px';
		
		var index=this.slides.length;
		
		while(index--)
		{
			var el=this.slides[index];
			el.style.width=this.width+'px';
			el.style.display='table-cell';
			el.style.verticalAlign='top'
		}
		
		this.slide(this.index,0);
		this.container.style.visibility='visible'
	},
	slide:function(index,duration){
		//alert(duration);
		
		var style=this.element.style;
		this.getWidth();
		
		style.webkitTransitionDuration
		=style.MozTransitionDuration
		=style.msTransitionDuration
		=style.OTransitionDuration
		=style.transitionDuration
		=duration+'ms';
		
		style.webkitTransform='translate3d('+ -(index*this.width)+'px,0,0)';
		style.msTransform
		=style.MozTransform
		=style.OTransform
		='translateX('+ -(index*this.width)+'px)';
		
		this.index=index;
	},
	beginAnimation:function(){
		var _this=this;
		
		if(this.delay)
		{
			var m = this.delay;
			var n = _this.delay;
			this.interval=(m)?setTimeout(function(){_this.nextPage(n)},m):0
		}
	},

//	prevPage:function(delay){
//		this.delay=delay||0;
//		clearTimeout(this.interval);
//		
//		if(this.index)
//			this.slide(this.index-1,this.speed);
//			
//		this.showPage()
//	},
	nextPage:function(delay){
		this.delay=delay||0;
		clearTimeout(this.interval);
		var n = (this.index<this.length-1)?this.index+1:0;
		this.loadItem(n);
		this.slide(n,this.speed);
		this.showPage();
	},
	stopAnimation:function(){
		this.delay=0;
		clearTimeout(this.interval);
	},
	resumeAnimation:function(){
		this.delay=this.options.autoPlay||0;this.beginAnimation();
	},
	handleEvent:function(e){
		switch(e.type){
			case'touchstart':this.onTouchStart(e);break;
			case'touchmove':this.onTouchMove(e);break;
			case'touchend':this.onTouchEnd(e);break;
			
			case'click':this.clicked(e);break;
			
			case'webkitTransitionEnd':
			case'msTransitionEnd':
			case'oTransitionEnd':
			case'transitionend':
			this.transitionEnd(e);break;
			case'resize':this.setup();break
		}
	},
	transitionEnd:function(e){
		//alert("transitionEnd .beginAnimation"+this.delay);
		//this.stopAnimation();
		
		if(this.delay){
			//alert("transitionEnd .beginAnimation"+this.delay);
			this.beginAnimation();
		}
			
		this.slides[this.index];
			
//		this.callback(e,this.index,this.slides[this.index])
	},
	
	onTouchStart:function(e){
		this.start={
			pageX:e.touches[0].pageX,pageY:e.touches[0].pageY,time:Number(new Date())
		};
		
		this.isScrolling=undefined;this.deltaX=0;
		this.element.style.webkitTransitionDuration=0
	},
	
	onTouchMove:function(e){
		if(e.touches.length>1||e.scale&&e.scale!==1)
			return;
		
		this.deltaX=e.touches[0].pageX-this.start.pageX;
		
		if(typeof this.isScrolling=='undefined')
		{
			this.isScrolling=!!(this.isScrolling||Math.abs(this.deltaX)<Math.abs(e.touches[0].pageY-this.start.pageY))
		}
		
		if(!this.isScrolling)
		{
			e.preventDefault();
			this.getWidth();
			clearTimeout(this.interval);
			this.deltaX=this.deltaX/(
			(!this.index&&this.deltaX>0||this.index==this.length-1&&this.deltaX<0)
			?
			(Math.abs(this.deltaX)/this.width+1)
			:
			1
			);
			
			this.loadItem(this.index+1);
			this.element.style.webkitTransform='translate3d('+(this.deltaX-this.index*this.width)+'px,0,0)'
		}
	},
	
	onTouchEnd:function(e)
	{
		this.getWidth();
		var isValidSlide=Number(new Date())-this.start.time<250 && Math.abs(this.deltaX)>20 ||
		Math.abs(this.deltaX)>this.width/2,isPastBounds =! this.index && this.deltaX>0 || 
		this.index==this.length-1 && this.deltaX<0;
		
		if(!this.isScrolling){
			this.slide(this.index+(isValidSlide&&!isPastBounds?(this.deltaX<0?1:-1):0),this.speed)
		}
		this.showPage();
	},
	
	clicked:function(e)
	{
		//点击事件处理
		if(this.zoomingAllowed)
		{		
			var box = this.container.parentNode.parentNode;
			
			if(box.hasClassName('win'))
			{
				window.location = this.items[this.index][1];
			}else{
				this.zoomIn();
			}

		}else{
            window.location.href=this.items[this.index][1];
		}
	},
	zoomIn:function(){	
		var rect = this.container.getBoundingClientRect();
		var box = this.container.parentNode.parentNode;
		box.className = 'win';
		var btnClose = document.createElement('div');
		btnClose.id='lightBoxBtnClose';
		btnClose.addEventListener('click',this.zoomOut, false);
		box.appendChild(btnClose);
		
		this.adjustSize(250);
	},
	zoomOut:function(){
		var _parent = this.parentNode;
		
		_parent.className = 'embed';
		_parent.removeChild(this);
		
		//css 
		//#lightBox.embed{margin:0 auto;width:150px;max-width:150px;}
		var size = 250;
		//

		$("#slider ul")[0].style.msTransform 
		= $("#slider ul")[0].style.MozTransform 
		= $("#slider ul")[0].style.OTransform 
		= 'translateX(' + -(noImg* size) + 'px)';
		
		$("#slider ul")[0].style.webkitTransform = 'translate3d(' + -(noImg* size) + 'px,0,0)';
		$("#slider li").css({"width":size});
		
		var n=$("#slider ul")[0].childNodes.length;
		
		$("#slider ul").css({"width":size*n});

	},
	adjustSize:function(size){
		$("#slider ul")[0].style.msTransform 
		= $("#slider ul")[0].style.MozTransform 
		= $("#slider ul")[0].style.OTransform 
		= 'translateX(' + -(noImg* size) + 'px)';
		
		$("#slider ul")[0].style.webkitTransform = 'translate3d(' + -(noImg* size) + 'px,0,0)';
		$("#slider li").css({"width":size});
		
		$("#slider ul").css({"width":size*this.items.length});
	},
	init:function(){
		var _html="";
		
		for(i=0;i<this.items.length;i++){
			_html+="<li></li>"
		}

		this.element.innerHTML=_html;
		
		var v = document.createElement('div');
		v.className = 'indicator';
		
		//0 显示小圆点
		//1 显示编号 形如 “1/10”
		this.indicator = document.createElement("ul");
		v.appendChild(this.indicator);

		this.element.parentNode.parentNode.appendChild(v);
		
		this.loadItem(0);
		this.showPage();
	},
	loadItem:function(n){	
		if(!this.element.getElementsByTagName("li")[n].getAttribute("name"))
		{
			this.element.getElementsByTagName("li")[n].innerHTML='<img src="'+this.items[n][0]+'"/>';
		
			if(n<(this.items.length-1))
				this.element.getElementsByTagName("li")[n+1].innerHTML='<img src="'+this.items[n+1][0]+'"/>';
		
			this.element.getElementsByTagName("li")[n].setAttribute("name",this.items[n][0])
		}
	},
	
	showPage:function()
	{
		var _html="";
		//显示小圆点
		for(i=0;i<this.items.length;i++){
			if(i==this.index)
				_html+="<li class='hilite'></li>"
			else
				_html+="<li></li>"
		}

		this.indicator.innerHTML= _html
	},
	getPos:function(){return this.index},

	getWidth:function(){this.width=this.container.getBoundingClientRect().width;}
};

window.addEventListener('load', setupInstantSearchForm, false);

function toggleWindow(obj)
{
	alert(obj);
}

$(function () {//重置商品介绍中的图片尺寸
    $(window).resize(function () {
        var imgs = $(".product-cont img");
        for (var index = 0; index < imgs.length; index++) {
            if ($(imgs[index]).width() > $(window).width()) {
                $(imgs[index]).removeAttr("width");
                $(imgs[index]).removeAttr("style");
                $(imgs[index]).removeAttr("height");
                $(imgs[index]).css("width", $(window).width() - 10);
            } else {
                if ($(imgs[index]).width() < 640 && $(window).width()<640) {
                    $(imgs[index]).css("width", $(window).width() - 10);
                } 
            }
        }
    }).resize();
});