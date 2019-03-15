/*
====================================================================================
add by zhangshijia 20121013
====================================================================================
*********************************************
收集表单数据
约定：
页面的INPUT表签添加FormObj=true属性
*********************************************
*/
$.fn.selectedElement = function (t) { if (!$.isPlainObject(t)) t = { css: !t ? '' : t }; var obj = $(this); obj.bind('selectstart', function () { return false }); this.unbind('click').bind('click', function (e) { if ('INPUT' == e.target.tagName) return; var o = $(this); if (e.shiftKey) { var s, s1 = obj.filter('[selectedElement]:first'), s2 = obj.index(this); s1 = s1.length == 0 ? 0 : obj.index(s1); if (s1 > s2) { s = s1 + 1; s1 = s2 } else { s = s2 + 1 } o = obj.slice(s1, s) } else if (e.ctrlKey) { o = obj.filter('[selectedElement]'); if (o.is(this)) { o = o.not(this) } else { o = o.add(this) } } obj.removeAttr('selectedElement'); o.attr('selectedElement', 1); if (!!t.css) { obj.removeClass(t.css); o.addClass(t.css) } if ($.isFunction(t.fn)) t.fn(obj, o) }); return obj };
function GetFormJsonData(id) {
    if (id == undefined)
        id = "FormObj";
    return $("[" + id + "]").serialize();
}
// 获取radio选择的值 以 ,隔开
function GetRadioCheckedValues(id) {
    if (id == undefined)
        id = 'ChkSingleID';
    var ids = "";
    var chks = $("input[type='radio'][name='" + id + "']:checked");
    for (var index = 0; index < chks.length; index++) {
        if (index > 0)
            ids += ",";
        ids += $(chks[index]).val();
    }
    return ids;
}
// 获取chk选择的值 以 ,隔开
function GetChkCheckedValues(id) {
    if (id == undefined)
        id = 'ChkSingleID';
    var ids = "";
    var chks = $("input[type='checkbox'][name='" + id + "']:checked");
    for (var index = 0; index < chks.length; index++) {
        if (index > 0)
            ids += ",";
        ids += $(chks[index]).val();
    }
    return ids;
}
//通用表单验证方法
function CheckForm(id, box) {

    if (id == undefined)
        id = 'FormObj';
    var formElement
    formElement = $("[" + id + "]");
    var str = "";
    var key = "";
    var value = "";
    var len = 0;
    var min = 0;
    var max = 0;
    var status = true;
    $.each(formElement, function (i, obj) {
        //debugger;

        min = $(obj).attr("min");
        max = $(obj).attr("max");
        min = min == undefined ? 0 : min;
        max = max == undefined ? 0 : max;
        key = $(obj).attr("id");
        value = $(obj).val();
        if (value != null)
            len = value.length;

        switch ($(obj).attr("type")) {
            case "text":
                if (!CheckText(key, len, min, max, box))
                    status = false;
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
                if (!CheckText(key, len, min, max, box))
                    status = false;
                break
        }
    });
    return status;

}
//输入长度验证
function CheckText(key, len, min, max, box) {
    //debugger;
    var status = true;
    if (min == 'notnull') {
        if (len == 0) {
            CheckNO(key, "", box);
            status = false;
        } else {
            CheckOK(key, '', box);
        }
    }
    else if (min == 'email') {
        status = CheckEmail(key, box);
    } else {
        if (min > 0 && max > 0) {
            if (len < min || len > max) {
                CheckNO(key, "长度限制在{ " + min + " - " + max + " }之间", box);
                status = false;
            } else {
                CheckOK(key, '', box);
            }
        } else if (min > 0) {
            if (len < min) {
                CheckNO(key, "长度不能小于 " + min, box);
                status = false;
            } else {
                CheckOK(key, '', box);
            }
        } else if (max > 0) {
            if (len > max) {
                CheckNO(key, "长度不能大于 " + max, box);
                status = false;
            } else {
                CheckOK(key, '', box);
            }
        }

    }
    return status;
}
//输入长度验证
function CheckLength(key, box) {
    var min = $("#" + key + "").attr("min");
    var max = $("#" + key + "").attr("max");
    var min = min == undefined ? 0 : min;
    var max = max == undefined ? 0 : max;
    var value = $("#" + key + "").val();
    var len = value.length;
    return CheckText(key, len, min, max, box);
}
//输入提示
function InputStatus(id, box) {
    //debugger;
    var obj = $("#" + id);
    var max = $(obj).attr("max");
    if (max == 0)
        return;
    var len = $(obj).val().length;
    if (box == undefined)
        box = "span";
    if (document.getElementById("Form" + id) == undefined)
        $("#" + id + "").after("<" + box + " id=\"Form" + id + "\"></" + box + ">");
    $("#Form" + id + "").removeClass("FormNO");
    $("#Form" + id + "").addClass("FormYES");
    if (len > max || len == max)
        $(obj).val($(obj).val().substring(0, max));
    $("#Form" + id + "").html("当前输入 " + len + " 个字符，最多 " + max + " 个，剩余 " + ((max - len) < 0 ? 0 : (max - len)) + " 个");

}
//验证成功
function CheckOK(id, msg, box) {

    if (msg == undefined)
        msg = "正确";
    if (box == undefined)
        box = "span";
    if (document.getElementById("Form" + id) == undefined)
        $("#" + id + "").after("<" + box + " id=\"Form" + id + "\"></" + box + ">");
    $("#Form" + id + "").removeClass("FormNO");
    $("#Form" + id + "").addClass("FormYES");
    $("#Form" + id + "").html(msg);
}
//验证失败
function CheckNO(id, msg, box) {
    if (msg == undefined)
        msg = "错误";
    if (box == undefined)
        box = "span";
    if (document.getElementById("Form" + id) == undefined)
        $("#" + id + "").after("<" + box + " id=\"Form" + id + "\"></" + box + ">");
    $("#Form" + id + "").removeClass("FormYES");
    $("#Form" + id + "").addClass("FormNO");
    $("#Form" + id + "").html(msg);
}

