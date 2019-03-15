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
    $("#div_" + title  + "").dialog("close");
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
        MsgBoxClose(href,title);
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
    $("#div_" + title + "").dialog("close");
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
function countDown(secs) { $("#backtime").html(secs); if (--secs > 0) { setTimeout("countDown(" + secs + ")", 1000); } }