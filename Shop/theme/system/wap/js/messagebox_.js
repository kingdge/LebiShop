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

function MsgBox(errFlag, err, href, mode, time) {
    if (mode == undefined)
        mode = "";
    if (time == undefined)
        time = 1000;
    err = msgstr(errFlag, err);
    $("#messgebox").html(err);
    setTimeout(function () { MsgBoxClose(href); }, time);
}
function msgstr(flag, err) {
    var str = '';
    switch (flag) {
        case 1:
            str = '<span style="color:#008000">';
            break;
        case 2:
            str = '<span style="color:#ff0000">';
            break;
        case 3:
            str = '<span style="color:#ffa500">';
            break;
        case 4:
            str = '<span style="color:#cccccc">';
            break;
    }
    str += err + '</span>';
    return str;
}
function MsgBoxClose(href, title) {
    if (href == '-')
        return false;
    if (href == undefined)
        href = "";
    if (href != "" && href != "?" && href != "-" && href != "#") {
        window.location = href;
    } else if (href == "#") {
        window.history.go(-1);
    } else if (href == "") {
        $("#messgebox").html('');
    }
    
}