//验证邮箱
function CheckEmail(key, box) {
    var str = $("#" + key + "").val();
    if (str == '') {
        CheckNO(key, "邮箱格式错误", box);
        return false;
    }
    var reg = /^([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    if (reg.test(str)) {
        CheckOK(key, "", box);
        return true;
    } else {
        CheckNO(key, "邮箱格式错误", box);
        return false;
    }
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
    if (div_ == undefined)
        div_ = "div_window";
    if ($("#" + div_)[0] == undefined) {
        $("body").after("<div id=\"" + div_ + "\"></div>");
    }
    $('#' + div_ + '').dialog({
        autoOpen: false,
        height: height_,
        width: width_,
        modal: false, //modal_,
        title: title_,
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $('#' + div_ + '').dialog('open');
    $('.ui-dialog-titlebar').attr("id", "ui-dialog-title-" + div_ + "");
    $('.ui-dialog-titlebar').attr("class", "ui-dialog-title-div_window ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix");
    $('#ui-dialog-title-' + div_ + '').show()
    $('#ui-dialog-title-areadiv').show()
    $('.ui-dialog-titlebar').show()
    $("#ui-dialog-title-div_error").hide()
    if (url_.indexOf("?") > -1) {
        url_ = url_ + "&random=" + Math.random();
    } else {
        url_ = url_ + "?random=" + Math.random();
    }
    $.ajax({
        type: "POST",
        url: url_,
        data: "{}",
        dataType: "html",
        beforeSend: function () {
            $('#' + div_ + '').html('<img src=' + WebPath + '/theme/system/systempage/admin/js/jqueryuicss/redmond/images/flag_loading.gif>');
        },
        success: function (data) {
            $('#' + div_ + '').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#' + div_ + '').html(textStatus);
        }
    });
}
function ShowWindow(title_, ediv_, width_, height_, modal_, div_) {
    if (div_ == undefined)
        div_ = "div_window";
    if ($("#" + div_)[0] == undefined) {
        $("body").after("<div id=\"" + div_ + "\"></div>");
    }
    $('#' + div_ + '').dialog({
        autoOpen: false,
        height: height_,
        width: width_,
        modal: false, //modal_,
        title: title_,
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $('#' + div_ + '').dialog('open');
    $('.ui-dialog-titlebar').attr("id", "ui-dialog-title-" + div_ + "");
    $('#ui-dialog-title-' + div_ + '').show()
    $('#ui-dialog-title-areadiv').show()
    $('.ui-dialog-titlebar').show()
    $("#ui-dialog-title-div_error").hide()
    $('#' + div_ + '').html($('#' + ediv_ + '').html());
}
/*
***********************************************
通用删除操作
***********************************************
*/
function DelObj(url, json) {
    if (!confirm("确认要删除？"))
        return false;
    if (url.indexOf("?") > -1) {
        url = url + "&random=" + Math.random();
    } else {
        url = url + "?random=" + Math.random();
    }
    $.ajax({
        type: "POST",
        url: url,
        data: json,
        error: function (state) {
            MsgBox(2, "Error", "");
            return false;
        },
        success: function (res) {

            if (res == "OK") {
                MsgBox(1, "", '?');
                return false;
            }
            else {

                MsgBox(2, res, "");
                return false;
            }
        }
    });
}
/*
***********************************************
更改下拉框内容
***********************************************
*/
function UpdateOption(objid, data, selectedvalue) {
    //参数字符串是JSON 数据格式[{name:'1',value:'0'},{name:'1',value:'0'}]
    data = eval("(" + data + ")");
    var stlect = "";
    $("#" + objid + "").empty();
    $("#" + objid + "").append("<option value='0'>请选择</option>");
    $.each(data, function (i, o) {
        stlect = "";
        if (selectedvalue == o.value) {
            stlect = "selected";
        }
        $("#" + objid + "").append("<option value='" + o.value + "' " + stlect + ">" + o.name + "</option>");
    });
}
//商品分类下拉框
function GetProductType(id) {
    $.ajax({
        type: 'POST',
        url: AdminPath + "/ajax/ajax_product.aspx?__Action=GetProductTypeList&random=" + Math.random(),
        dataType: 'html',
        data: { "id": id, "classname": "form-control" },
        success: function (data) {
            $("#ProductType_div").html(data);
            ChangePro_Type();
        }
    });
}
/**
***********************************************
获取远程HTML加载到指定ID的控件中
***********************************************
*/
function GetHtml(url, obj) {
    $.ajax({
        type: 'POST',
        url: url + "&random=" + Math.random(),
        dataType: 'html',
        success: function (data) {
            $("#" + obj + "").html(data);
        }
    });
}
//选择商品分类下拉框-选择动作
function SelectProductType(objid) {
    $.ajax({
        type: 'POST',
        url: AdminPath + "/ajax/ajax_product.aspx?__Action=GetProductTypeList&random=" + Math.random(),
        dataType: 'html',
        data: { "id": $("#" + objid).val(), "classname": "form-control" },
        success: function (data) {
            $("#ProductType_div").html(data);
            ChangePro_Type();
        }
    });
}
//编辑页面语言切换
function LanguageTab_EditPage(id) {
    $(".lang_table").hide();
    $("li", ".languagetab").removeClass();
    if ($("#li_" + id)[0] == undefined) {
        var tabs = $("li", ".languagetab");
        id = $(tabs[0]).attr('language');
    }
    $("#lang_" + id).show();
    $("#li_" + id).addClass("selected");
}
function selectrow(id) {

    var se = $("#" + id + "").attr("checked");
    if (se == "checked" || se == true)
        $("#" + id + "").attr("checked", false);
    else
        $("#" + id + "").attr("checked", true);
}
/*
*********************************************
全部选中/取消

约定：
CheckAll         主CheckBox的ID
CZSetID='true    副CheckBox中必须有的属性
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
//向指定容器中添加区域选择框
function GetAreaList(topid, area_id, divid) {
    if (divid == undefined)
        divid = "Area_id_div";
    $.ajax({
        type: 'POST',
        url: WebPath + "/Ajax/Ajax.aspx?__Action=GetAreaList&random=" + Math.random(),
        dataType: 'html',
        data: { "topid": topid, "area_id": area_id },
        success: function (data) {
            $("#" + divid).html(data);
        }
    });
}
//选择地区下拉框-选择动作
function SelectAreaList(topid, objid) {
    $.ajax({
        type: 'POST',
        url: WebPath + "/Ajax/Ajax.aspx?__Action=GetAreaList&random=" + Math.random(),
        dataType: 'html',
        data: { "topid": topid, "area_id": $("#" + objid).val() },
        success: function (data) {
            $("#Area_id_div").html(data);
        }
    });
}
//修改密码
function AdminPWD(id) {
    var title_ = "修改密码";
    var url_ = AdminPath + "/admin/admin_pwd_edit_window.aspx?id=" + id;
    var width_ = 400;
    var height_ = 'auto';
    var modal_ = true;
    EditWindow(title_, url_, width_, height_, modal_);
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
            MsgBox(4, "正在处理 ……", "-");
        },
        success: function (res) {
            //debugger;
            if (res.msg == "OK") {
                //MsgBox(1, "操作成功", '?');
                callback(res);
                return false;
            }
            else {
                MsgBox(2, res.msg, "");
                return false;
            }
        },
        error: function () {
            MsgBox(2, 'System Error!', "");
        }
    });
}
function resizeEditor(objname, num) {
    var obj = $("#" + objname);
    var h = obj.height();
    h = h + num < 60 ? 60 : h + num;
    obj.height(h);
}
var showTypeids = GetCookie("showTypeids");
function ShowProductTypeChild(ids, id, deep) {
    if (showTypeids == '' || showTypeids == undefined)
        showTypeids = ',';
    if (showTypeids.indexOf(',' + id + ',') < 0) {
        showTypeids = showTypeids + id + ',';
        SetCookie("showTypeids", showTypeids, 1);
    }
    var src = $("#img" + id + "").attr("src");
    if (src.indexOf("plus.gif") == -1) {
        $("#img" + id + "").attr("src", AdminImagePath + "/plus.gif");
        var arr = ids.split(',');
        for (var i in arr) {
            DoHide(arr[i]);
        }
        showTypeids = showTypeids.replace(',' + id + ',', ',');
        SetCookie("showTypeids", showTypeids, 1);
    }
    else {
        $.ajax({
            type: "POST",
            url: AdminPath + '/ajax/ajax_product.aspx?__Action=CreateTree&t=' + Math.random(),
            data: { "pid": id, "deep": deep },
            dataType: 'html',
            success: function (res) {
                $("#tr" + id).after(res);
            }
        });
        $("#img" + id + "").attr("src", AdminImagePath + "/minus.gif");
        $("tr[name='tr" + id + "']").each(function () {
            $(this).show();
        })
    }
}
function DoHide(id) {
    $("#img" + id + "").attr("src", AdminImagePath + "/plus.gif");
    $("#tr" + id + "").remove();
}

function ShowChild(ids, id) {
    //ids兼容旧版本
    var src = $("#img" + id + "").attr("src");
    if (src.indexOf("plus.gif") == -1) {
        $("#img" + id + "").attr("src", AdminImagePath + "/plus.gif");
        $("tr[name='tr" + id + "']").each(function () {
            $(this).hide();
        })
    }
    else {
        
        $("#img" + id + "").attr("src", AdminImagePath + "/minus.gif");
        $("tr[name='tr" + id + "']").each(function () {
            $(this).show();
        })
    }
}

//返回上一步
function GoBack() {
    window.location = refPage; //母板定义
}
var status = 1;
var _body_main_form_w = 0;
function switchSysBar() {
    if (1 == window.status) {
        window.status = 0;
        _body_main_form_w = $(window).width() - 40;
        $("#switchPoint").html('<img src="' + AdminImagePath + '/vertical/right.png">');
        $("#sidebar").hide();
        $("#sideplus a").attr("class", "cur");
        $("#sidecontent").attr("style", "margin-left:13px");
        $("#body_main").width($(window).width() - 20);
        $("#body_bottom").width($(window).width() - 20);
        $("#body_main_form").attr("style", "min-width:" + _body_main_form_w + "px;width:" + _body_main_form_w + "px");
        SetCookie("sidebar", "hide", 1);
    }
    else {
        window.status = 1;
        _body_main_form_w = $(window).width() - $("#sidebar").width() - 35;
        $("#switchPoint").html('<img src="' + AdminImagePath + '/vertical/left.png">');
        $("#sidebar").show();
        $("#sideplus a").attr("class", "");
        $("#sidecontent").attr("style", "");
        $("#body_main").width($(window).width() - $("#sidebar").width() - 17);
        $("#body_bottom").width($(window).width() - $("#sidebar").width() - 17);
        $("#body_main_form").attr("style", "min-width:" + _body_main_form_w + "px;width:" + _body_main_form_w + "px");
        SetCookie("sidebar", "", 1);
    }
}
//通用上传文件
function uploadFile(id) {
    $("#status_" + id).html('waitting...');
    $.ajaxFileUpload
            (
	            {
	                url: WebPath + '/ajax/fileupload.aspx',
	                secureuri: false,
	                fileElementId: 'file_' + id,
	                dataType: 'json',
	                success: function (data, status) {
	                    if (data.msg != 'OK') {
	                        MsgBox(2, data.msg, "");
	                        $("#status_" + id).html(data.msg);
	                    }
	                    else {
	                        var url = data.url;
	                        if (url.length > 0) {
	                            $("#" + id + "").val(url);
	                            $("#status_" + id).html('');
	                        }
	                    }
	                },
	                error: function (data, status, e) {
	                    MsgBox(2, data.error, "");
	                    $("#status_" + id).html(data.msg);
	                }
	            }
            )
}
function GetOrderMemo(id) {
    $.ajax({
        type: "POST",
        url: AdminPath + "/ajax/ajax_order.aspx?__Action=Order_Memo&random=" + Math.random(),
        data: { "id": id },
        success: function (res) {
            $("#submenu" + id).html(res);
        }
    });
}
function GetAdminSkin(id) {
    $.ajax({
        type: "POST",
        url: AdminPath + "/ajax/ajax_order.aspx?__Action=GetAdminSkin&random=" + Math.random(),
        data: { "id": id },
        success: function (res) {
            $("#submenu" + id).html(res);
        }
    });
}
function SetLanguage(c, n) {
    SetCookie("AdminLanguage", c, 1);
    SetCookie("AdminLanguageName", n, 1);
    window.location.reload();
}