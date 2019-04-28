$.fn.selectedElement = function (t) { if (!$.isPlainObject(t)) t = { css: !t ? '' : t }; var obj = $(this); obj.bind('selectstart', function () { return false }); this.unbind('click').bind('click', function (e) { if ('INPUT' == e.target.tagName) return; var o = $(this); if (e.shiftKey) { var s, s1 = obj.filter('[selectedElement]:first'), s2 = obj.index(this); s1 = s1.length == 0 ? 0 : obj.index(s1); if (s1 > s2) { s = s1 + 1; s1 = s2 } else { s = s2 + 1 } o = obj.slice(s1, s) } else if (e.ctrlKey) { o = obj.filter('[selectedElement]'); if (o.is(this)) { o = o.not(this) } else { o = o.add(this) } } obj.removeAttr('selectedElement'); o.attr('selectedElement', 1); if (!!t.css) { obj.removeClass(t.css); o.addClass(t.css) } if ($.isFunction(t.fn)) t.fn(obj, o) }); return obj };
function AddTips(s) {
    try {
        clearTimeout(RemoveTipsTimer);
    } catch (e) { }
    //if( s == '') return;
    var args = arguments;
    var id, position, width, height, mX, mY;
    var top = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop;
    var browser = navigator.userAgent;
    if (browser.indexOf("Firefox") > 0) {
        top = document.body.scrollTop;
    }
    if (args[1]) id = args[1];
    if (args[2]) position = args[2]; //显示位置,有 top bottom left right mouse(鼠标跟随) 等多种属性
    width = args[3];
    if (args[4]) height = args[4];
    if (!id) id = "dvTips";
    var dvTips = document.getElementById(id);
    if (!dvTips) {
        dvTips = document.createElement("DIV");
        dvTips.id = id;
        dvTips.className = "dvTips";
        dvTips.style.position = 'absolute';
        dvTips.style.padding = '0px';
        if (width) dvTips.style.width = width + "px";
        if (height) dvTips.style.height = height + "px";
        document.body.appendChild(dvTips);
    }
    dvTips.onmouseover = function () { clearTimeout(RemoveTipsTimer); }
    dvTips.onmouseout = function () { RemoveTips(id, true); }
    if (s != '') dvTips.innerHTML = s;
    try {
        mX = event.x ? event.x : event.pageX;
        mY = event.y ? event.y : event.pageY;
        if (position == 'top') {
            dvTips.style.top = getOffsetTop(event.srcElement) - (dvTips.clientHeight + 5) + "px";
            dvTips.style.left = getOffsetLeft(event.srcElement) + "px";
        } else if (position == 'bottom') {
            dvTips.style.top = getOffsetTop(event.srcElement) + (event.srcElement.clientHeight + 5) + "px";
            dvTips.style.left = getOffsetLeft(event.srcElement) + "px";
        } else if (position == 'left') {
            dvTips.style.top = getOffsetTop(event.srcElement) + "px";
            dvTips.style.left = getOffsetLeft(event.srcElement) - (dvTips.clientWidth + 5) + "px";
        } else if (position == 'right') {
            dvTips.style.top = getOffsetTop(event.srcElement) + "px";
            dvTips.style.left = getOffsetLeft(event.srcElement) + (event.srcElement.clientWidth + 5) + "px";
        } else {
            var left = mX + 5 + document.body.scrollLeft;
            if (left + parseInt(width) > document.body.scrollWidth) left = mX - 5 - width;
            dvTips.style.left = left + "px";
            dvTips.style.top = mY + 5 + top + "px";
        }
        dvTips.style.display = '';
    } catch (e) { }
}
var RemoveTipsTimer;
function RemoveTips() {
    var args = arguments;
    var id, delay;
    if (args[0]) id = args[0];
    if (typeof (args[1]) != 'undefined') delay = args[1];
    if (!id) id = 'dvTips';
    var dvTips = document.getElementById(id);
    if (dvTips) {
        if (typeof (delay) == 'undefined' || delay == true) RemoveTipsTimer = setTimeout(function () { dvTips.style.display = 'none' }, 200);
        else dvTips.style.display = 'none';
    }
}
var wpEvents = new function () {
    var self = this;
    this.arrEvents = [];
    this.addListener = function (obj, eventName, oCallback) {
        var ret;
        if (wpConsts._isIE) {
            ret = obj.attachEvent("on" + eventName, oCallback);
        }
        else {
            ret = obj.addEventListener(eventName, oCallback, false);
        }
        if (ret) this.arrEvents.push({ "obj": obj, "eventName": eventName, "oCallback": oCallback });
        return ret;
    };

    this.clearListener = function (obj, eventName) {
        for (var i = 0; i < this.arrEvents.length; i++) {
            if (this.arrEvents[i].obj == obj && this.arrEvents[i].eventName == eventName) {
                this.removeListener(obj, eventName, this.arrEvents[i].oCallback);
            }
        }
    };

    this.removeListener = function (obj, eventName, oCallback) {
        if (wpConsts._isIE) {
            obj.detachEvent("on" + eventName, oCallback);
        }
        else {
            obj.removeEventListener(eventName, oCallback, true);
        }
    };

    this.initWinEvents = function (oWin) {
        if (!oWin) return;
        __firefox(oWin);
    };

    /*使得firefox兼容IE的event*/
    function __firefox(oWin) {
        if (!oWin) oWin = window;
        HTMLElement.prototype.__defineGetter__("runtimeStyle", self.__element_style);
        oWin.constructor.prototype.__defineGetter__("event", function () { return self.__window_event(oWin); });
        Event.prototype.__defineGetter__("srcElement", self.__event_srcElement);
    }
    this.__element_style = function () {
        return this.style;
    }
    this.__window_event = function (oWin) {
        return __window_event_constructor();
    }
    this.__event_srcElement = function () {
        return this.target;
    }
    function __window_event_constructor(oWin) {
        if (!oWin) oWin = window;
        if (document.all) {
            return oWin.event;
        }
        var _caller = __window_event_constructor.caller;
        while (_caller != null) {
            var _argument = _caller.arguments[0];
            if (_argument) {
                var _temp = _argument.constructor;
                if (_temp.toString().indexOf("Event") != -1) {
                    return _argument;
                }
            }
            _caller = _caller.caller;
        }
        return null;
    }
    if (window.addEventListener) {
        __firefox();
    }
    /*end firefox*/
};
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
        CheckEmail(key, box);
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
        modal: modal_,
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
    $.ajax({
        type: 'POST',
        url: url_,
        data: '',
        dataType: 'html',
        beforeSend: function () {
            $('#' + div_ + '').html('<img src=' + WebPath + '/theme/system/systempage/admin/js/jqueryuicss/redmond/images/flag_loading.gif>');
        },
        success: function (data) {
            $('#' + div_ + '').html(data);
        },
        error: function () {
            $('#' + div_ + '').html('System Error!');
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
        modal: modal_,
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
        url: WebPath + "/Ajax/Ajax.aspx?__Action=GetAreaList",
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
        url: WebPath + "/Ajax/Ajax.aspx?__Action=GetAreaList",
        dataType: 'html',
        data: { "topid": topid, "area_id": $("#" + objid).val() },
        success: function (data) {
            $("#Area_id_div").html(data);
        }
    });
}
//选择商品分类下拉框
function GetProductType(id) {
    $.ajax({
        type: 'POST',
        url: WebPath + "/ajax/ajax.aspx?__Action=GetProductTypeList",
        dataType: 'html',
        data: { "id": id },
        success: function (data) {
            $("#ProductType_div").html(data);
            ChangePro_Type();
        }
    });
}
//选择商品分类下拉框-选择动作
function SelectProductType(objid) {
    $.ajax({
        type: 'POST',
        url: WebPath + "/ajax/ajax.aspx?__Action=GetProductTypeList",
        dataType: 'html',
        data: { "id": $("#" + objid).val() },
        success: function (data) {
            $("#ProductType_div").html(data);
            ChangePro_Type();
        }
    });
}
//修改密码
function AdminPWD(id) {
    var title_ = "修改密码";
    var url_ = AdminPath + "/admin/admin_pwd_edit_window.aspx?id=" + id;
    var width_ = 400;
    var height_ = 230;
    var modal_ = true;
    EditWindow(title_, url_, width_, height_, modal_);
}
//通用数据提交方法
function RequestAjax(url, jsondata, callback) {
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
function ShowChild(ids, id) {
    var src = $("#img" + id + "").attr("src");
    if (src.indexOf("plus.gif") == -1) {
        $("#img" + id + "").attr("src", AdminImagePath + "/plus.gif");
        var arr = ids.split(',');
        for (var i in arr) {
            DoHide(arr[i]);
        }
    }
    else {
        $("#img" + id + "").attr("src", AdminImagePath + "/minus.gif");
        $("tr[name='tr" + id + "']").each(function () {
            $(this).show();
        })
    }
}
function DoHide(id) {
    $("#img" + id + "").attr("src", AdminImagePath + "/plus.gif");
    $("#tr" + id + "").hide();

}
//返回上一步
function GoBack() {
    window.location = refPage; //母板定义
}
var status = 1;
function switchSysBar() {
    if (1 == window.status) {
        window.status = 0;
        $("#switchPoint").html('<img src="' + AdminImagePath + '/vertical/right.png">');
        $("#sidebar").hide();
        $("#sideplus a").attr("class", "cur");
        $("#sidecontent").attr("style", "margin-left:13px");
        $("#body_main").width($(window).width() - 20);
    }
    else {
        window.status = 1;
        $("#switchPoint").html('<img src="' + AdminImagePath + '/vertical/left.png">');
        $("#sidebar").show();
        $("#sideplus a").attr("class", "");
        $("#sidecontent").attr("style", "");
        $("#body_main").width($(window).width() - $("#sidebar").width() - 17);
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
//付款窗口
function PayWindow(tablename,keyid, name, price) {
    var title_ = name;
    var url_ = WebPath + "/inc/pay_window.aspx?tablename=" + tablename + "&keyid=" + keyid + "&money=" + price;
    var width_ = 400;
    var height_ = 250;
    var modal_ = true;
    EditWindow(title_, url_, width_, height_, modal_);
}