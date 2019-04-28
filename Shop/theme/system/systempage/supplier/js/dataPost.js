
//保存数据对象
//一般添加或更新
function SaveData(objName, postData, href) {
    if (href == undefined)
        href = "";
    var __Add = "";
    if ($("#IsAdd").attr("checked") == true)//是否勾选了复制添加
        __Add = "&__Add=1";
    $.ajax({
        type: "POST",
        url: AdminPath + "Ajax/UpData.aspx?__Action=Save&__Target=" + objName + __Add,
        data: postData,
        error: function (state) {
            MsgBox(2, "Error", "");
            return false;
        },
        success: function (res) {
            var jdata = eval("(" + res + ")"); //转换为json对象           
            if (jdata.err == "OK") {
                if (href.length > 1) {
                    MsgBox(1, "OK", href + jdata.ID);
                }
                else {
                    MsgBox(1, "OK", href);
                }
                return false;
            }
            else {

                MsgBox(2, jdata.err, "");
                return false;
            }
        },
        complete: function () {

        }
    });
}
/*
*********************************************
删除数据对象
说明：
objName:表的名字
postData:json格式的关键字参数，空时自动在属相“CZSetID=true”的复选框中提取
href:操作完成后返回地址
*********************************************
*/
function DelData(objName, postData, href, msg, ifalert) {


    if (postData == "") {
        var result = "";
        var chname = "";
        var chks = $("input[CZSetID='true']:checked");
        $.each(chks, function (i, o) {
            if (i > 0)
                result += ",";
            result += $(o).val();
            chname = $(o).attr("name")
        });
        if (result == "") {
            alert("Error!");
            return false;
        }
        else {
            result = "" + chname + ":\"" + result + "\"";
            postData = eval("({" + result + "})");

        }
    }
    if (msg != "NoAsk") {
        if (msg == undefined) {
            msg = "确定要删除吗?";
        }

        if (!confirm(msg)) {
            return false;
        }
    }

    if (href == undefined)
        href = "";
    $.ajax({
        type: "POST",
        url: AdminPath + "Ajax/UpData.aspx?__Action=Del&__Target=" + objName,
        data: postData,
        error: function (state) {
            MsgBox(2, "Error", "");
            return false;
        },
        success: function (res) {

            if (ifalert == true || ifalert == undefined) {
                if (res == "OK") {
                    MsgBox(1, "OK", href);
                }
                else {

                    MsgBox(2, res, "");
                }
            }
        },
        complete: function () {
        }
    });
    return true;
}
/*
*********************************************
添加新的属性
说明：
objName:名称
objValue:值
*********************************************
*/
function AddObj(objName, objValue) {
    var input = "<input FormObj=\"true\" id=\"" + objName + "\" name=\"" + objName + "\" type=\"hidden\" value=\"" + objValue + "\" />";
    $("#Editor").append(input);
}